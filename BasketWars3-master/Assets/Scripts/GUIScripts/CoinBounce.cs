using UnityEngine;
using System.Collections;

public class CoinBounce : MonoBehaviour {

	int m_max_height = 34;
	int m_min_height = 14;
	int m_bounce_1 = 20;
	int m_bounce_2 = 16;
	float dir = 1;
	float speed = 120;
	float wait_timer = 5;
	enum BounceState{
		First,
		Second,
		Third,
	}

	BounceState m_current_state = BounceState.First;

	void Update(){
		if(wait_timer <= 0){
			transform.localPosition += Vector3.up * dir * Time.deltaTime * speed;
			if(m_current_state == BounceState.First){
				if(dir == 1 && transform.localPosition.y >= m_max_height)
					dir = -1;
				else if(dir == -1 && transform.localPosition.y <= m_min_height){
					dir = 1;
					m_current_state = BounceState.Second;
					speed = 50;
				}
			}
			else if(m_current_state == BounceState.Second){
				if(dir == 1 && transform.localPosition.y >= m_bounce_1)
					dir = -1;
				else if(dir == -1 && transform.localPosition.y <= m_min_height){
					dir = 1;
					m_current_state = BounceState.Third;
					speed = 30;
				}
			}
			else if(m_current_state == BounceState.Third){
				if(dir == 1 && transform.localPosition.y >= m_bounce_2)
					dir = -1;
				else if(dir == -1 && transform.localPosition.y <= m_min_height){
					dir = 1;
					m_current_state = BounceState.First;
					wait_timer = 5;
					speed = 120;
				}
			}
		}
		else
			wait_timer -= Time.deltaTime;
	}
}
