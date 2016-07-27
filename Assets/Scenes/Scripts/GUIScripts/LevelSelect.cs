using UnityEngine;
using System.Collections;

public class LevelSelect : MonoBehaviour
{
    //Mode Select
    public TweenPosition m_campaign_mode_select;
    public TweenPosition m_Zombies_mode_select;
    public TweenPosition m_mode_select_title;
    //Map
    public UIPanel m_map_panel;
    public UIPanel m_misson_1;
    public UIPanel m_stalingrad;
    public UIPanel m_kursk;
    public UIPanel m_normandy;
    public UIPanel m_midway;
    public UIPanel m_bulge;
    //title screen
    public TweenPosition m_title_screen_upgrade;
    public TweenPosition m_title_screen_bottom;
    public TweenPosition m_title_screen_bottom_left;
    public TweenPosition m_title_screen_bottom_right;
    //public TweenPosition m_title_screen_username;
    //character select
    public TweenPosition m_charcter_select;
    public TweenPosition m_character_mid;
    public TweenPosition m_character_left;
    public TweenPosition m_character_right;
    public TweenPosition m_character_top;
    public TweenPosition m_character_bot;
    //Menus and info
    //public TweenPosition m_top_left;
    public TweenPosition m_top_mid;
    public TweenPosition m_shop_and_offers;
    //back buttons
    public TutorialCheck m_tutorial_check;
    public TweenAlpha m_back_portrait;

    public TweenAlpha m_blackout_panel;

    public bool m_is_on_map = false;


    public enum LevelNames
    {
        Britain,
        Stalingrad,
        Kursk,
        Normandy,
        Midway,
        Bulge,
        Mode_Select,
    }
    LevelNames m_level_to_load;

    public void Start()
    {
        Vector4 temp = m_map_panel.baseClipRegion;
        if (Camera.main.aspect < 1.8f && Camera.main.aspect > 1.7f)
        {
            temp.x = 180;
            temp.w = 720;
            temp.z = 1512;
        }
        m_map_panel.baseClipRegion = temp;
    }

    //Start Screen
    public void StartToMap()
    {
        if (GameManager.s_Inst.m_user_logged_in)
        {
            m_title_screen_upgrade.PlayReverse();
            m_title_screen_bottom.onFinishedForward.Clear();
            m_title_screen_bottom.onFinishedForward.Add(new EventDelegate(this, "MoveMapIn"));
            m_title_screen_bottom.PlayReverse();
            m_title_screen_bottom_left.PlayReverse();
            m_title_screen_bottom_right.PlayReverse();
            //m_title_screen_username.PlayReverse();
            m_level_to_load = LevelNames.Mode_Select;
            m_map_panel.gameObject.GetComponent<TweenPosition>().onFinishedForward.Clear();
        }
        else
        {
            /*if(KiiManager.s_inst.m_login_panel_is_down == false){
				KiiManager.LoginWithToken();
			}*/
        }
    }

    public void MoveInStartPanel()
    {
        m_title_screen_upgrade.PlayForward();
        m_title_screen_bottom.onFinishedForward.Clear();
        m_title_screen_bottom.PlayForward();
        m_title_screen_bottom_left.PlayForward();
        m_title_screen_bottom_right.PlayForward();
    }

    //Mode Select

    public void MapToStart()
    {
        m_map_panel.gameObject.GetComponent<TweenPosition>().onFinishedForward.Clear();
        m_map_panel.gameObject.GetComponent<TweenPosition>().onFinishedForward.Add(new EventDelegate(this, "MoveInStartPanel"));
    }

    //Blackout Panel
    public void MoveBOPanelIn()
    {
        m_blackout_panel.PlayForward();
    }

    public void MoveBOPanelOut()
    {
        m_blackout_panel.PlayReverse();
    }

    public void MoveCharacterSelectInBO()
    {
        m_character_left.PlayForward();
        m_character_right.PlayForward();
        m_character_top.PlayForward();
        m_character_mid.onFinishedForward.Clear();
        m_character_mid.PlayForward();
        m_character_bot.PlayForward();
    }

