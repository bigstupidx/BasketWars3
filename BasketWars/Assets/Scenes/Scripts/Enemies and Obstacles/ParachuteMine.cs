using UnityEngine;
using System.Collections;

public class ParachuteMine : MonoBehaviour 
{	
	//Animator anim;
	bool isExploded;
	public GameObject m_air_explosion;
	public GameObject m_explosion;
	public enum Type
	{
		Parachute,
		Floating,
	}
	
	public Type type;
	
	// Use this for initialization
	void Start () 
	{
		isExploded = false;
		
		//anim = GetComponent<Animator>(); // This script must be attached to the sprite to work.
	}
	
	// Update is called once per frame
	void Update () {
		if (!isExploded)
		{
			if (type == Type.Parachute) 
			{
				if (GameManager.m_level_complete || GameManager.m_nuke_explosion) 
				{
					doExplosion ();
				}				
			}
			else if (type == Type.Floating) 
			{
				if (GameManager.m_level_complete)
				{
					/*if (GlobalControl.Inst.stageLevel != 11) 
					{
						doExplosion ();
					}*/
					doExplosion();
				}				
			}			
		}
	}
	
	public void doAirExplosion () 
	{		
		Instantiate(m_air_explosion,transform.position,Quaternion.identity);
		DestroyThis();
	}

	public void doExplosion () 
	{		
		Instantiate(m_explosion,transform.position,Quaternion.identity);
		DestroyThis();
	}

	void OnCollisionEnter2D(Collision2D c)
	{
		if(c.gameObject.tag == "Ball")
		{
			c.gameObject.GetComponent<BallController>().KillBall();
		}
		if(c.gameObject.tag == "Ground")
			doExplosion();
		else
			doAirExplosion();
	}
	
    public void DestroyThis() 
	{
        Destroy(this.gameObject);
    }
}
