using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BallLauncher : MonoBehaviour {
	public Transform m_target;
	public GameObject m_ball;
	List<Vector3> m_points = new List<Vector3>();
	public float m_time_step;


	// Use this for initialization
	void Start () {
		Invoke("ThrowBall",2);
	}

	void ThrowBall(){
		GameObject ball = (GameObject)Instantiate(m_ball,transform.position,Quaternion.identity);
		ball.GetComponent<Rigidbody2D>().velocity = CalculateVelocity(Random.Range(2.0f,3.0f));
		Invoke("ThrowBall",Random.Range(1.0f,4.0f));		
	}

	Vector2 CalculateVelocity(float time_to_basket){
		float Vx = (m_target.position.x - transform.position.x) / time_to_basket;
		float Vy = (transform.position.y + 0.5f * (m_ball.GetComponent<Rigidbody2D>().gravityScale * Physics2D.gravity.y) 
		            * time_to_basket * time_to_basket - m_target.position.y)
					/time_to_basket;
		SetupPoints(time_to_basket,transform.position,new Vector3(Vx,-Vy,0));
		return new Vector2(Vx,-Vy);
	}

	void SetupPoints(float time, Vector3 position, Vector3 initVel){
		m_points.Clear();
		for(float i = 0; i < time; i += m_time_step){
			if(i == 0) {
				m_points.Add(position);
				continue;
			}
			m_points.Add(position + (initVel * i) + .5f * new Vector3(Physics2D.gravity.x, Physics2D.gravity.y,1) * i * i);
		}
	}

	void OnDrawGizmos(){
		Gizmos.color = Color.red;
		if(m_points.Count > 1){
			for(int i = 0; i < m_points.Count; i++){
				Gizmos.DrawSphere(m_points[i],.05f);
				if(i < m_points.Count -1)
					Gizmos.DrawLine(m_points[i],m_points[i+1]);
				else
					Gizmos.DrawLine(m_points[i],m_target.position);
			}
		}
	}
}
