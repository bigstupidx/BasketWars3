using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour 
{
	protected Animator anim;  
	public float m_speed;
	protected GameManager m_game_manager;
	// Use this for initialization
	protected virtual void Awake ()
	{
		anim = GetComponent<Animator>(); // This script must be attached to the sprite to work.	
		m_game_manager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
	}
	
	// Update is called once per frame
	protected virtual void Update () 
	{	
	}

	public virtual void Fire(Vector3 dir)
	{
        transform.right = dir;
        GetComponent<Rigidbody2D>().velocity = dir * m_speed;
	}
	protected virtual void  OnTriggerEnter2D (Collider2D c) 
	{
        if (c.gameObject.tag == "Zombie")
        {
            ZombieController temp = c.GetComponent<ZombieController>();
            if (temp.health == 1)
            {
                c.GetComponent<Animator>().Play("Shot");
                c.GetComponent<PolygonCollider2D>().enabled = false;
                GameManager.s_Inst.removeZombie(c.gameObject);
                
            } else
            {
                c.GetComponent<Animator>().Play("Health_Shot");
                temp.zombie_speed -= 1;
                temp.health -= 1;
            }
        }
        DestroyBullet();
	}

	public void DestroyBullet(){
        Destroy(gameObject);
	}
	
}
