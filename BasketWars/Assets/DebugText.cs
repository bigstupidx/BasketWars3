using UnityEngine;
using System.Collections;

public class DebugText : MonoBehaviour {

	public void DebugLog(){
		Debug.Log("This was called from the tween Forward delegate");
	}
	public void DebugLogBack(){
		Debug.Log("This was called from the tween reverse delegate");
	}
}
