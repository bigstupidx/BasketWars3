using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CharacterDisplay : MonoBehaviour {
	[SerializeField]
	public List<string> m_characters = new List<string>();
	int m_last_selected;
	// Update is called once per frame
	void Update () {
		if(m_last_selected != GameManager.m_character_chosen && GameManager.m_character_chosen >= 0){
			gameObject.GetComponent<UISprite>().spriteName = m_characters[GameManager.m_character_chosen];
			m_last_selected = GameManager.m_character_chosen;
		}
	}
}
