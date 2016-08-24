using UnityEngine;
using System.Collections;

public class BasketFollow : MonoBehaviour 
{
	public Transform m_anchor;

	// Update is called once per frame
	void Update () 
	{
		if(m_anchor != null)
			transform.position = m_anchor.position;
	}
}
