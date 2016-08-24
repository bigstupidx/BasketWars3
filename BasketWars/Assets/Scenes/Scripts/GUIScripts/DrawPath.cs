using UnityEngine;
using System.Collections;

public class DrawPath : MonoBehaviour 
{
	public TweenAlpha m_path_line;

	public void ShowPath(){
		m_path_line.ResetToBeginning();
		m_path_line.PlayForward();
	}
	public void HidePath(){
		m_path_line.PlayReverse();
	}
}
