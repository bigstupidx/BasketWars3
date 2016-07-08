using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LeaderboardManager : MonoBehaviour {
	public static LeaderboardManager s_inst;
	public string m_current_level_reported;
	public List<UILabel> m_score_labels = new List<UILabel>();
	public List<GameCenterScore> m_scores = new List<GameCenterScore>();
	public List<GameCenterLeaderboard> m_leaderboards = new List<GameCenterLeaderboard>();

	public void Start(){
		if(s_inst == null)
			s_inst = this;
		GameCenterBinding.loadLeaderboardTitles();
	}

	public static void SetScore(List<GameCenterScore> scores){	
		s_inst.m_scores = scores;
		int i = 0;
		Debug.Log("Setting Scores");
		foreach(GameCenterScore s in s_inst.m_scores){
			s_inst.m_score_labels[i].text = s.rank + ". " + s.alias + ": " + s.value;
		}
	}
	
 	public void GetLeaderboard(){
		if(GameCenterBinding.isPlayerAuthenticated() && m_leaderboards.Count > 0)
			GameCenterBinding.retrieveScores(false,GameCenterLeaderboardTimeScope.AllTime,1,25,m_leaderboards[0].leaderboardId);
	}

	public void SubmitScore(int score, string levelName){
		GameCenterBinding.reportScore(score,levelName);
		m_current_level_reported = levelName;
	}

	public void RetrieveScores(){
			GameCenterBinding.retrieveScores(false,GameCenterLeaderboardTimeScope.AllTime,1,25,m_current_level_reported);
	}

	public void RetrieveScoresWithName(string name){
			GameCenterBinding.retrieveScores(false,GameCenterLeaderboardTimeScope.AllTime,1,25,name);
	}
	

	public void GetPlayerScore(){
		GameCenterBinding.retrieveScoresForPlayerId(GameCenterBinding.playerAlias(),Application.loadedLevelName);
	}

	public void GetScoresAroundPlayer(List<GameCenterScore> scores){
		foreach(GameCenterScore s in scores){
			Debug.Log(s.rank);
		}
	}
}
