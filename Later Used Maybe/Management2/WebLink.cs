using UnityEngine;
using System.Collections;

public class WebLink : MonoBehaviour {
	public string m_url;
	public void GoToURL(){
		Application.OpenURL(m_url);
	}
}