    public void MoveCharacterSelectOutBO()
    {
       // GameManager.m_character_chosen = CharacterSelect.m_selected_character;
        m_character_left.PlayReverse();
        m_character_right.PlayReverse();
        m_character_top.PlayReverse();
        m_character_mid.onFinishedForward.Clear();
        m_character_mid.onFinishedForward.Add(new EventDelegate(this, "MoveBOPanelOut"));
        m_character_mid.PlayReverse();
        m_character_bot.PlayReverse();
    }

    //Map Panel
    public void MoveMapOut()
    {
        m_back_portrait.PlayForward();
        //m_top_left.PlayReverse();
        m_top_mid.PlayReverse();
        //m_top_right.PlayReverse();
        m_shop_and_offers.PlayReverse();
        m_charcter_select.PlayReverse();
        m_map_panel.gameObject.GetComponent<TweenPosition>().PlayReverse(); //Move the level select panel out.
        m_is_on_map = false;
        if (m_level_to_load == LevelNames.Mode_Select)
        {
            BackButtonStack.PopStack();
            m_map_panel.gameObject.GetComponent<TweenPosition>().onFinishedForward.Clear();
            m_map_panel.gameObject.GetComponent<TweenPosition>().onFinishedForward.Add(new EventDelegate(this, "MoveInStartPanel"));
        }
        if (m_level_to_load == LevelNames.Britain)
        {
            m_map_panel.gameObject.GetComponent<TweenPosition>().onFinishedForward.Clear();
            m_map_panel.gameObject.GetComponent<TweenPosition>().onFinishedForward.Add(new EventDelegate(this, "MoveBritainIn"));
        }
        if (m_level_to_load == LevelNames.Stalingrad)
        {
            m_map_panel.gameObject.GetComponent<TweenPosition>().onFinishedForward.Add(new EventDelegate(this, "MoveStalingradIn"));
            m_stalingrad.gameObject.GetComponent<TweenPosition>().onFinishedForward.Clear();
        }
        if (m_level_to_load == LevelNames.Kursk)
        {
            m_map_panel.gameObject.GetComponent<TweenPosition>().onFinishedForward.Add(new EventDelegate(this, "MoveKurskIn"));
            m_kursk.gameObject.GetComponent<TweenPosition>().onFinishedForward.Clear();
        }
        if (m_level_to_load == LevelNames.Normandy)
        {
            m_map_panel.gameObject.GetComponent<TweenPosition>().onFinishedForward.Add(new EventDelegate(this, "MoveNormandyIn"));
            m_normandy.gameObject.GetComponent<TweenPosition>().onFinishedForward.Clear();
        }
        if (m_level_to_load == LevelNames.Midway)
        {
            m_map_panel.gameObject.GetComponent<TweenPosition>().onFinishedForward.Add(new EventDelegate(this, "MoveMidwayIn"));
            m_midway.gameObject.GetComponent<TweenPosition>().onFinishedForward.Clear();
        }
        if (m_level_to_load == LevelNames.Bulge)
        {
            m_map_panel.gameObject.GetComponent<TweenPosition>().onFinishedForward.Add(new EventDelegate(this, "MoveBulgeIn"));
            m_bulge.gameObject.GetComponent<TweenPosition>().onFinishedForward.Clear();
        }
    }

    public void MoveMapIn()
    {
        m_tutorial_check.CheckTutorial();
        m_map_panel.gameObject.GetComponent<UIScrollView>().ResetPosition();
        m_back_portrait.PlayReverse();
        m_map_panel.gameObject.GetComponent<TweenPosition>().PlayForward(); //Move the level select panel in.
                                                                            //m_top_left.PlayForward();
        m_top_mid.PlayForward();
        m_shop_and_offers.PlayForward();
        m_charcter_select.PlayForward();
        m_is_on_map = true;
    }

