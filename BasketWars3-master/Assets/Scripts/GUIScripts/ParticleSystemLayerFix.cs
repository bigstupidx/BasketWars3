using UnityEngine;
using System.Collections;

public class ParticleSystemLayerFix : MonoBehaviour {

	void Start ()
	{
		GetComponent<ParticleSystem>().GetComponent<Renderer>().sortingLayerName = "Foreground";
	}
}
