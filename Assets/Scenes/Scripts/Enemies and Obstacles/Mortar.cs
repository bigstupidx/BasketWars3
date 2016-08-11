using UnityEngine;
using System.Collections;

public class Mortar : MonoBehaviour {
	public GameObject bullet;
	public Transform spawnPoint;
	
	public float startShootTime = 1.5f;
	public float shootInterval = 3.5f;
	
	private Animator anim;   // Link to the animated sprite
	private bool didExplode;
	private ExplodingProjectile bomb;
	public Vector2 m_mortar_dir;
	public GameObject m_explosion;
	
	// Use this for initialization
	void Start () {
		didExplode = false;
		anim = GetComponent<Animator>(); // This script must be attached to the sprite to work.
		InvokeRepeating ("doShoot", startShootTime, shootInterval);	
	}
	
	// Update is called once per frame
	void Update () {
		if (GameManager.m_level_complete && !didExplode) 
		{
			didExplode = true;
			Explode();
			CancelInvoke("doShoot");
		}
	}
	
	public void doShoot () 
	{
		if (GameManager.m_level_complete || GameManager.m_nuke_explosion)
			return;
		
		anim.Play("MortarFire");		
		Invoke("ShootBullet", 1.5f);
	}
	
	void ShootBullet () {
		if (GameManager.m_level_complete || GameManager.m_nuke_explosion)
			return;
		
		GameObject go = (GameObject) Instantiate(bullet, spawnPoint.position, Quaternion.identity);	
		go.transform.GetComponent<Rigidbody2D>().AddForce (m_mortar_dir);
	}

	public void Explode(){
		if(didExplode){
			Instantiate(m_explosion);
			gameObject.SetActive(false);
		}
	}
}
