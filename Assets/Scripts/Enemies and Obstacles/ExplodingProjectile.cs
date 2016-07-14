using UnityEngine;
using System.Collections;
#pragma warning disable 0414
public class ExplodingProjectile : MonoBehaviour 
{
	private float maxScale;
	private float currentScale;
	private	float deltaScale;
	bool isExploded = false;
	Vector3 m_start_pos;
	Animator anim;
	public GameObject m_explosion;
	public bool m_is_mortar = false;
	static bool m_is_focus_on = false;
	float m_random_explode;
	
	// Use this for initialization
	void Start ()
	{
		m_random_explode = Random.Range(0.25f,1.5f);
		m_start_pos = transform.position;
		GetComponent<Collider2D>().enabled = false;		
		currentScale = 0.8f;
		maxScale = 1.5f;
		deltaScale = 0.05f;
		anim = GetComponent<Animator>(); // This script must be attached to the sprite to work.
		transform.localScale =  new Vector3(0.8f, 0.8f, 1);
		InvokeRepeating ("increaseSize", 0.1f, 0.1f);
		GetComponent<Rigidbody2D>().gravityScale = 2.5f;			
	}
	
	// Update is called once per frame
	void Update () 
	{		
		if(!m_is_mortar){
			if(m_random_explode <= 0){
				CancelInvoke("increaseSize");
				CancelInvoke("doExplosion");
				Explode();		
			}
			if(GetComponent<Rigidbody2D>().velocity.y <= 0)
				m_random_explode -= Time.deltaTime;
		}
		if (!isExploded)
		{
			if (GameManager.m_level_complete || GameManager.m_nuke_explosion) 
			{
				CancelInvoke("increaseSize");
				CancelInvoke("doExplosion");
				Explode();								
			}			
		}
		if(GetComponent<Rigidbody2D>().velocity.y < 0 && !GetComponent<Collider2D>().enabled)
		{
			GetComponent<Collider2D>().enabled = true;
			if(m_is_mortar)
			{
				anim.Play("Mortar_bullet_top");
			}
		}
	}
	
	public void Explode (){
		if(!isExploded){
			Instantiate(m_explosion,transform.position,Quaternion.identity);
			Destroy(this.gameObject);
		}
	}
	
	void increaseSize (){		
		if (currentScale < maxScale){
			currentScale += deltaScale;
			this.gameObject.transform.localScale = new Vector3(currentScale, currentScale, 1);
		}else{
			CancelInvoke("increaseSize");			
		}
	}

	void OnCollisionEnter2D(Collision2D coll){
		if(coll.gameObject.tag == "Zombie"){
			coll.gameObject.GetComponent<Animator>().Play("Explode");
		}
		Explode();
	}
}
