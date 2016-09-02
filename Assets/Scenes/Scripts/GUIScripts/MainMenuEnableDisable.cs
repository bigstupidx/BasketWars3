using UnityEngine;
using System.Collections;

public class MainMenuEnableDisable : MonoBehaviour {
    public GameObject Battle_detail;

	void Awake () {
		//set ShootButton
		UIButton what = transform.Find("Settings Menu Panel/Tutorials").GetComponent<UIButton>();
		EventDelegate.Set( what.onClick, GameManager.s_Inst.GetComponent<SaveLoadManager>().Reset);  
	}

    public void MoveBattleDetailPanelIn()
    {
        NGUITools.SetActive(Battle_detail, true);
		Battle_detail.transform.GetChild (1).gameObject.GetComponent<TweenAlpha> ().PlayForward ();
    }

    public void MoveBattleDetailPanelOut()
    {
        NGUITools.SetActive(Battle_detail, false);
    }
}
