using UnityEngine;
using System.Collections;

public class EnableWithToggle : MonoBehaviour {
	public UIWidget m_widget;
	public UIToggle m_toggle_target;

	public void EnableWhenTrue(){
		m_widget.enabled = m_toggle_target.value;
	}

	public void EnableWhenFalse(){
		m_widget.enabled = !m_toggle_target.value;
	}
}
