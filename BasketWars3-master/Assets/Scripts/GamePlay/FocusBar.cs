using UnityEngine;
using System.Collections;

public class FocusBar : MonoBehaviour {

	UIProgressBar m_bar;
	TweenAlpha m_alpha_tween;
	bool m_focus_active;
	float m_max_focus_time = 10;
	float m_focus_timer;

	// Use this for initialization
	void Start () {
		m_bar = gameObject.GetComponent<UIProgressBar>();
		m_alpha_tween = gameObject.GetComponent<TweenAlpha>();
		m_focus_timer = m_max_focus_time;
	}
	
	
	// Update is called once per frame
	void Update () {
		if(m_focus_active){
			m_focus_timer -= Time.deltaTime;
			m_bar.value = m_focus_timer / m_max_focus_time;
			if(m_focus_timer <= 0){
				m_alpha_tween.PlayReverse();
				m_focus_active = false;
			}
		}
	}
	

	public void StarFocusTimer(){
		m_alpha_tween.PlayForward();
		m_focus_active = true;
	}
}
