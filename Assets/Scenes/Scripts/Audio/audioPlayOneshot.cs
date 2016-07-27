using UnityEngine;
using System.Collections;

public class audioPlayOneshot : MonoBehaviour {

	public void Play(){
		gameObject.GetComponent<AudioSource>().Play();
	}
}
