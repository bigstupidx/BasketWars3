using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class KiiBucketManager : MonoBehaviour {
/*	public KiiBucket m_user_bucket;
	public Uri m_story_uri;
	public Uri m_IAP_uri;
	public Uri m_LB_story_uri;
	public Uri m_LB_survival_uri;
	public Uri m_weapon_uri;
	KiiObject m_story_mode_obj;
	KiiObject m_IAP_obj;
	KiiObject m_LB_story_obj;
	KiiObject m_LB_Survival_obj;
	KiiObject m_weapon_obj;
	string m_story_mode_URI = "StoryModeURI";
	string m_IAP_URI = "IAPURI";
	string m_LB_story_URI = "LBstoryURI";
	string m_LB_survival_URI = "LBsurvivalURI";
	string m_weapon_URI = "WeaponsURI";
	bool is_admin = false;


	public void Init(){
		LoadUserObjects();
	}

	KiiBucket CreateUserBucket(string bucket_name){
		KiiBucket userBucket = KiiUser.CurrentUser.Bucket(bucket_name);
		return userBucket;
	}

	public void Logout(){
		GameManager.s_Inst.m_user_logged_in = false;
		m_user_bucket = null;
		m_story_uri = null;
		m_story_mode_obj = null;
		m_IAP_obj = null;
		m_LB_story_obj = null;
		m_LB_Survival_obj = null;
		PlayerPrefs.SetString(m_story_mode_URI,"null");
		PlayerPrefs.SetString(m_IAP_URI,"null");
		PlayerPrefs.SetString(m_LB_story_URI,"null");
		PlayerPrefs.SetString(m_LB_survival_URI,"null");
		GameManager.s_Inst.m_current_coins = 0;
		GameManager.s_Inst.m_bullets = 0;
		GameManager.s_Inst.m_current_nuke = 0;
		GameManager.s_Inst.m_current_shield = 0;
		GameManager.s_Inst.m_current_focus = 0;
		GameManager.s_Inst.m_current_guide = 0;
		StageUnlocker stage_unlocker = GameManager.s_Inst.gameObject.GetComponent<StageUnlocker>();
		stage_unlocker.m_Stalingrad_purchased = false;
		stage_unlocker.m_Stalingrad_purchased = false;
		stage_unlocker.m_Stalingrad_purchased = false;
		GameManager.s_Inst.m_unlimited_stamina = false;
		stage_unlocker.Init(0,0,0,0);
	}

	public void LoadedObjectsSucessfully(){
		if(GameManager.s_Inst != null){
			GameManager.s_Inst.m_user_logged_in = true;
			if(m_weapon_obj != null)
				GameManager.s_Inst.UpdateWeapons(m_weapon_obj);
			try{
				GameManager.s_Inst.m_characters_unlocked = (int)m_weapon_obj["Characters"];
			}catch(CloudException e){
				Debug.Log(e.Message);
				m_weapon_obj["Characters"] = 3;
				try{
					m_weapon_obj.Save();
				}catch(CloudException e2){
					Debug.Log(e2.Message);
				}
			}
			GameManager.s_Inst.m_britain_high_scores = m_story_mode_obj.GetString("BritainHighScores");
			GameManager.s_Inst.m_stalingrad_high_scores = m_story_mode_obj.GetString("StalingradHighScores");
			GameManager.s_Inst.m_kursk_high_scores = m_story_mode_obj.GetString("KurskHighScores");
			GameManager.s_Inst.m_normandy_high_scores = m_story_mode_obj.GetString("NormandyHighScores");
			GameManager.s_Inst.LoadRanksAndXP(m_story_mode_obj.GetString("CharacterRank"),m_story_mode_obj.GetString("CharacterXP"));
		}
		KiiObject k = m_story_mode_obj;
		LoadIAPData();
		gameObject.GetComponent<StageUnlocker>().Init((int)k["BritanUnlocked"],(int)k["StalingradUnlocked"],(int)k["KurskUnlocked"],(int)k["NormandyUnlocked"]);
		PowerupEquipper.Init();
		KiiManager.s_inst.m_logging_in_label.PlayReverse();
		GameObject.Find("CrateBGPanel").GetComponent<HandleTweens>().PlayReverse();
		//GameObject.Find("Shop Panel").GetComponent<ShopMenu>().UpdateLabels();
	}

	public void CreateUserBucket(){
		Debug.Log("Creating buckets");
		if(KiiUser.CurrentUser.Username.Contains("u."))
		   m_user_bucket = CreateUserBucket(FaceBookHandler.m_current_user.id + "_bucket");
		else{
			m_user_bucket = CreateUserBucket(KiiUser.CurrentUser.Username + "_bucket");
			Debug.Log(KiiUser.CurrentUser.Username + "_bucket");
		}
		CreateStoryModeObject();
		CreateIAPObject();
		CreateWeaponsObject();
		//m_IAP_bucket = CreateUserBucket(KiiUser.CurrentUser.Username + "_IAP_bucket");
		//m_leaderboards_story_bucket = CreateUserBucket(KiiUser.CurrentUser.Username + "_leaderboards_story_bucket");
		//m_leaderboards_survival_bucket = CreateUserBucket(KiiUser.CurrentUser.Username + "_leaderboards_survival_bucket");
	}

	public void LoadUserObjects(){
		if(PlayerPrefs.GetString(m_story_mode_URI,"null") != "null"){
			m_story_uri = new Uri(PlayerPrefs.GetString(m_story_mode_URI,"null"));
			try{
				m_story_mode_obj = KiiObject.CreateByUri(m_story_uri);
				m_story_mode_obj.Refresh();
				if(!m_story_mode_obj.Has("BritainHighScores")){
					m_story_mode_obj["BritainHighScores"] = "0,0,0,0,0,0,0,0,0,0,0";
					m_story_mode_obj["StalingradHighScores"] = "0,0,0,0,0,0,0,0,0,0,0";
					m_story_mode_obj["KurskHighScores"] = "0,0,0,0,0,0,0,0,0,0,0";
					m_story_mode_obj["NormandyHighScores"] = "0,0,0,0,0,0,0,0,0,0,0";
					m_story_mode_obj.Save();
				}
				if(!m_story_mode_obj.Has("CharacterRank")){
					Debug.Log("Setting Rank to 0 at load URI");
					m_story_mode_obj["CharacterRank"] = "0,0,0,0";
					m_story_mode_obj["CharacterXP"] = "0,0,0,0";
					m_story_mode_obj.Save();
				}
			}
			catch(CloudException e){
				Debug.LogError("Could Not Load Object: " + e.Message);
				GetUserBuckets();
				return;				
			}
		}
		else{
			Debug.Log("No Player Pref URI found");
			GetUserBuckets();
			return;
		}
		if(PlayerPrefs.GetString(m_IAP_URI,"null") != "null"){
			m_IAP_uri = new Uri(PlayerPrefs.GetString(m_IAP_URI,"null"));
			try{
				m_IAP_obj = KiiObject.CreateByUri(m_IAP_uri);
				m_IAP_obj.Refresh();
				if(!m_IAP_obj.Has("Unlocked Stalingrad")){
					m_IAP_obj["Unlocked Stalingrad"] = false;
					m_IAP_obj["Unlocked Kursk"] = false;
					m_IAP_obj["Unlocked Normandy"] = false;
					m_IAP_obj.Save();
				}
				if(!m_IAP_obj.Has("Unlimited Stamina")){
					m_IAP_obj["Unlimited Stamina"] = false;
					m_IAP_obj.Save();
				}
				m_IAP_obj.Save();				
			}
			catch(CloudException e){
				Debug.LogWarning("Could not load user objects: " +e.Message);
				GetUserBuckets();
				return;
			}
		}
		else{
			Debug.Log("No Player Pref URI found");
			GetUserBuckets();
			return;
		}
		if(PlayerPrefs.GetString(m_weapon_URI,"null") != "null"){
			m_weapon_uri = new Uri(PlayerPrefs.GetString(m_weapon_URI,"null"));
			try{
				m_weapon_obj = KiiObject.CreateByUri(m_weapon_uri);
				m_weapon_obj.Refresh();
			}
			catch(CloudException e){
				Debug.LogWarning("Could not load user objects: " +e.Message);
				GetUserBuckets();
				return;
			}
		}
		else{
			Debug.Log("No Player Pref URI found");
			GetUserBuckets();
			return;
		}
		LoadedObjectsSucessfully();
			
	}

	public void GetUserBuckets(){
		Debug.Log("Grabbing Bucket");
		try{
			if(KiiManager.s_inst.m_current_user != null)
				Debug.Log(KiiManager.s_inst.m_current_user.Displayname);
			else
				Debug.LogError("Kii user is NULL!!");
			if(FaceBookHandler.m_current_user != null){
				m_user_bucket = KiiUser.CurrentUser.Bucket(FaceBookHandler.m_current_user.id + "_bucket");
			}
			else{
			if(KiiUser.CurrentUser.Username == null){
			
				Debug.Log(KiiUser.CurrentUser.Displayname + "_bucket");
				m_user_bucket = KiiUser.CurrentUser.Bucket(KiiUser.CurrentUser.Displayname + "_bucket");
			}
			else
				m_user_bucket = KiiUser.CurrentUser.Bucket(KiiUser.CurrentUser.Username + "_bucket");
			Debug.Log("Got the Bucket!");
			}
		}
		catch(CloudException e){
			Debug.LogError("Could not get Bucket: "+ e.Message);
			CreateUserBucket();
		}
		KiiQuery allQuery = new KiiQuery();
		try{
			KiiQueryResult<KiiObject> result = m_user_bucket.Query(allQuery);
			// Alternatively, you can also do:
			// KiiQueryResult<KiiObject> result = Kii.bucket("myBuckets").query(null);
			if(result.Count == 0){
				Debug.Log("bucket is empty, Creating objects");
				CreateStoryModeObject();
				CreateIAPObject();
				CreateWeaponsObject();
			}
			else{
				foreach(KiiObject k in result){
					if((string)k["Name"] == "StoryMode"){
						m_story_mode_obj = k;
						if(!m_story_mode_obj.Has("BritainHighScores")){
						  	m_story_mode_obj["BritainHighScores"] = "0,0,0,0,0,0,0,0,0,0,0";
						 	m_story_mode_obj["StalingradHighScores"] = "0,0,0,0,0,0,0,0,0,0,0";
						  	m_story_mode_obj["KurskHighScores"] = "0,0,0,0,0,0,0,0,0,0,0";
						  	m_story_mode_obj["NormandyHighScores"] = "0,0,0,0,0,0,0,0,0,0,0";
							m_story_mode_obj.Save();
						}
						if(!m_story_mode_obj.Has("CharacterRank")){
							Debug.Log("Setting Rank to 0 at PARSE");
							m_story_mode_obj["CharacterRank"] = "0,0,0,0";
							m_story_mode_obj["CharacterXP"] = "0,0,0,0";
							m_story_mode_obj.Save();
						}
						PlayerPrefs.SetString(m_story_mode_URI,m_story_mode_obj.Uri.ToString());
					}
					else if((string)k["Name"] == "IAP"){
						m_IAP_obj = k;
						Debug.Log("Got IAP obj");
						PlayerPrefs.SetString(m_IAP_URI,m_IAP_obj.Uri.ToString());
						if(!m_IAP_obj.Has("Unlocked Stalingrad")){
							m_IAP_obj["Unlocked Stalingrad"] = false;
							m_IAP_obj["Unlocked Kursk"] = false;
							m_IAP_obj["Unlocked Normandy"] = false;
							m_IAP_obj.Save();
						}
						if(!m_IAP_obj.Has("Unlimited Stamina")){
							m_IAP_obj["Unlimited Stamina"] = false;
							m_IAP_obj.Save();
						}
					}
					else if((string)k["Name"] == "LBStory"){
						m_LB_story_obj = k;
						PlayerPrefs.SetString(m_LB_story_URI,m_LB_story_obj.Uri.ToString());
					}
					else if((string)k["Name"] == "LBSurvival"){
						m_LB_Survival_obj = k;
						PlayerPrefs.SetString(m_LB_survival_URI,m_LB_Survival_obj.Uri.ToString());
					}
					else if((string)k["Name"] == "Weapons"){
						m_weapon_obj = k;
						try{
							Debug.Log(k["Characters"]);
						}catch(CloudException e){
							Debug.Log(e.Message);
							k["Characters"] = 3;
							try{
								k.Save();
							}catch(CloudException e2){
								Debug.Log(e2.Message);
							}
						}
						PlayerPrefs.SetString(m_weapon_URI,m_weapon_obj.Uri.ToString());
					}
					else if((string)k["Name"] == "Achievements"){
						DeleteObject(k);
					}
					else
						Debug.LogError(k["Name"] + " Is not a recognized object");
				}
				if(m_story_mode_obj == null){
					CreateStoryModeObject();
				}
				if(m_IAP_obj == null){
					Debug.Log("Creating IAP obj");
					CreateIAPObject();
				}
				if(m_weapon_obj == null){
					CreateWeaponsObject();
				}
				LoadedObjectsSucessfully();
			}
		}
		catch (CloudException e){
			Debug.Log("Could get Query results: " + e.Message);
		}
	}

	void CreateStoryModeObject(){
		Debug.Log("Created story object");
		KiiObject obj = m_user_bucket.NewKiiObject();
		obj["Name"] = "StoryMode";
		if(!is_admin){
			obj["BritanUnlocked"] = 0x00000000;
			obj["StalingradUnlocked"] = 0x00000000;
			obj["KurskUnlocked"] = 0x00000000;
			obj["NormandyUnlocked"] = 0x00000000;
			obj["BritainHighScores"] = "0,0,0,0,0,0,0,0,0,0,0";
			obj["StalingradHighScores"] = "0,0,0,0,0,0,0,0,0,0,0";
			obj["KurskHighScores"] = "0,0,0,0,0,0,0,0,0,0,0";
			obj["NormandyHighScores"] = "0,0,0,0,0,0,0,0,0,0,0";
			Debug.Log("Setting Rank to 0 at create");
			obj["CharacterRank"] = "0,0,0,0";
			obj["CharacterXP"] = "0,0,0,0";
		}
		else if(is_admin){
			obj["BritanUnlocked"] = 0x003FFFFF;
			obj["StalingradUnlocked"] = 0x003FFFFF;
			obj["KurskUnlocked"] = 0x003FFFFF;
			obj["NormandyUnlocked"] = 0x003FFFFF;
		}
		obj["Rank"] = "private";
		obj["TimePlayed"] = 0;
		obj["Achievements"]  = 0;
		obj["Bosses"] = 0;
		try{
			obj.Save();
			Debug.Log(obj.Uri.ToString());
			PlayerPrefs.SetString(m_story_mode_URI,obj.Uri.ToString());
			LoadUserObjects();
		}
		catch(CloudException e){
			Debug.LogError("Could not save bucket to cloud: " + e.Message);
		}
	}

	public void SaveLevelInfo(int britain, int stalingrad, int kursk, int normandy){
		m_story_mode_obj["BritanUnlocked"] = britain;
		m_story_mode_obj["StalingradUnlocked"] = stalingrad;
		m_story_mode_obj["KurskUnlocked"] = kursk;
		m_story_mode_obj["NormandyUnlocked"] = normandy;
		m_story_mode_obj["BritainHighScores"] = GameManager.s_Inst.m_britain_high_scores;
		m_story_mode_obj["StalingradHighScores"] = GameManager.s_Inst.m_stalingrad_high_scores;
		m_story_mode_obj["KurskHighScores"] = GameManager.s_Inst.m_kursk_high_scores;
		m_story_mode_obj["NormandyHighScores"] = GameManager.s_Inst.m_normandy_high_scores;
		string ranks = string.Join(",", new List<int>(GameManager.s_Inst.m_current_rank).ConvertAll(i => i.ToString()).ToArray());
		m_story_mode_obj["CharacterRank"] = ranks;
		string xps = string.Join(",", new List<float>(GameManager.s_Inst.m_character_xp).ConvertAll(i => i.ToString()).ToArray());
		m_story_mode_obj["CharacterXP"] = xps;
		try
		{
			m_story_mode_obj.Save();
		}
		catch(CloudException e)
		{
			Debug.Log("Can not save: " + e.Message);
		}
	}

	void CreateIAPObject(){
		KiiObject obj = m_user_bucket.NewKiiObject();
		obj["Name"] = "IAP";
		obj["Coins"] = 0;
		obj["Ammo"] = 10;
		obj["Nuke"] = 1;
		obj["Jeep"] = 1;
		obj["Armor"] = 1;
		obj["Lives"] = 1;
		obj["SlowMo"] = 1;
		obj["Fullarc"] = 1;
		obj["Unlocked Stalingrad"] = false;
		obj["Unlocked Kursk"] = false;
		obj["Unlocked Normandy"] = false;
		obj["Unlimited Stamina"] = false;
		GameManager.s_Inst.m_current_coins = (int)obj["Coins"];
		GameManager.s_Inst.m_bullets = (int)obj["Ammo"];
		GameManager.s_Inst.m_current_nuke = (int)obj["Nuke"];
		GameManager.s_Inst.m_current_shield = (int)obj["Armor"];
		GameManager.s_Inst.m_current_focus = (int)obj["SlowMo"];
		GameManager.s_Inst.m_current_focus = (int)obj["Fullarc"];
		StageUnlocker stage_unlocker = GameManager.s_Inst.gameObject.GetComponent<StageUnlocker>();
		stage_unlocker.m_Stalingrad_purchased = obj.GetBoolean("Unlocked Stalingrad",false);
		stage_unlocker.m_Stalingrad_purchased = obj.GetBoolean("Unlocked Kursk",false);
		stage_unlocker.m_Stalingrad_purchased = obj.GetBoolean("Unlocked Normandy",false);
		GameManager.s_Inst.m_unlimited_stamina = obj.GetBoolean("Unlimited Stamina",false);
		try{
			obj.Save();
			PlayerPrefs.SetString(m_IAP_URI,obj.Uri.ToString());
		}
		catch(CloudException e){
			Debug.LogError("Could not save bucket to cloud: " + e.Message);
		}
	}

	void LoadIAPData(){
		if(m_IAP_obj != null){
			GameManager.s_Inst.m_current_coins = (int)m_IAP_obj["Coins"];
			GameManager.s_Inst.m_bullets = (int)m_IAP_obj["Ammo"];
			GameManager.s_Inst.m_current_nuke = (int)m_IAP_obj["Nuke"];
			if(GameManager.s_Inst.m_current_nuke < 0)
				GameManager.s_Inst.m_current_nuke = 0;
			GameManager.s_Inst.m_current_shield = (int)m_IAP_obj["Armor"];
			if(GameManager.s_Inst.m_current_shield < 0)
				GameManager.s_Inst.m_current_shield = 0;
			GameManager.s_Inst.m_current_focus = (int)m_IAP_obj["SlowMo"];
			if(GameManager.s_Inst.m_current_focus < 0)
				GameManager.s_Inst.m_current_focus = 0;
			GameManager.s_Inst.m_current_guide = (int)m_IAP_obj["Fullarc"];
			if(GameManager.s_Inst.m_current_guide < 0)
				GameManager.s_Inst.m_current_guide = 0;
			StageUnlocker stage_unlocker = GameManager.s_Inst.gameObject.GetComponent<StageUnlocker>();
			FixLevelUnlockFormat();
			stage_unlocker.m_Stalingrad_purchased = m_IAP_obj.GetBoolean("Unlocked Stalingrad");
			stage_unlocker.m_Kursk_purchased = m_IAP_obj.GetBoolean("Unlocked Kursk");
			stage_unlocker.m_Normandy_purchased = m_IAP_obj.GetBoolean("Unlocked Normandy");
			GameManager.s_Inst.m_unlimited_stamina = m_IAP_obj.GetBoolean("Unlimited Stamina");			
			GameManager.s_Inst.UpdateLabels();
		}
	}
	
	void FixLevelUnlockFormat(){
		//Fix stalingrad
		if(m_IAP_obj["Unlocked Stalingrad"].ToString().Equals("1"))
			m_IAP_obj["Unlocked Stalingrad"] = true;
		else if(m_IAP_obj["Unlocked Stalingrad"].ToString().Equals("0"))
			m_IAP_obj["Unlocked Stalingrad"] = false;
		//fix Kursk
		if(m_IAP_obj["Unlocked Kursk"].ToString().Equals("1"))
			m_IAP_obj["Unlocked Kursk"] = true;
		else if(m_IAP_obj["Unlocked Kursk"].ToString().Equals("0"))
			m_IAP_obj["Unlocked Kursk"] = false;
		//Fix Normandy
		if(m_IAP_obj["Unlocked Normandy"].ToString().Equals("1"))
			m_IAP_obj["Unlocked Normandy"] = true;
		else if(m_IAP_obj["Unlocked Normandy"].ToString().Equals("0"))
			m_IAP_obj["Unlocked Normandy"] = false;
	}

	public void SaveIAPBullets(int ammo){
		m_IAP_obj["Ammo"] = ammo;
		try{
			m_IAP_obj.Save();
		}
		catch(CloudException e){
			Debug.LogError("Could not save bucket to cloud: " + e.Message);
		}
	}

	/// <summary>
	/// Saves the IAP object.
	/// </summary>
	/// <param name="items">Items. Coins, Ammo, Nuke, Jeep, Armor, Lives, Slowmo, FullArc</param>

	public void SaveIAPObject(int[] items){
		if(m_IAP_obj != null){
			m_IAP_obj["Coins"] = items[0];
			m_IAP_obj["Ammo"] = items[1];
			m_IAP_obj["Nuke"] = items[2];
			m_IAP_obj["Armor"] = items[3];
			m_IAP_obj["SlowMo"] = items[4];
			m_IAP_obj["Fullarc"] = items[5];
			StageUnlocker stage_unlocker = GameManager.s_Inst.gameObject.GetComponent<StageUnlocker>();
			m_IAP_obj["Unlocked Stalingrad"] = stage_unlocker.m_Stalingrad_purchased ? true : false;
			m_IAP_obj["Unlocked Kursk"] = stage_unlocker.m_Kursk_purchased ? true : false;
			m_IAP_obj["Unlocked Normandy"] = stage_unlocker.m_Normandy_purchased ? true : false;
			m_IAP_obj["Unlimited Stamina"] = GameManager.s_Inst.m_unlimited_stamina ? true : false;
			try{
				m_IAP_obj.Save();
			}
			catch(CloudException e){
				Debug.LogError("Could not save bucket to cloud: " + e.Message);
			}
		}
	}

	void CreateLBStoryObject(){
		KiiObject obj = m_user_bucket.NewKiiObject();
		obj["Name"] = "LBStory";

		try{
			obj.Save();
			PlayerPrefs.SetString(m_LB_story_URI,obj.Uri.ToString());
		}
		catch(CloudException e){
			Debug.LogError("Could not save bucket to cloud: " + e.Message);
		}
	}

	void CreateLBSurvivalObject(){
		KiiObject obj = m_user_bucket.NewKiiObject();
		obj["Name"] = "LBSurvival";

		try{
			obj.Save();
			PlayerPrefs.SetString(m_LB_survival_URI,obj.Uri.ToString());
		}
		catch(CloudException e){
			Debug.LogError("Could not save bucket to cloud: " + e.Message);
		}
	}

	public void DeleteObject(KiiObject kiiobj){
		try{
			kiiobj.Delete();
			Debug.Log("Completed Deletion");
		}
		catch(CloudException e){
		Debug.LogError("Could not delete Object: " +e.Message);
		}
	}

	void CreateWeaponsObject(){
		Debug.Log("Creating Weapons object");
		KiiObject obj = m_user_bucket.NewKiiObject();
		obj["Name"] = "Weapons";
		obj["Pistol_owned"] = true;
		obj["Grenade_owned"] = false;
		obj["Machine_gun_owned"] = false;
		obj["Rifle_owned"] = false;
		obj["Equipped_Weapon"] = 0;
		obj["Characters"] = 3;
		try{
			obj.Save();
			PlayerPrefs.SetString(m_weapon_URI,obj.Uri.ToString());
			LoadUserObjects();
		}catch(CloudException e){
			Debug.LogError("Could not save bucket to cloud: " + e.Message);
		}
	}

	public void SaveWeaponObject(){
		if(m_weapon_obj != null){
			m_weapon_obj["Pistol_owned"] = GameManager.s_Inst.pistol_owned;
			m_weapon_obj["Grenade_owned"] = GameManager.s_Inst.grenade_owned;
			m_weapon_obj["Machine_gun_owned"] = GameManager.s_Inst.machine_gun_owned;
			m_weapon_obj["Rifle_owned"] = GameManager.s_Inst.rifle_owned;
			m_weapon_obj["Equipped_Weapon"] = GameManager.s_Inst.m_current_weapon;
			m_weapon_obj["Characters"] = GameManager.s_Inst.m_characters_unlocked;
			try{
				m_weapon_obj.Save();
			}
			catch(CloudException e){
				Debug.Log("Could not save bucket to cloud: " + e.Message);
			}
		}
	}*/
}

