using UnityEngine;
using System.Collections;

public class Rocket : MonoBehaviour {
	public Vector3 m_dir;
	public float m_speed;
	public Vector3 m_goal;
	public GameObject m_explosion;
	// Update is called once per frame
	void Update () {
		if(m_dir.magnitude != 0){
			transform.position += m_dir * m_speed * Time.deltaTime;
		}
		if((transform.position - m_goal).x > 0){
			Explode();
		}
	}

	public void SetGoal(Vector3 goal){
		m_goal = goal;
		m_dir = (m_goal - transform.position).normalized;
		transform.right = m_dir;
	}

	public void Explode(){
		Instantiate(m_explosion,transform.position,Quaternion.identity);
		Destroy(gameObject);
	}
}
