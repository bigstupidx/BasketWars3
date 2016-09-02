using UnityEngine;
using UnityEngine.SceneManagement;
using System.Text.RegularExpressions;
using System;

public class BattleStartButton : MonoBehaviour
{
    string m_level_name;
    public BattleDetailPanel panel;

    public void SetLevelToLoad(string name)
    {
        m_level_name = name;
        transform.parent.GetComponent<TweenPosition>().PlayForward();
        if (transform.parent.FindChild("Ball Selection") != null)
            transform.parent.FindChild("Ball Selection").GetComponent<CharacterSelect>().SetRank();
        if (panel != null) {
            panel.SetPanelContents(name);
            panel.SetLevel(Convert.ToInt32(name.Substring(name.Length - 2)));
        }
    }

    public void StartBattle()
    {
        SaveLoadManager.s_inst.SaveFile();
        GameManager.s_Inst.m_level_name_to_load = m_level_name;
		int level_num = Convert.ToInt32(m_level_name.Substring(m_level_name.Length - 2));
		Debug.Log (m_level_name);
        if (level_num < 11)
        {
            if (GameManager.s_Inst.gameObject.GetComponent<StaminaGuage>().m_stamina > 0)
            {
                GameManager.s_Inst.gameObject.GetComponent<StaminaGuage>().DecreaseStamina(1);
                GameManager.s_Inst.m_current_game_state = GameManager.GameState.Gameplay;
                SceneManager.LoadScene("LevelLoader");
            }
            else
            {
                GameObject.Find("Stamina Panel").GetComponent<TweenPosition>().PlayForward();
                GameObject.Find("CrateBGPanel").GetComponent<HandleTweens>().PlayForward();
            }
        }
		else if (level_num == 11)
        {
			if (GameManager.s_Inst.gameObject.GetComponent<StaminaGuage> ().m_stamina > 1) {
				GameManager.s_Inst.gameObject.GetComponent<StaminaGuage> ().DecreaseStamina (2);
				GameManager.s_Inst.m_current_game_state = GameManager.GameState.Boss;
				SceneManager.LoadScene ("LevelLoader");
			} else {
				GameObject.Find ("Stamina Panel").GetComponent<TweenPosition> ().PlayForward ();
				GameObject.Find ("CrateBGPanel").GetComponent<HandleTweens> ().PlayForward ();
			}
        }
    }

    public void SetLevelName(string name)
    {
        m_level_name = name;
    }

    public void MoveOutPanel()
    {
        transform.parent.GetComponent<TweenPosition>().PlayReverse();
    }
}