using UnityEngine;
using System.Collections;
[RequireComponent(typeof(AudioSource))]

public class PlayRimSound : MonoBehaviour {

	void OnCollisionEnter2D(Collision2D c){
		if(GetComponent<AudioSource>().isPlaying == false)
			GetComponent<AudioSource>().Play();
	}
}
