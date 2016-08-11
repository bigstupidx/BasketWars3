using UnityEngine;
using System.Collections;
#pragma warning disable 0414

public class ResizeTexture : MonoBehaviour
{
	public Camera m_camera;
	public void SetTexture(RenderTexture texture)
	{
		gameObject.GetComponent<UITexture>().mainTexture = texture;
		//gameObject.GetComponent<UITexture>().width = (int)m_camera.pixelWidth;

	}
}
