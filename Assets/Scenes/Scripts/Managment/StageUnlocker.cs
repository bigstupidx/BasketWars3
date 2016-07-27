using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public class StageUnlocker : MonoBehaviour
{
    public bool m_developermode;
    public int m_overworld_stage = 1;
    public int m_mission1_current_stage = 1;
    public int m_stalingrad_current_stage = 1;
    public int m_kursk_current_stage = 1;
    public int m_normandy_current_stage = 1;

    int m_britain_stage_progress = -1;
    int m_stalingrad_stage_progress = -1;
    int m_kursk_stage_progress = -1;
    int m_normandy_stage_progress = -1;
    public int[] m_britain_star_count;
    public int[] m_stalingrad_star_count;
    public int[] m_kursk_star_count;
    public int[] m_normandy_star_count;

    public GameObject m_britain_button;
    public GameObject m_stalingrad_button;
    public GameObject m_kursk_button;
    public GameObject m_normandy_button;

    GameObject[] map_controllers;
    public int m_total_stars_earned = 0;
    public int m_total_stars_earned_brit = 0;
    public int m_total_stars_earned_stal = 0;
    public int m_total_stars_earned_kursk = 0;
    public int m_total_stars_earned_norm = 0;
    bool m_Stalingrad_enabled;
    bool m_Kursk_enabled;
    bool m_Normandy_enabled;
    public bool m_Stalingrad_purchased;
    public bool m_Kursk_purchased;
    public bool m_Normandy_purchased;
    int m_times_loaded = 0;
    public UIPanel m_map_panel;
    public string m_highest_level = "";

    public void Init(int britain, int stalingrad, int kursk, int normandy)
    {
        m_britain_star_count = new int[11];
        m_stalingrad_star_count = new int[11];
        m_kursk_star_count = new int[11];
        m_normandy_star_count = new int[11];
        m_overworld_stage = 1;
        m_mission1_current_stage = 1;
        m_stalingrad_current_stage = 1;
        m_kursk_current_stage = 1;
        m_normandy_current_stage = 1;
        m_britain_stage_progress = britain;
        m_stalingrad_stage_progress = stalingrad;
        m_kursk_stage_progress = kursk;
        m_normandy_stage_progress = normandy;
        InitLevels(m_britain_star_count, m_britain_stage_progress);
        InitLevels(m_stalingrad_star_count, m_stalingrad_stage_progress);
        InitLevels(m_kursk_star_count, m_kursk_stage_progress);
        InitLevels(m_normandy_star_count, m_normandy_stage_progress);
        foreach (int i in m_britain_star_count)
        {
            if (i > 0)
                m_mission1_current_stage++;
            else
                break;
        }
        foreach (int i in m_stalingrad_star_count)
        {
            if (i > 0)
                m_stalingrad_current_stage++;
            else
                break;
        }
        foreach (int i in m_kursk_star_count)
        {
            if (i > 0)
                m_kursk_current_stage++;
            else
                break;
        }
        foreach (int i in m_normandy_star_count)
        {
            if (i > 0)
                m_normandy_current_stage++;
            else
                break;
        }
        m_highest_level = "Britain";
        if (m_britain_star_count[10] > 0)
        {
            m_highest_level = "Stalingrad";
            m_overworld_stage++;
            //m_stalingrad_button.GetComponent<TweenColor>().PlayForward();
            m_stalingrad_button.transform.GetChild(0).gameObject.SetActive(true);
            //m_stalingrad_button.transform.GetChild(0).collider.enabled = true;
            m_stalingrad_button.transform.GetChild(0).GetComponent<UIButton>().isEnabled = true;
            if (GameObject.Find("StalingradPlaceholder") != null)
                GameObject.Find("StalingradPlaceholder").SetActive(false);
        }
        if (GameManager.s_Inst.m_more_levels_unlocked)
        {
            if (m_stalingrad_star_count[10] > 0)
            {
                m_highest_level = "Kursk";
                m_overworld_stage++;
                //m_kursk_button.GetComponent<TweenColor>().PlayForward();
                m_kursk_button.transform.GetChild(0).gameObject.SetActive(true);
               // m_kursk_button.transform.GetChild(0).collider.enabled = true;
                m_kursk_button.transform.GetChild(0).GetComponent<UIButton>().isEnabled = true;
                if (GameObject.Find("KurskPlaceholder") != null)
                    GameObject.Find("KurskPlaceholder").SetActive(false);
            }
            if (m_kursk_star_count[10] > 0)
            {
                /*m_highest_level = "Normandy";
				m_overworld_stage++;
				//m_normandy_button.GetComponent<TweenColor>().PlayForward();
				m_normandy_button.transform.GetChild(0).gameObject.SetActive(true);
				m_normandy_button.transform.GetChild(0).collider.enabled = true;
				m_normandy_button.transform.GetChild(0).GetComponent<UIButton>().isEnabled = true;
				if(GameObject.Find("NormandyPlaceholder") != null)
					GameObject.Find("NormandyPlaceholder").SetActive(false);*/
            }
        }
        if (m_highest_level == "Britain")
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
        if (m_highest_level == "Stalingrad")
        {
            if (m_stalingrad_current_stage < 10)
            {
                m_highest_level += "0" + m_stalingrad_current_stage;
            }
            else
            {
                m_highest_level += m_stalingrad_current_stage;
            }
        }
        if (m_highest_level == "Kursk")
        {
            if (m_kursk_current_stage < 10)
            {
                m_highest_level += "0" + m_kursk_current_stage;
            }
            else
            {
                m_highest_level += m_kursk_current_stage;
            }
        }
        /*if(m_highest_level == "Normandy"){
			if(m_normandy_current_stage < 10){
				m_highest_level += "0" + m_normandy_current_stage;
			}else{
				m_highest_level += m_normandy_current_stage;
			}
		}*/
        GetTotalStars();
        if (m_britain_button == null)
            m_britain_button = GameObject.Find("Britain");
        m_britain_button.transform.GetChild(0).GetChild(0).GetComponent<UILabel>().text = m_total_stars_earned_brit.ToString();
        if (m_stalingrad_button == null)
            m_stalingrad_button = GameObject.Find("Stalingrad");
        m_stalingrad_button.transform.GetChild(0).GetChild(0).GetComponent<UILabel>().text = m_total_stars_earned_stal.ToString();
        if (m_kursk_button == null)
            m_kursk_button = GameObject.Find("Kursk");
        m_kursk_button.transform.GetChild(0).GetChild(0).GetComponent<UILabel>().text = m_total_stars_earned_kursk.ToString();
        //m_normandy_button.transform.GetChild(0).GetChild(0).GetComponent<UILabel>().text = m_total_stars_earned_norm.ToString();
        //AchievementManager.SetLeaderboardStars(m_total_stars_earned);

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
        m_total_stars_earned_brit = 0;
        m_total_stars_earned_stal = 0;
        m_total_stars_earned_kursk = 0;
        m_total_stars_earned_norm = 0;
        for (int i = 0; i < 10; i++)
        {
            m_total_stars_earned += m_britain_star_count[i];
            m_total_stars_earned_brit += m_britain_star_count[i];
            m_total_stars_earned += m_stalingrad_star_count[i];
            m_total_stars_earned_stal += m_stalingrad_star_count[i];
            m_total_stars_earned += m_kursk_star_count[i];
            m_total_stars_earned_kursk += m_kursk_star_count[i];
            m_total_stars_earned += m_normandy_star_count[i];
            m_total_stars_earned_norm += m_normandy_star_count[i];
        }
        return m_total_stars_earned;
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

    public void UpdateAllLevels()
    {
        SaveLoadManager.m_save_info.m_britain_progress = UpdateLevels(m_britain_star_count, m_britain_stage_progress);
        SaveLoadManager.m_save_info.m_stalingrad_progress = UpdateLevels(m_stalingrad_star_count, m_stalingrad_stage_progress);
        SaveLoadManager.m_save_info.m_kursk_progress = UpdateLevels(m_kursk_star_count, m_kursk_stage_progress);
        //m_normandy_stage_progress = UpdateLevels(m_normandy_star_count, m_normandy_stage_progress);
        SaveLoadManager.s_inst.SaveFile();
    }

    int UpdateLevels(int[] stars, int level)
    {
        for (int i = 0; i < 11; i++)
        {
            level = level | (stars[i] << (i * 2));
        }
        return level;
    }

    void SaveLevelInfo()
    {
        SaveLoadManager.s_inst.SaveFile();
        //gameObject.GetComponent<KiiBucketManager>().SaveLevelInfo(m_britain_stage_progress,m_stalingrad_stage_progress,m_kursk_stage_progress,m_normandy_stage_progress);
    }

    void OnLevelWasLoaded()
    {
        if (m_times_loaded > 0)
        {
            if (Application.loadedLevelName == "MainMenu")
            {
                if (gameObject.GetComponent<GameManager>() == GameManager.s_Inst)
                {
                    m_britain_button = GameObject.Find("Britain");
                    m_stalingrad_button = GameObject.Find("Stalingrad");
                    m_kursk_button = GameObject.Find("Kursk");
                    m_normandy_button = GameObject.Find("Normandy");
                    if (m_britain_stage_progress != -1)
                        Init(m_britain_stage_progress, m_stalingrad_stage_progress, m_kursk_stage_progress, m_normandy_stage_progress);
                }
            }
        }
        else
        {
            if (Application.loadedLevelName == "MainMenu")
            {
                m_times_loaded++;
            }
        }
    }

    /// <summary>
    /// Unlocks the next level.
    /// </summary>
    /// <param name='levelname'>
    /// Name of the level you are on, use Application.loadedLevelName()
    /// </param>
    /// <param name='stage'>
    /// Which stage of the level you are on (1-11)
    /// </param>
    public void UnlockNextLevel(string levelname, int stage)
    {
        //If we just beat a boss lets unlock the next level.
        if (stage == 11)
        {
            m_overworld_stage++;
            return;
        }
        //otherwise lets unlock the next stage
        if (levelname.Contains("Britain"))
        {
            if (m_mission1_current_stage <= stage)
            { //Check to make sure we actually need to unlock the next stage.
                m_mission1_current_stage++;
            }
        }
        if (levelname.Contains("Stalingrad"))
        {
            if (m_stalingrad_current_stage <= stage)
            { //Check to make sure we actually need to unlock the next stage.
                m_stalingrad_current_stage++;
            }
        }
        if (levelname.Contains("Kursk"))
        {
            if (m_kursk_current_stage <= stage)
            { //Check to make sure we actually need to unlock the next stage.
                m_kursk_current_stage++;
            }
        }
        if (levelname.Contains("Normandy"))
        {
            if (m_normandy_current_stage <= stage)
            { //Check to make sure we actually need to unlock the next stage.
                m_normandy_current_stage++;
            }
        }
    }

    /// <summary>
    /// Resets the players progress / Locks all but Britain01.
    /// </summary>
    public void ResetProgress()
    {
        Init(0, 0, 0, 0);
        SaveLoadManager.s_inst.SaveFile();
    }

    public bool IsLevelUnlocked(string levelname, int stage)
    {
        if (levelname.Contains("Mission_1"))
        {
            if (stage == 12 && m_total_stars_earned_brit >= 10)
                return true;
            if (stage == 13 && m_total_stars_earned_brit >= 20)
                return true;
            if (m_mission1_current_stage >= stage)
            { //Check to make sure we actually need to unlock the next stage.
                return true;
            }
        }
        else if (levelname.Contains("Stalingrad"))
        {
            if (stage == 12 && m_total_stars_earned_stal >= 10)
                return true;
            if (stage == 13 && m_total_stars_earned_stal >= 20)
                return true;
            if (m_stalingrad_current_stage >= stage)
            { //Check to make sure we actually need to unlock the next stage.
                return true;
            }
        }
        else if (levelname.Contains("Kursk"))
        {
            if (stage == 12 && m_total_stars_earned_kursk >= 10)
                return true;
            if (stage == 13 && m_total_stars_earned_kursk >= 20)
                return true;
            if (m_kursk_current_stage >= stage)
            { //Check to make sure we actually need to unlock the next stage.
                return true;
            }
        }

        else if (levelname.Contains("Normandy"))
        {
            if (stage == 12 && m_total_stars_earned_norm >= 10)
                return true;
            if (stage == 13 && m_total_stars_earned_norm >= 20)
                return true;
            if (m_normandy_current_stage >= stage)
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
        m_stalingrad_current_stage = 11;
        m_kursk_current_stage = 11;
        m_normandy_current_stage = 11;
    }

    public void SetDeveloperMode(bool tf)
    {
        if (tf)
            PlayerPrefs.SetInt("DeveloperMode", 1);
        else
            PlayerPrefs.SetInt("DeveloperMode", 0);
    }



    public void ShowStalingrad()
    {
        //m_stalingrad_button.GetComponent<TweenColor>().PlayForward();
        m_stalingrad_button.transform.GetChild(0).gameObject.SetActive(true);
        //m_stalingrad_button.transform.GetChild(0).collider.enabled = true;
        m_stalingrad_button.transform.GetChild(0).GetComponent<UIButton>().isEnabled = true;
        if (GameObject.Find("StalingradPlaceholder") != null)
            GameObject.Find("StalingradPlaceholder").SetActive(false);
    }

    public void ShowKursk()
    {
        //m_kursk_button.GetComponent<TweenColor>().PlayForward();
        m_kursk_button.transform.GetChild(0).gameObject.SetActive(true);
       // m_kursk_button.transform.GetChild(0).collider.enabled = true;
        m_kursk_button.transform.GetChild(0).GetComponent<UIButton>().isEnabled = true;
        if (GameObject.Find("KurskPlaceholder") != null)
            GameObject.Find("KurskPlaceholder").SetActive(false);
    }

    public void ShowNormandy()
    {
        //m_normandy_button.GetComponent<TweenColor>().PlayForward();
        m_normandy_button.transform.GetChild(0).gameObject.SetActive(true);
       // m_normandy_button.transform.GetChild(0).collider.enabled = true;
        m_normandy_button.transform.GetChild(0).GetComponent<UIButton>().isEnabled = true;
        if (GameObject.Find("NormandyPlaceholder") != null)
            GameObject.Find("NormandyPlaceholder").SetActive(false);
    }
}
