using UnityEngine;
using System.Collections;

public class ResetScrollView : MonoBehaviour
{

		// Use this for initialization
		void Start ()
		{
				gameObject.GetComponent<UIScrollView> ().ResetPosition ();
		}
		void OnLevelWasLoaded ()
		{
				gameObject.GetComponent<UIScrollView> ().ResetPosition ();
		}
}
