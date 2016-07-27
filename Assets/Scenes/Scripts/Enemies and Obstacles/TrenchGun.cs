using UnityEngine;
using System.Collections;

public class TrenchGun : MonoBehaviour 
{
	public float m_shot_time = 3;
	float m_shot_timer;
	float m_between_shot_time = 0.3f;
	float m_between_shot_timer;
	int m_shot_count = 0;
	public GameObject m_bullet;
	public Transform m_bullet_spawn;
	float m_rotation_dir = 1;
	float m_rotation_value = -30.0f;
	public float m_speed = 5;
	bool m_first_shot = false;
	// Use this for initialization
	void Start () 
	{
		m_shot_timer = m_shot_time;
		m_between_shot_timer = m_between_shot_time;
	}
	
	// Update is called once per frame
	void Update () 
	{
		m_shot_timer -= Time.deltaTime;
		if(m_shot_timer <= 0)
		{
			if(!m_first_shot)
			{
				FireBullet();
				m_first_shot = true;
			}
			m_between_shot_timer -= Time.deltaTime;
			if((transform.rotation.eulerAngles.z < 180 && m_rotation_dir == -1) || (transform.rotation.eulerAngles.z > 150 && m_rotation_dir == 1))
				transform.Rotate(Vector3.forward,m_rotation_value * m_rotation_dir * Time.deltaTime);
			if(m_between_shot_timer <= 0)
			{
				FireBullet();
				m_between_shot_timer = m_between_shot_time;
				m_shot_count++;
				if(m_shot_count > 2)
				{
					m_shot_timer = m_shot_time;
					m_shot_count = 0;
					m_rotation_dir *= -1;
					m_first_shot = false;
				}
			}
		}
	}

	void FireBullet()
	{
		GameObject go = (GameObject)Instantiate(m_bullet, m_bullet_spawn.position, m_bullet_spawn.rotation);
		go.GetComponent<Rigidbody2D>().velocity = -go.transform.right * m_speed;
	}
}
