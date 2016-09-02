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

		//set Pivot Points
		ZombieManager x = GameObject.Find("ZombieManager").GetComponent<ZombieManager>();
		if (x.pivot_points == 1) {
			foreach (GameObject pivot_point in pivot_points)
				pivot_point.SetActive (false);
			GameManager.s_Inst.soldier_position = 1;
		}

		x.transform.position.Set (x.transform.position.x, GameObject.FindGameObjectWithTag ("Player").transform.position.y - GameManager.s_Inst.soldier_position - 1, 0);


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
