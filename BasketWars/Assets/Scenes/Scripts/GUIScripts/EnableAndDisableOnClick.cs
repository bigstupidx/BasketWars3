using UnityEngine;
using System.Collections;

public class EnableAndDisableOnClick : MonoBehaviour {

	public void EnableOnClick(){
		gameObject.SetActive(true);
	}

	public void DisableOnClick(){
		gameObject.SetActive(false);
	}

}

