using UnityEngine;
using System.Collections;

public class Reset : MonoBehaviour {
	
	public enum ResetType{
		Progress,
		Stamina,
		Ammo,
		Coins
	}
	
	public ResetType type;
	GameObject m_level_management;
	void Start(){
		m_level_management = GameObject.Find("GameManagment");
	}

	public void OnClick(){
		if(type == ResetType.Progress){
			m_level_management.GetComponent<StageUnlocker>().SetDeveloperMode(false);
			m_level_management.GetComponent<StageUnlocker>().ResetProgress();			
		}
		else if(type == ResetType.Stamina)
			m_level_management.GetComponent<StaminaGuage>().RefillStamina();
	}
}
