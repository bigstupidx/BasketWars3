using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public class BattleDetailPanel : MonoBehaviour 
{
	public UISprite m_texture_box;
	public UISprite m_texture_box2;
	public GameObject stars;
	public TweenPosition m_tween;
	public UILabel m_stage_name;
	public string m_boss_name;
	public int m_level_num;
	public string m_level_string;
	StageUnlocker stage_unlocker;
	int m_unlocked_level;
	public bool m_is_locked = false;
	public CharacterSelect m_char_select;
	public UILabel m_high_score;
	public string[] m_image_name;
	public UIAtlas[] m_atlas;

	public void SetPanelContents(string level)
	{
		if(stage_unlocker == null)
			stage_unlocker = GameManager.s_Inst.gameObject.GetComponent<StageUnlocker>();
		m_level_string = level;
		m_high_score.text = GameManager.s_Inst.GetHighScore(level).ToString();
		if(m_high_score.text == "-1"){
			m_high_score.transform.parent.gameObject.SetActive(false);
		}
		else
			m_high_score.transform.parent.gameObject.SetActive(true);

		int stageLevel = System.Convert.ToInt32(Regex.Match(m_level_string, @"\d+").Value);
		m_texture_box.atlas.replacement = m_atlas[0];
		if(!m_texture_box.enabled){
			m_texture_box.enabled = true;
			m_texture_box2.enabled = false;
		}
		if(level.Contains("Britain")){
			m_unlocked_level = stage_unlocker.m_britain_current_stage;
		}
		else if(level.Contains("Stalingrad")){
			stageLevel += 12;
			m_unlocked_level = stage_unlocker.m_stalingrad_current_stage;
		}
		else if(level.Contains("Kursk")){
			stageLevel += 24;
			if(!m_texture_box2.enabled){
				m_texture_box.enabled = false;
				m_texture_box2.enabled = true;
			}
			m_unlocked_level = stage_unlocker.m_kursk_current_stage;
		}
		else if(level.Contains("Normandy")){
			stageLevel += 36;
			m_unlocked_level = stage_unlocker.m_normandy_current_stage;
		}
		stageLevel -= 1; //Covert to 0 based start;
		if(m_texture_box.enabled)
			m_texture_box.spriteName = m_image_name[stageLevel];
		if(m_texture_box2.enabled)
			m_texture_box2.spriteName = m_image_name[stageLevel];
		transform.GetChild(0).FindChild("BattleStartButton").GetComponent<BattleStartButton>().SetLevelName(m_level_string);
		int stars_num = GameManager.s_Inst.GetComponent<StarManager>().GetStars(level);
		for(int i = 0; i < 3; i++)
		{
			if(i < stars_num)
				stars.transform.GetChild(i).GetComponent<UISprite>().enabled = true;
			else
				stars.transform.GetChild(i).GetComponent<UISprite>().enabled = false;
		}
	}

	public void NextLevel(){
		m_level_num++;
		if(m_level_num >11)
			m_level_num = 1;
		SetLabels();
	}

	public void LastLevel(){
		m_level_num--;
		if(m_level_num < 1)
			m_level_num = 11;
		SetLabels();
	}

	public void SetLabels(){
		if(m_level_string.Contains("Britain")){
			m_stage_name.text = "STAGE 1-";
			m_boss_name = "THE GRENADIER";
			if(m_level_num == 12){
				m_stage_name.text = "BALL BARRAGE";
				SetPanelContents(m_level_string);
				return;
			}
			else if(m_level_num == 13){
				m_stage_name.text = "BONUS 2";
				SetPanelContents(m_level_string);
				return;
			}
		}else if(m_level_string.Contains("Stalingrad")){
			m_stage_name.text = "STAGE 2-";
			m_boss_name = "ARMORED CAR";
			if(m_level_num == 12){
				m_stage_name.text = "TARGET ASSAULT";
				SetPanelContents(m_level_string);
				return;
			}
			else if(m_level_num == 13){
				m_stage_name.text = "BONUS 2";
				SetPanelContents(m_level_string);
				return;
			}
		}else if(m_level_string.Contains("Kursk")){
			m_stage_name.text = "STAGE 3-";
			m_boss_name = "THE TANK";
			if(m_level_num == 12){
				m_stage_name.text = "BAZOOKA BLITZ";
				SetPanelContents(m_level_string);
				return;
			}
			else if(m_level_num == 13){
				m_stage_name.text = "BONUS 2";
				SetPanelContents(m_level_string);
				return;
			}
		}else if(m_level_string.Contains("Normandy")){
			m_stage_name.text = "STAGE 4-";
			m_boss_name = "THE BUNKER";
			if(m_level_num == 12){
				m_stage_name.text = "BONUS 1";
				SetPanelContents(m_level_string);
				return;
			}
			else if(m_level_num == 13){
				m_stage_name.text = "BONUS 2";
				SetPanelContents(m_level_string);
				return;
			}
		}
		if(m_level_num < 10){
			m_stage_name.text += m_level_num.ToString();
			stars.transform.parent.gameObject.SetActive(true);
			char[] temp = m_level_string.ToCharArray();
			temp[m_level_string.Length-2] = '0';
			temp[m_level_string.Length-1] = m_level_num.ToString()[0];
			m_level_string = new string(temp);
		}
		if(m_level_num == 10){
			m_stage_name.text += m_level_num.ToString();
			stars.transform.parent.gameObject.SetActive(true);
			char[] temp = m_level_string.ToCharArray();
			temp[m_level_string.Length-1] = '0';
			temp[m_level_string.Length-2] = '1';
			m_level_string = new string(temp);
		}
		if(m_level_num == 11){
			m_stage_name.text = m_boss_name;
			stars.transform.parent.gameObject.SetActive(false);
			char[] temp = m_level_string.ToCharArray();
			temp[m_level_string.Length-1] = '1';
			temp[m_level_string.Length-2] = '1';
			m_level_string = new string(temp);
		}
		if(m_level_num < 12){
			m_is_locked = (m_level_num > m_unlocked_level) ? true : false;
			if(m_is_locked)
				m_char_select.LockLaunchButton();
			else
				m_char_select.UnlockLaunchButton();
		}
		SetPanelContents(m_level_string);
	}

	public void SetLevel(int num){
		m_level_num = num;
		SetLabels();
	}

}
