using UnityEngine;
using System.Collections;

public class ToggleEnableState : MonoBehaviour {

	public void EnableObject(){
		gameObject.SetActive(true);
	}

	public void DisableObject(){
		gameObject.SetActive(false);
	}
}
