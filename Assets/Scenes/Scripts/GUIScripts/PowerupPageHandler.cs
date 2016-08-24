using UnityEngine;
using System.Collections;

public class PowerupPageHandler : MonoBehaviour {

	public GameObject m_refill_label;
	public GameObject m_equip_button;
	public GameObject m_equipped_stamp;
	public UILabel m_powerup_count;
	int m_current_count;
	public enum PowerupType{
		Nuke,
		Armor,
		Ace,
		Focus
	}
	public PowerupType m_type;

	// Use this for initialization
	public void Init (int count) {
		m_powerup_count.text = string.Format("{0:n0}", count);;
		m_current_count = count;
		if(m_type.ToString().CompareTo(GameManager.s_Inst.m_equipped_powerup.ToString()) == 0  && m_current_count > 0){
			TurnOnStamp();
		}else if(m_current_count > 0){
			TurnOnButton();
		}else{
			TurnOnLabel();
		}
	}
	

	void TurnOnStamp () {
		m_equipped_stamp.SetActive(true);
		m_refill_label.SetActive(false);
		m_equip_button.SetActive(false);
	}

	void TurnOnLabel(){
		m_refill_label.SetActive(true);
		m_equipped_stamp.SetActive(false);
		m_equip_button.SetActive(false);
	}

	void TurnOnButton(){
		m_equip_button.SetActive(true);
		m_equipped_stamp.SetActive(false);
		m_refill_label.SetActive(false);
	}
}
