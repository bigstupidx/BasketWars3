using UnityEngine;
using System.Collections;

public class GUIEnableDisable : MonoBehaviour {

    public GameObject level_complete;
    public GameObject pause_menu;

	// Use this for initialization
	void Awake () {
        NGUITools.SetActive(level_complete, false);
        NGUITools.SetActive(pause_menu, false);
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
}
