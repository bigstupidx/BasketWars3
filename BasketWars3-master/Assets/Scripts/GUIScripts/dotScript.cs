using UnityEngine;
using System.Collections;

public class dotScript : MonoBehaviour {
	UILabel m_label;
	float m_speed = 0.3f;
	// Use this for initialization
	void Start () {
		m_label = gameObject.GetComponent<UILabel>();
		Invoke("AddDot",m_speed);
	}

	void AddDot(){
		m_label.text += ". ";
		if(m_label.text.Length > 6)
			m_label.text = "";
		Invoke("AddDot",m_speed);
	}
}
