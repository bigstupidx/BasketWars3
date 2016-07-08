using UnityEngine;
using System.Collections;

public class GameCenterButtonHandler : MonoBehaviour {
	public GameObject m_achievements;
	public GameObject m_leaderboards;
	public GameObject m_login;


	// Use this for initialization
	void Start () {
		Invoke("CheckStatus",2);
	}
	
	void CheckStatus(){
		if(GameCenterBinding.isPlayerAuthenticated()){
			m_achievements.SetActive(true);
			m_leaderboards.SetActive(true);
			m_login.SetActive(false);
		}else{
			m_achievements.SetActive(false);
			m_leaderboards.SetActive(false);
			m_login.SetActive(true);
		}
		Invoke("CheckStatus",2);
	}

	public void Login(){
		GameCenterBinding.authenticateLocalPlayer();
	}
}
