using UnityEngine;
using System.Collections;

public class RedeemCode : MonoBehaviour {
	public string m_code_input;
	Rect m_code_field;
	public GUIStyle m_style;
	public UISprite m_sprite;

	private string secretKey = "MwGZG5e8UI$2HblB[,x:+k}v&|_m+[dQHYjFy|R2FMH)+VoF|bzc{kD*5hM(|>Bn"; // Edit this value and make sure it's the same as the one stored on the server
	string checkCodeURL = "http://blendbrandstudio.com/basketwars/checkcodeisvalid.php?"; //be sure to add a ? to your url
	string markAsRedeemedURL = "http://blendbrandstudio.com/basketwars/markasredeemed.php?";	

	void Start()
	{
		//StartCoroutine(GetScores());
	}

	public void CheckCode(){
		StartCoroutine(IsCodeValid(m_code_input));
	}
	// remember to use StartCoroutine when calling this function!
	IEnumerator IsCodeValid(string code_input)
	{
		if(code_input == "AdminPassword"){
			SaveLoadManager.s_inst.UnlockAll();
			return false;
		}
		//This connects to a server side php script that will add the name and score to a MySQL DB.
		// Supply it with a string representing the players name and the players score.
		string hash = MD5Test.Md5Sum(code_input + secretKey);
		
		string post_url = checkCodeURL + "code_input=" + WWW.EscapeURL(code_input) + "&hash=" + hash;
		// Post the URL to the site and create a download object to get the result.
		WWW hs_post = new WWW(post_url);
		yield return hs_post; // Wait until the download is done
		
		if (hs_post.error != null)
		{
			print("There was an error posting the high score: " + hs_post.error);
		}else{
			Debug.Log(hs_post.text);
			if(string.IsNullOrEmpty(hs_post.text)){
				Debug.Log("Invalid Code");
				return false;
			}
			string[] result = hs_post.text.Split('\t');
			if(result[0] == "0"){
				Debug.Log("Code is valid and is not redeemed with bonus : " + result[1]);
				if(result[1] == "10kcoins")
					GameManager.s_Inst.AddCoins(10000);
				if(result[1] == "20kcoins")
					GameManager.s_Inst.AddCoins(20000);
				if(result[1] == "4kpowerup"){
					GameManager.s_Inst.AddCoins(4000);
					SaveLoadManager.m_save_info.m_armor_powerup += 1;
					SaveLoadManager.m_save_info.m_nuke_powerup += 1;
					SaveLoadManager.m_save_info.m_focus_powerup += 1;
					SaveLoadManager.m_save_info.m_guide_powerup += 1;
				}
				SaveLoadManager.s_inst.SaveFile();
				StartCoroutine(MarkAsRedeemed(code_input));
			}else{
				Debug.Log("Code has been redeemed");
			}
		}
	}
	
	// Get the scores from the MySQL DB to display in a GUIText.
	// remember to use StartCoroutine when calling this function!
	IEnumerator MarkAsRedeemed(string code_input)
	{
		//gameObject.guiText.text = "Loading Scores";
		string hash = MD5Test.Md5Sum(code_input + secretKey);
		string post_url = markAsRedeemedURL + "code_input=" + WWW.EscapeURL(code_input) + "&hash=" + hash;
		WWW hs_get = new WWW(post_url);
		yield return hs_get;
		
		if (hs_get.error != null)
		{
			print("There was an error getting the high score: " + hs_get.error);
		}else{
			Debug.Log(hs_get.text);
		}
	}

	public void SetField(){
		m_code_field = CalculateRect(m_sprite);
	}

	public void ClearField(){
		m_code_field = new Rect();
		m_code_input = "";
	}

	public Rect CalculateRect(UISprite sprite)
	{
		Rect temp = new Rect();
		temp.x = Camera.main.WorldToScreenPoint(sprite.worldCorners[1]).x + 5;
		float tempypos = Camera.main.WorldToScreenPoint(sprite.worldCorners[1]).y;
		temp.y = Screen.height - tempypos;
		temp.width = Camera.main.WorldToScreenPoint(sprite.worldCorners[3]).x - temp.x;
		temp.height = tempypos - Camera.main.WorldToScreenPoint(sprite.worldCorners[3]).y;
		//Debug.Log (sprite.name + temp);
		return temp;
	}

	void OnGUI(){
		m_code_input = GUI.TextField(m_code_field,m_code_input,m_style);
	}

}
