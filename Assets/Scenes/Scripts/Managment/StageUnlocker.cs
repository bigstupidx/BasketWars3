using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public class StageUnlocker : MonoBehaviour
{
    public int m_overworld_stage = 1;

    public int m_mission1_current_stage = 1;
    int m_mission1_stage_progress = -1;
    public int[] m_mission1_star_count;
    public GameObject m_mission1_stars_label;

    GameObject[] map_controllers;
    public int m_total_stars_earned = 0;
    public int m_total_stars_earned_mission1 = 0;

    int m_times_loaded = 0;
    public UIPanel m_map_panel;
    public string m_highest_level = "";

    public void Init(int mission1)
    {
        m_mission1_star_count = new int[11];
    
        m_overworld_stage = 1;
        m_mission1_current_stage = 1;
        m_mission1_stage_progress = mission1;

        InitLevels(m_mission1_star_count, m_mission1_stage_progress);
        foreach (int i in m_mission1_star_count)
         {
             if (i > 0)
                 m_mission1_current_stage++;
             else
                 break;
         } 
        m_highest_level = "Mission_1_";
        if (m_highest_level == "Mission_1_")
        {
            if (m_mission1_current_stage < 10)
            {
                m_highest_level += "0" + m_mission1_current_stage;
            }
            else
            {
                m_highest_level += m_mission1_current_stage;
            }
        }
        GetTotalStars();
        m_mission1_stars_label.GetComponent<UILabel>().text = m_total_stars_earned_mission1.ToString();

        map_controllers = GameObject.FindGameObjectsWithTag("MapController");
        GameManager.s_Inst.m_first_init = true;
       foreach (GameObject g in map_controllers)
        {
            g.GetComponent<MapController>().Init();
        }
    }

    int GetTotalStars()
    {
        m_total_stars_earned = 0;
        m_total_stars_earned_mission1 = 0;
        for (int i = 0; i < 10; i++)
        {
            m_total_stars_earned += m_mission1_star_count[i];
            m_total_stars_earned_mission1 += m_mission1_star_count[i];
        }
        return m_total_stars_earned;
    }

    public int GetStars(string m_level)
    {
        int num = System.Convert.ToInt32(m_level.Substring(m_level.Length - 2));
        if (m_level.Contains("Mission_1"))
            return m_mission1_star_count[num - 1];
        return -1;
    }

    public void CheckAchievements()
    {/*
        if (m_britain_star_count[10] > 0)
            AchievementManager.Grenadier();
        if (m_stalingrad_star_count[10] > 0)
            AchievementManager.ArmoredCar();
        if (m_kursk_star_count[10] > 0)
            AchievementManager.Tank();
        if (m_normandy_star_count[10] > 0)
            AchievementManager.Bunker();
        AchievementManager.BritainVeteran((float)m_total_stars_earned_brit);
        AchievementManager.StalingradVeteran((float)m_total_stars_earned_stal);
        AchievementManager.KurskVeteran((float)m_total_stars_earned_kursk);
        AchievementManager.NormandyVeteran((float)m_total_stars_earned_norm);
        AchievementManager.KeepTruckin(m_total_stars_earned);
        AchievementManager.HalfwayThere(m_total_stars_earned);*/
    }

    void InitLevels(int[] stars, int level)
    {
        for (int i = 0; i < 11; i++)
        {
            stars[i] = (level & (3 << (i * 2))) >> i * 2;
        }
    }

	public void update_level(int level_to_update,int stars)
	{
		if (m_mission1_star_count [level_to_update - 1] < stars) {
			m_total_stars_earned_mission1 += stars - m_mission1_star_count [level_to_update - 1];
			if (m_mission1_star_count[level_to_update - 1] == 0)
				m_mission1_current_stage++;
			m_mission1_star_count [level_to_update - 1] = stars;
			SaveLoadManager.m_save_info.m_mission_1_progress = UpdateLevels(m_mission1_star_count, m_mission1_stage_progress);
			SaveLoadManager.s_inst.SaveFile(); 
		}
	}

    public void UpdateAllLevels()
    {
		SaveLoadManager.m_save_info.m_mission_1_progress = UpdateLevels(m_mission1_star_count, m_mission1_stage_progress);
        SaveLoadManager.s_inst.SaveFile(); 
    }

    private int UpdateLevels(int[] stars, int level)
    {
        for (int i = 0; i < 11; i++)
        {
            level = level | (stars[i] << (i * 2));
        }
        return level;
    }

	public void update_mission_star_labels() {
		if (m_mission1_stars_label == null)
			m_mission1_stars_label = GameObject.Find ("Mission_1").transform.GetChild (0).FindChild ("CurrentBattleStars").gameObject;
		m_mission1_stars_label.GetComponent<UILabel>().text = m_total_stars_earned_mission1.ToString();
	}

    void SaveLevelInfo()
    {
        SaveLoadManager.s_inst.SaveFile();
    }

    public void UnlockNextLevel(string levelname, int stage)
    {
        //If we just beat a boss lets unlock the next level.
        if (stage == 11)
        {
            m_overworld_stage++;
            return;
        }
        //otherwise lets unlock the next stage
        if (levelname.Contains("Mission_1"))
        {
            if (m_mission1_current_stage <= stage)
            { //Check to make sure we actually need to unlock the next stage.
                m_mission1_current_stage++;
            }
        }
    }

    public void ResetProgress()
    {
        Init(0);
        SaveLoadManager.s_inst.SaveFile();
    }

    public bool IsLevelUnlocked(string levelname, int stage)
    {
        if (levelname.Contains("Mission_1"))
        {
            if (stage == 12 && m_total_stars_earned_mission1 >= 10)
                return true;
            if (stage == 13 && m_total_stars_earned_mission1 >= 20)
                return true;
            if (m_mission1_current_stage >= stage)
            { //Check to make sure we actually need to unlock the next stage.
                return true;
            }
        }
        return false;
    }

    public void UnlockAll()
    {
        m_overworld_stage = 4;
        m_mission1_current_stage = 11;
    }
}