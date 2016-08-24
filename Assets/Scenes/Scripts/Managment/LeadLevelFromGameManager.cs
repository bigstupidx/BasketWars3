using UnityEngine;
using System.Collections;

public class LeadLevelFromGameManager : MonoBehaviour {

	public UIProgressBar m_loading_bar;
	AsyncOperation m_async;
	string m_level_name_to_load;

	void Start () {
		Time.timeScale = 1;
		if(GameManager.s_Inst == null)
			return;
		m_level_name_to_load = GameManager.s_Inst.m_level_name_to_load;
		ChangeLevel();
	}
	public void ChangeLevel(){
		if(GameManager.s_Inst != null){
			if(GameManager.s_Inst.m_marked_for_destroy){
				GameObject go = GameManager.s_Inst.gameObject;
				GameManager.s_Inst.m_marked_for_destroy = false;
				GameManager.s_Inst = null;				
				DestroyImmediate(go);
			}
			StartCoroutine ("LoadLevel");
		}
	}
	
	IEnumerator LoadLevel(){
		m_async = Application.LoadLevelAsync(m_level_name_to_load);
		m_loading_bar.value = m_async.progress;
		yield return m_async;
	}
	
	void Update(){
		if(m_async != null){
			m_loading_bar.value = m_async.progress;
		}
	}
}
