using UnityEngine;
using System.Collections;

public class ChangeNumberWithBaskets : MonoBehaviour {
	UILabel m_label;
	public bool m_count_down = false;
	public int m_baskets_to_make;
	void Start(){
		m_label = gameObject.GetComponent<UILabel>();
	}
	// Update is called once per frame
	void Update () {
		if(GameManager.s_Inst != null){
			if(m_count_down){
				if(GameManager.m_baskets_made < m_baskets_to_make){
					m_label.text = (m_baskets_to_make - GameManager.m_baskets_made).ToString();
				}else{
					m_label.text = "0?";
				}
			}else{
				m_label.text = GameManager.m_baskets_made.ToString();
			}
		}
	}
}
