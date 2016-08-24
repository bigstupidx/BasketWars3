using UnityEngine;
using System.Collections;

public class ParachuteMineGenerator : MonoBehaviour {
	public Transform bomb;
	
	public float startTime = 2.0f;
	public float interval = 8f;

	// Use this for initialization
	void Start () {
		InvokeRepeating ("generateParachuteMine", startTime, interval);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	// generate parachute mine
	void generateParachuteMine () {
		if (!GameManager.m_level_complete && !GameManager.m_nuke_explosion) 
		{
			Instantiate(bomb, this.gameObject.transform.position, Quaternion.identity);
		}
		else
			CancelInvoke("generateParachuteMine");
	}
}
