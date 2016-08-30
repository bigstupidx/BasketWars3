using UnityEngine;
using System.Collections;

public class RocketExplosion : MonoBehaviour {
	protected virtual void  OnTriggerEnter2D (Collider2D c) {
		if (c.gameObject.tag == "Zombie" || c.gameObject.tag == "ZombieLarge") {
			c.GetComponent<ZombieController>().prep_DestoryZombie ();
			c.GetComponent<Animator> ().Play ("Explode");
		}
	}
	public void DestroyExplosion() {
		Destroy (gameObject);
	}
}
