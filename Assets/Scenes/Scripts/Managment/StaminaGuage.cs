using UnityEngine;
using System.Collections;
using System;
#pragma warning disable 0618
public class StaminaGuage : MonoBehaviour 
{
	int min_time = 480;
	public int m_stamina;
	public int m_last_update;
	public UILabel m_stamina_counter;
	public UIProgressBar m_stamaina_slider;
	public UISprite m_timer_sprite;
	public int m_max_stamina = 10;
	public int m_next_menu;
	public int m_curr_time;
	public string m_time_string;
	public int server_time;
	public int device_time;
	public int adjusted_device_time;
	public float m_timer = 480;
	public long m_paused_time;
	public long m_unpaused_time;

	/// <summary>
	/// Grabs the stamina bar in the level and sets it.
	/// </summary>
	void OnLevelWasLoaded()
	{
		if(Application.loadedLevelName == "MainMenu"){
			if(!GameManager.s_Inst.m_unlimited_stamina && GameObject.Find("UnlimitedLabel") != null){
				GameObject.Find("UnlimitedLabel").SetActive(false);
			}else{
				m_stamina = 10;
			}
		}
		if(GameObject.Find("StaminaMeter") != null){
			m_stamina = PlayerPrefs.GetInt("StaminaValue");
			if(GameObject.Find("StaminaBar") != null){
				m_stamaina_slider = GameObject.Find("StaminaBar").GetComponent<UIProgressBar>();
				if(m_stamaina_slider != null){
					m_stamina_counter = m_stamaina_slider.transform.FindChild("Label").GetComponent<UILabel>();
					m_timer_sprite = m_stamaina_slider.transform.FindChild("Timer").GetComponent<UISprite>();
				}
				if(m_stamina_counter != null){
					m_stamina_counter.text = m_time_string;					
					if(m_stamaina_slider != null){
						m_stamaina_slider.value = (m_stamina-1)/10.0f;
						if(m_stamina <= 0){
							m_stamaina_slider.transform.FindChild("TopProgress").GetComponent<UISprite>().enabled = false;
						}
					}
				}	
			}
		}
	}
	
	void Start()
	{
		if(GameManager.s_Inst.m_unlimited_stamina){
			m_stamina = 10;
		}
		m_stamina = PlayerPrefs.GetInt("StaminaValue",-1);
		if(m_stamina < 0)
			m_stamina = m_max_stamina;
		if(m_stamina_counter != null)
			m_stamina_counter.text = m_time_string;	
		m_stamaina_slider.value = (m_stamina-1)/10.0f;
		if(m_stamaina_slider.value <0){
			m_stamaina_slider.transform.FindChild("TopProgress").GetComponent<UISprite>().enabled = false;
		}
		else
			m_stamaina_slider.transform.FindChild("TopProgress").GetComponent<UISprite>().enabled = true;
		m_last_update = PlayerPrefs.GetInt("LastUpdate",-1);
		if(Application.internetReachability != NetworkReachability.NotReachable){
			StartCoroutine(FetchTime(true));
		}
	}

	void Init()
	{
		if(server_time >= m_last_update + min_time){
			int diff = server_time - m_last_update;
				while( diff > min_time){
					if(m_stamina < m_max_stamina){
						IncreaseStamina(1);
						m_last_update += min_time;
						PlayerPrefs.SetInt("LastUpdate",m_last_update);
						diff -= min_time;
					}else{
						diff = 0;
						m_last_update = server_time;
						PlayerPrefs.SetInt("LastUpdate",m_last_update);
					}
				}
			if(diff > 0)
				m_timer = diff;
		}
	}

	IEnumerator FetchTime(bool init){
		string url = "http://basketwars.com/time";
		WWW www = new WWW(url);
		yield return www;
		string time = www.text;
		string[] str = time.Split(new char[]{'.'});
		server_time = (int)(Convert.ToInt64(str[0]));
		if(m_last_update == -1){
			/*if(GameManager.s_Inst.m_IS_LITE_MODE)
				m_timer = min_time_LITE;
			else*/
				m_timer = min_time;
		}
		else{
			/*if(GameManager.s_Inst.m_IS_LITE_MODE)
				m_timer = m_last_update + min_time_LITE - server_time;
			else*/
				m_timer = m_last_update + min_time - server_time;
		}
		if(init)
			Init();
	}

	void OnApplicationPause(bool paused){
		if(paused){
			m_paused_time = DateTime.Now.Ticks;
			if(m_stamina < m_max_stamina){
				//UnityEngine.iOS.NotificationServices.CancelAllLocalNotifications();
				float m_full_time;
				/*if(GameManager.s_Inst.m_IS_LITE_MODE){
					m_full_time = (m_max_stamina - m_stamina) * min_time_LITE + m_timer;
				}else{*/
					m_full_time = (m_max_stamina - m_stamina) * min_time + m_timer;
				//}
				//SetLocalNotification(m_full_time,"Stamina Meter Full!","Your stamina has fully refilled!",1);
			}
		}
		if(!paused){
			m_unpaused_time = DateTime.Now.Ticks;
			long diff = m_unpaused_time - m_paused_time;
			m_timer -= diff / 10000000.0f;
		}
	}
	
