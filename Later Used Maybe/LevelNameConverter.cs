using UnityEngine;
using System.Collections;
using System.Text.RegularExpressions;
using System;

public class LevelNameConverter : MonoBehaviour {
	public static string GetLevelName(){
		string levelName = Application.loadedLevelName;
		int stageLevel = Convert.ToInt32(Regex.Match(levelName, @"\d+").Value);
		string return_name = "";
		if(levelName.Contains("Britain")){
			return_name = "Britain ";
		}
		if(levelName.Contains("Stalingrad")){
			return_name = "Stalingrad ";
		}
		if(levelName.Contains("Kursk")){
			return_name = "Kursk ";
		}
		if(levelName.Contains("Normandy")){
			return_name = "Normandy ";
		}
		if(stageLevel <= 10)
	  		return_name += stageLevel;
		if(stageLevel == 11)
	  		return_name += "Boss";
		if(stageLevel == 12)
	  		return_name += "Bonus";
		return return_name;
	}
}
