using UnityEngine;
using System.Collections;

public class Rocket : Weapon {
	public GameObject m_explosion;

	protected override void OnTriggerEnter2D (Collider2D c)
	{
		if (c.gameObject.tag == "Zombie" || c.gameObject.tag == "ZombieLarge") {
			ZombieController temp = c.GetComponent<ZombieController> ();
			if (temp.get_pivot_row () + 1 == pivot_row) {
				Instantiate (m_explosion, transform.position, Quaternion.identity);
				DestroyBullet ();
			}
		} else {
			Instantiate (m_explosion, transform.position, Quaternion.identity);
			DestroyBullet ();
		}
	}
}
