using UnityEngine;
using System.Collections;

public class BasketShaker : MonoBehaviour 
{
	void OnCollisionEnter2D(Collision2D col)
	{
		gameObject.GetComponent<Animator>().Play ("BasketShaking");
		gameObject.GetComponent<AudioSource>().Play();
	}
}
