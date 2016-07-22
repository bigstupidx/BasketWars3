using UnityEngine;
using System.Collections;

public class BlimpMover : MonoBehaviour {

	public GameObject m_blimp_start;
	public GameObject m_blimp_end;
	public float m_speed;
	[SerializeField]
	public Target[] m_targets;
	public ExplodePost m_post;
	public float m_sin_wave_multiplyer = 1;
	public bool m_is_mini_blimp = false;
	bool m_did_finish_moving;
	Vector3 m_dir;
	float up;
	bool m_targets_gone = false;
	float m_starting_height;

	// Use this for initialization
	void Start () {
		if(m_is_mini_blimp)
			transform.position = m_blimp_start.transform.position - new Vector3(0,4,0);
		else
			transform.position = m_blimp_start.transform.position;
		m_starting_height = transform.position.y;
		m_dir = (m_blimp_end.transform.position - m_blimp_start.transform.position).normalized;
	}
	
	// Update is called once per frame
	void Update () {
		if(m_did_finish_moving){
			transform.position += (m_dir + Vector3.up).normalized * m_speed * 1.5f * Time.deltaTime;
			if(transform.position.x < 0 && transform.position.y > 25){
				int num_hits = 0;
				foreach(Target t in m_targets){
					if(t != null){
						num_hits += t.m_hits_left + 1;
					}
				}
				if(num_hits > 0)
					GameManager.s_Inst.AddScore(-5 * num_hits);
				Destroy(this.gameObject);
			}
		}else{
			up += Time.deltaTime; 
			transform.position += m_dir * m_speed * Time.deltaTime;
			transform.position = new Vector3(transform.position.x,m_starting_height +(Mathf.Sin(up) * m_sin_wave_multiplyer), transform.position.z);
			if(transform.position.x < m_blimp_end.transform.position.x){
				m_did_finish_moving = true;			
			}
		}
		bool targets = true;
		foreach(Target t in m_targets){
			if(t != null){
				if(t.m_hits_left >= 0)
					targets = false;
			}
		}
		if(targets && !m_targets_gone){
			m_post.Explode();
			m_did_finish_moving = true;
			m_targets_gone = true;
		}
	}
}