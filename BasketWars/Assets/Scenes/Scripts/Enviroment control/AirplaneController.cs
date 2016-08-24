using UnityEngine;
using System.Collections;

public class AirplaneController : MonoBehaviour 
{
	Vector3 m_dir = Vector3.right;
	public float m_speed = 5;
	float m_start_timer = 3;
	bool m_flying = false;
	Vector3 m_start_pos;
	public float m_fly_length;
	public bool random_speed = false;
	// Use this for initialization
	void Start () 
	{
		if(random_speed)
			m_speed += Random.Range(-1.0f,1.0f);
		m_start_timer += Random.Range(-1.0f,1.0f);
		m_start_pos = transform.position;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(!m_flying)
		{
			m_start_timer -= Time.deltaTime;
			if(m_start_timer <= 0)
				m_flying = true;
		}
		else
		{
			transform.position += m_dir * m_speed * Time.deltaTime;
			if(Mathf.Abs(transform.position.x - m_start_pos.x) > m_fly_length)
				transform.position = m_start_pos;
		}
	}
}
