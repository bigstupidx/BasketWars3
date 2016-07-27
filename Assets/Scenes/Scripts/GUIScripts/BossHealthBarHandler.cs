using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BossHealthBarHandler : MonoBehaviour {
	public UIProgressBar m_health_bar;
	//public ParticleSystem m_particles;
	public Transform m_bar_left;
	public Transform m_bar_right;
	Vector3 m_bar_dir;
	public Transform m_bar_end_anchor;
	public GameObject[] m_tween_objects;
	List<UITweener> m_damage_state_tweens = new List<UITweener>();
	public float m_max_life;
	float m_life_bar_goal;
	float m_life_bar_start;
	float m_bar_progress = 0;

	void Start(){
		m_bar_dir = m_bar_right.position - m_bar_left.position;
		m_life_bar_goal = 1;
		foreach(GameObject g in m_tween_objects){
			UITweener[] temp = g.GetComponents<UITweener>();
			foreach(UITweener t in temp){
				m_damage_state_tweens.Add(t);
			}
		}
	}
	

	// Update is called once per frame
	void Update () {
		if(m_health_bar.value != m_life_bar_goal){
			m_bar_progress += Time.deltaTime;
			m_health_bar.value = Mathf.Lerp(m_life_bar_start,m_life_bar_goal,m_bar_progress);
			m_bar_end_anchor.position = m_bar_left.position + (m_bar_dir * m_health_bar.value);
		}else{
			EndTweens();
		}
	}

	public void TookDamage(){
		//m_particles.Emit(20);
		m_life_bar_start = m_health_bar.value;
		m_life_bar_goal = (float)GameManager.m_boss_life / m_max_life;
		m_bar_progress = 0;
		foreach(UITweener t in m_damage_state_tweens){
			t.ResetToBeginning();
			t.PlayForward();
		}
	}

	public void EndTweens(){
		foreach(UITweener t in m_damage_state_tweens){
			t.ResetToBeginning();
			t.enabled = false;
		}
	}
}
