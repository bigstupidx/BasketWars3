using UnityEngine;
using System.Collections;

public class MissileSpawner : MonoBehaviour {

	public GameObject[] m_spawn_points;
	public Transform m_target_start;
	public Transform m_target_end;
	public GameObject m_missile;
	public float m_spawn_time;

	// Use this for initialization
	void Start () {
		Invoke("SpawnMissile",m_spawn_time);
	}
	
	// Update is called once per frame
	void SpawnMissile () {
		int spawn_point = Random.Range(0,m_spawn_points.Length);
		GameObject go = (GameObject)Instantiate(m_missile,m_spawn_points[spawn_point].transform.position,Quaternion.identity);
		float percent = Random.Range(0.0f,1.0f);
		Vector3 dir = m_target_end.position - m_target_start.position;
		Vector3 target = m_target_start.position + (dir * percent);
		dir = target - go.transform.position;
		go.GetComponent<Missile>().m_dir = dir.normalized;
		go.transform.up = -dir.normalized;
		Invoke("SpawnMissile",m_spawn_time);
	}
}
