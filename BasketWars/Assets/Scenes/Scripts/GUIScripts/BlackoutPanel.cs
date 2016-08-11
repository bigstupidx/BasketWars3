using UnityEngine;
using System.Collections;

public class BlackoutPanel : MonoBehaviour 
{
	public TweenAlpha m_alpha;
	public void MoveIn(){
		m_alpha.PlayForward();
		GetComponent<Collider>().enabled = true;
	}
	public void MoveOut(){
		m_alpha.PlayReverse();
		GetComponent<Collider>().enabled = false;
	}
}
