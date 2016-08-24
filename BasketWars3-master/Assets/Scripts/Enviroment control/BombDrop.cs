using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BombDrop : MonoBehaviour 
{
	public GameObject m_bomb;
	List<GameObject> m_bomb_list = new List<GameObject>();
	float m_drop_timer;
	int m_drop_count;
	// Use this for initialization
	void Start () 
	{
		m_drop_timer = Random.Range(2,6);
	}
	
	// Update is called once per frame
	void Update () 
	{
		m_drop_timer -= Time.deltaTime;
		if(m_drop_timer <= 0)
		{
			InvokeRepeating("DropBomb",0.1f,0.5f);
			m_drop_timer = Random.Range(4,6);
		}
		if(m_drop_count >= 5)
		{
			CancelInvoke("DropBomb");
			m_drop_count = 0;
		}
		for(int i = 0; i < m_bomb_list.Count;)
		{
			if(m_bomb_list[i].transform.position.y < 14.40244f)
			{
				GameObject go = m_bomb_list[i];
				m_bomb_list.RemoveAt(i);
				Destroy(go);
				continue;
			}
			i++;
		}
	}

	void DropBomb()
	{
		GameObject go = (GameObject)Instantiate(m_bomb, transform.position,Quaternion.identity);
		m_bomb_list.Add(go);
		m_drop_count++;
	}
}
