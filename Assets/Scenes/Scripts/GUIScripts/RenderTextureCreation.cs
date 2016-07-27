using UnityEngine;
using System.Collections;

public class RenderTextureCreation : MonoBehaviour 
{
	RenderTexture m_r_texture;
	public ResizeTexture m_resizer;
	public Camera m_camera;
	public Transform leftside;
	// Use this for initialization
	void Start () 
	{
		//m_camera.aspect = 1.777778f;
		
		/*m_r_texture = new RenderTexture(2048,1536,16,RenderTextureFormat.ARGB32);
		//m_r_texture.width = Screen.width;
		//m_r_texture.height = Screen.height;
		m_r_texture.isPowerOfTwo = false;
		m_r_texture.name = "RenderScene";
		m_camera.targetTexture = m_r_texture;
		m_resizer.SetTexture(m_r_texture);
		GameManager.SetCameraRect();*/
	}
}
