using UnityEngine;
using System.Collections;
using System.Collections.Generic; 
using System.Runtime.Serialization.Formatters.Binary; 
using System.IO;

public class SaveLoadManager : MonoBehaviour {

	[System.Serializable]
	public struct SaveInfo{
		public int m_characters;
		public int m_coins;
		public int m_nuke_powerup;
		public int m_armor_powerup;
		public int m_focus_powerup;
		public int m_guide_powerup;
		public int m_mission_1_progress;
		public int m_stalingrad_progress;
		public int m_kursk_progress;
		public int m_times_played;
		public bool m_stalingrad_unlocked;
		public bool m_kursk_unlocked;
		public string m_britain_high_scores;
		public string m_stalingrad_high_scores;
		public string m_kursk_high_scores;
		public string m_rank_per_character;
		public string m_rank_xp_per_character;
	};
    
	public static SaveInfo m_save_info;
	public static SaveLoadManager s_inst;
	bool m_save_file_is_open;
	string m_save_file_path;
	// Use this for initialization
	void Awake () {
		if(s_inst == null){
			s_inst = GameObject.Find("GameManagment").GetComponent<SaveLoadManager>();
		}
		m_save_file_path =	Application.persistentDataPath + "/Save1.bwf";
		LoadSaveFile();
        GameManager.s_Inst.UpdateLabels();
	}
	
	void InitNewSaveData(){
		m_save_info.m_characters = 3;
		m_save_info.m_coins = 10000; //0;
		m_save_info.m_nuke_powerup = 0;
		m_save_info.m_armor_powerup = 0;
		m_save_info.m_focus_powerup = 0;
		m_save_info.m_guide_powerup = 0;
        m_save_info.m_mission_1_progress = 87; //0; 5
		m_save_info.m_stalingrad_progress = 0;
		m_save_info.m_kursk_progress = 0;
		m_save_info.m_times_played = 0;
		m_save_info.m_stalingrad_unlocked = false;
		m_save_info.m_kursk_unlocked = false;
		m_save_info.m_britain_high_scores = "0,0,0,0,0,0,0,0,0,0,0";
		m_save_info.m_stalingrad_high_scores = "0,0,0,0,0,0,0,0,0,0,0";
		m_save_info.m_kursk_high_scores = "0,0,0,0,0,0,0,0,0,0,0";
		m_save_info.m_rank_per_character = "1,0,0,0";
		m_save_info.m_rank_xp_per_character = "0,0,0,0";
	}

	public void LoadSaveFile(){
      /*  if (File.Exists(m_save_file_path)){ //do we have a save?
			BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(m_save_file_path,FileMode.Open);
			m_save_info = (SaveInfo)bf.Deserialize(file);
            file.Close();
            SaveFile();
		}else{ //create a new file */
			InitNewSaveData();
			SaveFile();
	//	}
		GameManager.s_Inst.LoadRanksAndXP(m_save_info.m_rank_per_character,m_save_info.m_rank_xp_per_character);
		PowerupEquipper.Init();
		StageUnlocker stage_unlocker = GameManager.s_Inst.gameObject.GetComponent<StageUnlocker>();
		stage_unlocker.Init(m_save_info.m_mission_1_progress);
		GameManager.s_Inst.m_britain_high_scores = m_save_info.m_britain_high_scores;
		GameManager.s_Inst.m_stalingrad_high_scores = m_save_info.m_stalingrad_high_scores;
		GameManager.s_Inst.m_kursk_high_scores = m_save_info.m_kursk_high_scores;
    }

    public void SaveFile(){
		BinaryFormatter bf = new BinaryFormatter();
		FileStream file = File.Create(m_save_file_path);
		bf.Serialize(file,m_save_info);
		file.Close();
	}

	public void UnlockAll(){
		m_save_info.m_mission_1_progress = 4194303;
		m_save_info.m_stalingrad_progress = 4194303;
		m_save_info.m_kursk_progress = 4194303;
		SaveFile();
		Application.LoadLevelAsync(Application.loadedLevelName);
	}
}
