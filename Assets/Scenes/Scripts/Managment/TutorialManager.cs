using UnityEngine;
using System.Collections;

public class TutorialManager : MonoBehaviour {

	public GameObject m_next_button;
	public BlackoutPanel m_blackout_panel;
	public BlackoutPanel m_blackout_powerup_panel;
	public TweenPosition m_powerup_tray_tween;
	public TweenPosition m_menu_panel_tween;

	public enum TutorialProgress{
		Tutorial1Start,
		Tutorial1Finished,
		Tutorial2BeforeShot,
		Tutorial2DuringShot,
		Tutorial2AfterShot,
		Tutorial3Start,
		Tutorial3ShowingMenu,
		Tutorial3ShowingMenuRetry,
		Tutorial3ShowingMenuQuit,
		Tutorial3ShowingMenuResume,
		Tutorial3StarIndicators,
		Tutorial3Timer,
		Tutorial3Flag,
		Tutorial3Ball,
		Tutorial3Lives,
		Tutorial3Ammo,
		Tutorial3ShowingPowerup,
		Tutorial3ShowingPowerupTray,
		Tutorial3ShowingPowerupLife,
		Tutorial3ShowingPowerupJeep,
		Tutorial3ShowingPowerupArmor,
		Tutorial3ShowingPowerupSlowMo,
		Tutorial3ShowingPowerupArc,
		Tutorial3ShowingPowerupNuke,
		Tutorial3TryPowerup,
		Tutorial3Finished,
		Tutorial3Complete
	};
	public static TutorialManager s_Inst;
	public TutorialProgress m_current_progress;

	// Use this for initialization
	void Start () {
		s_Inst = this;
		if(Application.loadedLevelName == "Tutorial1"){
		}
		if(Application.loadedLevelName == "Tutorial2"){
			m_current_progress = TutorialProgress.Tutorial2BeforeShot;
		}
		if(Application.loadedLevelName == "Tutorial3"){
			m_current_progress = TutorialProgress.Tutorial3Start;
			m_next_button.GetComponent<UIWidget>().alpha = 0;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnLevelWasLoaded(){
		PauseGame();
	}

	public void PauseGame(){
		GameManager.m_is_paused = true;
		Time.timeScale = 0;
	}

	public void ResumeGame(){
		GameManager.m_is_paused = false;
		GameManager.s_Inst.ResetTime();
	}

	public void SkipTutorial(){
		GameManager.s_Inst.m_level_name_to_load = "MainMenu";
		GameManager.s_Inst.m_marked_for_destroy = true;
		PlayerPrefs.SetInt("TutorialPassed",1);
		Application.LoadLevelAsync("LevelLoader");
	}

	public void StartNextLevel(){
		if(Application.loadedLevelName == "Tutorial1"){
			GameManager.s_Inst.m_level_name_to_load = "Tutorial2";
			if(m_current_progress == TutorialProgress.Tutorial1Start)
				m_current_progress = TutorialProgress.Tutorial2BeforeShot;
		}
		if(Application.loadedLevelName == "Tutorial2")
			GameManager.s_Inst.m_level_name_to_load = "Tutorial3";
		if(Application.loadedLevelName == "Tutorial3"){
			GameManager.s_Inst.m_level_name_to_load = "MainMenu";
			GameManager.s_Inst.m_marked_for_destroy = true;
			PlayerPrefs.SetInt("TutorialPassed",1);
		}
		Application.LoadLevelAsync("LevelLoader");
	}


	public void Tutorial2NextStep(){
		if(m_current_progress == TutorialProgress.Tutorial2BeforeShot){
			Time.timeScale = 0.1f;
			m_current_progress = TutorialProgress.Tutorial2DuringShot;
			GameObject.Find("overlayDarken").GetComponent<SpriteRenderer>().enabled = true;
			GameObject.Find("NetFront").GetComponent<SpriteRenderer>().sortingLayerName = "Play Area";
			GameObject.Find("NetFront").GetComponent<SpriteRenderer>().sortingOrder = 5;
			GameObject.Find("TargetLabel").GetComponent<UILabel>().enabled = true;
			return;
		}
		if(m_current_progress == TutorialProgress.Tutorial2DuringShot){
			GameManager.s_Inst.ResetTime();
			m_current_progress = TutorialProgress.Tutorial2AfterShot;
			GameObject.Find("overlayDarken").GetComponent<SpriteRenderer>().enabled = false;
			GameObject.Find("NetFront").GetComponent<SpriteRenderer>().sortingLayerName = "Foreground";
			GameObject.Find("NetFront").GetComponent<SpriteRenderer>().sortingOrder = 0;
			GameObject.Find("TargetLabel").GetComponent<UILabel>().enabled = false;
			return;
		}
	}

	public void Tutorial3NextStep(){
		if(m_current_progress == TutorialProgress.Tutorial3Start)
			m_blackout_panel.MoveIn();
		m_current_progress += 1;
		if(m_current_progress > TutorialProgress.Tutorial3Complete)
			m_current_progress = TutorialProgress.Tutorial3Complete;
		if(m_current_progress == TutorialProgress.Tutorial3Finished){
			m_blackout_panel.MoveOut();
			GameManager.m_is_paused = false;
		}
		if(m_current_progress == TutorialProgress.Tutorial3TryPowerup){
			m_blackout_powerup_panel.MoveOut();
			m_blackout_panel.MoveIn();
			m_blackout_panel.GetComponent<Collider>().enabled = false;
		}
		if(m_current_progress == TutorialProgress.Tutorial3Start || 
		   m_current_progress == TutorialProgress.Tutorial3ShowingMenu || 
		   m_current_progress == TutorialProgress.Tutorial3ShowingPowerup ||
		   m_current_progress == TutorialProgress.Tutorial3ShowingMenuResume || 
		   m_current_progress == TutorialProgress.Tutorial3Finished ||
		   m_current_progress == TutorialProgress.Tutorial3TryPowerup ||
		   m_current_progress == TutorialProgress.Tutorial3Complete){
			m_next_button.GetComponent<Collider>().enabled = false;
			m_next_button.GetComponent<UIButton>().isEnabled = false;
		}
		else{
			m_next_button.GetComponent<Collider>().enabled = true;
			m_next_button.GetComponent<UIButton>().isEnabled = true;
		}
		if(m_current_progress == TutorialProgress.Tutorial3ShowingPowerupTray){
			m_blackout_panel.MoveOut();
			m_blackout_powerup_panel.MoveIn();
			m_blackout_powerup_panel.gameObject.GetComponent<UIWidget>().depth = -1;
		}
		if(m_current_progress == TutorialProgress.Tutorial3ShowingPowerupLife){
			m_blackout_powerup_panel.gameObject.GetComponent<UIWidget>().depth = 100;
		}
	}


	public void OpenMenu(){
		if(m_current_progress == TutorialProgress.Tutorial3ShowingMenu){
			Tutorial3NextStep();
			m_menu_panel_tween.PlayForward();
		}
	}

	public void UsedPowerup(){
		if(m_current_progress == TutorialProgress.Tutorial3TryPowerup){
			Tutorial3NextStep();
			m_powerup_tray_tween.PlayReverse();
		}
	}
	public void BringDownTray(){
		if(m_current_progress == TutorialProgress.Tutorial3ShowingPowerup){
			Tutorial3NextStep();
			m_powerup_tray_tween.PlayForward();
		}
	}

	public void UnpauseGame(){
		if(m_current_progress == TutorialProgress.Tutorial3ShowingMenuResume){
			Tutorial3NextStep();
			m_menu_panel_tween.PlayReverse();
		}
	}
}
