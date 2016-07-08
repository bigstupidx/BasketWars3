using UnityEngine;
using System.Collections;

public class BonusDetailHandler : MonoBehaviour {
	public UISprite m_level_sprite;
	public UISprite m_soldier_sprite;
	public UILabel	m_level_name;
	public UILabel m_high_score_label;
	public string[] m_level_sprite_names;
	public string[] m_soldier_sprite_names;

	public void SetDetails(string level){
		//Handles the Level Title and soldier sprites
		if(level.Contains("Britain")){
			if(level.Contains("12")){
				m_level_name.text = "BALL BARRAGE";
				m_soldier_sprite.spriteName = m_soldier_sprite_names[1]; // Shotgun
			}
		}
		else if(level.Contains("Stalingrad")){
			if(level.Contains("12")){
				m_level_name.text = "TARGET ASSAULT";
				m_soldier_sprite.spriteName = m_soldier_sprite_names[0]; // Pistol
			}
		}
		else if(level.Contains("Kursk")){
			if(level.Contains("12")){
				m_level_name.text = "BAZOOKA BLITZ";
				m_soldier_sprite.spriteName = m_soldier_sprite_names[2]; // Bazooka
			}
		}
		m_high_score_label.text = string.Format("{0:n0}",GameManager.s_Inst.GetHighScore(level));
	}

}
