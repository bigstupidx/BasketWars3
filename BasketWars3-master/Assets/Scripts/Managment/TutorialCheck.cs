using UnityEngine;
using System.Collections;

public class TutorialCheck : MonoBehaviour {
	public void CheckTutorial () {
		int seen_tutorial = PlayerPrefs.GetInt("TutorialSeen",0);
		//int seen_tutorial = 0;
		if(seen_tutorial == 0){
			gameObject.GetComponent<TweenPosition>().PlayForward();
			GameObject.Find("CrateBGPanel").GetComponent<HandleTweens>().PlayForward();
			PlayerPrefs.SetInt("TutorialSeen",1);
		}
	}
}
