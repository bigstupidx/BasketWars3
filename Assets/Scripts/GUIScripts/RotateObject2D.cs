using UnityEngine;
using System.Collections;

public class RotateObject2D : MonoBehaviour 
{
	public float m_speed;	
	// Update is called once per frame
	void Update () {
		transform.Rotate(Vector3.forward,m_speed * RealTime.deltaTime);
	}
}
