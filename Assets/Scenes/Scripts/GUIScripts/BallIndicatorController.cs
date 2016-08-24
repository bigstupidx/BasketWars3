using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BallIndicatorController : MonoBehaviour {
	List<BallController> m_balls = new List<BallController>();
	public UISprite m_ball_marker_one;
	// Use this for initialization
	void Start () {
		GameObject[] balls = GameObject.FindGameObjectsWithTag("Ball");
		foreach(GameObject g in balls){
			m_balls.Add(g.GetComponent<BallController>());
		}
	}
	
	// Update is called once per frame
	void Update () {
		int num_balls = 0;
		foreach(BallController b in m_balls){
			if(!b.m_is_in_air)
				num_balls++;
		}
		if(num_balls >= 1)
			m_ball_marker_one.enabled = true;
		else
			m_ball_marker_one.enabled = false;
	}
}
