using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Collections;
using System.Text.RegularExpressions;
using System;

public class GameManager : MonoBehaviour
{
    public bool m_alt_throwing;
    public Vector2 m_alt_distance;
    public bool m_more_levels_unlocked;
	static public bool m_nuke_explosion = false;
	[HideInInspector]
    public bool m_did_drag;
    [HideInInspector]
    public Rect m_drag_area;
    [HideInInspector]
    public Transform m_tap_point;
    Vector2 m_shoot_direction;
    [HideInInspector]
    public float m_throw_height_limit;
    [HideInInspector]
    public SoldierController m_soldier;
    [HideInInspector]
    public Texture2D m_debug_area;
    [HideInInspector]
    public float throwHeightLimit;
    [HideInInspector]
    public List<TweenPosition> m_menus = new List<TweenPosition>();
    [HideInInspector]
    public List<Vector3> m_clicks = new List<Vector3>();
    static public bool m_ball_has_been_thrown = false;
    static public bool m_level_complete = false;
    [HideInInspector]
    public Vector3 GrenadeThrowDirection;
    [HideInInspector]
    public Vector3 MortarShootDirection;
    static public bool m_is_paused = false;
    //GameObject[] m_targets;
    [HideInInspector]
    int m_current_stage;
    [HideInInspector]
    public float m_max_force_x = 50;
    [HideInInspector]
    public float m_max_force_y = 50;
    [HideInInspector]
    public float m_power_multiplyer;
    float m_nuke_timer = 10;
    public static int m_boss_life = 3;
    public static int shotCountForBoss = 0;
    [HideInInspector]
    public Vector2 m_drag_range;
    [HideInInspector]
    public int m_3_star_score;
    [HideInInspector]
    public int m_2_star_score;
    [HideInInspector]
    public int m_1_star_score;
    [HideInInspector]
    public bool m_user_logged_in = false;
    [HideInInspector]
    public bool m_did_beat_time;
    [HideInInspector]
    public bool m_beat_britain = false;
    [HideInInspector]
    public bool m_is_Demo;
    #region Stat Variables
    [HideInInspector]
    public float m_current_volume = 1;
    public static int m_character_chosen = -1;
    //public int m_characters_unlocked = 3;
    [HideInInspector]
    public int m_bullets;
	public int max_m_bullets;
    public int m_lives;
    public int m_max_lives;
	public int m_armor = 0;
    public static int m_baskets_made = 0;
    [HideInInspector]
    public UILabel m_basket_counter_label;
    [HideInInspector]
    public int m_total_targets_hit;
    [HideInInspector]
    public float m_game_speed = 1.0f;
    [HideInInspector]
    public int m_current_weapon;
    [HideInInspector]
    public int m_last_battle = 0;
    [HideInInspector]
    public bool m_go_to_map = false;
    [HideInInspector]
    public bool m_building_shake = false;
    [HideInInspector]
    public bool m_marked_for_destroy = false;
    public enum CurrentLevel
    {
        Mission_1,
        Britain,
        Stalingrad,
        Kursk,
        Normandy,
        Midway,
        Bulge
    };
    float max_height;
    float time;
    float xfactor;
    public AudioClip m_passed_clip;
    public AudioClip m_failed_clip;
    [HideInInspector]
    public int m_3_star_coins;
    [HideInInspector]
    public int m_2_star_coins;
    [HideInInspector]
    public int m_1_star_coins;

    [HideInInspector]
    public CurrentLevel m_current_level_played;

    [HideInInspector]
    public bool pistol_owned = true;
    [HideInInspector]
    public bool machine_gun_owned = false;
    [HideInInspector]
    public bool rifle_owned = false;
    [HideInInspector]
    public bool grenade_owned = false;

    [HideInInspector]
    public bool m_grenade_in_air = false;
    [HideInInspector]
    public bool m_sniper_mode = false;
    [HideInInspector]
    public GameObject m_sniper_dot;
    [HideInInspector]
    public string m_level_name_to_load;
    [HideInInspector]
    public string m_last_level_name;
    [HideInInspector]
    public bool m_failed_level = false;
    [HideInInspector]
    public bool m_waiting_for_stamina_refill = false;
    [HideInInspector]
    public bool m_first_shot = true;

    #endregion
    public enum GameType
    {
        Normal,
        Zombie,
        Challenge
    }
    public GameType m_game_type;
    public enum GameState
    {
        MainMenu,
        Tutorial,
        Gameplay,
        Credits,
        Demo,
        Bonus_Level
    }
    public GameState m_current_game_state;
    public enum Powerup
    {
        None,
        Ace,
		Armor,
		Nuke
    }
    public Powerup m_equipped_powerup;
    public static GameManager s_Inst;
    public float m_game_timer;
    public float m_game_total_time;
    [HideInInspector]
    public UIProgressBar m_time_progress_bar;
    [HideInInspector]
    public BasketStarHandler m_star_basket_handler;
    [HideInInspector]
    public UILabel m_time_left_label;
    [HideInInspector]
    public int m_3_star_xp;
    [HideInInspector]
    public int m_2_star_xp;
    [HideInInspector]
    public int m_1_star_xp;
    [SerializeField]
    public int[] m_current_rank;
    [SerializeField]
    public float[] m_character_xp;
    [SerializeField]
    public float[] m_character_xp_max;
    [SerializeField]
    public string m_britain_high_scores;
    public string m_stalingrad_high_scores;
    public string m_kursk_high_scores;
    public string m_normandy_high_scores;
    float theta;
    float vert_power;
    GameObject[] m_balls;
    int m_ball_index = 0;
    UILabel m_score_label;
    UILabel m_multiplyer_label;
    int m_bonus_score;
    ShowPointsAdded m_points_added;
    ShowPointsAdded m_points_minus;
    int m_score_multiplyer = 1;
    int m_hits_till_next_multiplyer = 0;
    bool m_first_loss = true;
    int m_last_timer_digit = 0;
    public bool m_is_in_focus_mode = false;
    public bool m_unlimited_stamina = false;
    public bool m_first_init = false;
    public Powerup m_boss_reward;
    private List<GameObject> zombies;
    private List<int> dead;
	private GameObject hoop;
	private Transform hoopTrans;
    private GUIEnableDisable GUIMANAGER;
    public int soldier_position = 2;
 	private Vector3 pos;
    public GameObject mine;
    // Use this for initialization
    void Awake()
    {
        if (s_Inst != null && s_Inst != this)
        { // If a GameManager exsists, Kill this one
            Destroy(this.gameObject);
        }
        if (s_Inst == null)
        {
            s_Inst = this;
        }
        GameObject.DontDestroyOnLoad(this.gameObject);

                                        //Grab some data from play prefs
        m_character_chosen = PlayerPrefs.GetInt("CharacterChosen", 0);
        int temp = PlayerPrefs.GetInt("LastLevel", -1);
        if (temp == -1)
        {
            PlayerPrefs.SetInt("LastLevel", 0);
            temp = 0;
        }
        m_current_level_played = (CurrentLevel)PlayerPrefs.GetInt("LastLevel", -1);
        //Don't do these in the Tutorial
        if (m_current_game_state == GameState.Gameplay) { 
            m_soldier = GameObject.FindGameObjectWithTag("Player").GetComponent<SoldierController>();
            m_is_paused = true;
            Time.timeScale = 0;
        }
        zombies = new List<GameObject>();
        dead = new List<int>();
    }

