using UnityEngine;
using System.Collections;

public class GUIEnableDisable : MonoBehaviour {

    public GameObject level_complete;
    public GameObject pause_menu;
    public GameObject failed_level;

	// Use this for initialization
	void Awake () {
		//set ShootButton
		UIButton what = transform.Find("Camera/ShootGunButton").GetComponent<UIButton>();
		EventDelegate.Set( what.onClick, GameManager.s_Inst.SoldierFire);  

        NGUITools.SetActive(level_complete, false);
        NGUITools.SetActive(pause_menu, false);
        NGUITools.SetActive(failed_level, false);
    }
	
	// Update is called once per frame
	void Update () {
	
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
