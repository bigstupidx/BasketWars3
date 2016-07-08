using UnityEngine;
using System.Collections;

public class FlurryCustomEvent : MonoBehaviour {
	public string m_event_name;

	public void FireEvent(){
		if(string.IsNullOrEmpty(m_event_name))
			return;
		FlurryAnalytics.logEvent(m_event_name,false);
	}
}
