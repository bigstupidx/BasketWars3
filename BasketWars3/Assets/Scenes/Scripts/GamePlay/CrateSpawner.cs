using UnityEngine;
using System.Collections;

public class CrateSpawner : MonoBehaviour {
	public GameObject m_crate;
	public float m_spawn_time;

	// Use this for initialization
	void Start () {
		Invoke("SpawnCrate",m_spawn_time);
	}
	
	void SpawnCrate(){
		if(transform.childCount < 8){
			GameObject go = (GameObject)Instantiate(m_crate,transform.position,Quaternion.identity);
			go.transform.parent = this.transform;
		}
		Invoke("SpawnCrate",m_spawn_time);
	}
	
}
