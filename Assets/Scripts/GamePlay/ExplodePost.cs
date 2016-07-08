using UnityEngine;
using System.Collections;

public class ExplodePost : MonoBehaviour {
	[SerializeField]
	public GameObject[] m_explosion_points;
	public GameObject m_explosion;

	public void Explode(){
		foreach(GameObject g in m_explosion_points){
			Instantiate(m_explosion,g.transform.position,Quaternion.identity);
		}
		Destroy(this.gameObject);
	}
	
}
