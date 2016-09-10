using UnityEngine;
using System.Collections;

public class BulletController : MonoBehaviour {

	// Update is called once per frame
	void Update () {
		GameManager.s_Inst.add_bullet (Time.deltaTime / 4);
	}
}
