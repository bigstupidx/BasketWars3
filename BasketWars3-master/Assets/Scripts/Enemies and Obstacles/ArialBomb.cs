using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class ArialBomb : MonoBehaviour 
{	
	Vector3 storePosition;
	bool isExploded = false;
	public float m_start_delay;
	public float m_drop_time = 1;
	float m_drop_timer;
	public GameObject m_explosion;
	public float m_gravity_scale;
	bool m_can_drop = false;
	// Use this for initialization
	void Start () 
	{
		storePosition = transform.position;
		if(m_start_delay > 0){
			Invoke("CanDrop",m_start_delay);
			GetComponent<Rigidbody2D>().gravityScale = 0;
		}else{
			resetBomb();
		}
	}
	
	// Update is called once per frame
	void Update () 
	{		
		if(m_can_drop){
			if (!isExploded)
			{
				if (GameManager.m_level_complete || GameManager.m_nuke_explosion) 
				{
					doExplosion();
					GetComponent<Renderer>().enabled = false;
				}	
			}		
			else
			{
				if(!GameManager.m_nuke_explosion){
					m_drop_timer += Time.deltaTime;
				}
				if(m_drop_timer >= m_drop_time){
					resetBomb();
				}
			}
		}
	}
	
	public void doExplosion ()	
	{
		if(!isExploded) 
		{
			Instantiate(m_explosion,transform.position,Quaternion.identity);
			transform.GetComponent<Rigidbody2D>().gravityScale = 0;
			transform.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
			transform.GetComponent<Rigidbody2D>().angularVelocity = 0;
			transform.rotation = Quaternion.identity;
			transform.position = storePosition;
			isExploded = true;
		}
	}
	
	public void resetBomb () 
	{
		isExploded = false;
		m_drop_timer = 0;
		Invoke ("dropBomb", 0.01f);
	}
	
	void dropBomb () 
	{
		this.gameObject.transform.GetComponent<Rigidbody2D>().gravityScale = m_gravity_scale;
	}

	void OnCollisionEnter2D(Collision2D c)
	{
		if(c.gameObject.tag == "Ball")
		{
			c.gameObject.GetComponent<BallController>().KillBall();
		}
		doExplosion();
	}

	void CanDrop(){
		m_can_drop = true;
		dropBomb();
	}
}
