using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TutorialScroll : MonoBehaviour 
{
	public List<Transform> m_tutorial_panels = new List<Transform>();
	int m_current_tutorial = 0;
	public UILabel m_tutorial_count;
	// Use this for initialization

	public void NextTutorial(){
		m_current_tutorial++;
		if(m_current_tutorial >= m_tutorial_panels.Count)
			m_current_tutorial = 0;
		UpdatePanels();
	}

	public void PrevTutorial(){
		m_current_tutorial--;
		if(m_current_tutorial < 0)
			m_current_tutorial = m_tutorial_panels.Count -1;
		UpdatePanels();
	}

	void UpdatePanels()
	{
		int i = 0;
		m_tutorial_count.text = "" + (m_current_tutorial + 1);
		foreach(Transform t in m_tutorial_panels){
			float x_pos = (i - m_current_tutorial) * 2000;
			t.localPosition = new Vector3(x_pos,0,0);
			i++;
		}
	}
}
