using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ChangeRank : MonoBehaviour {
	public bool m_is_one_rank_higher = false;
	[SerializeField]
	public List<string> m_icon_names = new List<string>();
	// Use this for initialization
	void Start () {
		Invoke ("UpdateRankSymbol",0.1f);
	}

	void UpdateRankSymbol () {
		if(GameManager.s_Inst != null)
		{
			if(m_is_one_rank_higher){
				if(GameManager.s_Inst.m_current_rank[GameManager.m_character_chosen] + 1 >= m_icon_names.Count){
					gameObject.GetComponent<UISprite>().enabled = false;
					return;
				}
				gameObject.GetComponent<UISprite>().spriteName = m_icon_names[GameManager.s_Inst.m_current_rank[GameManager.m_character_chosen] + 1];
			}else{				
				gameObject.GetComponent<UISprite>().spriteName = m_icon_names[GameManager.s_Inst.m_current_rank[GameManager.m_character_chosen]];
			}
		}
		else
		Invoke ("UpdateRankSymbol",0.5f);
	}

	public void SetRank(int rank){
		gameObject.GetComponent<UISprite>().spriteName = m_icon_names[rank];
	
	}
}
