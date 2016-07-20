using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class StageButton : MonoBehaviour
{

    public enum StageName
    {
        Mission_1,
        Normandy,
        Britain,
        Stalingrad,
        Kursk,
        Midway,
        Bulge,
        MainMenu,
        Reset,
        Failed
    }

    public StageName stage;
    public int stageLevel = 1;
    public GameObject m_stars;
    GameObject m_level_manager;

    // Use this for initialization
    void Awake()
    {
        m_level_manager = GameObject.Find("GameManagment");
        if (Application.loadedLevelName == "MainMenu")
        {
            if (m_stars == null)
            {
                if (transform.FindChild("Stars") != null)
                {
                    m_stars = transform.FindChild("Stars").gameObject;
                }
                else
                {
                    Debug.Log("Stars were not found on " + gameObject.name);
                };

            }		
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnLevelWasLoaded()
    {
        m_level_manager = GameObject.FindGameObjectWithTag("GameManager");
    }

    public void OnClick()
    {
        Debug.Log("Whats going on");
        string loadName = "";
        string stageName = "";
        if (m_level_manager == null)
            m_level_manager = GameObject.FindGameObjectWithTag("GameManager");
        if (stage == StageName.Failed)
        {
            GameManager.s_Inst.FinishedLevel();
        }
        else if (stage == StageName.MainMenu)
        {
            if (GameManager.s_Inst.m_waiting_for_stamina_refill)
            {
                GameManager.s_Inst.m_waiting_for_stamina_refill = false;
            }
            if (GameManager.s_Inst.m_current_game_state == GameManager.GameState.Gameplay)
            {
                GameManager.s_Inst.m_go_to_map = true;
            }
            m_level_manager.GetComponent<GameManager>().m_current_game_state = GameManager.GameState.MainMenu;
            GameManager.s_Inst.SaveIAPItems();
            GameManager.s_Inst.m_last_level_name = Application.loadedLevelName;
            GameManager.s_Inst.m_level_name_to_load = "MainMenu";
            Application.LoadLevel("LevelLoader");
        }
        /*if(m_level_manager.GetComponent<StaminaGuage>().m_stamina <= 0){
			if(m_level_manager.GetComponent<GameManager>().m_current_game_state == GameManager.GameState.MainMenu){
				m_level_manager.GetComponent<ShopMenu>().MoveInStaminaPanel();
			}
			else if(m_level_manager.GetComponent<GameManager>().m_current_game_state == GameManager.GameState.Gameplay){
				m_level_manager.GetComponent<GameManager>().MoveInMenu("StaminaPanel");
			}
			return;
		}*/
        else if (stage == StageName.Reset)
        {
            if (GameManager.s_Inst.m_current_game_state == GameManager.GameState.Tutorial)
            {
                Application.LoadLevelAsync(Application.loadedLevel);
                return;
            }
            if (m_level_manager.GetComponent<StaminaGuage>().m_stamina > 0)
            {
                m_level_manager.GetComponent<StaminaGuage>().DecreaseStamina(1);
                GameManager.s_Inst.SaveIAPItems();
                GameManager.s_Inst.m_level_name_to_load = Application.loadedLevelName;
                //GameObject.FindWithTag("StaminaBarRemove").GetComponent<StaminaAnimation>().SetBlip((float)GameManager.s_Inst.GetComponent<StaminaGuage>().m_stamina/(float)GameManager.s_Inst.GetComponent<StaminaGuage>().m_max_stamina);
                Application.LoadLevel("LevelLoader");
            }
            else
            {
                GameManager.s_Inst.m_waiting_for_stamina_refill = true;
                GameObject.Find("StaminaPanel").GetComponent<TweenPosition>().PlayForward();
                GameObject.Find("BlackoutPanel").GetComponent<BlackoutPanel>().MoveIn();
            }
        }
        else if (stage == StageName.Britain)
        {
            if (GameManager.s_Inst.m_current_game_state == GameManager.GameState.Gameplay)
            {
                GameManager.s_Inst.m_current_level_played = GameManager.CurrentLevel.Britain;
                PlayerPrefs.SetInt("LastLevel", (int)GameManager.s_Inst.m_current_level_played);
                GameManager.s_Inst.m_current_game_state = GameManager.GameState.MainMenu;
                GameManager.s_Inst.m_go_to_map = true;
                GameManager.s_Inst.m_level_name_to_load = "MainMenu";
                Application.LoadLevel("LevelLoader");
                return;
            }
            else
            {
                stageName = "Britain";
                GameManager.s_Inst.m_current_level_played = GameManager.CurrentLevel.Britain;
                PlayerPrefs.SetInt("LastLevel", (int)GameManager.s_Inst.m_current_level_played);
            }
        }
        else if (stage == StageName.Mission_1)
        {
            if (GameManager.s_Inst.m_current_game_state == GameManager.GameState.Gameplay)
            {
                GameManager.s_Inst.m_current_level_played = GameManager.CurrentLevel.Mission_1;
                PlayerPrefs.SetInt("LastLevel", (int)GameManager.s_Inst.m_current_level_played);
                GameManager.s_Inst.m_current_game_state = GameManager.GameState.MainMenu;
                GameManager.s_Inst.m_go_to_map = true;
                GameManager.s_Inst.m_level_name_to_load = "MainMenu";
                SceneManager.LoadScene("LevelLoader");
                return;
            }
            else
            {
                stageName = "Mission_1_";
                GameManager.s_Inst.m_current_level_played = GameManager.CurrentLevel.Mission_1;
                PlayerPrefs.SetInt("LastLevel", (int)GameManager.s_Inst.m_current_level_played);
            }
        }
        else if (stage == StageName.Stalingrad)
        {
            if (GameManager.s_Inst.m_current_game_state == GameManager.GameState.Gameplay)
            {
                GameManager.s_Inst.m_current_level_played = GameManager.CurrentLevel.Stalingrad;
                PlayerPrefs.SetInt("LastLevel", (int)GameManager.s_Inst.m_current_level_played);
                GameManager.s_Inst.m_current_game_state = GameManager.GameState.MainMenu;
                GameManager.s_Inst.m_go_to_map = true;
                GameManager.s_Inst.m_level_name_to_load = "MainMenu";
                Application.LoadLevel("LevelLoader");
                return;
            }
            else
            {
                stageName = "Stalingrad";
                GameManager.s_Inst.m_current_level_played = GameManager.CurrentLevel.Stalingrad;
                PlayerPrefs.SetInt("LastLevel", (int)GameManager.s_Inst.m_current_level_played);
            }
        }
        else if (stage == StageName.Kursk)
        {
            if (GameManager.s_Inst.m_current_game_state == GameManager.GameState.Gameplay)
            {
                GameManager.s_Inst.m_current_level_played = GameManager.CurrentLevel.Kursk;
                PlayerPrefs.SetInt("LastLevel", (int)GameManager.s_Inst.m_current_level_played);
                GameManager.s_Inst.m_current_game_state = GameManager.GameState.MainMenu;
                GameManager.s_Inst.m_go_to_map = true;
                GameManager.s_Inst.m_level_name_to_load = "MainMenu";
                Application.LoadLevel("LevelLoader");
                return;
            }
            else
            {
                stageName = "Kursk";
                GameManager.s_Inst.m_current_level_played = GameManager.CurrentLevel.Kursk;
                PlayerPrefs.SetInt("LastLevel", (int)GameManager.s_Inst.m_current_level_played);
            }
        }
        else if (stage == StageName.Normandy)
        {
            if (GameManager.s_Inst.m_current_game_state == GameManager.GameState.Gameplay)
            {
                GameManager.s_Inst.m_current_level_played = GameManager.CurrentLevel.Normandy;
                PlayerPrefs.SetInt("LastLevel", (int)GameManager.s_Inst.m_current_level_played);
                GameManager.s_Inst.m_current_game_state = GameManager.GameState.MainMenu;
                GameManager.s_Inst.m_go_to_map = true;
                GameManager.s_Inst.m_level_name_to_load = "MainMenu";
                Application.LoadLevel("LevelLoader");
                return;
            }
            else
            {
                stageName = "Normandy";
                GameManager.s_Inst.m_current_level_played = GameManager.CurrentLevel.Normandy;
                PlayerPrefs.SetInt("LastLevel", (int)GameManager.s_Inst.m_current_level_played);
            }
        }
        else if (stage == StageName.Midway)
        {
            if (GameManager.s_Inst.m_current_game_state == GameManager.GameState.Gameplay)
            {
                GameManager.s_Inst.m_current_level_played = GameManager.CurrentLevel.Midway;
                PlayerPrefs.SetInt("LastLevel", (int)GameManager.s_Inst.m_current_level_played);
                GameManager.s_Inst.m_current_game_state = GameManager.GameState.MainMenu;
                GameManager.s_Inst.m_go_to_map = true;
                GameManager.s_Inst.m_level_name_to_load = "MainMenu";
                Application.LoadLevel("LevelLoader");
                return;
            }
            else
            {
                stageName = "Midway";
                GameManager.s_Inst.m_current_level_played = GameManager.CurrentLevel.Midway;
                PlayerPrefs.SetInt("LastLevel", (int)GameManager.s_Inst.m_current_level_played);
            }
        }
        else if (stage == StageName.Bulge)
        {
            if (GameManager.s_Inst.m_current_game_state == GameManager.GameState.Gameplay)
            {
                GameManager.s_Inst.m_current_level_played = GameManager.CurrentLevel.Bulge;
                PlayerPrefs.SetInt("LastLevel", (int)GameManager.s_Inst.m_current_level_played);
                GameManager.s_Inst.m_current_game_state = GameManager.GameState.MainMenu;
                GameManager.s_Inst.m_go_to_map = true;
                GameManager.s_Inst.m_level_name_to_load = "MainMenu";
                Application.LoadLevel("LevelLoader");
                return;
            }
            else
            {
                stageName = "Bulge";
                GameManager.s_Inst.m_current_level_played = GameManager.CurrentLevel.Bulge;
                PlayerPrefs.SetInt("LastLevel", (int)GameManager.s_Inst.m_current_level_played);
            }
        }
        if (false == m_level_manager.GetComponent<StageUnlocker>().IsLevelUnlocked(stageName, stageLevel))
        {
            return;
        }
        switch (stageLevel)
        {
            case 1:
                loadName = stageName + "01";
                break;

            case 2:
                loadName = stageName + "02";
                break;

            case 3:
                loadName = stageName + "03";
                break;

            case 4:
                loadName = stageName + "04";
                break;

            case 5:
                loadName = stageName + "05";
                break;

            case 6:
                loadName = stageName + "06";
                break;

            case 7:
                loadName = stageName + "07";
                break;

            case 8:
                loadName = stageName + "08";
                break;

            case 9:
                loadName = stageName + "09";
                break;

            case 10:
                loadName = stageName + "10";
                break;

            case 11:
                loadName = stageName + "11";
                GameManager.m_boss_life = 3;
                break;
            case 12:
                loadName = stageName + "12";
                break;
            case 13:
                loadName = stageName + "13";
                break;
        }
        Debug.Log(loadName);
        if (GameManager.s_Inst.m_current_game_state == GameManager.GameState.MainMenu && stageLevel <= 11) // Normal levels.
            GameObject.Find("BattleStartButton").GetComponent<BattleStartButton>().SetLevelToLoad(loadName);
        else if (GameManager.s_Inst.m_current_game_state == GameManager.GameState.MainMenu && stageLevel > 11)
        { //bonus stages
          //TODO: If we don't have enough coins we need to pop up with coin shop.
            if (stageLevel == 12 && SaveLoadManager.m_save_info.m_coins < 50) // if we have less then 50 coins
                return;
            else if (stageLevel == 13 && SaveLoadManager.m_save_info.m_coins < 100) // If we have less then 100 coins
                return;
            else
            {
               // GameObject.Find("BonusStartButton").GetComponent<BattleStartButton>().SetLevelToLoad(loadName);
                GameObject.Find("Bonus Detail Panel").GetComponent<BonusDetailHandler>().SetDetails(loadName);
            }
        }
        else
        {
            GameManager.s_Inst.gameObject.GetComponent<StaminaGuage>().DecreaseStamina(1);
            GameManager.s_Inst.m_current_game_state = GameManager.GameState.Gameplay;
#if UNITY_IPHONE
            //FlurryBinding.logEvent("Entered: " + loadName,false);
#endif
            GameObject.Find("Loading Screen").GetComponent<TweenPosition>().PlayForward();
            GameManager.s_Inst.SaveIAPItems();
            GameManager.s_Inst.m_level_name_to_load = loadName;
            SceneManager.LoadScene("LevelLoader");
        }
    }

    public void SetStars()
    {
        if (m_stars == null)
        {
            if (transform.FindChild("Stars") != null)
            {
                m_stars = transform.FindChild("Stars").gameObject;
            }
            else
            {

            }
        }
        if (m_stars != null)
        {
            string stage_name = stage.ToString();
            if (stageLevel < 10)
                stage_name += "0" + stageLevel.ToString();
            else if (stageLevel >= 10)
                stage_name += stageLevel.ToString();
            int stars = 0; // GameManager.s_Inst.gameObject.GetComponent<StarManager>().GetStars(stage_name);
            if (stars >= 0)
            { // Stupid fix to stop map init calls before kii is init, breaking the WHOLE GAME
                if (stageLevel <= 11)
                {
                    if (stars == 0)
                    {
                        m_stars.SetActive(false);
                    }
                    else
                    {
                        for (int i = 0; i < stars; i++)
                        {
                            m_stars.transform.GetChild(i).GetComponent<UISprite>().enabled = true;
                        }
                        for (int i = stars; i < 3; i++)
                        {
                            m_stars.transform.GetChild(i).GetComponent<UISprite>().enabled = false;
                        }
                    }
                }
                else
                {
                    bool m_active = GameManager.s_Inst.gameObject.GetComponent<StageUnlocker>().IsLevelUnlocked(stage_name, stageLevel);
                    if (m_active)
                    {
                        //m_stars.SetActive(false);
                        m_stars.transform.FindChild("Star1").gameObject.SetActive(false);
                        if (m_stars.transform.parent.name == "B1")
                            m_stars.transform.FindChild("Label").GetComponent<UILabel>().text = "50";
                        if (m_stars.transform.parent.name == "B2")
                            m_stars.transform.FindChild("Label").GetComponent<UILabel>().text = "100";
                    }
                    else
                    {
                        m_stars.transform.FindChild("Coin").gameObject.SetActive(false);
                    }
                }
            }
        }
        else
        {
            Debug.Log(stage + stageLevel + " Failed");
        }
    }
}