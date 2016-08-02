using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public class BattleDetailPanel : MonoBehaviour
{
    public UISprite m_texture_box;
    public UISprite m_texture_box2;
    public GameObject stars;
    public UILabel m_stage_name;
    public string m_boss_name;
    public int m_level_num;
    public string m_level_string;
    StageUnlocker stage_unlocker;
    int m_unlocked_level;
    public bool m_is_locked = false;
    public CharacterSelect m_char_select;
    public UILabel m_high_score;
    public string[] m_image_name;
    public UIAtlas[] m_atlas;

    public void SetPanelContents(string level)
    {
        m_level_string = level;
        if (stage_unlocker == null)
            stage_unlocker = GameManager.s_Inst.gameObject.GetComponent<StageUnlocker>();
        m_high_score.text = GameManager.s_Inst.GetHighScore(level).ToString();
        if (m_high_score.text == "-1")
        {
            m_high_score.transform.parent.gameObject.SetActive(false);
        }
        else
            m_high_score.transform.parent.gameObject.SetActive(true);

        int stageLevel = System.Convert.ToInt32(level.Substring(level.Length - 2));
        m_texture_box.atlas.replacement = m_atlas[0];
        if (!m_texture_box.enabled)
        {
            m_texture_box.enabled = true;
            m_texture_box2.enabled = false;
        }
        if (level.Contains("Mission_1"))
        {
            m_unlocked_level = stage_unlocker.m_mission1_current_stage;
        }
        stageLevel -= 1; //Covert to 0 based start;
        if (m_texture_box.enabled)
            m_texture_box.spriteName = m_image_name[stageLevel];
        if (m_texture_box2.enabled)
            m_texture_box2.spriteName = m_image_name[stageLevel];
        int stars_num = 0; //GameManager.s_Inst.GetComponent<StarManager>().GetStars(level);
        for (int i = 0; i < 3; i++)
        {
            if (i < stars_num)
                stars.transform.GetChild(i).GetComponent<UISprite>().enabled = true;
            else
                stars.transform.GetChild(i).GetComponent<UISprite>().enabled = false;
        }
    }

    public void NextLevel()
    {
        m_level_num++;
        if (m_level_num > 11)
            m_level_num = 1;
        SetLabels();
        SetPanelContents(m_level_string);
    }

    public void LastLevel()
    {
        m_level_num--;
        if (m_level_num < 1)
            m_level_num = 11;
        SetLabels();
        SetPanelContents(m_level_string);
    }

    public void SetLabels()
    {
        if (m_level_string.Contains("Mission_1"))
        {
            m_stage_name.text = "Battle 1-";
            m_boss_name = "THE GRENADIER";
            if (m_level_num == 12)
            {
                m_stage_name.text = "BALL BARRAGE";
                SetPanelContents(m_level_string);
                return;
            }
            else if (m_level_num == 13)
            {
                m_stage_name.text = "BONUS 2";
                SetPanelContents(m_level_string);
                return;
            }
        }
        if (m_level_num <= 10)
        {
            m_stage_name.text += m_level_num.ToString();
            stars.transform.parent.gameObject.SetActive(true);
        }
        if (m_level_num == 11)
        {
            m_stage_name.text = m_boss_name;
            stars.transform.parent.gameObject.SetActive(false);
        }
        if (m_level_num < 12)
        {
            m_is_locked = (m_level_num > m_unlocked_level) ? true : false;
        if (m_is_locked)
                m_char_select.LockLaunchButton();
            else
                m_char_select.UnlockLaunchButton();
        }
        char[] temp = m_level_string.ToCharArray();
        m_level_string = m_level_string.Substring(0, m_level_string.Length - 2) + ((m_level_num < 10) ? "0" : "") + m_level_num.ToString();
    }

    public void SetLevel(int num)
    {
        m_level_num = num;
        SetLabels();
    }

}
