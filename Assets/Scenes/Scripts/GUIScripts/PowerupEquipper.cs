using UnityEngine;
using System.Collections;

public class PowerupEquipper : MonoBehaviour
{
    public static PowerupEquipper s_inst;
    public PowerupPageHandler m_nuke_page;
    public PowerupPageHandler m_armor_page;
    public PowerupPageHandler m_focus_page;
    public PowerupPageHandler m_ace_powerup_page;
    public GameObject m_powerup_button;
    public GameObject m_powerup_equipped_button;
    public UISprite m_powerup_icon;

    public void Awake()
    {
        s_inst = this;
    }

    public static void Init()
    {
        if (s_inst == null)
            s_inst = GameObject.Find("PowerupsShop").GetComponent<PowerupEquipper>();
        s_inst.m_nuke_page.Init(SaveLoadManager.m_save_info.m_nuke_powerup);
        s_inst.m_armor_page.Init(SaveLoadManager.m_save_info.m_armor_powerup);
        s_inst.m_focus_page.Init(SaveLoadManager.m_save_info.m_focus_powerup);
		s_inst.m_ace_powerup_page.Init(SaveLoadManager.m_save_info.m_ace_powerup);
        if (GameManager.s_Inst.m_equipped_powerup != GameManager.Powerup.None)
        {
            s_inst.m_powerup_button.SetActive(false);
            s_inst.m_powerup_equipped_button.SetActive(true);
           // s_inst.m_powerup_icon.spriteName = "Powerup" + GameManager.s_Inst.m_equipped_powerup.ToString() + "Retina";
        }
    }

    public void OnLevelWasLoaded()
    {
        Init();
    }

    public void EquipPowerup(string powerup_name)
    {
		if (powerup_name == "ACE") {
			GameManager.s_Inst.m_equipped_powerup = GameManager.Powerup.Ace;
		} else if (powerup_name == "NUKE")
			GameManager.s_Inst.m_equipped_powerup = GameManager.Powerup.Nuke;
		else if (powerup_name == "ARMOR")
			GameManager.s_Inst.m_equipped_powerup = GameManager.Powerup.Armor;
        Init();
    }
}