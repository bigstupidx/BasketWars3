using UnityEngine;
using System;
using System.Collections;
#pragma warning disable 0618

public class KiiManager : MonoBehaviour 
{
	/*public string m_user_id;
	public string m_user_email;
	public string m_user_password;
	public string m_user_confirm_password;
	public string m_new_password;
	public static KiiManager s_inst;
	public KiiUser m_current_user;
	bool m_logged_in_with_facebook = false;

	//Username Panel
	public TweenPosition m_login_panel_tween;
	public UISprite m_username_sprite;
	public UILabel m_username_Label;
	public UISprite m_password_sprite;
	public UILabel m_password_Label;
	public UILabel m_incorrect_pass_username;

	//Email register Panel
	public TweenPosition m_r_register_panel_tween;
	public UISprite m_r_username_sprite;
	public UILabel m_r_username_Label;
	public UISprite m_r_email_sprite;
	public UILabel m_r_email_Label;
	public UISprite m_r_password_sprite;
	public UILabel m_r_password_Label;
	public UISprite m_r_confirm_password_sprite;
	public UILabel m_r_confirm_password_Label;

	//Change Password Panel
	public TweenPosition m_change_panel_tween;
	public UISprite m_c_confirm_password_sprite;
	public UILabel m_c_confirm_password_Label;
	public UISprite m_new_password_sprite;
	public UILabel m_new_password_label;
	public UISprite m_old_password_sprite;
	public UILabel m_old_password_label;

	//Reset Password Panel
	public TweenPosition m_reset_password_tween;
	public UISprite m_reset_password_sprite;
	public UILabel m_reset_password_label;
	
	public GameObject m_no_internet;

	public GUIStyle m_my_style;
	Rect m_username_rect;
	Rect m_password_rect;
	Rect m_confirm_password_rect;
	Rect m_email_rect;
	Rect m_new_password_rect;

	public UIPanel m_login_panel;
	public TweenPosition m_logging_in_label;
	public UIPanel m_register_panel;
	public UIPanel m_username_panel;

	public bool m_login_panel_is_down;
	bool m_register_panel_is_down;
	bool m_username_panel_is_down;

	static public bool m_waiting_for_facebook = false;

	public UILabel m_user_label;
	public UILabel m_user_label_2;
	public UILabel m_user_label_3;
	public GameObject m_user_label_go;
	bool m_logged_in_as_guest = false;
	public KiiPushPlugin m_kii_push_plugin;

	public enum RegisterGUIState{
		NONE,
		REGISTER,
		SIGNIN,
		CHANGEPASSWORD,
		RESETPASSWORD
	}
	public RegisterGUIState m_gui_state = RegisterGUIState.NONE;
	void Awake()
	{
#if UNITY_IOS
		NotificationServices.RegisterForRemoteNotificationTypes(RemoteNotificationType.Alert);
		NotificationServices.RegisterForRemoteNotificationTypes(RemoteNotificationType.Badge);
		NotificationServices.RegisterForRemoteNotificationTypes(RemoteNotificationType.Sound);
#endif
		if(GameManager.s_Inst.m_current_game_state == GameManager.GameState.Tutorial)
			return;
		if(GameManager.s_Inst != null){
			if(GameManager.s_Inst.gameObject != this.gameObject){
				return;
			}
		}
		if(s_inst == null)
			s_inst = this;
		Kii.Initialize("cf1f5e51","678a46c578791da413ae2aa129de329d", Kii.Site.US);
		//PlayerPrefs.SetString("AccessToken","null");
		if(PlayerPrefs.GetInt("IsLoggedInAsGuest",-1) == 1){
			m_logged_in_as_guest = true;
		}
		CheckConnection();
		//Make sure to reset the user label stuff.
		m_user_label_go.GetComponent<TweenAlpha>().PlayReverse();
		m_user_label_go.transform.FindChild("Username").GetComponent<TweenAlpha>().PlayReverse();
		//Logout();

	}

	public void Logout()
	{
		KiiUser.LogOut();
		m_current_user = null;
#if UNITY_IOS
		FaceBookHandler.Logout();
#endif
		gameObject.GetComponent<KiiBucketManager>().Logout();
		PlayerPrefs.SetString("AccessToken","null");
		PlayerPrefs.SetInt("DidFacebookLogin",0);
		PlayerPrefs.SetInt("IsLoggedInAsGuest",0);
		//m_user_label.gameObject.SetActive(true);
	//	m_user_label_go.SetActive(false);
		//m_user_label_2.text = "";
		GameManager.s_Inst.m_marked_for_destroy = true;
		GameManager.s_Inst.m_level_name_to_load = "MainMenu";
		Application.LoadLevelAsync("LevelLoader");
	}

	void  CheckConnection()
	{	
		if(iPhoneSettings.internetReachability == iPhoneNetworkReachability.ReachableViaWiFiNetwork || iPhoneSettings.internetReachability == iPhoneNetworkReachability.ReachableViaCarrierDataNetwork)
		{
			Init();
		}
		else{
			m_no_internet.SetActive(true);
			GameObject.Find("Start").GetComponent<UIButton>().isEnabled = false;
		}
	}

	void Init () 
	{
		m_logged_in_with_facebook = (PlayerPrefs.GetInt("DidFacebookLogin",0) == 1) ? true : false ;
		//Debug.Log("Access Token " + PlayerPrefs.GetString("AccessToken"));
		m_my_style.fontSize = (int)(Screen.height * 0.03906250f);
		if(PlayerPrefs.GetInt("IsLoggedInAsGuest",-1) == 1){
			m_logged_in_as_guest = true;
		}
		if(KiiUser.CurrentUser == null && !m_logged_in_with_facebook){
			LoginWithToken();
		}else if(m_logged_in_with_facebook){
			FacebookLogin();
		}else{
			SetLoginName();
			gameObject.GetComponent<KiiBucketManager>().Init();
		}
	}

	void SucessfullyLoggedIn()
	{
		if(m_logged_in_with_facebook){
			PlayerPrefs.SetString("AccessToken",FaceBookHandler.GetToken());
		}else{
			PlayerPrefs.SetString("AccessToken",KiiUser.AccessToken);
		}
		SetLoginName();
		//Debug.Log("Access Token " + PlayerPrefs.GetString("AccessToken"));
		GameObject.Find("CrateBGPanel").GetComponent<HandleTweens>().PlayReverse();
		gameObject.GetComponent<KiiBucketManager>().Init();
		if(KiiUser.CurrentUser.Username == null){
			if(FaceBookHandler.m_current_user != null)
			{
				FacebookDoneInit();
				return;
			}
			m_waiting_for_facebook = true;
			return;
		}
		if(m_current_user == null)
		{
			Debug.Log(KiiUser.CurrentUser);
			Debug.LogError("No User Logged in!!");
			return;
		}
	}

	public void ResetPassword(){
		if(m_user_email != null && m_user_email != string.Empty){
			try{
				KiiUser.ResetPassword(m_user_email);
			}catch(CloudException e){
				Debug.LogError("Could not reset password: " + e.Message);
			}
		}
	}

	void OnLevelWasLoaded(){
		if(GameManager.s_Inst != null){
			if(GameManager.s_Inst.gameObject != this.gameObject){
				return;
			}
		}
		if(KiiUser.CurrentUser == null && FaceBookHandler.m_current_user == null){
			return;
		}
		if(Application.loadedLevelName == "MainMenu"){
			if(PlayerPrefs.GetInt("IsLoggedInAsGuest",-1) == 1){
				m_logged_in_as_guest = true;
			}
			if(m_user_label == null){
				if(GameObject.Find("Logged In Label"))
					m_user_label = GameObject.Find("Logged In Label").gameObject.GetComponent<UILabel>();
				else
					Debug.LogError("Couldn't find User Label");
			}
			if(m_user_label_2 == null){
				if(GameObject.Find("Username_2"))
					m_user_label_2 = GameObject.Find("Username_2").gameObject.GetComponent<UILabel>();
				else
					Debug.LogError("Couldn't find username 2 label");
					m_user_label_3 = GameObject.Find("Username_3").gameObject.GetComponent<UILabel>();				
			}
			if(m_user_label_go == null){
				m_user_label_go = GameObject.Find("User Label go");
				if(m_user_label_go == null)
					Debug.LogError("Couldn't find user label go");
			}
			SetLoginName();
		}
	}

	void SetLoginName(){
		if(FaceBookHandler.m_current_user != null){
			m_user_label.gameObject.GetComponent<TweenAlpha>().PlayForward();
			m_user_label_go.GetComponent<TweenAlpha>().PlayForward();
			m_user_label_go.transform.FindChild("Username").GetComponent<TweenAlpha>().PlayForward();
			m_user_label_go.transform.FindChild("Username").GetComponent<UILabel>().text = FaceBookHandler.m_current_user.first_name + " " + FaceBookHandler.m_current_user.last_name ;
			m_user_label_2.text = FaceBookHandler.m_current_user.first_name + " " + FaceBookHandler.m_current_user.last_name;
			m_user_label_3.text = FaceBookHandler.m_current_user.first_name + " " + FaceBookHandler.m_current_user.last_name;
			// m_user_label.text = "Logged in as: " + FaceBookHandler.m_current_user.first_name + " " + FaceBookHandler.m_current_user.last_name;
			TrialpayIntegration.Init(FaceBookHandler.m_current_user.id);
			return;
		}else if(!m_logged_in_as_guest){
			m_user_label.GetComponent<TweenAlpha>().PlayForward();
			m_user_label_go.GetComponent<TweenAlpha>().PlayForward();
			m_user_label_go.transform.FindChild("Username").GetComponent<TweenAlpha>().PlayForward();
			m_user_label_go.transform.FindChild("Username").GetComponent<UILabel>().text = KiiUser.CurrentUser.Username;
			m_user_label_2.text = KiiUser.CurrentUser.Username;
			m_user_label_3.text = KiiUser.CurrentUser.Username;
		}
		// m_user_label.text = "Logged in as: " + KiiUser.CurrentUser.Username;
		if(m_logged_in_as_guest){
			m_user_label.GetComponent<TweenAlpha>().PlayForward();
			m_user_label_go.GetComponent<TweenAlpha>().PlayForward();
			m_user_label_go.transform.FindChild("Username").GetComponent<TweenAlpha>().PlayForward();
			m_user_label_go.transform.FindChild("Username").GetComponent<UILabel>().text = "Guest";
			m_user_label_2.text = "Guest";
			m_user_label_3.text = "Guest";
#if UNITY_IOS
			if(SystemInfo.deviceType != DeviceType.Desktop)
				TrialpayIntegration.Init(SystemInfo.deviceUniqueIdentifier);
#endif
			if(m_current_user != null)
				m_current_user.Displayname = "Guest";	
		}
		else{
#if UNITY_IOS
			if(SystemInfo.deviceType != DeviceType.Desktop)
				TrialpayIntegration.Init(KiiUser.CurrentUser.Username);
#endif
		}
	}
	 
	public static void FacebookDoneInit()
	{
		s_inst.SetLoginName();
	}
	
#region Logins
	public static void LoginWithToken(){
		string access_Token = PlayerPrefs.GetString("AccessToken","null");
		if(access_Token != "null" && access_Token.Length > 0)
		{
			try{
				s_inst.m_current_user = KiiUser.LoginWithToken(access_Token);
				if(KiiUser.CurrentUser.Username.Contains("u.")){
#if UNITY_IOS
					s_inst.gameObject.GetComponent<FaceBookHandler>().Init();
#endif
				}
				s_inst.SucessfullyLoggedIn();
			}
			catch(CloudException e){
				Debug.LogError("Could not Log in user with token: " + e.StackTrace);
				return;
			}
		}
		else
		{
			GameObject go = GameObject.Find("Message Panel");
			if(go != null){
				if(go.transform.localPosition == go.GetComponent<TweenPosition>().to){
					go.GetComponent<TweenPosition>().PlayReverse();
				}
			}
			if(s_inst.m_login_panel == null)
				s_inst.m_login_panel = GameObject.Find("Login Panel").GetComponent<UIPanel>();
			s_inst.m_login_panel.GetComponent<TweenPosition>().PlayForward();
			s_inst.m_username_panel_is_down = true;
			s_inst.m_login_panel_is_down = true;

		}
	}

	public void LoginWithUsernameOnClick()
	{
		if(m_user_id.Length > 2)
			LoginWithUsername();
		else{
			m_incorrect_pass_username.enabled = true;
			m_incorrect_pass_username.text = "Username and Passwords must be at least 3 characters long";
		}
	}

	public void LoginWithUsername(){
		StartCoroutine("UsernameLogin");
	}

	IEnumerator UsernameLogin(){
		m_username_panel.GetComponent<TweenPosition>().PlayReverse();
		m_username_panel.GetComponent<TweenPosition>().onFinishedForward.Clear();
		m_logging_in_label.PlayForward();
		yield return new WaitForSeconds(0.5f);
		try{
			m_current_user = KiiUser.LogIn(m_user_id,m_user_password);
			Debug.Log(m_current_user);
			PlayerPrefs.SetInt("DidFacebookLogin",0);
			m_user_password = string.Empty;
			m_user_id = string.Empty;
			SucessfullyLoggedIn();
		}
		catch(CloudException e){
			Debug.Log("Wrong");
			if(e.Message != null){
				Debug.LogError(e.Message);
				m_incorrect_pass_username.enabled = true;
				m_incorrect_pass_username.text = "Incorrect Username or Password";
				m_user_password = "";
			}
		}
//*
#if UNITY_IPHONE
		KiiPushInstallation.DeviceType deviceType = KiiPushInstallation.DeviceType.IOS;
#elif UNITY_ANDROID
		KiiPushInstallation.DeviceType deviceType = KiiPushInstallation.DeviceType.ANDROID;
#endif
		
		s_inst.m_kii_push_plugin = GameObject.Find("KiiPushPlugin").GetComponent<KiiPushPlugin>();
		s_inst.m_kii_push_plugin.RegisterPush((string pushToken, Exception e0) => {
			KiiUser.LogIn(s_inst.m_user_id,s_inst.m_user_password, (KiiUser user, Exception e1) => {
				KiiUser.PushInstallation(true).Install(pushToken, deviceType, (Exception e2) => {
					Debug.Log("Registered Device.");
				});
			});
		});
//*
	}

	public static void LoginWithUsername(string username, string password){
		Debug.Log("Called in guest login");
		try{
			s_inst.m_current_user = KiiUser.LogIn(username,password);
			Debug.Log("Logged in " + s_inst.m_current_user.Username);
			if(s_inst.m_username_panel_is_down){
				s_inst.m_login_panel.GetComponent<TweenPosition>().PlayReverse();
				GameObject.Find("CrateBGPanel").GetComponent<HandleTweens>().PlayReverse();
				s_inst.m_username_panel_is_down = false;
				PlayerPrefs.SetInt("DidFacebookLogin",0);
				s_inst.SucessfullyLoggedIn();
			}
		}
		catch(CloudException e){
			if(e.Message != null){
				s_inst.m_incorrect_pass_username.enabled = true;
				s_inst.m_incorrect_pass_username.text = "Incorrect Username or Password";
				s_inst.m_user_password = "";
			}
		}	
//*
#if UNITY_IPHONE
		KiiPushInstallation.DeviceType deviceType = KiiPushInstallation.DeviceType.IOS;
#elif UNITY_ANDROID
		KiiPushInstallation.DeviceType deviceType = KiiPushInstallation.DeviceType.ANDROID;
#endif
		
		s_inst.m_kii_push_plugin = GameObject.Find("KiiPushPlugin").GetComponent<KiiPushPlugin>();
		s_inst.m_kii_push_plugin.RegisterPush((string pushToken, Exception e0) => {
			KiiUser.LogIn(username, password, (KiiUser user, Exception e1) => {
				KiiUser.PushInstallation(true).Install(pushToken, deviceType, (Exception e2) => {
					Debug.Log("Registered Device.");
				});
			});
		}); 
//*
	}
	public void LoginFacebook(){
		StartCoroutine("FacebookLoginCoRoutine");
	}
	IEnumerator FacebookLoginCoRoutine(){
		s_inst.m_login_panel.GetComponent<TweenPosition>().PlayReverse();
		s_inst.m_login_panel.GetComponent<TweenPosition>().onFinishedForward.Clear();
		m_logging_in_label.PlayForward();
		yield return new WaitForSeconds(0.5f);
		try{
			m_current_user = KiiUser.LoginWithFacebookToken(FaceBookHandler.GetToken());
			if(FaceBookHandler.m_current_user.email != null)
				KiiUser.ChangeEmail(FaceBookHandler.m_current_user.email);
			m_current_user.Displayname = FaceBookHandler.m_current_user.first_name + FaceBookHandler.m_current_user.last_name;
			// Send updated user data to the backend
			m_current_user.Update();
			PlayerPrefs.SetInt("DidFacebookLogin",1);
			SucessfullyLoggedIn();
		}
		catch(CloudException e){
			Debug.LogError ("Could not login User with Facebook :" + e.StackTrace);
		}	
	}

	public void LoginAsGuest(){
		StartCoroutine("GuestLogin");
	}

	IEnumerator GuestLogin(){
		m_logged_in_as_guest = true;
		PlayerPrefs.SetInt("IsLoggedInAsGuest",1);
		Debug.Log("Logging in as a guest");
		s_inst.m_login_panel.GetComponent<TweenPosition>().PlayReverse();
		s_inst.m_login_panel.GetComponent<TweenPosition>().onFinishedForward.Clear();
		m_logging_in_label.PlayForward();
		yield return new WaitForSeconds(0.5f);
		BuildUserWithOptions(SystemInfo.deviceUniqueIdentifier+"test",null,"GuestPassword");
	}

#endregion
	
	public void BuildUser() // Called from a NGUI button
	{
		if(m_user_id.Length > 0 && m_user_email.Length > 0 && m_user_password.Length >0){
			if(m_user_password.Equals(m_user_confirm_password)){
				BuildUserWithOptions(m_user_id, m_user_email, m_user_password);
			}else{
				m_incorrect_pass_username.enabled = true;
				m_incorrect_pass_username.text = "Passwords do not match.";
				m_user_password = "";
				m_user_confirm_password = "";
			}
		}
	}

	public void BuildUserWithOptions(string username, string email, string password)
	{
		KiiUser.Builder builder;
		builder = KiiUser.BuilderWithName(username);
		if(email != null){
			builder.WithEmail(email);
		}
		KiiUser user = builder.Build();

		try{
			user.Register(password);
		}
		catch(CloudException e){
			Debug.Log("User Creation failed: " + e.Message);
			if(e.Message.Contains("Conflict happens") && m_logged_in_as_guest){
				LoginWithUsername(username,password);
			}
			if(e.Message.Contains("Invalid username format")){
				Debug.LogError("Bad username Format");
				//Needs new username format
				return;				
			}
			if(e.Message.Contains("Invalid Email format")){
				Debug.LogError("Bad Email Format");
				//Needs new Email format
				return;
			}
			if(e.Message.Contains("Invalid Password format")){
				Debug.LogError("Bad Password Format");
				//Needs new Password format
				return;				
			}
			if(e.Message.Contains("network related exception")){
				m_login_panel = GameObject.Find("Login Panel").GetComponent<UIPanel>();
				m_login_panel.GetComponent<TweenPosition>().PlayReverse();
				m_no_internet.SetActive(true);
				GameObject.Find("Start").GetComponent<UIButton>().isEnabled = false;
				return;
			}
		}
		m_current_user = user;
		if(m_register_panel_is_down)
		{
			s_inst.m_register_panel.GetComponent<TweenPosition>().PlayReverse();
			s_inst.m_register_panel.GetComponent<TweenPosition>().onFinishedForward.Clear();
			m_logging_in_label.PlayForward();
			m_register_panel_is_down = false;
		}
		s_inst.SucessfullyLoggedIn();		
	}
	
	public void FacebookLogin()
	{
#if UNITY_IOS
		gameObject.GetComponent<FaceBookHandler>().Init(); // logs into facebook then calls the LoginFacebook method above.
#endif
	}

	void OnGUI()
	{
		if(m_gui_state == RegisterGUIState.REGISTER){
			m_user_id = GUI.TextField(m_username_rect,m_user_id,m_my_style);
			m_user_password = GUI.PasswordField(m_password_rect,m_user_password,'*',m_my_style);
			m_user_confirm_password = GUI.PasswordField(m_confirm_password_rect,m_user_confirm_password,'*',m_my_style);
			m_user_email = GUI.TextField(m_email_rect,m_user_email,m_my_style);
		}
		else if(m_gui_state == RegisterGUIState.SIGNIN){
			m_user_id = GUI.TextField(m_username_rect,m_user_id,m_my_style);
			m_user_password = GUI.PasswordField(m_password_rect,m_user_password,'*',m_my_style);
		}
		else if(m_gui_state == RegisterGUIState.CHANGEPASSWORD){
			m_user_password = GUI.PasswordField(m_password_rect,m_user_password,'*',m_my_style);;
			m_new_password = GUI.PasswordField(m_new_password_rect,m_new_password,'*',m_my_style);;
			m_user_confirm_password = GUI.PasswordField(m_confirm_password_rect,m_user_confirm_password,'*',m_my_style);;
		}
		else if(m_gui_state == RegisterGUIState.RESETPASSWORD){
			m_user_email = GUI.TextField(m_email_rect,m_user_email);
		}
	}

	public Rect CalculateRect(UISprite sprite)
	{
		Rect temp = new Rect();
		temp.x = Camera.main.WorldToScreenPoint(sprite.worldCorners[1]).x;
		float tempypos = Camera.main.WorldToScreenPoint(sprite.worldCorners[1]).y;
		temp.y = Screen.height - tempypos;
		temp.width = Camera.main.WorldToScreenPoint(sprite.worldCorners[3]).x - temp.x;
		temp.height = tempypos - Camera.main.WorldToScreenPoint(sprite.worldCorners[3]).y;
		//Debug.Log (sprite.name + temp);
		return temp;
	}

	//This is called when the Register Panel is moved on or off screen and handles the Unity GUI for us.
	public void HandleRegisterPanel(){
		if(m_r_register_panel_tween.transform.localPosition == m_r_register_panel_tween.to){ //If the panel in on screen
			//Set input field rectangles.
			m_gui_state = RegisterGUIState.REGISTER;
			m_username_rect = CalculateRect(m_r_username_sprite);
			m_password_rect = CalculateRect(m_r_password_sprite);
			m_confirm_password_rect = CalculateRect(m_r_confirm_password_sprite);
			m_email_rect = CalculateRect(m_r_email_sprite);
			m_register_panel_is_down = true;
			 Debug.Log("Register is in");
		}
		else if(m_r_register_panel_tween.transform.localPosition == m_r_register_panel_tween.from){ //If the panel is off screen
			//Clear input fields
			if(m_gui_state == RegisterGUIState.REGISTER){
				m_username_rect = new Rect();
				m_password_rect = new Rect();
				m_confirm_password_rect = new Rect();
				m_email_rect = new Rect();
				m_gui_state = RegisterGUIState.NONE;
			}
			m_user_password = string.Empty;
			m_user_id = string.Empty;
			m_user_confirm_password = string.Empty;
			m_user_email = string.Empty;
			m_register_panel_is_down = false;
		}
		else
			Debug.LogError("The Panel " + m_change_panel_tween.gameObject.name + " is not in a TO or FROM position.");
	}

	//This is called when the Login Panel is moved on or off screen and handles the Unity GUI for us.
	public void HandleLoginPanel(){
		if(m_login_panel_tween.transform.localPosition == m_login_panel_tween.to){ //If the panel in on screen
			//Set input field rectangles.
			m_gui_state = RegisterGUIState.SIGNIN;
			m_username_rect = CalculateRect(m_username_sprite);
			m_password_rect = CalculateRect(m_password_sprite);
			m_login_panel_is_down = true;
			m_username_panel_is_down = true;
		}
		else if(m_login_panel_tween.transform.localPosition == m_login_panel_tween.from){ //If the panel is off screen
			//Clear input fields
			if(m_gui_state == RegisterGUIState.SIGNIN){ 
				m_username_rect = new Rect();
				m_password_rect = new Rect();
				m_gui_state = RegisterGUIState.NONE;
			}
			//m_user_password = string.Empty;
			//m_user_id = string.Empty;
			m_login_panel_is_down = false;	
			m_username_panel_is_down = false;
		}
		else
			Debug.LogError("The Panel " + m_change_panel_tween.gameObject.name + " is not in a TO or FROM position.");
	}

	//This is called when the Change Password Panel is moved on or off screen and handles the Unity GUI for us.
	public void HandleChangePasswordPanel(){
		if(m_change_panel_tween.transform.localPosition == m_change_panel_tween.to){ //If the panel in on screen
			//Set input field rectangles.
			m_gui_state = RegisterGUIState.CHANGEPASSWORD;
			m_new_password_rect = CalculateRect(m_new_password_sprite);
			m_confirm_password_rect = CalculateRect(m_c_confirm_password_sprite);
			m_password_rect = CalculateRect(m_old_password_sprite);
		}
		else if(m_change_panel_tween.transform.localPosition == m_change_panel_tween.from){ //If the panel is off screen
			//Clear input fields
			if(m_gui_state == RegisterGUIState.CHANGEPASSWORD){
				m_new_password_rect = new Rect();
				m_confirm_password_rect = new Rect();
				m_password_rect = new Rect();
				m_gui_state = RegisterGUIState.NONE;
			}
			m_user_password = string.Empty;
			m_new_password = string.Empty;
			m_user_confirm_password = string.Empty;
		}
		else
			Debug.LogError("The Panel " + m_change_panel_tween.gameObject.name + " is not in a TO or FROM position.");
	}

	//This is called when the Reset Password Panel is moved on or off screen and handles the Unity GUI for us.
	public void HandleResetPasswordPanel(){
		if(m_reset_password_tween.transform.localPosition == m_reset_password_tween.to){ //If the panel in on screen
			//Set input field rectangles.
			m_gui_state = RegisterGUIState.RESETPASSWORD;
			m_email_rect = CalculateRect(m_reset_password_sprite);
		
		}
		else if(m_reset_password_tween.transform.localPosition == m_reset_password_tween.from){ //If the panel is off screen
			//Clear input fields
			if(m_gui_state == RegisterGUIState.RESETPASSWORD){
				m_email_rect = new Rect();
				m_gui_state = RegisterGUIState.NONE;
			}
			m_user_email = string.Empty;
		}
		else
			Debug.LogError("The Panel " + m_change_panel_tween.gameObject.name + " is not in a TO or FROM position.");
	}

	public void ChangePassword(){
		if(m_new_password.Equals(m_user_confirm_password)){
			try{
				KiiUser.ChangePassword(m_new_password,m_user_password);
				Debug.Log("Changed Password!");
				PlayerPrefs.SetString("AccessToken","null");
				m_user_password = string.Empty;
				m_new_password = string.Empty;
				m_user_confirm_password = string.Empty;
			}catch(CloudException e){
				Debug.LogError("Change password failed: " + e.Message);
			}
		}
		else
			Debug.LogError("New Passwords do not match.");

	}
*/
}
