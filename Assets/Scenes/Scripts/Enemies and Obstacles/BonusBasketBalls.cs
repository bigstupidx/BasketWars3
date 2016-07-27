using UnityEngine;
using System.Collections;

public class BonusBasketBalls : MonoBehaviour {
	public GameObject m_explosion;
	public GameObject m_air_explosion;

	void OnCollisionEnter2D(Collision2D c)
	{
		if(c.gameObject.tag == "Ground"){
			Instantiate(m_explosion, transform.position + (Vector3.up * 0.75f), Quaternion.identity);
			Destroy(gameObject);
		}
	}

	public void Explode(){
		GameManager.s_Inst.AddScore(50);
		Instantiate(m_air_explosion, transform.position, Quaternion.identity);
		Destroy(gameObject);
	}
}
