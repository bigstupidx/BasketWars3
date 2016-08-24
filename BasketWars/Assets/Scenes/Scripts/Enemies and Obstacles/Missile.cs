using UnityEngine;
using System.Collections;

public class Missile : MonoBehaviour {
	public Vector3 m_dir;
	public float m_speed;
	public GameObject m_explosion;
	public GameObject m_particles;
	

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.position += m_dir * m_speed * Time.deltaTime;
	}

	void OnCollisionEnter2D(Collision2D coll){
		m_particles.transform.position = transform.position;
		m_particles.transform.parent = null;
		Instantiate(m_explosion,transform.position,Quaternion.identity);
		Destroy(gameObject);
	}
}
