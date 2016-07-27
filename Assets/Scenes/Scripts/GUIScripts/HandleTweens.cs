using UnityEngine;
using System.Collections;

public class HandleTweens : MonoBehaviour {
	[SerializeField]
	public UITweener[] m_tweens;

	public void PlayForward(){
		foreach(UITweener t in m_tweens){
			t.PlayForward();
		}
	}

	public void PlayReverse(){
		foreach(UITweener t in m_tweens){
			t.PlayReverse();
		}
	}
}
