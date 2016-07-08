using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UnLockLevelHandler : MonoBehaviour 
{
	public UISprite m_level_icon;
	public UILabel m_unlock_title;
	public UILabel m_level_name_text;
	public UIButton m_purchase_button;
	public GameObject m_upgrade_button;
	[SerializeField]
	public List<string> m_level_icons;
	
	public void SetupUnlockStalingrad(){
		m_purchase_button.onClick.Clear();
		m_purchase_button.onClick.Add(new EventDelegate(GameManager.s_Inst.gameObject.GetComponent<ShopMenu>(),"BuyStalingradUnlock"));
		m_purchase_button.onClick.Add(new EventDelegate(m_purchase_button.gameObject.GetComponent<PlayAudioClip>(),"PlayAudio"));
		m_level_icon.spriteName = m_level_icons[0];
		m_unlock_title.text = "UNLOCK STALINGRAD";
		m_level_name_text.text = "STALINGRAD";
	}

	public void SetupUnlockKursk(){
		m_purchase_button.onClick.Clear();
		m_purchase_button.onClick.Add(new EventDelegate(GameManager.s_Inst.gameObject.GetComponent<ShopMenu>(),"BuyKurskUnlock"));
		m_purchase_button.onClick.Add(new EventDelegate(m_purchase_button.gameObject.GetComponent<PlayAudioClip>(),"PlayAudio"));
		m_level_icon.spriteName = m_level_icons[1];
		m_unlock_title.text = "UNLOCK KURSK";
		m_level_name_text.text = "KURSK";
	}

	public void SetupUnlockNormandy(){
		m_purchase_button.onClick.Clear();
		m_purchase_button.onClick.Add(new EventDelegate(GameManager.s_Inst.gameObject.GetComponent<ShopMenu>(),"BuyNormandyUnlock"));
		m_purchase_button.onClick.Add(new EventDelegate(m_purchase_button.gameObject.GetComponent<PlayAudioClip>(),"PlayAudio"));
		m_level_icon.spriteName = m_level_icons[2];
		m_unlock_title.text = "UNLOCK NORMANDY";
		m_level_name_text.text = "NORMANDY";
	}
}
