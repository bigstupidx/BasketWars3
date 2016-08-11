using UnityEngine;
using System.Collections;

public class StaminaRefillPanel : MonoBehaviour {
	public UILabel m_time_label;
	public UILabel m_time_label_note;
	public UILabel m_number_left;
	public UIProgressBar m_progress_bar;	

	void Update () {
		if(GameManager.s_Inst != null ){
			StaminaGuage SG = GameManager.s_Inst.gameObject.GetComponent<StaminaGuage>();
			if(SG.m_stamina < 10){
				m_time_label.enabled = true;
				m_time_label_note.enabled = true;
				int seconds = (int)SG.m_timer;
				int mins = seconds / 60;
				seconds %= 60;
				m_time_label.text = mins.ToString() + "m " + seconds.ToString() + "s";
				m_number_left.text = SG.m_stamina.ToString();
				m_progress_bar.value = (float)SG.m_stamina / (float)SG.m_max_stamina;
			}else{
				m_time_label.enabled = false;
				m_time_label_note.enabled = false;
				m_number_left.text = "10";
				m_progress_bar.value = 1;
			}
		}
	}
}
