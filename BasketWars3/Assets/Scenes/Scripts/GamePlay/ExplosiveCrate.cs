using UnityEngine;
using System.Collections;

public class ExplosiveCrate : MonoBehaviour {
	public GameObject m_explosion;
	public int m_points;
	public float m_force = 10;
	public int m_hits_left = 0;

	public void Start(){
		m_hits_left = Random.Range(0,4);
		ChangeColor();
	}

	public void Explode(){
		if(m_hits_left > 0){
			m_hits_left--;
			ChangeColor();
		}else{
			GameObject[] crates = GameObject.FindGameObjectsWithTag("Obstacle");
			foreach(GameObject g in crates){
				Vector2 dir = (g.transform.position - transform.position);
				float force = m_force - dir.magnitude;
				if(force > 0)
					g.GetComponent<Rigidbody2D>().AddRelativeForce(dir.normalized * force,ForceMode2D.Impulse);
			}
			Instantiate(m_explosion,transform.position,Quaternion.identity);
			GameManager.s_Inst.AddScore(m_points);
			Destroy(this.gameObject);
		}
	}

	void ChangeColor(){
		SpriteRenderer s = gameObject.GetComponent<SpriteRenderer>();
		if(m_hits_left == 3){
			s.color = Color.blue;
		}else if(m_hits_left == 2){
			s.color = Color.green;
		}else if(m_hits_left == 1){
			s.color = Color.yellow;
		}else if(m_hits_left == 0){
			s.color = Color.white;
		}
	}
}
