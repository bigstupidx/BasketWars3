using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BossGrenader : MonoBehaviour {

	public bool m_is_boss = false;
	public Transform grenade;
	public Transform spawnPoint;
	public GameObject explosion;
	public UISlider bossBlood;
	
	public float startTime;
	public float period;
	
	public static Dictionary<Transform,bool> bombPool = new Dictionary<Transform, bool>();
	
	private Animator anim;   // Link to the animated sprite
	private Animator explosionAnim;   // Link to the animated sprite
	private bool didExploded;
	int explosion_count = 20;
	public BossHealthBarHandler m_boss_health_bar;
	
	// Use this for initialization
	void Start () {
		didExploded = false;
		anim = GetComponent<Animator>(); // This script must be attached to the sprite to work.
		explosionAnim = explosion.GetComponent<Animator>(); // This script must be attached to the sprite to work.
		bombPool.Clear();
		Transform obj = (Transform) Instantiate(grenade, new Vector3 (0f, -3.9f, 0), Quaternion.identity);
		bombPool.Add(obj, true);
		InvokeRepeating ("ThrowingBomb", startTime, period);
	}
	
	// Update is called once per frame
	void Update () {
		if (GameManager.m_level_complete && !didExploded){
			didExploded = true;
			//cannon.renderer.enabled = false;
			explosionAnim.Play("BunkerExplosion");
			CancelInvoke("ThrowingBomb");
			if (GameManager.m_boss_life < 3)
				anim.Play("BritainBossDamagedThrow");			
		}
		if (!GameManager.m_level_complete && didExploded){
			didExploded = false;
		}
	}
	
	void ThrowingBomb () {
		if (GameManager.m_boss_life < 3)
			anim.Play("BritainBossDamagedThrow");
		else
			anim.Play ("BritainBossThrow");
		Invoke ("GenerateBomb", 0.44f);
	}
	
	void GenerateBomb () {
		Transform result = null;
		foreach(var obj in bombPool)
		{
			if (obj.Value)
			{
				Transform t = obj.Key;
				result = t;
				bombPool[t] = false;
				break;
			}
		}
		if (result == null) {
			Transform theObj = (Transform)Instantiate(grenade, new Vector3 (0f, -3.9f, 0), Quaternion.identity);
			bombPool.Add(theObj, false);
			result = theObj;
		}
		result.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
		result.eulerAngles = Vector3.zero;
		result.GetComponent<Rigidbody2D>().gravityScale = 2;
		result.position = spawnPoint.position;
		result.GetComponent<Rigidbody2D>().AddForce (new Vector2(Random.Range(-250.0f, -320.0f), Random.Range(700.0f, 950.0f)));
		BRGrenade theBomb = result.GetComponent<BRGrenade>();
		theBomb.reset ();
	}

	public void RemoveHealth(){
		GameManager.m_boss_life--;
		m_boss_health_bar.TookDamage();
		if(GameManager.m_boss_life > 0){
			CancelInvoke("ThrowingBomb");
			InvokeRepeating ("ThrowingBomb", period * 0.4f, period * 0.4f);
			Instantiate(explosion, transform.position, Quaternion.identity);
		}else{
			InvokeRepeating("ExplosionTime",0.1f,0.1f);
		}
	}

	void ExplosionTime(){
		explosion_count--;
		if(explosion_count <= 0){
			CancelInvoke("ExplosionTime");
			return;
		}
		Vector2 temp = Random.insideUnitCircle * Random.Range(1f,5f);
		Instantiate(explosion, transform.position + new Vector3(temp.x,temp.y), Quaternion.identity);
	}

	void ExplosionCompleteDelegate() {
		if (GameManager.m_boss_life == 0) {
			Destroy (this.gameObject);
		}
		else {
			period -= 0.9f;
			InvokeRepeating ("ThrowingBomb", startTime, period);			
		}
	}

	public void StartGrenadierGruntSound(){
		GetComponent<AudioSource>().Play ();
	}
}
