using UnityEngine;
using System.Collections;

public class ShotgunBullet : Weapon {

	protected override void  OnTriggerEnter2D (Collider2D c) 
	{
		if (c.gameObject.tag == "Zombie" || c.gameObject.tag == "ZombieLarge")
		{
			ZombieController temp = c.GetComponent<ZombieController>();
			if (temp.get_pivot_row () + 1 == pivot_row) {
				if (temp.health == 1) {
					c.GetComponent<Animator> ().Play ("Shot");
					temp.prep_DestoryZombie ();

				} else {
					c.GetComponent<Animator> ().Play ("Health_Shot");
					temp.zombie_speed -= 0.2f;
					temp.health -= 1;
				}
				DestroyBullet ();
			}
		} else 
			DestroyBullet();
	}
}
