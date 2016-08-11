using UnityEngine;
using System.Collections;

public class MuteSound : MonoBehaviour {
	public UISprite m_sound_icon;
	public Sprite m_enabled;
	public Sprite m_disabled;
	public UILabel m_button_label;

	public void Start(){
		if(PlayerPrefs.GetInt("Game Volume",1) == 0){
			GameManager.s_Inst.m_current_volume = 0;
			AudioListener.volume = GameManager.s_Inst.m_current_volume;
			if(m_button_label != null)
				m_button_label.text = "OFF";
			//m_button_label.text = "UNMUTE AUDIO";
			//m_sound_icon.sprite2D = m_disabled;
		}
	}

	public void ToggleSound(){
		if(GameManager.s_Inst.m_current_volume == 0){
			PlayerPrefs.SetInt("Game Volume",1);
			GameManager.s_Inst.m_current_volume = 1;
			if(m_button_label != null)
				m_button_label.text = "ON";
			//m_button_label.text = "MUTE AUDIO";
			//m_sound_icon.sprite2D = m_enabled;
			
		}
		else{
			PlayerPrefs.SetInt("Game Volume",0);
			GameManager.s_Inst.m_current_volume = 0;
			if(m_button_label != null)
				m_button_label.text = "OFF";
			//m_button_label.text = "UNMUTE AUDIO";
			//m_sound_icon.sprite2D = m_disabled;
			
		}

		AudioListener.volume = GameManager.s_Inst.m_current_volume;
	}
}
