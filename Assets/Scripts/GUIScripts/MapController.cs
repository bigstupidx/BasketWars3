using UnityEngine;
using System.Collections;
using System.Collections.Generic;
#if false
using KiiCorp.Cloud.Storage;
using KiiCorp.Cloud.Unity;

/// <summary>
/// To be used on the individual map objects for each stage. This controls when and which icons appear for each stage.
/// </summary>

public class MapController : MonoBehaviour{
	public enum Level_Name{
		Britain = 1,
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
			if(stage_unlocker.m_highest_level.Contains(m_level_name.ToString())){
				
			}
		}
	}

	void OnLevelWasLoaded(){
		if(GameManager.s_Inst.m_first_init)
			Init();
	}

	int GetStageNumber(){
		if(m_level_name == Level_Name.Britain)
			return stage_unlocker.m_britain_current_stage;
		else if(m_level_name == Level_Name.Stalingrad)
			return stage_unlocker.m_stalingrad_current_stage;
		else if(m_level_name == Level_Name.Kursk)
			return stage_unlocker.m_kursk_current_stage;
		else if(m_level_name == Level_Name.Normandy)
			return stage_unlocker.m_normandy_current_stage;
		/*else if(m_level_name == Level_Name.Midway)
			return PlayerPrefs.GetInt(StageUnlocker.m_Midway_level_string);
		else if(m_level_name == Level_Name.Bulge)
			return PlayerPrefs.GetInt(StageUnlocker.m_Bulge_level_string);*/
		else
			return -1;
	}
}
#endif