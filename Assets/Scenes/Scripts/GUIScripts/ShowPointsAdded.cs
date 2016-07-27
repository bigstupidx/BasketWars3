using UnityEngine;
using System.Collections;

public class ShowPointsAdded : MonoBehaviour {
	public UILabel m_points;
	public UITweener m_tween;
	public float m_display_time;

	public void AddPoints(int points){
		m_points.text = "+" + points;
		m_tween.ResetToBeginning();
		m_tween.PlayForward();
		Invoke("HidePoints",m_display_time + m_tween.duration + m_tween.delay);
	}

	void HidePoints(){
		m_tween.PlayReverse();
	}
}
