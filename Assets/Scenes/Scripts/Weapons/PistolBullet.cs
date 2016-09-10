using UnityEngine;
using System.Collections;

public class PistolBullet : Weapon 
{
	protected override void  OnTriggerEnter2D (Collider2D c) 
	{
		if (c.gameObject.tag == "Zombie" || c.gameObject.tag == "ZombieLarge")
		{
			ZombieController temp = c.GetComponent<ZombieController>();
			if (temp.get_pivot_row () + 1 == pivot_row) {
				if (GameManager.s_Inst.dealDamageZombie (temp, 1)) {
					temp.GetComponent<Animator> ().Play ("Shot");
				} else
					temp.GetComponent<Animator> ().Play ("ShotBack");
			}
			DestroyBullet();
		} else 
			DestroyBullet();
	}

}
