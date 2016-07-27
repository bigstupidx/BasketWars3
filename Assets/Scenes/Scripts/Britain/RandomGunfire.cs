using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Animator))]
public class RandomGunfire : MonoBehaviour 
{
	
	float m_gunfire_timer = 0;
	public Animator m_animator;
	public float m_timer_multiplyer = 1;
	// Update is called once per frame
	void start()
	{
		//m_animator = transform.GetComponent<Animator>();
	}
	void Update () 
	{
		m_gunfire_timer -= Time.deltaTime;
		if(m_gunfire_timer <= 0)
		{
			m_animator.Play("Fire");
			m_gunfire_timer = Random.Range(0.01f * m_timer_multiplyer,0.5f * m_timer_multiplyer);
		}
	}
}