    public void MoveOutStartToMap()
    {
        m_title_screen_upgrade.PlayReverse();
        m_title_screen_bottom.onFinishedForward.Clear();
        m_title_screen_bottom.onFinishedForward.Add(new EventDelegate(this, "MoveMapInFromLevel"));
        m_title_screen_bottom.PlayReverse();
        m_title_screen_bottom_left.PlayForward();
        m_title_screen_bottom_right.PlayReverse();
        //m_title_screen_username.PlayReverse();
    }

    public void MoveMapInFromLevel()
    {
        m_map_panel.GetComponent<TweenPosition>().onFinishedForward.Add(new EventDelegate(this, "FixUpTweenScripts"));
        m_map_panel.gameObject.GetComponent<TweenPosition>().PlayForward(); //Move the level select panel in.
        m_back_portrait.PlayReverse();
        //m_top_left.PlayForward();
        m_top_mid.PlayForward();
        //m_top_right.PlayForward();
        m_shop_and_offers.PlayForward();
        m_charcter_select.PlayForward();
        if (GameManager.s_Inst.m_current_level_played == GameManager.CurrentLevel.Britain)
        {
            MoveMission1In();
        }
        if (GameManager.s_Inst.m_current_level_played == GameManager.CurrentLevel.Stalingrad)
        {
            MoveStalingradIn();
        }
        if (GameManager.s_Inst.m_current_level_played == GameManager.CurrentLevel.Kursk)
        {
            MoveKurskIn();
        }
        if (GameManager.s_Inst.m_current_level_played == GameManager.CurrentLevel.Normandy)
        {
            MoveNormandyIn();
        }
        if (GameManager.s_Inst.m_current_level_played == GameManager.CurrentLevel.Midway)
        {
            MoveMidwayIn();
        }
        if (GameManager.s_Inst.m_current_level_played == GameManager.CurrentLevel.Bulge)
        {
            MoveBulgeIn();
        }
        m_is_on_map = true;
    }

    public void FixUpTweenScripts()
    {
        m_title_screen_bottom.onFinishedForward.Clear();
        m_title_screen_bottom.onFinishedForward.Add(new EventDelegate(this, "MoveModeSelectIn"));
        m_map_panel.GetComponent<TweenPosition>().onFinishedForward.Clear();
    }

    public void ToggleSlideView()
    {
        //m_map_panel.gameObject.GetComponent<UIScrollView>().enabled = !m_map_panel.gameObject.GetComponent<UIScrollView>().enabled;
    }

    #region Move Functions
    public void MoveMission1In()
    {
        m_misson_1.transform.GetChild(0).localPosition = new Vector3(0, -180, 0);
        m_misson_1.transform.GetChild(0).GetComponent<TweenAlpha>().PlayForward();
        m_map_panel.GetComponent<TweenAlpha>().PlayForward();
    }

    public void MoveBritainOut()
    {
        m_misson_1.transform.GetChild(0).GetComponent<TweenAlpha>().PlayReverse();
        //BackButtonStack.PopStack();
        m_map_panel.gameObject.GetComponent<TweenPosition>().onFinishedForward.Clear();
    }

    public void MoveBritainOutToMode()
    {
        m_misson_1.gameObject.GetComponent<TweenPosition>().PlayReverse();
        m_misson_1.gameObject.GetComponent<TweenPosition>().onFinishedForward.Clear();
        //MoveModeSelectIn();
    }

    public void MoveStalingradIn()
    {
        m_stalingrad.transform.GetChild(0).localPosition = new Vector3(0, -180, 0);
        m_stalingrad.transform.GetChild(0).GetComponent<TweenAlpha>().PlayForward();
        m_map_panel.GetComponent<TweenAlpha>().PlayForward();
        //BackButtonStack.PushStack(new EventDelegate(this,"MoveStalingradOut"));
    }

