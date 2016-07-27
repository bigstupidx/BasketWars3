using UnityEngine;
using System.Collections;

public class AirBlastController : MonoBehaviour 
{
	float m_blast_time;
	float min_time = 2;
	float max_time = 7;
	Animator m_animator;
	public Rect m_max_movement = new Rect(2,2,2,2); // use this as a max and min movement area for each blast.
	bool m_is_animating = false;
	Vector3 m_move_dir;
	public float m_speed;
	Vector3 m_start_pos;
	// Use this for initialization
	void Start () 
	{
		m_start_pos = transform.position;
		m_animator = gameObject.GetComponent<Animator>();
		m_blast_time = Random.Range(min_time,max_time);
	}
	
	// Update is called once per frame
	void Update () 
	{
		m_blast_time -= Time.deltaTime;
		if(m_blast_time < 0)
		{
			m_is_animating = true;
			m_animator.Play("Blast");
			m_blast_time = Random.Range(min_time,max_time);
		}
		else if(!m_is_animating)
		{
			transform.position += m_move_dir * m_speed * Time.deltaTime;
			if(transform.position.x > m_start_pos.x + m_max_movement.width || transform.position.x < m_start_pos.x - m_max_movement.x)
			{
				m_move_dir.x *= -1;
			}
			if(transform.position.y > m_start_pos.y + m_max_movement.height|| transform.position.y < m_start_pos.y - m_max_movement.y)
			{
				m_move_dir.y *= -1;
			}
		}
	}

	/// <summary>
	/// Called when the blast animation is finished and returns to Idle. Should only ever be called from the animation.
	/// </summary>
	public void StoppedAnimating()
	{
		if(m_is_animating)
		{
			m_is_animating = false;
			m_move_dir = Random.insideUnitCircle;
		}
	}

	public void StartDistantExplosionSound (){
		GetComponent<AudioSource>().Play ();
	}
}