	/// <summary>
	/// Decreases the stamina value and saves it off to playerprefs.
	/// </summary>
	/// <param name='val'>
	/// Value to decrease by.
	/// </param>
	public void DecreaseStamina(int val)
	{
		if(!GameManager.s_Inst.m_unlimited_stamina){
			if(m_stamina == m_max_stamina)
			{
					m_timer = min_time;
			}
			m_stamina -= val;
			if(m_stamina < 0)
				m_stamina = 0;
			PlayerPrefs.SetInt("StaminaValue",m_stamina);
			if(m_stamina_counter != null)
				m_stamina_counter.text = m_time_string;	
			if(m_stamaina_slider != null){
				m_stamaina_slider.value = (m_stamina-1)/10.0f;
				if(m_stamaina_slider.value <0){
					m_stamaina_slider.transform.FindChild("TopProgress").GetComponent<UISprite>().enabled = false;
				}
			}
		}
		//GameObject.FindWithTag("StaminaBarRemove").GetComponent<StaminaAnimation>().SetBlip((float)m_stamina/(float)m_max_stamina);
	}
	
	/// <summary>
	/// Increases the staminavalue and save it off in playerprefs.
	/// </summary>
	/// <param name='val'>
	/// Value to increase by.
	/// </param>
	public void IncreaseStamina(int val)
	{
		m_stamina += val;
		if(m_stamina > m_max_stamina)
		{
			m_stamina = m_max_stamina;
			/*if(GameManager.s_Inst.m_IS_LITE_MODE)
				m_timer = min_time_LITE;
			else*/
				m_timer = min_time;
		}
		PlayerPrefs.SetInt("StaminaValue",m_stamina);
		if(m_stamina_counter != null)
			m_stamina_counter.text = m_time_string;	
		if(m_stamaina_slider != null){
			m_stamaina_slider.value = (m_stamina-1)/10.0f;
			if(m_stamaina_slider.value < 0){
				m_stamaina_slider.transform.FindChild("TopProgress").GetComponent<UISprite>().enabled = false;
			}
		}
	}
	
	
	/// <summary>
	/// Gets the stamina value.
	/// </summary>
	/// <returns>
	/// The stamina value.
	/// </returns>
	public int GetStamina()
	{
		return m_stamina;
	}
	
	/// <summary>
	/// Checks to see if 30 mmins has passed then increases stamina.
	/// </summary>
	void Update()
	{
		if(GameManager.s_Inst != null && !GameManager.s_Inst.m_unlimited_stamina && GameManager.s_Inst.m_current_game_state == GameManager.GameState.MainMenu){
			if(m_stamina < m_max_stamina)
			{
				if(GameManager.s_Inst.m_current_game_state == GameManager.GameState.MainMenu && m_timer_sprite != null)
					m_timer_sprite.color = new Color(1,1,1,1);
				m_timer -= RealTime.deltaTime;
				int seconds = (int)m_timer;
				int mins = seconds / 60;
				seconds %= 60;
				if(seconds < 10)
					m_time_string = "+" + mins.ToString() + ":0" + seconds.ToString();
				else
					m_time_string = "+" + mins.ToString() + ":" + seconds.ToString();
				if(m_stamina_counter != null)
					m_stamina_counter.text = m_time_string;	
				if(m_timer <= 0)
				{
					AddStaminaFromTime();
				}
			}
			else{
				if(m_stamina_counter != null && m_timer_sprite != null){
					m_stamina_counter.text = "";
					m_timer_sprite.color = new Color(1,1,1,0);
				}
			}
    		
			GameObject temp1 = GameObject.Find ("StaminaBar");
			if (temp1 != null) {
				m_stamaina_slider = temp1.GetComponent<UIProgressBar> ();
				m_stamina_counter = m_stamaina_slider.transform.FindChild("Label").GetComponent<UILabel>();
			}
			if(m_stamina_counter != null && m_timer_sprite != null){
				m_stamina_counter.text = "";
				m_timer_sprite.color = new Color(1,1,1,0);
			}
		}
	}
	
	/// <summary>
	/// Adds the stamina then increases the time it checks 8 mins. This allows it to recharge stamina after a large break.
	/// </summary>
	void AddStaminaFromTime()
	{
		while(m_timer <= 0){
			IncreaseStamina(1);
			/*if(GameManager.s_Inst.m_IS_LITE_MODE){
				m_last_update += min_time_LITE;
				m_timer += min_time_LITE;
			}else{*/
				m_last_update += min_time;
				m_timer += min_time;
			//}
		}
	}
	
	/// <summary>
	/// Refills the stamina guage.
	/// </summary>  
	public void RefillStamina()
	{
		GameManager m_manager = GetComponent<GameManager>();
		m_stamina = m_max_stamina;
		PlayerPrefs.SetInt("StaminaValue",m_stamina);
		if(m_stamina_counter != null)
			m_stamina_counter.text = m_time_string;	
		if(m_stamaina_slider != null){
			m_stamaina_slider.value = (m_stamina-1)/10.0f;
			if(m_stamaina_slider.value <0){
				m_stamaina_slider.transform.FindChild("TopProgress").GetComponent<UISprite>().enabled = false;
			}
		}
		if(m_manager.m_current_game_state == GameManager.GameState.Gameplay)
		{
			m_manager.MoveOutMenu("StaminaPanel","MoveInlevelFailed");
		}
	}

	//public void SetLocalNotification(float _time, string _title, string _body, int _badge_number){
	//	UnityEngine.iOS.LocalNotification m_note = new UnityEngine.iOS.LocalNotification();
	//	m_note.fireDate = DateTime.Now.AddSeconds(_time);
	//	m_note.alertAction = _title;
	//	m_note.alertBody = _body;
	//	m_note.applicationIconBadgeNumber = _badge_number;
	//	m_note.hasAction = true;//?
	//	m_note.repeatCalendar = UnityEngine.iOS.CalendarIdentifier.GregorianCalendar;
	//	m_note.repeatInterval = UnityEngine.iOS.CalendarUnit.Era;
	//	m_note.soundName = UnityEngine.iOS.LocalNotification.defaultSoundName;
	//	UnityEngine.iOS.NotificationServices.ScheduleLocalNotification(m_note);
	//}
}