    /*	public void AddCoins(int coins){
            SaveLoadManager.m_save_info.m_coins += coins;
            if(m_current_game_state == GameState.MainMenu){
                UpdateLabels();
            }
            SaveLoadManager.s_inst.SaveFile();
            AchievementManager.SpoilsOfWar((float)SaveLoadManager.m_save_info.m_coins);
            AchievementManager.DeepPockets((float)SaveLoadManager.m_save_info.m_coins);
            AchievementManager.BuddingEntrepreneur((float)SaveLoadManager.m_save_info.m_coins);
        }*/

    public void RemoveCoins(int coins)
    {
        SaveLoadManager.m_save_info.m_coins -= coins;
        UpdateLabels();
        SaveLoadManager.s_inst.SaveFile();
    }

    public bool IsWeaponOwned(int weapon)
    {
        if (weapon == 0)
            return pistol_owned;
        if (weapon == 1)
            return grenade_owned;
        if (weapon == 2)
            return machine_gun_owned;
        if (weapon == 3)
            return rifle_owned;
        else
            return false;
    }

    public void UnlockWeapon(int weapon)
    {
        if (weapon == 1)
            grenade_owned = true;
        if (weapon == 2)
            machine_gun_owned = true; ;
        if (weapon == 3)
            rifle_owned = true;
        else
            return;
    }

    /*public void UpdateWeapons(KiiObject obj){
		pistol_owned = (bool)obj["Pistol_owned"];
		grenade_owned = (bool)obj["Grenade_owned"];
		machine_gun_owned = (bool)obj["Machine_gun_owned"];
		rifle_owned = (bool)obj["Rifle_owned"];
		m_current_weapon = (int)obj["Equipped_Weapon"];
	}*/

    public void SaveIAPItems()
    {
        SaveLoadManager.s_inst.SaveFile();
    }

    public void UpdateLabels()
    {
        if (GameObject.Find("AmmoCounter") != null)
            GameObject.Find("AmmoCounter").GetComponent<UILabel>().text = m_bullets.ToString();
        if (GameObject.Find("Coinslabel") != null && SaveLoadManager.s_inst != null)
            GameObject.Find("Coinslabel").GetComponent<UILabel>().text = string.Format("{0:n0}", SaveLoadManager.m_save_info.m_coins);
        if (GameObject.Find("SecondaryCoinLabel") != null && SaveLoadManager.s_inst != null)
            GameObject.Find("SecondaryCoinLabel").GetComponent<UILabel>().text = string.Format("{0:n0}", SaveLoadManager.m_save_info.m_coins);
        //if(GameObject.Find("User Details") != null)
        //GameObject.Find("User Details").GetComponent<UIButton>().onClick.Add(new EventDelegate(gameObject.GetComponent<KiiManager>(),"Logout"));
    }

    public void UpdateAmmoLabel()
    {
        if (GameObject.Find("Ammo Left") != null)
            GameObject.Find("Ammo Left").GetComponent<UILabel>().text = m_bullets.ToString() + "/" + max_m_bullets.ToString();
        if (GameObject.Find("AmmoLeftBar") != null)
            GameObject.Find("AmmoLeftBar").GetComponent<UIProgressBar>().value = (float)m_bullets / (float)max_m_bullets;
    }

    public void UpdateLivesLabel()
    {
        if (GameObject.Find("Life Left") != null)
            GameObject.Find("Life Left").GetComponent<UILabel>().text = m_lives.ToString() + "/" + m_max_lives.ToString();
        if (GameObject.Find("BaseLifeLeftBar") != null)
            GameObject.Find("BaseLifeLeftBar").GetComponent<UIProgressBar>().value = (float)m_lives / (float)m_max_lives;
    }

	public void UpdateArmorLabel()
	{
		if (m_armor == 0 && GameObject.Find ("ArmorPowerup") != null)
			GameObject.Find ("ArmorPowerup").SetActive (false);
		else if (GameObject.Find ("Armor Left") != null)
			GameObject.Find ("Armor Left").GetComponent<UILabel> ().text = m_armor.ToString ();
	}

    public void RemoveLife(int damage)
    {
		if (m_armor > 0) {
			m_armor -= damage;
			if (m_armor < 0) {
				m_lives += m_armor;
				m_armor = 0;
			}
			UpdateArmorLabel ();
		} else
			m_lives -= damage;

        if (m_lives <= 0)
        {
			StartCoroutine(FailedLevel());
        }
        else
        {
            UpdateLivesLabel();
        }
    }

