using UnityEngine;
using System.Collections;
using System;

public class DailyReward : MonoBehaviour {

	string m_last_time_srting = "TimeSinceLastOpen";
	string m_num_of_consecutive_opens_string = "Consecutive_days";
	int m_time_needed = 86400;
	int server_time;
	//60 = shield
	//70 = focus
	//80 = guide
	//90 = nuke
	int[] rewards = {10,20,30,50,60,
					 10,20,30,50,70,
					 10,20,30,50,80,
					 10,20,30,50,90};
	public string[] m_icon_names;
	int m_num_days = 0;

	public BlackoutPanel m_blackout_panel;
	public UILabel m_coins_label;
	public UILabel m_days_label;
	public UISprite m_reward_icon;

	void Start () {
		m_num_days = PlayerPrefs.GetInt(m_num_of_consecutive_opens_string,-1);
		StartCoroutine(FetchTime());
	}

	void OnLevelWasLoaded () {
		if(Application.loadedLevelName == "MainMenu")
			StartCoroutine(FetchTime());
	}

	IEnumerator FetchTime(){
		string url = "http://basketwars.com/time";
		WWW www = new WWW(url);
		yield return www;
		string time = www.text;
		string[] str = time.Split(new char[]{'.'});
		server_time = (int)(Convert.ToInt64(str[0])) / m_time_needed;
		CheckTime();
	}

	void CheckTime(){
		int last_time = PlayerPrefs.GetInt(m_last_time_srting,-1);
		if(last_time == -1){
			last_time = server_time;
			m_num_days = 0;
			PlayerPrefs.SetInt(m_last_time_srting,server_time);
			PlayerPrefs.SetInt(m_num_of_consecutive_opens_string, m_num_days);
		}else{
			if(server_time - last_time == 0)
				gameObject.SetActive(false);
		}
		if(server_time - last_time > 1){
			m_num_days = 0;
			PlayerPrefs.SetInt(m_last_time_srting,server_time);
			PlayerPrefs.SetInt(m_num_of_consecutive_opens_string, m_num_days);
			return;
		}
		if(server_time - last_time > 0 || m_num_days == -1){
			SetRewardToAvailable();
		}
	}

	void SetRewardToAvailable(){
		Debug.Log("Days = " + m_num_days);
		gameObject.GetComponent<TweenPosition>().PlayForward();
		if(m_num_days > rewards.Length-1){
			m_num_days = 0;
			return;
		}
		m_blackout_panel.MoveIn();
		if(rewards[m_num_days] == 60){
			m_coins_label.text = "+1 Sheild";
			m_reward_icon.spriteName = m_icon_names[1];
		}else if(rewards[m_num_days] == 70){
			m_coins_label.text = "+1 Focus";
			m_reward_icon.spriteName = m_icon_names[2];
		}else if(rewards[m_num_days] == 80){
			m_coins_label.text = "+1 Guide";
			m_reward_icon.spriteName = m_icon_names[3];
		}else if(rewards[m_num_days] == 90){
			m_coins_label.text = "+1 Nuke";
			m_reward_icon.spriteName = m_icon_names[4];	
		}else{
			m_coins_label.text = "+" + rewards[m_num_days].ToString();
			m_reward_icon.spriteName = m_icon_names[0];
		}
		if(m_num_days == 0)
			m_days_label.text = "1 Day";
		else
			m_days_label.text = (m_num_days+1) + " Days";
	}

	public void ClaimReward(){
		gameObject.GetComponent<TweenPosition>().PlayReverse();
		m_blackout_panel.MoveOut();
		if(rewards[m_num_days] == 60){
			SaveLoadManager.m_save_info.m_armor_powerup++;
		}
		else if(rewards[m_num_days] == 70){
			SaveLoadManager.m_save_info.m_focus_powerup++;
		}
		else if(rewards[m_num_days] == 80){
			SaveLoadManager.m_save_info.m_guide_powerup++;
		}
		else if(rewards[m_num_days] == 90){
			SaveLoadManager.m_save_info.m_nuke_powerup++;
		}else{
			GameManager.s_Inst.AddCoins(rewards[m_num_days]);
		}
		SaveLoadManager.s_inst.SaveFile();
		m_num_days++;
		PlayerPrefs.SetInt(m_last_time_srting,server_time);
		PlayerPrefs.SetInt(m_num_of_consecutive_opens_string, m_num_days);
	}
}
