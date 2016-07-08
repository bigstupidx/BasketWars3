using UnityEngine;
using System.Collections;

public class CrashingPlane : MonoBehaviour 
{
	Vector2 m_start_pos;
	public Transform m_other_pos;
	bool m_start = true;
	public Transform m_flight_dir;
	public float m_speed;
	public GameObject m_water_explosion;
	float m_timer = 3;
	// Use this for initialization
	void Start () 
	{
		m_start_pos = transform.position;
	}

	void Update()
	{
		if(m_timer <= 0)
			transform.position += m_flight_dir.right * m_speed * Time.deltaTime;
		else
			m_timer -= Time.deltaTime;
	}
	void OnTriggerEnter2D(Collider2D c)
	{
		Instantiate(m_water_explosion,transform.position,Quaternion.identity);		
		if(m_start)
			transform.position = m_other_pos.position;
		else
			transform.position = m_start_pos;
		m_start = !m_start;
		m_timer = Random.Range(3,10);
	}
}
