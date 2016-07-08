using UnityEngine;
using System.Collections;
using System.IO;
using System.Text;
using System;

public class TextInput : MonoBehaviour 
{
	string m_email = "";
	string m_name_first = "";
	string m_name_last = "";
	string m_path;
	string m_backup_path;
	FileStream m_file_stream;
	StreamReader m_string_reader;
	StreamWriter m_string_writer;
	//TextAsset m_email_list = null;
	StringBuilder sb;
	public GUIStyle my_skin;
	public string m_level_string;
	public UIWidget m_wid;
	public Rect m_email_rect;
	public Rect m_fname_rect;
	public Rect m_lname_rect;
	string m_first_level_name;
	UILabel m_score_text;
	UILabel m_time_text;
	float m_round_time = 30;
	public GameObject m_start_button;
	public GameObject m_press_sprite;

	public Texture2D m_retina_sprite;
	GameObject m_basket_item;
	GameObject m_countdown_item;
	GameObject m_explosion_item;

	bool m_is_press = false;
	bool time_out = false;
	bool m_not_loading = true;
	// Use this for initialization
	void Start () 
	{
		if(Screen.width == 1024)
		{
			my_skin.normal.background = m_retina_sprite;
			my_skin.fontSize = 33;
			my_skin.border.top = 24;
			my_skin.border.bottom = 30;
		}
		m_first_level_name = Application.loadedLevelName;
		m_path = Application.persistentDataPath + "/EmailRecords.txt";
		Debug.Log(m_path);
		m_backup_path = Application.persistentDataPath + "/EmailRecordsBU.txt";
		if(!File.Exists(m_path))
		{
			m_string_writer = File.CreateText(m_path);
			m_string_writer.WriteLine("First,Last,Email,Score,Time,Press");
			m_string_writer.Close();
			m_string_writer = null;
		}
		if(!File.Exists(m_backup_path))
		{
			m_string_writer = File.CreateText(m_backup_path);
			m_string_writer.WriteLine("First,Last,Email,Score,Time,Press");
			m_string_writer.Close();
			m_string_writer = null;
		}
		
		sb = new StringBuilder();
		m_string_reader = new StreamReader(m_path);
		sb.Append(m_string_reader.ReadToEnd());
		m_string_writer = new StreamWriter(m_backup_path);
		m_string_writer.Write(sb.ToString());
		m_string_writer.Close();
		m_string_reader.Close();

	}

	void OnLevelWasLoaded()
	{
		m_not_loading = true;
		if(GameManager.s_Inst.m_current_game_state == GameManager.GameState.Demo)
		{
			GameManager.s_Inst.m_bullets = 0;
			m_score_text = GameObject.Find("ScoreCounter").GetComponent<UILabel>();
			m_time_text = GameObject.Find("Timer").GetComponent<UILabel>();
			GameObject.Find("StartButtonDemo").GetComponent<UIButton>().onClick.Add(new EventDelegate(this,"UnpauseGame"));
			GameObject.Find("SubmitButtonDemo").GetComponent<UIButton>().onClick.Add(new EventDelegate(this,"EndOfDemo"));
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(GameManager.s_Inst.m_current_game_state == GameManager.GameState.MainMenu)
		{
			if(m_email.Length > 0 && m_name_first.Length > 0)
				m_start_button.GetComponent<UIButton>().isEnabled = true;
			else
				m_start_button.GetComponent<UIButton>().isEnabled = false;
		}
		if(GameManager.s_Inst.m_current_game_state == GameManager.GameState.Demo)
		{
			if(m_score_text != null)
				m_score_text.text = GameManager.m_baskets_made.ToString();
			m_round_time -= Time.deltaTime * Time.timeScale;
			if(m_round_time > -1)
			{
				if(m_time_text != null)
					m_time_text.text = ":" + ((int)m_round_time).ToString();
			}
			if(m_round_time <= 0 && !GameManager.m_ball_has_been_thrown)
				TimeRanOut();
		}
	}

	void OnGUI()
	{
		if(Application.loadedLevelName == m_first_level_name && m_not_loading)
		{
			m_email = GUI.TextField(new Rect(Screen.width * m_email_rect.x, Screen.height * m_email_rect.y,Screen.width * m_email_rect.width, Screen.height * m_email_rect.height),m_email,my_skin);
			m_name_first = GUI.TextField(new Rect(Screen.width * m_fname_rect.x, Screen.height * m_fname_rect.y,Screen.width * m_fname_rect.width, Screen.height * m_fname_rect.height),m_name_first,my_skin);
			m_name_last = GUI.TextField(new Rect(Screen.width * m_lname_rect.x, Screen.height * m_lname_rect.y,Screen.width * m_lname_rect.width, Screen.height * m_lname_rect.height),m_name_last,my_skin);
		}
	}

	public void StartButtonPressed()
	{
		GameManager.m_baskets_made = 0;
		GameManager.s_Inst.m_current_game_state = GameManager.GameState.Demo;
		Time.timeScale = 0;
		m_round_time = 30;
		m_not_loading = false;
		GameObject.Find("LoadingScreen").GetComponent<TweenPosition>().PlayForward();
		GameManager.s_Inst.SaveIAPItems();
		GameManager.s_Inst.m_level_name_to_load = m_level_string;
		Application.LoadLevel("LevelLoader");
	}

	public void UnpauseGame()
	{
		Time.timeScale = 1;
		GameObject.Find("TutorialWindow").GetComponent<TweenPosition>().PlayForward();
	}

	public void TimeRanOut()
	{
		if(!time_out)
		{
			time_out = true;
			Time.timeScale = 0;
			GameObject.Find("FinishWindow").GetComponent<TweenScale>().PlayForward();
			GameObject.Find("FinishWindow").GetComponent<TweenColor>().PlayForward();
			GameObject.Find("FinishWindow").GetComponent<TweenScale>().onFinishedForward.Add(new EventDelegate(this,"TimeRanOutSecondStage"));
		}
	}

	public void TimeRanOutSecondStage()
	{
		//basket Trans
		m_basket_item = GameObject.Find("Score");
		m_basket_item.GetComponent<TweenPosition>().PlayForward();
		m_basket_item.GetComponent<TweenScale>().PlayForward();
		m_basket_item.transform.GetChild(0).GetComponent<TweenColor>().PlayForward();
		//ScoreTrans
		m_countdown_item = GameObject.Find("Countdown");
		m_countdown_item.GetComponent<TweenColor>().PlayForward();
		m_countdown_item.transform.GetChild(0).GetComponent<TweenColor>().PlayForward();
		m_countdown_item.transform.GetChild(0).GetChild(0).GetComponent<TweenColor>().PlayForward();

	}

	public void	EndOfDemo()
	{
		sb.Append(m_name_first + "," + m_name_last + "," + m_email + "," + GameManager.m_baskets_made + "," + DateTime.Now.ToString() + "," + m_is_press.ToString() + "\n");
		m_string_writer = new StreamWriter(m_path);
		m_string_writer.Write(sb.ToString());
		m_string_writer.Close();
		Destroy(GameManager.s_Inst.gameObject);
		GameObject.Find("LoadingScreen").GetComponent<TweenPosition>().PlayForward();
		GameManager.s_Inst.SaveIAPItems();
		Application.LoadLevel("DemoMenu");
	}

	public void TogglePress()
	{
		m_is_press = !m_is_press;
		m_press_sprite.SetActive(m_is_press);
	}
}
