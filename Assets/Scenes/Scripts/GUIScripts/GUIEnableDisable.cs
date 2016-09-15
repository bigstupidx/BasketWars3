using UnityEngine;
using System.Collections;

public class GUIEnableDisable : MonoBehaviour {

    public GameObject level_complete;
    public GameObject pause_menu;
    public GameObject failed_level;
	public GameObject[] pivot_points;

	// Use this for initialization
	void Awake () {
		//set ShootButton
		UIButton what = transform.Find("Camera/ShootGunButton").GetComponent<UIButton>();
		EventDelegate.Set( what.onClick, GameManager.s_Inst.SoldierFire);  

		if (GameManager.s_Inst.m_current_game_state == GameManager.GameState.Gameplay) {
			//set Pivot Points for normal mode
			ZombieManager x = GameObject.Find ("ZombieManager").GetComponent<ZombieManager> ();
			if (x.pivot_points == 1) {
				foreach (GameObject p in pivot_points)
					p.SetActive (false);
				GameManager.s_Inst.soldier_position = 1;
				x.gameObject.transform.position = new Vector2 (x.gameObject.transform.position.x, GameObject.FindGameObjectWithTag ("Player").transform.position.y);
			} else if (x.pivot_points == 2) {
				pivot_points [1].SetActive (false);
				GameManager.s_Inst.soldier_position = 2;
				x.gameObject.transform.position = new Vector2 (x.gameObject.transform.position.x, GameObject.FindGameObjectWithTag ("Player").transform.position.y -1);
			}
			
		} else if (GameManager.s_Inst.m_current_game_state == GameManager.GameState.Boss) {
			BossController boss = GameObject.FindGameObjectWithTag ("Boss").GetComponent<BossController> ();
			float i = GameObject.FindGameObjectWithTag ("Player").transform.position.y -0.4f; 
			boss.set_row (new Vector2 (17, i), new Vector2 (17, i + 1), new Vector2 (17, i + 2));
		}

        NGUITools.SetActive(level_complete, false);
        NGUITools.SetActive(pause_menu, false);
        NGUITools.SetActive(failed_level, false);
    }


    public void MovePauseMenuIn()
    {
        NGUITools.SetActive(pause_menu, true);
    }

    public void MovePauseMenuOut()
    {
        NGUITools.SetActive(pause_menu, false);
    }

    public void MoveLevelCompleteIn()
    {
        NGUITools.SetActive(level_complete, true);
        GameManager.s_Inst.MoveInMenu(level_complete);
    }

    public void MoveLevelFailedIn()
    {
        NGUITools.SetActive(failed_level, true);
        GameManager.s_Inst.MoveInMenu(failed_level);
    }
}
