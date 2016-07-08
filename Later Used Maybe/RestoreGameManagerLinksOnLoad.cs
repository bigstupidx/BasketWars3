using UnityEngine;
using System.Collections;

public class RestoreGameManagerLinksOnLoad : MonoBehaviour {
	public UIButton m_logout_button;
	public UIButton m_paid_refill;
	public UIButton m_unlimited_stamina;
	public UIButton m_reset_password;
	public UIButton m_login;
	public UIButton m_facebook_login;
	public UIButton m_guest_login;
	public UIButton m_register_button;
	public UIButton m_change_password_button;
	public UIButton m_achievement_button;
	public UIButton m_leaderbaord_button;
	public UIButton m_unlock_level_button;
	public GameObject m_britain;
	public GameObject m_stalingrad;
	public GameObject m_kursk;
	public GameObject m_normandy;

	void OnLevelWasLoaded(){
		if(!m_logout_button.onClick.Contains(new EventDelegate(GameManager.s_Inst.gameObject.GetComponent<KiiManager>(),"Logout")))
			m_logout_button.onClick.Add(new EventDelegate(GameManager.s_Inst.gameObject.GetComponent<KiiManager>(),"Logout"));

		if(!m_paid_refill.onClick.Contains(new EventDelegate(GameManager.s_Inst.gameObject.GetComponent<ShopMenu>(),"BuyStaminaRefill")))
			m_paid_refill.onClick.Add(new EventDelegate(GameManager.s_Inst.gameObject.GetComponent<ShopMenu>(),"BuyStaminaRefill"));

		if(!m_unlimited_stamina.onClick.Contains(new EventDelegate(GameManager.s_Inst.gameObject.GetComponent<ShopMenu>(),"BuyUnlimitedStamina")))
			m_unlimited_stamina.onClick.Add(new EventDelegate(GameManager.s_Inst.gameObject.GetComponent<ShopMenu>(),"BuyUnlimitedStamina"));

		if(!m_reset_password.onClick.Contains(new EventDelegate(GameManager.s_Inst.gameObject.GetComponent<KiiManager>(),"ResetPassword")))
			m_reset_password.onClick.Add(new EventDelegate(GameManager.s_Inst.gameObject.GetComponent<KiiManager>(),"ResetPassword"));
	
		if(!m_login.onClick.Contains(new EventDelegate(GameManager.s_Inst.gameObject.GetComponent<KiiManager>(),"LoginWithUsernameOnClick")))
			m_login.onClick.Add(new EventDelegate(GameManager.s_Inst.gameObject.GetComponent<KiiManager>(),"LoginWithUsernameOnClick"));

		if(!m_facebook_login.onClick.Contains(new EventDelegate(GameManager.s_Inst.gameObject.GetComponent<KiiManager>(),"FacebookLogin")))
			m_facebook_login.onClick.Add(new EventDelegate(GameManager.s_Inst.gameObject.GetComponent<KiiManager>(),"FacebookLogin"));

		if(!m_guest_login.onClick.Contains(new EventDelegate(GameManager.s_Inst.gameObject.GetComponent<KiiManager>(),"LoginAsGuest")))
			m_guest_login.onClick.Add(new EventDelegate(GameManager.s_Inst.gameObject.GetComponent<KiiManager>(),"LoginAsGuest"));

		if(!m_register_button.onClick.Contains(new EventDelegate(GameManager.s_Inst.gameObject.GetComponent<KiiManager>(),"BuildUser")))
			m_register_button.onClick.Add(new EventDelegate(GameManager.s_Inst.gameObject.GetComponent<KiiManager>(),"BuildUser"));

		if(!m_change_password_button.onClick.Contains(new EventDelegate(GameManager.s_Inst.gameObject.GetComponent<KiiManager>(),"ChangePassword")))
			m_change_password_button.onClick.Add(new EventDelegate(GameManager.s_Inst.gameObject.GetComponent<KiiManager>(),"ChangePassword"));

		if(!m_achievement_button.onClick.Contains(new EventDelegate(GameManager.s_Inst.gameObject.GetComponent<AchievementManager>(),"ShowAchievements")))
			m_achievement_button.onClick.Add(new EventDelegate(GameManager.s_Inst.gameObject.GetComponent<AchievementManager>(),"ShowAchievements"));

		if(!m_leaderbaord_button.onClick.Contains(new EventDelegate(GameManager.s_Inst.gameObject.GetComponent<AchievementManager>(),"ShowLeaderboards")))
			m_leaderbaord_button.onClick.Add(new EventDelegate(GameManager.s_Inst.gameObject.GetComponent<AchievementManager>(),"ShowLeaderboards"));
		         
		if(GameManager.s_Inst.gameObject.GetComponent<StageUnlocker>().m_britain_button == null){
			GameManager.s_Inst.gameObject.GetComponent<StageUnlocker>().m_britain_button = m_britain;
		}
		if(GameManager.s_Inst.gameObject.GetComponent<StageUnlocker>().m_stalingrad_button == null){
			GameManager.s_Inst.gameObject.GetComponent<StageUnlocker>().m_stalingrad_button = m_stalingrad;
		}
		if(GameManager.s_Inst.gameObject.GetComponent<StageUnlocker>().m_kursk_button == null){
			GameManager.s_Inst.gameObject.GetComponent<StageUnlocker>().m_kursk_button = m_kursk;
		}
		if(GameManager.s_Inst.gameObject.GetComponent<StageUnlocker>().m_normandy_button == null){
			GameManager.s_Inst.gameObject.GetComponent<StageUnlocker>().m_normandy_button = m_normandy;
		}		
	}

}
