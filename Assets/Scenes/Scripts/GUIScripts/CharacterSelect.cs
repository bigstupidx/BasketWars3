using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CharacterSelect : MonoBehaviour
{
    [SerializeField]
    public List<string> m_character_array = new List<string>();
    public List<string> m_rank_Array = new List<string>();
    public UIAtlas m_rank_atlas;
    public string[] m_names = { "Ace", "Svetlana", "Diesel", "Major" };
    public static int m_selected_character = 0;
    public UISprite m_character_sprite;
    public UILabel m_name_label;
    public UIProgressBar m_progress_bar;
    public UILabel m_XP_label;
    public UISprite m_rank_sprite;
    public GameObject m_xp_group;
    BattleDetailPanel m_detail_panel;


    //Lock stuff
    public GameObject m_unlock_group;
    public UILabel m_cost_label;
    public UIButton m_launch_button;
    public UILabel m_launch_text;
    public UISprite m_lock_icon;

    void Start()
    {
        m_detail_panel = transform.parent.parent.GetComponent<BattleDetailPanel>();
        m_selected_character = GameManager.m_character_chosen;
        UpdateLabels();
    }

    void OnLevelWasLoaded()
    {
        if (GameManager.s_Inst != null)
        {
            m_selected_character = GameManager.m_character_chosen;
            UpdateLabels();
        }
    }

    public void CharacterNext()
    {
        m_selected_character++;
        if (m_selected_character > m_character_array.Count - 1)
            m_selected_character = 0;
        GameManager.m_character_chosen = m_selected_character;
        PlayerPrefs.SetInt("CharacterChosen", m_selected_character);
        UpdateLabels();
    }

    public void CharacterPrev()
    {
        m_selected_character--;
        if (m_selected_character < 0)
            m_selected_character = m_character_array.Count - 1;
        GameManager.m_character_chosen = m_selected_character;
        PlayerPrefs.SetInt("CharacterChosen", m_selected_character);
        UpdateLabels();
    }

    public void UpdateLabels()
    {
        int num = 1 << m_selected_character;
        if ((SaveLoadManager.m_save_info.m_characters & num) != 0)
        {
            m_xp_group.SetActive(true);
            m_unlock_group.SetActive(false);
            UnlockLaunchButton();
        }
        else
        {
            m_xp_group.SetActive(false);
            m_unlock_group.SetActive(true);
            LockLaunchButton();
            if (m_selected_character == 1)
                m_cost_label.text = "1,000";
            if (m_selected_character == 2)
                m_cost_label.text = "2,250";
            if (m_selected_character == 3)
                m_cost_label.text = "2,750";
        }
        m_character_sprite.spriteName = m_character_array[m_selected_character];
        m_name_label.text = m_names[m_selected_character];
        m_progress_bar.value = GameManager.s_Inst.m_character_xp[m_selected_character] / GameManager.s_Inst.m_character_xp_max[GameManager.s_Inst.m_current_rank[m_selected_character]];
        m_XP_label.text = GameManager.s_Inst.m_character_xp[m_selected_character] + "/" + GameManager.s_Inst.m_character_xp_max[GameManager.s_Inst.m_current_rank[m_selected_character]];
        SetRank();
    }

    public void BuyCharacter()
    {
        bool unlocked = GameManager.s_Inst.BuyCharacter(m_selected_character);
        if (unlocked)
        {
            UpdateLabels();
        }
    }

    public void SetRank()
    {
        m_rank_sprite.spriteName = m_rank_Array[GameManager.s_Inst.m_current_rank[GameManager.m_character_chosen]];
    }

    public void LockLaunchButton()
    {
        m_lock_icon.enabled = true;
        m_launch_text.enabled = false;
        m_launch_button.isEnabled = false;
    }

    public void UnlockLaunchButton()
    {
        if (m_detail_panel == null)
            m_detail_panel = transform.parent.parent.GetComponent<BattleDetailPanel>();
        if (!m_detail_panel.m_is_locked)
        {
            m_lock_icon.enabled = false;
            m_launch_text.enabled = true;
            m_launch_button.isEnabled = true;
        }
    }
}