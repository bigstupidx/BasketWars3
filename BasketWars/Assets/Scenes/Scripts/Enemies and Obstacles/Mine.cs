using UnityEngine;
using System.Collections;

public class Mine : MonoBehaviour {

    public float lightTimer;
    public Animator mineAnimator;
    public SpriteRenderer renderer;
    public Sprite redLight;
    public Sprite greenLight;
    private double timer;

	// Use this for initialization
	void Start () {
        renderer = this.GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;
        if (timer > lightTimer)
        {
            timer = 0;
            if (renderer.sprite == redLight)
            {
                renderer.sprite = greenLight;
            }

            else
            {
                renderer.sprite = redLight;
            }
        }

	}


}
