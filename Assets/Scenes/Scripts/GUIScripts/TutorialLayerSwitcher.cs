using UnityEngine;
using System.Collections;

public class TutorialLayerSwitcher : MonoBehaviour {
	public enum UIElementType{
		Panel,
		Widget
	};
	public bool m_toggle;
	public UIElementType m_elemnt_type;
	public TutorialManager.TutorialProgress m_trigger_progress;
	public int m_depth_to_switch;
	int m_starting_depth;
	UIWidget m_widget;
	UIPanel m_panel;

	// Use this for initialization
	void Start () {
		if(m_elemnt_type == UIElementType.Widget){
			m_widget = gameObject.GetComponent<UIWidget>();
			m_starting_depth = m_widget.depth;
		}
		else if(m_elemnt_type == UIElementType.Panel){
			m_panel = gameObject.GetComponent<UIPanel>();
			m_starting_depth = m_panel.depth;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if(m_elemnt_type == UIElementType.Widget){
			if(TutorialManager.s_Inst.m_current_progress == m_trigger_progress)
				m_widget.depth = m_depth_to_switch;
			if(TutorialManager.s_Inst.m_current_progress != m_trigger_progress && m_widget.depth != m_starting_depth)
				m_widget.depth = m_starting_depth;
		}
		else if (m_elemnt_type == UIElementType.Panel){
			if(TutorialManager.s_Inst.m_current_progress == m_trigger_progress)
				m_panel.depth = m_depth_to_switch;
			if(TutorialManager.s_Inst.m_current_progress != m_trigger_progress && m_panel.depth != m_starting_depth)
				m_panel.depth = m_starting_depth;
		}
		if(m_toggle){
			if(TutorialManager.s_Inst.m_current_progress == m_trigger_progress)
				m_widget.alpha = 1;
			else
				m_widget.alpha = 0;
		}
	}
}
