using UnityEngine;
using System.Collections;

public class FaceBookHandler : MonoBehaviour 
{
	static bool m_facebook_init = false;
	public IDictionary m_friends;
	public static FacebookMeResult m_current_user = null;
	// Use this for initialization
#if UNITY_IOS
	public void Init () 
	{
		if(!m_facebook_init)
		{
			FacebookBinding.init();
			m_facebook_init = true;
		}
		FacebookBinding.login();
		if(FacebookBinding.isSessionValid()){
			//Facebook.instance.getFriends(OnGetFriendsComplete);
			//Debug.Log("FB: Valid session");
			GetMe();
		}
		else
		{
			//Debug.Log("FB: invalid session waiting for login");
			InvokeRepeating("GetMe",0.5f,0.5f);
		}
	}

	public static void Logout()
	{
		FacebookBinding.logout();
		m_current_user = null;
	}

	public static string GetToken()
	{
		return FacebookBinding.getAccessToken();
	}

	public void GetMe()
	{
		if(FacebookBinding.isSessionValid())
		{
			//Debug.Log("FB: Session is now valid Getting ME");
			Facebook.instance.getMe(OnGetMeComplete);
			CancelInvoke("GetMe");
		}
	}

	void OnGetFriendsComplete(string error,object result)
	{
		if(error != null)		
			Debug.Log(string.Format("Error = {0}",error));	
		if(result == null){
			Debug.Log("No result");
		return;
		}
		else		
		{		
			//Prime31.Utils.logObject(result);	
			FacebookFriendsResult fri = (FacebookFriendsResult) result;
			foreach(FacebookFriend f in fri.data)
			{
				m_friends.Add(f.id,f.name);
			}
		}	
	}

	public void OnGetMeComplete(string error, object result)
	{
		if(error != null)		
			Debug.Log(string.Format("Error= {0}",error));	
		if(result == null){
			Debug.Log("No result");
			GetMe();
			return;
		}
		Debug.Log("Got Me");
		m_current_user = (FacebookMeResult) result;
		//if(m_current_user != null && KiiManager.s_inst.m_current_user == null){
			//Debug.Log ("Logged into facebook through facebook init()");
			//KiiManager.s_inst.LoginFacebook();
	//	}
		//if(KiiManager.m_waiting_for_facebook)
			//KiiManager.FacebookDoneInit();
	}
	// Update is called once per frame
	void Update () 
	{
	
	}
#endif
}
