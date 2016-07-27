using UnityEngine;
using System.Collections;

public class BossTimeBarHandler : MonoBehaviour {
	public GameObject m_reward_two;
	public GameObject m_reward_three;
	public Transform m_left_side;
	public Transform m_right_side;

	public float m_reward_two_time;
	public float m_reward_three_time;
	public float m_level_time;
	UITweener[] m_reward_two_tweens;
	UITweener[] m_reward_three_tweens;
	public GameManager.Powerup m_powerup_to_give;
	// Use this for initialization
	void Start () {
		if(!Application.loadedLevelName.Contains("11")){
			DestroyImmediate(this);
			return;
		}
		m_reward_two.SetActive(true);
		m_reward_three.SetActive(true);
		Vector3 dir = m_right_side.localPosition - m_left_side.localPosition;
		m_reward_two.transform.localPosition = m_left_side.localPosition + dir.normalized * (dir.magnitude * ((float)m_reward_two_time / (float)m_level_time));
		m_reward_three.transform.localPosition = m_left_side.localPosition + dir.normalized * (dir.magnitude * ((float)m_reward_three_time / (float)m_level_time));
		m_reward_two_tweens = m_reward_two.GetComponents<UITweener>();
		TweenRewardTwo(true);
		m_reward_three_tweens = m_reward_three.GetComponents<UITweener>();
		TweenRewardThree(true);
		GameManager.s_Inst.m_boss_reward = m_powerup_to_give;
	}
	
	// Update is called once per frame
	void Update () {
		if(GameManager.s_Inst.m_game_timer < m_reward_three_time && m_reward_three.activeSelf){
			TweenRewardThree(false);
			GameManager.s_Inst.m_boss_reward = GameManager.Powerup.None;
		}
		if(GameManager.s_Inst.m_game_timer < m_reward_two_time && m_reward_two.activeSelf){
			TweenRewardTwo(false);
		}
	}

	void TweenRewardTwo(bool forward){
		foreach(UITweener t in m_reward_two_tweens){
			t.Play(forward);
		}

	}
	void TweenRewardThree(bool forward){
		foreach(UITweener t in m_reward_three_tweens){
			t.Play(forward);
		}
	}
}
