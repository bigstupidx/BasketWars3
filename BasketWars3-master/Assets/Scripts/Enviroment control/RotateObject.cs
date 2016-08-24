using UnityEngine;
using System.Collections;

public class RotateObject : MonoBehaviour 
{
	public float m_speed;
	float starting_speed;
	float curr_time = 0;
	float lerp_time;
	void Start(){
		starting_speed = m_speed;
	}
	// Update is called once per frame
	void Update (){
		transform.Rotate(Vector3.forward,m_speed * RealTime.deltaTime);
	}

	public IEnumerator LerpToStop(float time){
		curr_time = 0;
		lerp_time = curr_time/time;
		while(lerp_time < 1){
			m_speed = Mathf.Lerp(starting_speed,0,lerp_time);
			curr_time += Time.deltaTime;
			lerp_time = curr_time/time;
			yield return null;
		}
		m_speed = 0;
	}

	public IEnumerator LerpToFullSpeed(float time){
		 curr_time = 0;
		 lerp_time = curr_time/time;
		while(lerp_time < 1){
			m_speed = Mathf.Lerp(0,starting_speed,lerp_time);
			curr_time += Time.deltaTime;
			lerp_time = curr_time/time;
			yield return null;
		}
		m_speed = starting_speed;
	}
}
