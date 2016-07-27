using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class PlayAudioOnHit : MonoBehaviour {
	public void OnTriggerEnter2D(Collider2D c)
	{
		if(c.gameObject.tag == "Weapon")
			GetComponent<AudioSource>().Play();
	}
}
