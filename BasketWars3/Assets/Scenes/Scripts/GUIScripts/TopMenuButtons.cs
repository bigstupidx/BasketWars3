using UnityEngine;
using System.Collections;

public class TopMenuButtons : MonoBehaviour 
{
	public UIToggle m_toggle;
	public TweenPosition m_setting_panel;

	public void ToggleSettingPanel()
	{
		//Show Menu
		if(!m_toggle.value){
			m_setting_panel.PlayForward();
		}
		//Hide Menu
		else{
			m_setting_panel.PlayReverse();
		}
	}

	public void ClosePanel()
	{
		//Show Menu
		if(m_toggle.value){
			m_setting_panel.PlayReverse();
			m_toggle.value = false;
		}
	}
}
