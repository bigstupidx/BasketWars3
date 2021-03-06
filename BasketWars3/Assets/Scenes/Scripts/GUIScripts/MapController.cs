﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MapController : MonoBehaviour{
	public enum Level_Name{
		Mission_1,
		Stalingrad,
		Kursk,
		Normandy,
		Midway,
		Bulge
	}

	public Level_Name m_level_name;
	[SerializeField]
	public List<GameObject> m_stage_buttons;
	int m_stages_unlocked;
	StageUnlocker stage_unlocker;

	void Awake(){
		stage_unlocker = GameManager.s_Inst.gameObject.GetComponent<StageUnlocker>();
	}

	public void Init(){
		if(stage_unlocker == null)
			stage_unlocker = GameManager.s_Inst.gameObject.GetComponent<StageUnlocker>();
		m_stages_unlocked = GetStageNumber();
		foreach(GameObject g in m_stage_buttons){
			StageButton sb = g.GetComponent<StageButton>();
			sb.SetStars();
			if(sb.stageLevel > m_stages_unlocked && sb.stageLevel <= 11){
				g.GetComponent<UIButton>().GetComponent<Collider>().enabled = false;
				g.GetComponent<UIButton>().isEnabled = false;
				g.transform.FindChild("Num").gameObject.SetActive(false);
				g.transform.FindChild("Lock").gameObject.SetActive(true);
			}else if(sb.stageLevel > 11){
				Transform t = sb.m_stars.transform.FindChild("Coin");
				if(!t.gameObject.activeSelf){
					g.GetComponent<UIButton>().GetComponent<Collider>().enabled = false;
					g.GetComponent<UIButton>().isEnabled = false;
					g.transform.FindChild("Num").gameObject.SetActive(false);
					g.transform.FindChild("Lock").gameObject.SetActive(true);
				}
			}
		}
	}

	void OnLevelWasLoaded(){
		if(GameManager.s_Inst.m_first_init)
			Init();
	}

	int GetStageNumber(){
        if (m_level_name == Level_Name.Mission_1)
        {
            return stage_unlocker.m_mission1_current_stage;
        } 
        else
            return -1;
	}

    public void enableButtons()
    {
        foreach (GameObject i in m_stage_buttons)
        {
            StageButton sb = i.GetComponent<StageButton>();
            if (sb.stageLevel <= m_stages_unlocked)
                i.GetComponent<BoxCollider>().enabled = true;
        }

        transform.FindChild("Background").FindChild("Close Button").GetComponent<BoxCollider>().enabled = true;
    }

    public void disableButtons()
    {
        foreach (GameObject i in m_stage_buttons)
            i.GetComponent<BoxCollider>().enabled = false;

        transform.FindChild("Background").FindChild("Close Button").GetComponent<BoxCollider>().enabled = false;
    }
}