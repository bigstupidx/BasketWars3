using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BackButtonStack : MonoBehaviour {
	public static BackButtonStack s_inst;
	public UIButton m_button;
	List<EventDelegate> m_delegate_list = new List<EventDelegate>();
	void Awake(){
		if(s_inst == null)
			s_inst = this;
	}

	public static void PushStack(EventDelegate _delegate){
		s_inst.StartCoroutine(PushStackAsync(_delegate));
	}

	static IEnumerator PushStackAsync(EventDelegate _delegate){
		if(s_inst.m_delegate_list.Count <= 0){// no items in the stack yet. Time to show the button
			s_inst.m_button.gameObject.GetComponent<TweenPosition>().delay = 0.35f;
			s_inst.m_button.gameObject.GetComponent<TweenPosition>().PlayForward();
		}
		yield return null;
		if(s_inst.m_delegate_list.Count > 0) //Remove the old delegate from the button
			s_inst.m_button.onClick.RemoveAt(s_inst.m_button.onClick.Count-1);
		s_inst.m_delegate_list.Add(_delegate); // Add it to the stack
		s_inst.m_button.onClick.Add(_delegate); // Add new delegate to button
	}

	public static void PopStack(){
		s_inst.StartCoroutine(PopStackAsync());
	}

	static IEnumerator PopStackAsync(){
		yield return null;
		if(s_inst.m_delegate_list.Count > 0){ // If the stack is not empty
			s_inst.m_delegate_list.RemoveAt(s_inst.m_delegate_list.Count-1); // pop off the last item in the stack
			s_inst.m_button.onClick.RemoveAt(s_inst.m_button.onClick.Count-1); // Remove the old delegate
			if(s_inst.m_delegate_list.Count > 0){ // If we are still not empty
				s_inst.m_button.onClick.Add(s_inst.m_delegate_list[s_inst.m_delegate_list.Count-1]); // And add the next on on the stack
			}else{// If the stack is empty hide the button
			s_inst.m_button.gameObject.GetComponent<TweenPosition>().delay = 0;
				s_inst.m_button.gameObject.GetComponent<TweenPosition>().PlayReverse();
			}
		}
	}
}
