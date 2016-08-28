using UnityEngine;
using System.Collections;

public class BRGrenade : MonoBehaviour {

	Vector3 storePosition = new Vector3(1000,1000,1000);
	bool isExploded;
	public GameObject m_explosion;
	// Use this for initialization
	void Start () {
		//storePosition = transform.position;
		isExploded = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (!isExploded)
		{
			if (GameManager.m_level_complete) {
				CancelInvoke("doExplosion");
				doExplosion ();				
			}
		}	
	}
	
	public void doExplosion () {
		if(!isExploded) {
			Instantiate(m_explosion,transform.position,Quaternion.identity);
			isExploded = true;
			//transform.collider2D.isTrigger = true;
			transform.GetComponent<Rigidbody2D>().gravityScale = 0;
			transform.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
			ExplosionCompleteDelegate();
		}
	}

	public void OnCollisionEnter2D(Collision2D c){
		if(c.gameObject.tag == "Ball"){
			doExplosion();
			c.gameObject.GetComponent<BallController>().KillBall();
		}
	}
	// This is called once the Throw animation has compelted playing
    public void ExplosionCompleteDelegate() {
		transform.position = storePosition;
		BossGrenader.bombPool[transform] = true;
    }
	
	public void reset () {
		isExploded = false;
		//transform.collider2D.isTrigger = false;
		
		float t = Random.Range(0.75f, 2.5f);
		Invoke("doExplosion", t);		
	}
}
