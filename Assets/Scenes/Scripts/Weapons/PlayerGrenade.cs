using UnityEngine;
using System.Collections;

public class PlayerGrenade : Weapon{

	public GameObject m_explosion;

	protected override void OnTriggerEnter2D(Collider2D c){
		if(c.gameObject.tag != "Ball"){
			Explode();
		}
		if(c.gameObject.tag == "Obstacle"){
			Destroy(c.gameObject);
		}
	}

    public void Explode(){
		Instantiate(m_explosion,transform.position,Quaternion.identity);
		GameManager.s_Inst.m_grenade_in_air = false;
		Destroy(gameObject);
	}
}
