using UnityEngine;
using System.Collections;

public class BlimpSpawner : MonoBehaviour {
	public float m_time_till_spawn;
	public GameObject m_blimp_end;
	public GameObject[] m_blimp_prefabs;
	public GameObject[] m_blimp_mini_prefabs;

	// Use this for initialization
	void Start () {
		Invoke("SpawnBlimp",0.1f);
		Invoke("SpawnMiniBlimp",15.0f);
	}

	void SpawnBlimp(){
		int choice = Random.Range(0,m_blimp_prefabs.Length);
		GameObject go = (GameObject)Instantiate(m_blimp_prefabs[choice],transform.position,Quaternion.identity);
		BlimpMover b = go.GetComponent<BlimpMover>();
		b.m_blimp_start = this.gameObject;
		b.m_blimp_end = m_blimp_end;
		b.m_speed = Mathf.Lerp(3,2,GameManager.s_Inst.m_game_timer/60.0f);
		Invoke("SpawnBlimp",m_time_till_spawn);
	}

	void SpawnMiniBlimp(){
		GameObject go = (GameObject)Instantiate(m_blimp_mini_prefabs[0],transform.position,Quaternion.identity);
		BlimpMover b = go.GetComponent<BlimpMover>();
		b.m_blimp_start = this.gameObject;
		b.m_blimp_end = m_blimp_end;
		Invoke("SpawnMiniBlimp",15.0f);
	}
}
