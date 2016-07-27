using UnityEngine;
using System.Collections;

public class BasketRim : MonoBehaviour 
{
	void OnCollisionEnter2D(Collision2D col)
	{
		gameObject.GetComponent<AudioSource>().Play();
	}
}
