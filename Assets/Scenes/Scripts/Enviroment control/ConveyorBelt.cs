using UnityEngine;
using System.Collections;

public class ConveyorBelt : MonoBehaviour 
{
	float m_dir = 1;
	public float m_speed = 3;
	public Transform m_height_limit;
	float m_bottom_limit;
	public float m_pause_timer = 0;
	float m_pause_time;

	void Start()
	{
		m_bottom_limit = transform.position.y;
		m_pause_time = m_pause_timer;
	}

	// Update is called once per frame
	void Update () 
	{
		if(m_pause_time <= 0){
			if(transform.position.y >= m_height_limit.position.y)
			{
				m_dir = -1;
				m_pause_time = m_pause_timer;
			}
			else if(transform.position.y < m_bottom_limit)
			{
				m_dir = 1;
				m_pause_time = m_pause_timer;
			}

			transform.position += Vector3.up * m_dir * m_speed * Time.deltaTime;
		}
		else
			m_pause_time -= Time.deltaTime;
	}

	public void PauseHoop(){
		m_pause_time = 20;
	}
}
