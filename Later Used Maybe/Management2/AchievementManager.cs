using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AchievementManager : MonoBehaviour 
{
	public static AchievementManager s_Inst;
	bool m_slam_dunk = false;
	bool m_ding = false;
	bool m_swish = false;
	bool m_spoils_of_war = false;
	bool m_youre_pretty_good_at_this = false;
	bool m_grenadier = false;
	bool m_armored_car = false;
	bool m_deep_pockets = false;
	bool m_Keep_truckin = false;
	bool m_budding_entrepreneur = false;
	bool m_britain_veteran = false;
	bool m_stalingrad_veteran = false;
	bool m_halfway_there = false;
	bool m_tank= false;
	bool m_bunker= false;
	bool m_kursk_veteran= false;
	bool m_normandy_veteran= false;

	List<GameCenterAchievement> m_achievements;
	// Use this for initialization
	void Start () {
		if(s_Inst == null)
			s_Inst = this;
		GameCenterBinding.getAchievements();
	}

	public static void CheckAchievementsFromStageUnlocker(){
		s_Inst.gameObject.GetComponent<StageUnlocker>().CheckAchievements();
	}

	public static void SetLeaderboardStars(int stars){
		GameCenterBinding.reportScore((long)stars,"stars_leaderboard");
		//Debug.Log("Leaderboard Stars " + stars + " " + (long)stars);
	}


	public static void SetAchievementsList(List<GameCenterAchievement> list){
		s_Inst.m_achievements = list;
		foreach(GameCenterAchievement a in s_Inst.m_achievements){
			if(a.identifier == "slam_dunk")
				s_Inst.m_slam_dunk = a.completed;
			if(a.identifier == "ding")
				s_Inst.m_ding = a.completed;
			if(a.identifier == "swish")
				s_Inst.m_swish = a.completed;
			if(a.identifier == "spoils_of_war")
				s_Inst.m_spoils_of_war = a.completed;
			if(a.identifier == "youre_pretty_good_at_this"){
				s_Inst.m_youre_pretty_good_at_this = a.completed;
				if(a.completed != true)
					GameManager.s_Inst.m_total_targets_hit = (int)a.percentComplete * 2;
			}
			if(a.identifier == "grenadier")
				s_Inst.m_grenadier = a.completed;
			if(a.identifier == "armored_car")
				s_Inst.m_armored_car = a.completed;
			if(a.identifier == "tank")
				s_Inst.m_tank = a.completed;
			if(a.identifier == "bunker")
				s_Inst.m_bunker = a.completed;
			if(a.identifier == "deep_pockets")
				s_Inst.m_deep_pockets = a.completed;
			if(a.identifier == "Keep_truckin")
				s_Inst.m_Keep_truckin = a.completed;
			if(a.identifier == "halfway_there")
				s_Inst.m_halfway_there = a.completed;
			if(a.identifier == "budding_entrepreneur")
				s_Inst.m_budding_entrepreneur = a.completed;
			if(a.identifier == "britain_veteran")
				s_Inst.m_britain_veteran = a.completed;
			if(a.identifier == "stalingrad_veteran")
				s_Inst.m_stalingrad_veteran = a.completed;
			if(a.identifier == "kursk_veteran")
				s_Inst.m_kursk_veteran = a.completed;
			if(a.identifier == "noramndy_veteran")
				s_Inst.m_normandy_veteran = a.completed;
		}
		CheckAchievementsFromStageUnlocker();			
	}

	public static void SlamDunk(){
		if(GameCenterBinding.isPlayerAuthenticated() && s_Inst.m_slam_dunk != true){
			GameCenterBinding.reportAchievement("slam_dunk",100);
			s_Inst.m_slam_dunk = true;
		}
	}

	public static void Ding(){
		if(GameCenterBinding.isPlayerAuthenticated() && s_Inst.m_ding != true){
			GameCenterBinding.reportAchievement("ding",100);
			s_Inst.m_ding = true;
		}
	}

	public static void Swish(){
		if(GameCenterBinding.isPlayerAuthenticated() && s_Inst.m_swish != true){
			GameCenterBinding.reportAchievement("swish",100);
			s_Inst.m_swish = true;
		}
	}

	public static void SpoilsOfWar(float coins){
		if(GameCenterBinding.isPlayerAuthenticated() && s_Inst.m_spoils_of_war != true){
			GameCenterBinding.reportAchievement("spoils_of_war",coins);
			if(coins >= 100)
				s_Inst.m_spoils_of_war = true;
		}
	}

	public static void YourePrettyGoodAtThis(int targets_hit){
		if(GameCenterBinding.isPlayerAuthenticated() && s_Inst.m_youre_pretty_good_at_this != true){
			GameCenterBinding.reportAchievement("youre_pretty_good_at_this",targets_hit/2);
			if(targets_hit >= 200)
				s_Inst.m_youre_pretty_good_at_this = true;
		}
	}

	public static void Grenadier(){
		if(GameCenterBinding.isPlayerAuthenticated() && s_Inst.m_grenadier != true){
			GameCenterBinding.reportAchievement("grenadier",100);
			s_Inst.m_grenadier = true;
		}
	}

	public static void ArmoredCar(){
		if(GameCenterBinding.isPlayerAuthenticated() && s_Inst.m_armored_car != true){
			GameCenterBinding.reportAchievement("armored_car",100);
			s_Inst.m_armored_car = true;
		}
	}

	public static void Tank(){
		if(GameCenterBinding.isPlayerAuthenticated() && s_Inst.m_tank != true){
			GameCenterBinding.reportAchievement("tank",100);
			s_Inst.m_tank = true;
		}
	}

	public static void Bunker(){
		if(GameCenterBinding.isPlayerAuthenticated() && s_Inst.m_bunker != true){
			GameCenterBinding.reportAchievement("bunker",100);
			s_Inst.m_bunker = true;
		}
	}

	public static void DeepPockets(float coins){
		if(GameCenterBinding.isPlayerAuthenticated() && s_Inst.m_deep_pockets != true){
			GameCenterBinding.reportAchievement("deep_pockets",coins/5);
			if(coins >= 500)
				s_Inst.m_deep_pockets = true;
		}
	}

	public static void KeepTruckin(int stars){
		if(GameCenterBinding.isPlayerAuthenticated() && s_Inst.m_Keep_truckin != true){
			GameCenterBinding.reportAchievement("Keep_truckin",Mathf.Max(((stars/50) * 100),100));
			s_Inst.m_Keep_truckin = true;
		}
	}

	public static void HalfwayThere(int stars){
		if(GameCenterBinding.isPlayerAuthenticated() && s_Inst.m_halfway_there != true){
			GameCenterBinding.reportAchievement("halfway_there",Mathf.Max(((stars/100) * 100),100));
			s_Inst.m_halfway_there = true;
		}
	}

	public static void BuddingEntrepreneur(float coins){
		if(GameCenterBinding.isPlayerAuthenticated() && s_Inst.m_budding_entrepreneur != true){
			GameCenterBinding.reportAchievement("budding_entrepreneur",coins/10);
			if(coins >= 1000)
				s_Inst.m_budding_entrepreneur = true;
		}
	}

	public static void BritainVeteran(float britain_stars){
		if(GameCenterBinding.isPlayerAuthenticated() && s_Inst.m_britain_veteran != true){
			GameCenterBinding.reportAchievement("britain_veteran",(britain_stars / 33f) * 100);
			if(britain_stars >= 33)
				s_Inst.m_britain_veteran = true;
		}
	}

	public static void StalingradVeteran(float stalingrad_stars){
		if(GameCenterBinding.isPlayerAuthenticated() && s_Inst.m_stalingrad_veteran != true){
			GameCenterBinding.reportAchievement("stalingrad_veteran",(stalingrad_stars / 33f) * 100);
			if(stalingrad_stars >= 33)
				s_Inst.m_stalingrad_veteran = true;
		}
	}

	public static void KurskVeteran(float kursk_stars){
		if(GameCenterBinding.isPlayerAuthenticated() && s_Inst.m_kursk_veteran != true){
			GameCenterBinding.reportAchievement("kursk_veteran",(kursk_stars / 33f) * 100);
			if(kursk_stars >= 33)
				s_Inst.m_kursk_veteran = true;
		}
	}

	public static void NormandyVeteran(float normandy_stars){
		if(GameCenterBinding.isPlayerAuthenticated() && s_Inst.m_normandy_veteran != true){
			GameCenterBinding.reportAchievement("noramndy_veteran",(normandy_stars / 33f) * 100);
			if(normandy_stars >= 33)
				s_Inst.m_normandy_veteran = true;
		}
	}
	
	public void ShowLeaderboards()
	{
		if(GameCenterBinding.isPlayerAuthenticated()){
			//GameCenterBinding.showLeaderboardWithTimeScopeAndLeaderboard(GameCenterLeaderboardTimeScope.AllTime,"stars_leaderboard");
			GameCenterBinding.showLeaderboardWithTimeScopeAndLeaderboard(GameCenterLeaderboardTimeScope.AllTime,"test_board_BW_1");
		}
		else
			GameCenterBinding.authenticateLocalPlayer();
	}

	public void ShowAchievements()
	{
		if(GameCenterBinding.isPlayerAuthenticated())
			GameCenterBinding.showAchievements();
	}
}
