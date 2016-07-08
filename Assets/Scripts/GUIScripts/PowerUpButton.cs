using UnityEngine;
using System.Collections;

public class PowerUpButton : MonoBehaviour 
{
	public GameObject m_nuke_prefab;
	// Use this for initialization
	void Start () {
		if(GameManager.s_Inst.m_equipped_powerup == GameManager.Powerup.None)
			gameObject.SetActive(false);
	}

	public void OnClick(){
		if(GameManager.s_Inst.m_equipped_powerup == GameManager.Powerup.AcePowerup){
            gameObject.SetActive(false);
		}
	}

	void TurnOffFocusMode(){
		GameManager.s_Inst.m_is_in_focus_mode = false;
		GameManager.m_nuke_explosion = false;
		GameObject[] obs = GameObject.FindGameObjectsWithTag("Obstacle");
		foreach(GameObject g in obs){
			if(g.GetComponent<TweenPosition>() != null){
				g.GetComponent<TweenPosition>().duration /= 2;
			}
		}
	}
}
