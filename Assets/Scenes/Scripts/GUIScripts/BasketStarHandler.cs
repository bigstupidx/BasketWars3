using UnityEngine;
using System.Collections;

public class BasketStarHandler : MonoBehaviour {

	public UIProgressBar m_progress;
	public TweenScale m_star_one;
	//public TweenScale m_star_one_frame;
	public TweenScale m_star_two;
	//public TweenScale m_star_two_frame;
	public TweenScale m_star_three;
	//public TweenScale m_star_three_frame;
	public Transform m_left;
	public Transform m_right;
	int m_current_score = 0;
	float m_current_value;
	float m_goal_value;
	float m_time = 0;

	public void Init(){
		Debug.Log (gameObject);
	/*	m_1_star_score = GameManager.s_Inst.m_1_star_score;
		m_2_star_score = GameManager.s_Inst.m_2_star_score;
		m_3_star_score = GameManager.s_Inst.m_3_star_score;
		Vector3 dir = m_right.localPosition - m_left.localPosition;
		Vector3 position1 = m_left.localPosition + dir.normalized * (dir.magnitude * ((float)m_1_star_score / (float)m_3_star_score));
		Vector3 position2 = m_left.localPosition + dir.normalized * (dir.magnitude * ((float)m_2_star_score / (float)m_3_star_score));
		Vector3 position3 = m_left.localPosition + dir.normalized * dir.magnitude - new Vector3(30,0,0);
		m_star_one.transform.localPosition = new Vector3(position1.x,m_star_one.transform.localPosition.y,m_star_one.transform.localPosition.z);
		m_star_two.transform.localPosition = new Vector3(position2.x,m_star_one.transform.localPosition.y,m_star_one.transform.localPosition.z);
		m_star_three.transform.localPosition = new Vector3(position3.x,m_star_one.transform.localPosition.y,m_star_one.transform.localPosition.z); */
	}

	public void AddPoint(){
	/*	m_current_score++;
		m_goal_value = (float)m_current_score/(float)m_3_star_score;
		if(m_current_score == m_1_star_score){
			m_star_one.PlayForward();
			//m_star_one_frame.PlayForward();
		}
		if(m_current_score == m_2_star_score){
			m_star_two.PlayForward();
			//m_star_two_frame.PlayForward();
		}
		if(m_current_score == m_3_star_score){
			m_star_three.PlayForward();
			//m_star_three_frame.PlayForward(); 
		}*/
	}

	public void Update(){
		if(m_progress.value != m_goal_value){
			m_progress.value = Mathf.Lerp(m_current_value,m_goal_value,m_time);
			m_time += Time.deltaTime * 4;
		}else{
			m_current_value = m_goal_value;
			m_time = 0;
		}
	}
}
