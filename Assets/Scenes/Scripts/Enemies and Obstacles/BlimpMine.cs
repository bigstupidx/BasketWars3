using UnityEngine;
using System.Collections;

[RequireComponent(typeof(PolygonCollider2D))]
public class BlimpMine : MonoBehaviour 
{
	public float m_speed;
	public Vector2 m_height_min_max;
	float m_dir_mod = 1;
	public GameObject m_explosion;

	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(transform.position.y > m_height_min_max.y && m_dir_mod > 0)
			SwitchDirection();
		else if(transform.position.y < m_height_min_max.x && m_dir_mod < 0)
			SwitchDirection();
		else
		{
			if(Random.Range(0,500)%500 == 0)
				SwitchDirection();
		}
		transform.position += m_speed * Vector3.up * m_dir_mod * Time.deltaTime;
	}

	void OnTriggerEnter2D()
	{
		Instantiate(m_explosion,transform.position,Quaternion.identity);
		Destroy(this.gameObject);
	}


	void SwitchDirection()
	{
		m_dir_mod *= -1;
	}
}
