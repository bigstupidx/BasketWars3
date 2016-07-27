using UnityEngine;
using System.Collections;

public class BulletController : MonoBehaviour {

	// Use this for initialization
	void Start () {
        StartCoroutine(BulletEverySecond());
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public IEnumerator BulletEverySecond()
    {
        while (true)
        {
            GameManager.s_Inst.add_bullet();
            yield return new WaitForSeconds(4f);
        }
    }
}
