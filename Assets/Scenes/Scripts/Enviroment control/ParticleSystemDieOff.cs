using UnityEngine;
using System.Collections;

public class ParticleSystemDieOff : MonoBehaviour {
	ParticleSystem m_sys;

	void Start(){
		m_sys = gameObject.GetComponent<ParticleSystem>();
	}

	// Update is called once per frame
	void Update () {
		if(transform.parent == null){
			if(m_sys.enableEmission == true){
				m_sys.startSize = 4;
				m_sys.startSpeed = .5f;
				m_sys.Emit(10);
			}
			m_sys.enableEmission = false;
			if(m_sys.particleCount == 0)
				Destroy(gameObject);
		}
	}
}