    public void MoveStalingradOut()
    {
        m_stalingrad.transform.GetChild(0).GetComponent<TweenAlpha>().PlayReverse();
        //BackButtonStack.PopStack();
        m_map_panel.gameObject.GetComponent<TweenPosition>().onFinishedForward.Clear();
    }

    public void MoveKurskIn()
    {
        m_kursk.transform.GetChild(0).localPosition = new Vector3(0, -180, 0);
        m_kursk.transform.GetChild(0).GetComponent<TweenAlpha>().PlayForward();
        m_map_panel.GetComponent<TweenAlpha>().PlayForward();
        //BackButtonStack.PushStack(new EventDelegate(this,"MoveKurskOut"));
    }

    public void MoveKurskOut()
    {
        m_kursk.transform.GetChild(0).GetComponent<TweenAlpha>().PlayReverse();
        //BackButtonStack.PopStack();
        m_map_panel.gameObject.GetComponent<TweenPosition>().onFinishedForward.Clear();
    }

    public void MoveNormandyIn()
    {
        m_normandy.transform.GetChild(0).localPosition = new Vector3(0, -180, 0);
        m_normandy.transform.GetChild(0).GetComponent<TweenAlpha>().PlayForward();
        //BackButtonStack.PushStack(new EventDelegate(this,"MoveNormandyOut"));
    }

    public void MoveNormandyOut()
    {
        m_normandy.transform.GetChild(0).GetComponent<TweenAlpha>().PlayReverse();
        //BackButtonStack.PopStack();
        m_map_panel.gameObject.GetComponent<TweenPosition>().onFinishedForward.Clear();
    }

    public void MoveMidwayIn()
    {
        m_midway.gameObject.GetComponent<TweenPosition>().PlayForward();
    }

    public void MoveMidwayOut()
    {
        m_midway.gameObject.GetComponent<TweenPosition>().PlayReverse();
        if (m_is_on_map)
        {
            m_map_panel.gameObject.GetComponent<TweenPosition>().onFinishedForward.Clear();
            m_midway.gameObject.GetComponent<TweenPosition>().onFinishedForward.Add(new EventDelegate(this, "MoveMapIn"));
        }
        //else
        //MoveModeSelectIn();
    }

    public void MoveBulgeIn()
    {
        m_bulge.gameObject.GetComponent<TweenPosition>().PlayForward();
    }

    public void MoveBulgeOut()
    {
        m_bulge.gameObject.GetComponent<TweenPosition>().PlayReverse();
        if (m_is_on_map)
        {
            m_map_panel.gameObject.GetComponent<TweenPosition>().onFinishedForward.Clear();
            m_bulge.gameObject.GetComponent<TweenPosition>().onFinishedForward.Add(new EventDelegate(this, "MoveMapIn"));
        }
        //else
        //MoveModeSelectIn();

    }
    #endregion

    #region Button Functions
    public void BritainLevel()
    {
        m_level_to_load = LevelNames.Britain;
        //MoveMapOut();
    }

    public void StalingradLevel()
    {
        m_level_to_load = LevelNames.Stalingrad;
        //MoveMapOut();
    }

    public void KurskLevel()
    {
        m_level_to_load = LevelNames.Kursk;
        MoveMapOut();
    }

    public void NormandyLevel()
    {
        m_level_to_load = LevelNames.Normandy;
        MoveMapOut();
    }

    public void MidwayLevel()
    {
        m_level_to_load = LevelNames.Midway;
        MoveMapOut();
    }

    public void BulgeLevel()
    {
        m_level_to_load = LevelNames.Bulge;
        MoveMapOut();
    }

    public void ModeSelect()
    {
        GameManager.s_Inst.m_level_name_to_load = Application.loadedLevelName;
        Application.LoadLevel("LevelLoader");
        //level_to_load = LevelNames.Mode_Select;
        //if(m_is_on_map)
        //	MoveMapOut();
        //else if(m_current_map != null)
        //		m_current_map.GetComponent<MapController>().InvokeHideStages();

    }

    #endregion
}