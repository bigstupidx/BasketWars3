using UnityEngine;
using System.Collections;

public class Grenader : MonoBehaviour {
	public GameObject grenade;
	public Transform spawnPoint;
	
	public float startThrowTime = 1f;
	public float throwInterval = 2.5f;

	private Animator anim;   // Link to the animated sprite
	private bool didExplode;
	private ExplodingProjectile bomb;
	public Vector2 m_dir_grenade;
	public GameObject m_explosion;
	
	// Use this for initialization
	void Start (){
		didExplode = false;
		anim = GetComponent<Animator>();
		InvokeRepeating ("doThrow", startThrowTime, throwInterval);
	}
	
	// Update is called once per frame
	void Update (){
		if (GameManager.m_level_complete && !didExplode){
			didExplode = true;
			CancelInvoke("doThrow");
			Explode();
		}
	}
	
	public void doThrow (){
		if (GameManager.m_level_complete || GameManager.m_nuke_explosion)
			return;
		didExplode = false;
		anim.Play("GrenadierThrow");
		Invoke("generateGrenade", 0.7f);
	}
	
	void generateGrenade (){
		if (GameManager.m_level_complete || GameManager.m_nuke_explosion)
			return;
		GameObject go = (GameObject)Instantiate(grenade, spawnPoint.position, Quaternion.identity);
		go.transform.GetComponent<Rigidbody2D>().AddForce (m_dir_grenade);
	}

	public void StartGrenadeThrowSound (){
		GetComponent<AudioSource>().Play ();
	}

	public void Explode(){
		if(didExplode){
			Instantiate(m_explosion);
			gameObject.SetActive(false);
		}
	}
}
