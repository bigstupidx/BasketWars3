using UnityEngine;
using System.Collections;

public class BasketBalloon : MonoBehaviour 
{
	public float m_speed;
	public Vector2 m_height_min_max;
	float m_dir_mod = 1;
	
	// Update is called once per frame
	void Update () 
	{
		if(transform.position.y > m_height_min_max.y && m_dir_mod > 0)
			SwitchDirection();
		if(transform.position.y < m_height_min_max.x && m_dir_mod < 0)
			SwitchDirection();
		transform.position += m_speed * Vector3.up * m_dir_mod * Time.deltaTime;
	}

	void SwitchDirection()
	{
		m_dir_mod *= -1;
	}
}
