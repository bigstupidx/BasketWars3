using UnityEngine;
using System.Collections;

public class BossGunBullet : MonoBehaviour {
	private Animator anim;   // Link to the animated sprite
	private bool didExplode = false;
	public GameObject m_explosion;
	
	public enum BulletType
	{
		MachineGun,
		Canon,
	}
	
	public BulletType type;
	
	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>(); // This script must be attached to the sprite to work.	
	}
	
	// Update is called once per frame
	void Update () {
		if (!didExplode) 
		{
			if (GameManager.m_level_complete) {
				didExplode = true;
				Instantiate(m_explosion,transform.position,Quaternion.identity);
				transform.GetComponent<Rigidbody2D>().velocity = Vector3.zero;	
				transform.position = new Vector3(1000,1000,0);
			}
		}	
	}
	
	void OnTriggerEnter2D (Collider2D c) {
		if (didExplode)
			return;
		
		if (c.gameObject.tag == "BallSniperBullet") {
			anim.Play("ArialExplosion");
			
			didExplode = true;
			Instantiate(m_explosion,transform.position,Quaternion.identity);
			transform.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
			transform.position = new Vector3(1000,1000,0);
		}
		
		if (c.gameObject.tag == "BallBomb") {						
				didExplode = true;
			Instantiate(m_explosion,transform.position,Quaternion.identity);
			transform.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
			transform.position = new Vector3(1000,1000,0);
		}
		
		if (c.gameObject.tag == "Wall") {
			Instantiate(m_explosion,transform.position,Quaternion.identity);
			transform.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
			transform.position = Vector3.zero;
			didExplode = false;
			transform.position = new Vector3(1000,1000,0);
		}
			
		if (c.gameObject.name == "Ground") {
			Instantiate(m_explosion,transform.position,Quaternion.identity);
			transform.eulerAngles = Vector3.zero;
			transform.GetComponent<Rigidbody2D>().velocity = Vector3.zero;		
			didExplode = true;
			transform.position = new Vector3(1000,1000,0);
		}
			
		if (c.gameObject.tag == "Ball") {
			Instantiate(m_explosion,transform.position,Quaternion.identity);
			c.gameObject.GetComponent<BallController>().KillBall();
			transform.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
			didExplode = true;
			transform.position = new Vector3(1000,1000,0);
		}
	}
		
	public void ResetBullet(){
		if(didExplode)
			didExplode = false;
	}
	
	// This is called once the Throw animation has compelted playing
    void ExplosionCompleteDelegate() {
        //Destroy(bullet);
		transform.position = new Vector3(0f, -9f, 0);
		if (type == BulletType.MachineGun) {
			anim.Play("MachineGun");	
		}
		else if (type == BulletType.Canon) {
			anim.Play("BossBunkerBullet");
		}
		
		didExplode = false;
    }
}
