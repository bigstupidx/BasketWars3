using UnityEngine;
using System.Collections;

public class PauseGame : MonoBehaviour {

	public void Pause(){
		Time.timeScale = 0;
		GameManager.m_is_paused = true;
	}

	public void UnPause(){
		Time.timeScale = 1;
		GameManager.m_is_paused = false;
	}
}
