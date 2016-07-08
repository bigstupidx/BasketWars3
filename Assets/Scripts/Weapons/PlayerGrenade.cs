using UnityEngine;
using System.Collections;

public class PlayerGrenade : Weapon{

	public GameObject m_explosion;

	protected override void Awake(){
		m_bullet_cost = 1;
		base.Awake();
	}
	protected override void OnTriggerEnter2D(Collider2D c){
		if(c.gameObject.tag != "Ball"){
			Explode();
		}
		if(c.gameObject.tag == "Obstacle"){
			Destroy(c.gameObject);
		}
	}

	protected override void Update()
	{

	}

    public void Explode(){
		Instantiate(m_explosion,transform.position,Quaternion.identity);
		GameManager.s_Inst.m_grenade_in_air = false;
		Destroy(gameObject);
	}
}
