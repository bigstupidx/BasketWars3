using UnityEngine;
using System.Collections;

public class MainMenuEnableDisable : MonoBehaviour {
    public GameObject Battle_detail;

    public void MoveBattleDetailPanelIn()
    {
        NGUITools.SetActive(Battle_detail, true);
    }

    public void MoveBattleDetailPanelOut()
    {
        NGUITools.SetActive(Battle_detail, false);
    }
}
