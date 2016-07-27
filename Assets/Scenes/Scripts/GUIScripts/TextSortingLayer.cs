using UnityEngine;
using System.Collections;

public class TextSortingLayer : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Renderer renderer = this.gameObject.GetComponent<Renderer>();
        renderer.sortingOrder = 3;
        renderer.sortingLayerName = "Background";
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
