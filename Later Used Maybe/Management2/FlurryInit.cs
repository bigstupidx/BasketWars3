using UnityEngine;
using System.Collections;

public class FlurryInit : MonoBehaviour 
{
	static bool m_flurry_has_init = false;
	void Start () 
	{
#if UNITY_IPHONE
		if(!m_flurry_has_init){
			FlurryAnalytics.startSession("QFFDVQD8YH4X8SDV6F3X"); //Internal Key
			FlurryAnalytics.logEvent("Launched Game",false);
			m_flurry_has_init = true;
		}
#endif
	}
	
}
