using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameplayMenus : MonoBehaviour 
{ 

	GameManager m_game_manager;
	public TweenPosition m_powerup_tray;
	void Start()
	{
		if(GameObject.FindWithTag("GameManager") != null)
			m_game_manager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
	}

	public void MoveInPauseMenu()
	{
		Time.timeScale = 0;
		m_game_manager.MoveInMenu("Menu Panel");
	}

	public void MoveOutPauseMenu()
	{
		Debug.Log("Move Out");
		Time.timeScale = 1;
		m_game_manager.MoveOutMenu("Menu Panel");
	}

	public void MoveInPowerUps()
	{
		Time.timeScale = 0;
		m_powerup_tray.PlayForward();
		GameManager.m_is_paused = true;
	}
	
	public void MoveOutPowerUps()
	{
		GameManager.s_Inst.ResetTime();
		m_powerup_tray.PlayReverse();
		GameManager.m_is_paused = false;
	}
	
	public void NukeButton()
	{
		//Use the Nuke
		MoveOutPowerUps();
	}
	
	public void JeepButton()
	{
		//Use the jeep
		MoveOutPowerUps();
	}
	
	public void LifeButton()
	{
		//Add a Life
		MoveOutPowerUps();
	}
	
	public void OtherButton()
	{
		//Make another powerup?
		MoveOutPowerUps();
	}

}
