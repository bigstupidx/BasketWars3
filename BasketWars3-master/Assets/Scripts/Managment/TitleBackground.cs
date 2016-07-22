using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TitleBackground : MonoBehaviour 
{
	[SerializeField]
	List<Sprite> m_backgrounds = new List<Sprite>();
	// Use this for initialization
	void Start () {
		SetBackground();
	}

	void SetBackground(){
		if(GameManager.s_Inst.m_current_level_played == GameManager.CurrentLevel.Britain){
			gameObject.GetComponent<UI2DSprite>().sprite2D = m_backgrounds[0];
		}
		else if(GameManager.s_Inst.m_current_level_played == GameManager.CurrentLevel.Stalingrad){
			gameObject.GetComponent<UI2DSprite>().sprite2D = m_backgrounds[1];
		}
	}

	void OnLevelWasLoaded(){
		SetBackground();
	}

}
