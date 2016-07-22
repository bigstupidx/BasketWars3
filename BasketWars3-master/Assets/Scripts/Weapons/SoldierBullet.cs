using UnityEngine;
using System.Collections;

public class SoldierBullet : MonoBehaviour
{
	public float m_speed;
	public int m_damage = 3;
	// Use this for initialization
	void Start (){
	
	}
	
	// Update is called once per frame
	void Update (){
		transform.position += transform.right * m_speed * Time.deltaTime;
	}

	void OnTriggerEnter2D(Collider2D c){
		Destroy(this.gameObject);
	}
}
