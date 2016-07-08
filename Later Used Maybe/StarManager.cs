using UnityEngine;
using System.Collections;
using System.Text.RegularExpressions;
using System;

#pragma warning disable 0414
public class StarManager : MonoBehaviour 
{
	int m_max_stars = 200;
	int m_curr_stars;
	public bool m_target_hit = false;
	public bool m_first_shot = true;
	//public string starnumber;
	public UILabel m_star_label;
	StageUnlocker m_stage_unlocker;
	
	/// <summary>
	/// Sets the total stars on the main menu.
	/// </summary>
	void Start()
	{
		m_stage_unlocker = gameObject.GetComponent<StageUnlocker>();
		//starnumber = GetTotalStars().ToString() + "/200";
		//m_star_label.text = starnumber;
	}

	/// <summary>
	/// Reset the bools used to determine stars.
	/// </summary>
	public void ResetBools()
	{
		m_target_hit = false;
		m_first_shot = true;
	}
	
	/// <summary>
	/// Called from the traget manager if all targets are hit.
	/// </summary>
	public void TargetHit()
	{
		m_target_hit = true;
	}
	
	/// <summary>
	/// Called when the first shot failed.
	/// </summary>
	public void FirstShotFailed()
	{
		m_first_shot = false;
	}
	
	/*public void SetStarText(UILabel label)
	{
		label.text = starnumber;
	}*/
	
	/// <summary>
	/// Called when the stage is complete and the number of stars earned is required.
	/// </summary>
	/// <returns>
	/// Number of stars earned.
	/// </returns>
	public int CalcStars()
	{
		int num_stars = 0;
		if(GameManager.m_baskets_made >= GameManager.s_Inst.m_3_star_score){
			num_stars = 3;
			return num_stars;
		}
		if(GameManager.m_baskets_made>= GameManager.s_Inst.m_2_star_score){
			num_stars = 2;
			return num_stars;
		}
		if(GameManager.m_baskets_made >= GameManager.s_Inst.m_1_star_score){
			num_stars = 1;
			return num_stars;
		}
		return num_stars;
	}
	
	/// <summary>
	/// Pushes the star info to the playerprefs.
	/// </summary>
	/// <param name='m_level'>
	/// Which level the info is for.
	/// </param>
	/// <param name='num_stars'>
	/// How many stars were earned this level.
	/// </param>
	public void SetStars(string m_level, int num_stars)
	{
		int num = Convert.ToInt32(Regex.Match(m_level, @"\d+").Value);
		if(m_level.Contains("Britain"))
			m_stage_unlocker.m_britain_star_count[num-1] = num_stars;
		else if(m_level.Contains("Stalingrad"))
			m_stage_unlocker.m_stalingrad_star_count[num-1] = num_stars;
		else if(m_level.Contains("Kursk"))
			m_stage_unlocker.m_kursk_star_count[num-1] = num_stars;
		else if(m_level.Contains("Normandy"))
			m_stage_unlocker.m_normandy_star_count[num-1] = num_stars;
		m_stage_unlocker.UpdateAllLevels();
		ResetBools();
	}
	
	/// <summary>
	/// Gets the number of stars earned for this level.
	/// </summary>
	/// <returns>
	/// Number of stars earned this level.
	/// </returns>
	/// <param name='m_level'>
	/// Level Name.
	/// </param>
	public int GetStars(string m_level)
	{		
		if(m_stage_unlocker == null)
			m_stage_unlocker = gameObject.GetComponent<StageUnlocker>();
		
		if(m_stage_unlocker.m_britain_star_count.Length > 0 &&
		   m_stage_unlocker.m_stalingrad_star_count.Length > 0 && 
		   m_stage_unlocker.m_kursk_star_count.Length > 0 &&
		   m_stage_unlocker.m_normandy_star_count.Length > 0){
			int num = Convert.ToInt32(Regex.Match(m_level, @"\d+").Value);
			if(num > 11)
				return 3;
			if(m_level.Contains("Britain"))
			   return m_stage_unlocker.m_britain_star_count[num-1];
			else if(m_level.Contains("Stalingrad"))
			   return m_stage_unlocker.m_stalingrad_star_count[num-1];
			else if(m_level.Contains("Kursk"))
			   return m_stage_unlocker.m_kursk_star_count[num-1];
			else if(m_level.Contains("Normandy"))
			   return m_stage_unlocker.m_normandy_star_count[num-1];
		}
		return -1;
	}
	
	/// <summary>
	/// Gets the total stars earned from the entire game.
	/// </summary>
	/// <returns>
	/// The total stars earned.
	/// </returns>
	public int GetTotalStars()
	{
		int stars = 0;
		string Britain = "Britain";
		string Stalingrad = "Stalingrad";
		string Kursk = "Kursk";
		string Normandy = "Normandy";
		string Midway = "Midway";
		string Bulge = "Bulge";
		
		for(int i = 1; i < 12; i++)
		{
			string level;
			if(i < 10)
				level = "0"+i.ToString()+"Stars";
			else if( i == 10)
				level = i.ToString()+"Stars";
			else if(i == 11)
				level = "BossStars";
			else
			{
				Debug.LogError("How did you get here?");
				return -1;
			}
			stars += PlayerPrefs.GetInt(Britain+level);
			stars += PlayerPrefs.GetInt(Stalingrad+level);
			stars += PlayerPrefs.GetInt(Kursk+level);
			stars += PlayerPrefs.GetInt(Normandy+level);
			stars += PlayerPrefs.GetInt(Midway+level);
			stars += PlayerPrefs.GetInt(Bulge+level);
		}
		return stars;
	}
	
	/// <summary>
	/// Resets the star progress to 0 stars.
	/// </summary>
	public void ResetStarProgress()
	{
		string Britain = "Britain";
		string Stalingrad = "Stalingrad";
		string Kursk = "Kursk";
		string Normandy = "Normandy";
		string Midway = "Midway";
		string Bulge = "Bulge";
		
		for(int i = 1; i < 12; i++)
		{
			string level;
			if(i < 10)
				level = "0"+i.ToString()+"Stars";
			else if( i == 10)
				level = i.ToString()+"Stars";
			else if(i == 11)
				level = "BossStars";
			else
			{
				Debug.LogError("How did you get here?");
				return;
			}
			PlayerPrefs.SetInt(Britain+level,0);
			PlayerPrefs.SetInt(Stalingrad+level,0);
			PlayerPrefs.SetInt(Kursk+level,0);
			PlayerPrefs.SetInt(Normandy+level,0);
			PlayerPrefs.SetInt(Midway+level,0);
			PlayerPrefs.SetInt(Bulge+level,0);
		}
	}
}
