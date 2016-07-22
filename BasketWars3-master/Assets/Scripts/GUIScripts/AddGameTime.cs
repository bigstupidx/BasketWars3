using UnityEngine;
using System.Collections;

public class AddGameTime : MonoBehaviour {
	
	public void AddTime () {
		if(SaveLoadManager.m_save_info.m_coins >= 1000){
			GameManager.s_Inst.RemoveCoins(1000);
			GameManager.s_Inst.m_game_timer = 15;
			GameManager.m_is_paused = false;	
		}else{
			GameManager.s_Inst.FinishedLevel();
		}
	}
}
