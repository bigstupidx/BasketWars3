using UnityEngine;
using System.Collections;

public class LoadLevelOnFinish : MonoBehaviour {

	public string levelName;
	public UIProgressBar m_loading_bar;
	public TweenAlpha m_jabb_logo;
	public TweenAlpha m_copyright_tween;
	public TweenPosition m_loading_bar_tween;
	public TweenAlpha m_loading_alpha;
	public UI2DSprite m_background;
	AsyncOperation m_async;

	public void Start(){
		if(GameObject.Find("Camera").GetComponent<Camera>().aspect >= 1.7f && GameObject.Find("Camera").GetComponent<Camera>().aspect <= 1.8f){
			// m_background.width = 1280;
		}
		if(PlayerPrefs.GetInt("SkipLogo",0) == 1){
			m_loading_bar_tween.PlayForward();
			m_loading_alpha.value = m_loading_alpha.to;

		}else{
			m_jabb_logo.PlayForward();
			m_copyright_tween.PlayForward();
			PlayerPrefs.SetInt("SkipLogo",1);
		}
	}
	
	public void ChangeLevel(){
		StartCoroutine ("LoadLevel");
	}
	
	IEnumerator LoadLevel(){
		m_async = Application.LoadLevelAsync(levelName);
		m_loading_bar.value = m_async.progress;
		yield return m_async;
	}

	void Update(){
		if(m_async != null){
			m_loading_bar.value = m_async.progress;
		}
	}
}