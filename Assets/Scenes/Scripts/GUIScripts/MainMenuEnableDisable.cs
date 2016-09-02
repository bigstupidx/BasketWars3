using UnityEngine;
using System.Collections;

public class MainMenuEnableDisable : MonoBehaviour {
    public GameObject Battle_detail;

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