    void OnLevelWasLoaded()
    {
        if (s_Inst != null && s_Inst != this)
        {
            Destroy(this.gameObject);
            return;
        }
        if (Application.loadedLevelName == "LevelLoader")
            return;
        m_failed_level = false;
        m_first_shot = true;
        AudioListener.volume = m_current_volume;
        if (Time.timeScale != m_game_speed)
            Time.timeScale = m_game_speed;
        if (m_current_game_state == GameState.MainMenu)
        {
            UpdateLabels();
            if (m_go_to_map)
            {
                if (PlayerPrefs.GetInt("FailedCount", 0) >= 3)
                {
                    GameObject.Find("SkipLevelPrompt").GetComponent<TweenPosition>().PlayForward();
                    GameObject.Find("CrateBGPanel").GetComponent<HandleTweens>().PlayForward();
                    PlayerPrefs.SetInt("FailedCount", 0);
                }
                //GameObject.Find("Anchor - C").GetComponent<LevelSelect>().MoveOutStartToMap();
                /*				if(!m_last_level_name.Contains("11")){ // if the level was not a boss fight lets load back into the battle map.
                                    if(m_last_level_name.Contains("Britain"))
                                        GameObject.Find("Anchor - C").GetComponent<LevelSelect>().MoveBritainIn();
                                    if(m_last_level_name.Contains("Stalingrad"))
                                        GameObject.Find("Anchor - C").GetComponent<LevelSelect>().MoveStalingradIn();
                                    if(m_last_level_name.Contains("Kursk"))
                                        GameObject.Find("Anchor - C").GetComponent<LevelSelect>().MoveKurskIn();
                                }//else we go back to the main map. */

                m_go_to_map = false;
            }

        }
        else if (m_current_game_state == GameState.Gameplay)
        {
            m_baskets_made = 0;
            m_first_loss = true;
            m_balls = GameObject.FindGameObjectsWithTag("Ball");
            GUIMANAGER = GameObject.Find("GUIRoot (2D)").GetComponent<GUIEnableDisable>();
            //GetComponent<StarManager>().ResetBools();
            UpdateAmmoLabel();
            m_ball_has_been_thrown = false;
            string stage_num = Regex.Match(Application.loadedLevelName, @"\d+").Value;
            if (stage_num.Length > 0)
            {
                //StageButton stage_button = GameObject.Find("NextButton").GetComponent<StageButton>();
                m_current_stage = int.Parse(stage_num);
                /*			if(m_current_stage <= 10){
                                if(Application.loadedLevelName.Contains("Britain"))
                                    stage_button.stage = StageButton.StageName.Britain;
                                else if(Application.loadedLevelName.Contains("Stalingrad"))
                                    stage_button.stage = StageButton.StageName.Stalingrad;
                                stage_button.stageLevel = (m_current_stage+1);
                            }*/
                if (m_current_stage == 11)
                {
                    //	GameObject.Find("NextButton").GetComponent<StageButton>().stage = StageButton.StageName.MainMenu;
                    m_boss_life = 3;
                }
            }
            m_level_complete = false;
            m_is_paused = false;
            if (GameObject.FindGameObjectWithTag("Player") != null)
                m_soldier = GameObject.FindGameObjectWithTag("Player").GetComponent<SoldierController>();
            if (GameObject.Find("TapPoint") != null)
                m_tap_point = GameObject.Find("TapPoint").transform;
            if (gameObject.GetComponent<DrawTrajectory>().enabled == false)
            {
                gameObject.GetComponent<DrawTrajectory>().enabled = true;
            }
            m_menus.Clear();
            GameObject[] menus = GameObject.FindGameObjectsWithTag("MenuPanels");
            foreach (GameObject g in menus)
            {
                if (g.GetComponent<TweenPosition>() != null)
                {
                    m_menus.Add(g.GetComponent<TweenPosition>());
                }
            }
        }
        else if (m_current_game_state == GameState.Demo)
        {
            m_ball_has_been_thrown = false;
            m_level_complete = false;
            m_is_paused = false;
            m_soldier = GameObject.FindGameObjectWithTag("Player").GetComponent<SoldierController>();
            m_tap_point = GameObject.Find("TapPoint").transform;
            if (gameObject.GetComponent<DrawTrajectory>().enabled == false)
            {
                gameObject.GetComponent<DrawTrajectory>().enabled = true;
            }
            m_menus.Clear();
            GameObject[] menus = GameObject.FindGameObjectsWithTag("MenuPanels");
            foreach (GameObject g in menus)
            {
                if (g.GetComponent<TweenPosition>() != null)
                {
                    m_menus.Add(g.GetComponent<TweenPosition>());
                }
            }
        }
        else if (m_current_game_state == GameState.Tutorial)
        {
            m_ball_has_been_thrown = false;
            m_level_complete = false;
            m_is_paused = false;
            m_soldier = GameObject.FindGameObjectWithTag("Player").GetComponent<SoldierController>();
            m_tap_point = GameObject.Find("TapPoint").transform;
            if (gameObject.GetComponent<DrawTrajectory>().enabled == false)
            {
                gameObject.GetComponent<DrawTrajectory>().enabled = true;
            }
        }
        else if (m_current_game_state == GameState.Bonus_Level)
        {
            if (gameObject.GetComponent<DrawTrajectory>().enabled == false)
            {
                gameObject.GetComponent<DrawTrajectory>().enabled = true;
            }
            //Level Init stuff here for bonus levels.
            m_soldier = GameObject.FindGameObjectWithTag("Player").GetComponent<SoldierController>();
            m_soldier.SwitchToGun();
            m_score_label = GameObject.Find("Score Label").GetComponent<UILabel>();
            m_points_added = GameObject.Find("AddPoints").GetComponent<ShowPointsAdded>();
            m_points_added.gameObject.SetActive(false);
            m_points_minus = GameObject.Find("SubPoints").GetComponent<ShowPointsAdded>();
            m_points_minus.gameObject.SetActive(false);
            m_multiplyer_label = GameObject.Find("ChainMultiplier").GetComponent<UILabel>();
            m_multiplyer_label.gameObject.SetActive(false);
            m_time_left_label = GameObject.Find("TimeLeft").GetComponent<UILabel>();
            m_time_progress_bar = GameObject.Find("TimeLeftBar").GetComponent<UIProgressBar>();
            m_menus.Clear();
            GameObject[] menus = GameObject.FindGameObjectsWithTag("MenuPanels");
            foreach (GameObject g in menus)
            {
                if (g.GetComponent<TweenPosition>() != null)
                {
                    m_menus.Add(g.GetComponent<TweenPosition>());
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
		if (!m_failed_level && (m_current_game_state == GameState.Gameplay || m_current_game_state == GameState.Tutorial)
           && SceneManager.GetActiveScene().name != "LevelLoader")
            CheckMouse();
    }

    public void FinishedLevel()
    {
        if (m_current_game_state == GameState.Gameplay)
        {
            if (!Application.loadedLevelName.Contains("11"))
            {
                //int stars = gameObject.GetComponent<StarManager>().CalcStars();
                /*if(stars == 0 && m_first_loss){
					m_first_loss = false;
					m_is_paused = true;
					GameObject.Find("2nd Chance").GetComponent<TweenPosition>().PlayForward();
					GameObject.Find("BlackoutPanel").GetComponent<BlackoutPanel>().MoveIn();
					//Trigger 15s bonus;
				}else{ */
                //	SetHighScore();
                //	if(stars > 0)
                PassedLevel();
                //	else
                //	FailedLevel();
                //}
            }
            else
            {
                if (m_boss_life <= 0)
                {
                    PassedLevel();
                }
                else
                {
                    if (m_first_loss)
                    {
                        m_first_loss = false;
                        m_is_paused = true;
                        GameObject.Find("2nd Chance").GetComponent<TweenPosition>().PlayForward();
                        GameObject.Find("BlackoutPanel").GetComponent<BlackoutPanel>().MoveIn();
                    }
                    else
                    {
                        FailedLevel();
                    }
                }
            }
        }
        if (m_current_game_state == GameState.Bonus_Level)
        {
            // Do the level finish stuff;
            PassedLevel();
        }
    }

    public void MadeBasket()
    {
        m_baskets_made++;
        explodeZombies();
        add_bullet();
        add_bullet();
        
        //Level 3, moving hoop
        if (Application.loadedLevel == 4 && Application.loadedLevel != null)
        {
            hoop = GameObject.FindGameObjectWithTag("Hoop");

            int x = UnityEngine.Random.Range(15, 20);
            Vector3 temp = new Vector3(x, 11.4303f, 0);
            hoop.transform.position = temp;
        }

        //if there's a blinking mine
        
        if(GameObject.Find("MineLight") != null && GameObject.Find("MineLight").GetComponent<SpriteRenderer>().sprite.name == "bullseye_lights_green")
        {
            //explodeZombies();
            dropMine();
        }
    }

    public void dropMine()
    {
        int mine_x = UnityEngine.Random.Range(7, 18);
        Vector3 mine_position = new Vector3(mine_x, 13);
        print(mine_x);
        GameObject go = (GameObject)Instantiate(mine, mine_position, Quaternion.identity);

    }


    public void add_bullet()
    {
        if (m_bullets < max_m_bullets)
        {
            m_bullets++;
            UpdateAmmoLabel();
        }
    }

    public void explodeZombies()
    {
        int count = 0;
        int zombieKill = 0;
        if (zombies.Count < 6)
        {
            zombieKill = zombies.Count;
        }
        else
        {
            zombieKill = 5;
        }

        List<KeyValuePair<double, int>> zombieProximity = new List<KeyValuePair<double, int>>();

        for (int j = 0; j < zombies.Count; j++)
        {
            double dist = Vector3.Distance(zombies[j].transform.position, m_soldier.transform.position);
            zombieProximity.Add(new KeyValuePair<double, int>(dist, j));
        }

        zombieProximity.Sort((x, y) => x.Value.CompareTo(y.Value));

        

        for (int i = 0; i < zombieKill; i++)
        {
            print(zombies[zombieProximity[i].Value]);
            if (zombies[zombieProximity[i].Value].GetComponent<ZombieController>().health == 1)
            {
                dead.Add(zombieProximity[i].Value);
                count++;
            }
            else
            {
                zombies[zombieProximity[i].Value].GetComponent<ZombieController>().health -= 1;
            }
        }
        for (int j = dead.Count-1; j >= 0; j--)
        {
                zombies[dead[j]].GetComponent<Transform>().position += new Vector3(0.8f, 0.4f);
                zombies[dead[j]].GetComponent<Animator>().Play("Explode");
				zombies[dead[j]].GetComponent<ZombieController> ().prep_DestoryZombie ();
        }
        
        dead.Clear();

    }

	public void killAllZombies()
	{
		int count = 0;
		//Instantly kills all no matter health
		for (int i = 0; i < zombies.Count; i++) {
			dead.Add (i);
			count++;
		}
		for (int j = dead.Count - 1; j >= 0; j--) {
			zombies[dead[j]].GetComponent<Transform>().position += new Vector3(0.8f, 0.4f);
			zombies[dead[j]].GetComponent<Animator>().Play("Explode");
			zombies[dead[j]].GetComponent<ZombieController> ().prep_DestoryZombie ();
		}
		dead.Clear ();
	}

    public void addZombie(GameObject zombie)
    {
        zombies.Add(zombie);
    }

    public void removeZombie(GameObject zombie)
    {
        zombies.Remove(zombie);
    }

    public bool noZombie()
    {
        return zombies.Count == 0;
    }

    public void AddScore(int score)
    {
        if (score > 0)
        {
            m_hits_till_next_multiplyer++;
        }
        if (m_hits_till_next_multiplyer == 5)
        {
            m_hits_till_next_multiplyer = 0;
            m_score_multiplyer++;
            if (m_score_multiplyer > 5)
                m_score_multiplyer = 5;
            m_multiplyer_label.text = "X " + m_score_multiplyer;
        }
        if (score > 0)
        {
            m_bonus_score += score * m_score_multiplyer;
            m_points_added.AddPoints(score * m_score_multiplyer);
        }
        else
        {
            m_bonus_score += score;
            m_points_added.AddPoints(score);
        }
        m_points_added.AddPoints(score * m_score_multiplyer);
        m_score_label.text = string.Format("{0:n0}", m_bonus_score); // Formats the number to have commas
    }

    public void ResetMultiplyer()
    {
        m_score_multiplyer = 1;
        m_multiplyer_label.text = "X " + m_score_multiplyer;
    }

    public void PassedLevel()
    {
        if (m_current_game_state == GameState.Gameplay && m_current_stage != 11)
        {
            AudioSource[] audios = GameObject.Find("RenderCamera").GetComponents<AudioSource>();
            foreach (AudioSource a in audios)
            {
                a.Stop();
            }
            GetComponent<AudioSource>().clip = m_passed_clip;
            GetComponent<AudioSource>().Play();
            //if(m_baskets_made > GetHighScore(Application.loadedLevelName))
            //LeaderboardManager.s_inst.SubmitScore(m_baskets_made,Application.loadedLevelName);
            //else
            //LeaderboardManager.s_inst.RetrieveScoresWithName(Application.loadedLevelName);
            UpdateLevelCompletePanel();
            //	GetComponent<StageUnlocker>().UnlockNextLevel(Application.loadedLevelName,m_current_stage);
        }
        if (m_current_game_state == GameState.Gameplay && m_current_stage == 11)
        {
            if (Application.loadedLevelName.Contains("Britain"))
            {
                GameObject.Find("Boss_Grenader_0001").GetComponent<BossGrenader>().RemoveHealth();
            }
            else if (Application.loadedLevelName.Contains("Stalingrad"))
            {
                GameObject.Find("armored_car_base").GetComponent<BossArmedCar>().RemoveHealth();
            }
            else if (Application.loadedLevelName.Contains("Kursk"))
            {
                GameObject.Find("tank_boss_base").GetComponent<BossTank>().RemoveHealth();
            }
            //else if(Application.loadedLevelName.Contains("Normandy")){
            //GameObject.Find("bunker_cannon_pivot").GetComponent<BossBunker>().RemoveHealth();
            //}
            if (m_boss_life <= 0)
            {
                if (Application.loadedLevelName.Contains("Britain"))
                    m_beat_britain = true;
                GameObject.Find("RenderCamera").GetComponent<AudioSource>().Stop();
                GetComponent<AudioSource>().clip = m_passed_clip;
                GetComponent<AudioSource>().Play();
                UpdateLevelCompletePanel();
                //GetComponent<StageUnlocker>().UnlockNextLevel(Application.loadedLevelName,m_current_stage);
                SaveIAPItems();
            }
        }
        else if (m_current_game_state == GameState.Tutorial)
        {
            AudioSource[] audios = GameObject.Find("RenderCamera").GetComponents<AudioSource>();
            foreach (AudioSource a in audios)
            {
                a.Stop();
            }
            GetComponent<AudioSource>().clip = m_passed_clip;
            GetComponent<AudioSource>().Play();
            GameObject.Find("LevelComplete").GetComponent<TweenPosition>().PlayForward();
            GameObject.Find("BlackoutPanel").GetComponent<TweenAlpha>().PlayForward();
            m_is_paused = true;
            Time.timeScale = 0;
            if (Application.loadedLevelName == "Tutorial3")
            {
                GameObject.Find("Tutorial GUIRoot (2D)").GetComponent<TutorialManager>().Tutorial3NextStep();
            }
        }
        else if (m_current_game_state == GameState.Demo)
        {
            GameObject.FindGameObjectWithTag("Ball").GetComponent<BallController>().ResetBall();
            m_baskets_made++;
        }
        else if (m_current_game_state == GameState.Bonus_Level)
        {
            AudioSource[] audios = GameObject.Find("RenderCamera").GetComponents<AudioSource>();
            Time.timeScale = 0;
            foreach (AudioSource a in audios)
            {
                a.Stop();
            }
            GetComponent<AudioSource>().clip = m_passed_clip;
            GetComponent<AudioSource>().Play();
            SetHighScore();
            //if(m_bonus_score > GetHighScore(Application.loadedLevelName))
            //LeaderboardManager.s_inst.SubmitScore(m_baskets_made,Application.loadedLevelName);
            //else
            //LeaderboardManager.s_inst.RetrieveScoresWithName(Application.loadedLevelName);
            UpdateLevelCompletePanel();
            //report score and grab leaderboard
        }
    }

	public IEnumerator FailedLevel()
    {
        if (!m_level_complete)
        {
			m_failed_level = true;
			GameObject.Find ("DeathScreen").GetComponent<Animator>().Play("GroundExplosion");	
			yield return new WaitForSeconds (8);

			 GetComponent<AudioSource>().clip = m_failed_clip;
            GetComponent<AudioSource>().Play();
            AudioSource[] audios = GameObject.Find("RenderCamera").GetComponents<AudioSource>();
            foreach (AudioSource a in audios)
            {
                a.Stop();
            }
            Time.timeScale = 0;
            GUIMANAGER.MoveLevelFailedIn();
        }
    }

	public void reactivate_zombies()
	{
		foreach (GameObject zombie in zombies) {
			zombie.GetComponent<ZombieController> ().zombie_move_again();
		}
	}

    #region Touch Controls for shooting the ball.
    void CheckTouches()
    {
        if (m_balls == null)
            return;
        bool freeball = false;
        int i = 0;
        foreach (GameObject b in m_balls)
        {
            if (!b.GetComponent<BallController>().m_is_in_air)
            {
                freeball = true;
                m_ball_index = i;
                break;
            }
            i++;
        }
        if (m_level_complete || m_is_paused || m_building_shake || !freeball) // Lets not try to throw if its not time to.
            return;
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 p = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -8)); // Grab the point that where we click
            if (m_drag_area.Contains(p))
            { //if the area we defined contains the point p
                m_tap_point.position = new Vector3(p.x, p.y, -8); // move the tap point icon here
                DrawTrajectory.drawPath = true;
                m_did_drag = true; // and start the drag functionality
                m_soldier.StartThrow(); // Start the throwing animation
            }
        }

        if (m_did_drag)
        {
            Vector3 p = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0)) + Vector3.up * 3;
            p.z = -8;// Grab the point the mouse is at now.
            if (m_alt_throwing)
            {
                Vector3 ball_to_point = p - m_balls[m_ball_index].transform.position;
                m_drag_range = ball_to_point * 2.5f;
            }
            if (m_drag_range.x > m_max_force_x)
            {
                m_drag_range.x = m_max_force_x;
            }
            if (m_drag_range.y > m_max_force_y)
            {
                m_drag_range.y = m_max_force_y;
            }
            m_shoot_direction = m_drag_range;
            DrawTrajectory.m_shoot_direction = m_shoot_direction;
            DrawTrajectory.drawPath = true;
        }

        if (Input.GetMouseButtonUp(0) && freeball && m_did_drag)
        {
            if (Application.loadedLevelName == "Tutorial2")
            {
                if (TutorialManager.s_Inst.m_current_progress == TutorialManager.TutorialProgress.Tutorial2BeforeShot)
                {
                    TutorialManager.s_Inst.Tutorial2NextStep();
                }
            }
            m_did_drag = false;
            PullArrow.doShoot = true;
            DrawTrajectory.drawPath = false;
            m_balls[m_ball_index].GetComponent<BallController>().Throw(m_shoot_direction);
            m_tap_point.position = new Vector3(0, 0, 5);
            m_soldier.FinishThrow(); // Finish the throwing animation
        }
    }
    #endregion
    #region Mouse Controls
    void CheckMouse()
    {
        if (m_balls == null)
            return;
        bool freeball = false;
        int i = 0;
        foreach (GameObject b in m_balls)
        {
            if (!b.GetComponent<BallController>().m_is_in_air)
            {
                freeball = true;
                m_ball_index = i;
                break;
            }
            i++;
        }
        if (m_level_complete || m_is_paused || m_building_shake || !freeball) // Lets not try to throw if its not time to.
            return;
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 p = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -8)); // Grab the point that where we click
            if (m_drag_area.Contains(p))
            { //if the area we defined contains the point p
                m_tap_point.position = new Vector3(p.x, p.y, -8); // move the tap point icon here
                DrawTrajectory.drawPath = true;
                m_did_drag = true; // and start the drag functionality
                m_soldier.StartThrow(); // Start the throwing animation
            }
        }

        if (m_did_drag)
        {
            Vector3 p = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0)) + Vector3.up * 3;
            p.z = -8;// Grab the point the mouse is at now.
            if (m_alt_throwing)
            {
                Vector3 ball_to_point = p - m_balls[m_ball_index].transform.position;
                m_drag_range = ball_to_point * 2.5f;
            }
            if (m_drag_range.x > m_max_force_x)
            {
                m_drag_range.x = m_max_force_x;
            }
            if (m_drag_range.y > m_max_force_y)
            {
                m_drag_range.y = m_max_force_y;
            }
            m_shoot_direction = m_drag_range;
            DrawTrajectory.m_shoot_direction = m_shoot_direction;
            DrawTrajectory.drawPath = true;
        }

        if (Input.GetMouseButtonUp(0) && m_did_drag)
        {
            m_did_drag = false;
            PullArrow.doShoot = true;
            DrawTrajectory.drawPath = false;
            m_balls[m_ball_index].GetComponent<BallController>().Throw(m_shoot_direction);
            m_tap_point.position = new Vector3(0, 0, 5);
            m_soldier.FinishThrow(); // Finish the throwing animation
        }
    }
    #endregion

    void CheckTouchesFire()
    {
        //Set Arm of Soldier and Fire gun
        if (Input.touches.Length > 0)
        {
            Touch t = Input.touches[0];
            if (t.phase == TouchPhase.Began)
            {
                m_soldier.ShootGun();
            }
        }
    }

    public void SoldierFire()
    {
        m_soldier.ShootGun();
    }

    public void MoveInMenu(string menu_name)
    {
        Debug.Log(menu_name);
        foreach (TweenPosition t in m_menus)
        {
            if (t.gameObject.name == menu_name)
            {
                if (menu_name == "LevelComplete")
                    Invoke("CallInBlackout", 1.5f);
                else
                    CallInBlackout();
                t.enabled = true;
                t.PlayForward();
                t.onFinishedForward.Clear();
                m_is_paused = true;
                return;
            }
        }
    }

    public void MoveInMenu(GameObject menu)
    {
        TweenPosition t = menu.GetComponent<TweenPosition>();
        if (menu.name == "LevelComplete")
            Invoke("CallInBlackout", 1.5f);
        else
            CallInBlackout();
        t.enabled = true;
        t.PlayForward();
        t.onFinishedForward.Clear();
        m_is_paused = true;
        return;
    }

    void CallInBlackout()
    {
        if (Application.loadedLevelName != "MainMenu" && Application.loadedLevelName != "LevelLoader")
        {
            GameObject.Find("BlackoutPanel").GetComponent<BlackoutPanel>().MoveIn();
            Time.timeScale = 0;
        }
    }

    //Special method for when we refresh our stamina and need to bring in the level failed prompt.
    public void MoveInlevelFailed()
    {
        foreach (TweenPosition t in m_menus)
        {
            if (t.gameObject.name == "FailedPanel")
            {
                t.enabled = true;
                t.PlayForward();
                m_is_paused = true;
                return;
            }
        }
    }

    public void MoveOutMenu(string menu_name)
    {
        foreach (TweenPosition t in m_menus)
        {
            if (t.gameObject.name == menu_name)
            {
                t.enabled = true;
                t.PlayReverse();
                m_is_paused = false;
                return;
            }
        }
    }

    public void MoveOutMenu(string menu_name, string method_to_call)
    {
        foreach (TweenPosition t in m_menus)
        {
            if (t.gameObject.name == menu_name)
            {
                t.enabled = true;
                t.PlayReverse();
                m_is_paused = false;
                t.onFinishedForward.Clear();
                t.onFinishedForward.Add(new EventDelegate(this, method_to_call));
                return;
            }
        }
    }

    void OnGUI()
    {
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawSphere(new Vector3(m_drag_area.x, m_drag_area.y, 0), 0.1f);
        Gizmos.DrawSphere(new Vector3(m_drag_area.x + m_drag_area.width, m_drag_area.y, 0), 0.1f);
        Gizmos.DrawSphere(new Vector3(m_drag_area.x, m_drag_area.y + m_drag_area.height, 0), 0.1f);
        Gizmos.DrawSphere(new Vector3(m_drag_area.x + m_drag_area.width, m_drag_area.y + m_drag_area.height, 0), 0.1f);
        Gizmos.DrawLine(new Vector3(m_drag_area.x, m_drag_area.y, 0), new Vector3(m_drag_area.x + m_drag_area.width, m_drag_area.y, 0)); //Top Line
        Gizmos.DrawLine(new Vector3(m_drag_area.x, m_drag_area.y + m_drag_area.height, 0), new Vector3(m_drag_area.x + m_drag_area.width, m_drag_area.y + m_drag_area.height, 0)); //Bottom Line
        Gizmos.DrawLine(new Vector3(m_drag_area.x, m_drag_area.y, 0), new Vector3(m_drag_area.x, m_drag_area.y + m_drag_area.height, 0)); //Left Line
        Gizmos.DrawLine(new Vector3(m_drag_area.x + m_drag_area.width, m_drag_area.y, 0), new Vector3(m_drag_area.x + m_drag_area.width, m_drag_area.y + m_drag_area.height, 0)); //Right Line
        if (Camera.main != null)
        {
            Vector3 p = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane));
            Gizmos.color = Color.yellow;
            Gizmos.DrawSphere(p, 0.1F);
        }
    }

    public void DropTheBomb()
    {

    }

    public void ResetTime()
    {
        Time.timeScale = m_game_speed;
    }

    public bool BuyCharacter(int character)
    {
        if ((SaveLoadManager.m_save_info.m_characters >> character & 1) != 1)
        {
            if (character == 1)
            {
                if (SaveLoadManager.m_save_info.m_coins >= 1000)
                {
                    RemoveCoins(1000);
                    SaveLoadManager.m_save_info.m_characters = SaveLoadManager.m_save_info.m_characters | 1 << character;
                    SaveLoadManager.s_inst.SaveFile();
                    return true;
                }
            }
            if (character == 2)
            {
                if (SaveLoadManager.m_save_info.m_coins >= 2250)
                {
                    RemoveCoins(2250);
                    SaveLoadManager.m_save_info.m_characters = SaveLoadManager.m_save_info.m_characters | 1 << character;
                    SaveLoadManager.s_inst.SaveFile();
                    return true;
                }
            }
            if (character == 3)
            {
                if (SaveLoadManager.m_save_info.m_coins >= 2750)
                {
                    RemoveCoins(2750);
                    SaveLoadManager.m_save_info.m_characters = SaveLoadManager.m_save_info.m_characters | 1 << character;
                    SaveLoadManager.s_inst.SaveFile();
                    return true;
                }
            }
            return false;
        }
        else
            return true;
    }

    public int GetHighScore(string level_name)
    {
        int num = Convert.ToInt32(Regex.Match(level_name, @"\d+").Value) - 1;
        if (num == 10) // no highscore for boss;
            return -1;
        if (num == 11)//change the bonus level to 10
            num = 10;
        if (level_name.Contains("Britain"))
        {
            string[] scores = m_britain_high_scores.Split(',');
            return Convert.ToInt32(scores[num]);
        }
        if (level_name.Contains("Stalingrad"))
        {
            string[] scores = m_stalingrad_high_scores.Split(',');
            return Convert.ToInt32(scores[num]);
        }
        if (level_name.Contains("Kursk"))
        {
            string[] scores = m_kursk_high_scores.Split(',');
            return Convert.ToInt32(scores[num]);
        }
        if (level_name.Contains("Normandy"))
        {
            string[] scores = m_normandy_high_scores.Split(',');
            return Convert.ToInt32(scores[num]);
        }
        return -1;
    }

    public void SetHighScore()
    {
        int num = Convert.ToInt32(Regex.Match(Application.loadedLevelName, @"\d+").Value) - 1;
        if (num == 10) // no highscore for boss;
            return;
        if (num == 11)//change the bonus level to 10
            num = 10;
        if (Application.loadedLevelName.Contains("Britain"))
        {
            string[] scores = m_britain_high_scores.Split(',');
            if (num < 10)
            {
                if (m_baskets_made > Convert.ToInt32(scores[num]))
                {
                    scores[num] = m_baskets_made.ToString();
                    m_britain_high_scores = string.Join(",", scores, 0, scores.Length);
                }
            }
            else
            {
                if (m_bonus_score > Convert.ToInt32(scores[num]))
                {
                    scores[num] = m_bonus_score.ToString();
                    m_britain_high_scores = string.Join(",", scores, 0, scores.Length);
                }
            }
        }
        else if (Application.loadedLevelName.Contains("Stalingrad"))
        {
            string[] scores = m_stalingrad_high_scores.Split(',');
            if (num < 10)
            {
                if (m_baskets_made > Convert.ToInt32(scores[num]))
                {
                    scores[num] = m_baskets_made.ToString();
                    m_britain_high_scores = string.Join(",", scores, 0, scores.Length);
                }
                else
                {
                    if (m_bonus_score > Convert.ToInt32(scores[num]))
                    {
                        scores[num] = m_bonus_score.ToString();
                        m_stalingrad_high_scores = string.Join(",", scores, 0, scores.Length);
                    }
                }
            }
        }
        else if (Application.loadedLevelName.Contains("Kursk"))
        {
            string[] scores = m_kursk_high_scores.Split(',');
            if (num < 10)
            {
                if (m_baskets_made > Convert.ToInt32(scores[num]))
                {
                    scores[num] = m_baskets_made.ToString();
                    m_britain_high_scores = string.Join(",", scores, 0, scores.Length);
                }
                else
                {
                    if (m_bonus_score > Convert.ToInt32(scores[num]))
                    {
                        scores[num] = m_bonus_score.ToString();
                        m_kursk_high_scores = string.Join(",", scores, 0, scores.Length);
                    }
                }
            }
        }
        else if (Application.loadedLevelName.Contains("Normandy"))
        {
            string[] scores = m_normandy_high_scores.Split(',');
            if (num < 10)
            {
                if (m_baskets_made > Convert.ToInt32(scores[num]))
                {
                    scores[num] = m_baskets_made.ToString();
                    m_britain_high_scores = string.Join(",", scores, 0, scores.Length);
                }
                else
                {
                    if (m_bonus_score > Convert.ToInt32(scores[num]))
                    {
                        scores[num] = m_bonus_score.ToString();
                        m_normandy_high_scores = string.Join(",", scores, 0, scores.Length);
                    }
                }
            }
        }
    }

    public void TutorialStarted()
    {

    }

    public void LoadRanksAndXP(string rank, string xp)
    {
        string[] ranks = rank.Split(',');
        string[] xps = xp.Split(',');
        int i = 0;
        foreach (string s in ranks)
        {
            GameManager.s_Inst.m_current_rank[i] = Convert.ToInt32(s);
            i++;
        }
        i = 0;
        foreach (string s in xps)
        {
            GameManager.s_Inst.m_character_xp[i] = Convert.ToInt32(s);
            i++;
        }
    }

    public void AddXP(int _xp)
    {
        if (m_current_rank[m_character_chosen] < m_character_xp_max.Length)
        {
            m_character_xp[m_character_chosen] += _xp;
            if (m_character_xp[m_character_chosen] >= m_character_xp_max[m_current_rank[m_character_chosen]])
            {
                m_character_xp[m_character_chosen] -= m_character_xp_max[m_current_rank[m_character_chosen]];
                m_current_rank[m_character_chosen]++;
                AddReward();
            }
        }
    }

    public void AddReward()
    {
        int level = m_current_rank[m_character_chosen];
        //add the reward.
        switch (level)
        {
            case 1:
                break;
            default:
                break;
        }
        //Show the reward screen.
    }

    public void UpdateLevelCompletePanel()
    {
        if (m_current_game_state == GameState.Gameplay)
        {
            GUIMANAGER.MoveLevelCompleteIn();
            m_level_complete = true;
            /*			if(Application.loadedLevelName == gameObject.GetComponent<StageUnlocker>().m_highest_level)
                            PlayerPrefs.SetInt("FailedCount",0); */
            //Check the star count
            int stars = 0; //gameObject.GetComponent<StarManager>().CalcStars();
            if (m_baskets_made >= m_3_star_score)
            {
                //AchievementManager.SlamDunk();
            }
            //if(m_first_shot)
            //AchievementManager.Swish();
            //Update left half of the level Complete panel
            GameObject level_complete = GameObject.Find("LevelComplete");
            UILabel coin_reward = GameObject.Find("Reward Counter").GetComponent<UILabel>();
            UIProgressBar xp_bar = level_complete.transform.FindChild("RankSection").GetComponent<UIProgressBar>();
            if (stars == 3)
            {
                AddXP(m_3_star_xp);
                //AddCoins(m_3_star_coins);
                coin_reward.text = "+" + m_3_star_coins;
            }
            else if (stars == 2)
            {
                AddXP(m_2_star_xp);
                //AddCoins(m_2_star_coins);
                coin_reward.text = "+" + m_2_star_coins;
            }
            else if (stars == 1)
            {
                AddXP(m_1_star_xp);
                //AddCoins(m_1_star_coins);
                coin_reward.text = "+" + m_1_star_coins;
            }
            xp_bar.value = m_character_xp[m_character_chosen] / m_character_xp_max[m_current_rank[m_character_chosen]];
            //gameObject.GetComponent<StarManager>().SetStars(Application.loadedLevelName,stars);

            //Update left half of the level Complete panel
            UILabel high_score = level_complete.transform.FindChild("High Score").GetChild(0).GetComponent<UILabel>();
            high_score.text = GetHighScore(Application.loadedLevelName).ToString();

            //Set the stars on the level complete menu
            GameObject star = GameObject.Find("Stars");
            if (star != null)
            {
                for (int i = 0; i < stars; i++)
                {
                    star.transform.GetChild(i).GetComponent<TweenAlpha>().PlayForward();
                    star.transform.GetChild(i).GetComponent<TweenScale>().PlayForward();
                    star.transform.GetChild(i).GetComponent<TweenRotation>().PlayForward();
                }
            }
        }
        else if (m_current_game_state == GameState.Bonus_Level)
        {
            m_level_complete = true;
            MoveInMenu("LevelComplete");
            GameObject.Find("CurrentScoreLabel").GetComponent<UILabel>().text = string.Format("{0:n0}", m_bonus_score);
            GameObject.Find("HighScoreLabel").GetComponent<UILabel>().text = string.Format("{0:n0}", GetHighScore(Application.loadedLevelName));
            //gameObject.GetComponent<StageUnlocker>().UpdateAllLevels();
        }
    }

    public void LoadTutorial()
    {
        m_marked_for_destroy = true;
        m_level_name_to_load = "Tutorial1";
        Application.LoadLevelAsync("LevelLoader");
    }

    public void GenerateLeaderboardData()
    {
        for (int i = 0; i < 250; i++)
        {
            int score = UnityEngine.Random.Range(1, 10000000);
            //GameCenterBinding.reportScore(score,"test_board_BW_1");
        }

    }

    public void SetCameraRect()
    {
        Vector3 TL = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, Camera.main.nearClipPlane));
        m_drag_area.x = TL.x + 2;
        m_drag_area.y = TL.y + 1.29f;
        m_drag_area.width = (Camera.main.transform.position.x - TL.x) * 1.6f;
        m_drag_area.height = (Camera.main.transform.position.y - TL.y) * 1.6f;
    }
}