﻿using UnityEngine;
using System.Collections;

public class Destroyexplosion : MonoBehaviour 
{
	public void DestoryThis()
	{
		Destroy (this.gameObject);
		GameManager.s_Inst.killAllZombies ();
	}
}
