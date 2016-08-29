using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour 
{
	public float m_speed;
	protected int pivot_row;

	public virtual void Fire(Vector3 dir)
	{
		GetComponent<Rigidbody2D>().velocity = dir * m_speed;
	}
	protected virtual void  OnTriggerEnter2D (Collider2D c) 
	{
		DestroyBullet();
	}

	public void DestroyBullet(){
        Destroy(gameObject);
	}

	public void set_pivot_row(int x)
	{
		pivot_row = x;
	}

	public int get_pivot_row()
	{
		return pivot_row;
	}	
}
