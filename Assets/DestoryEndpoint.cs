using UnityEngine;
using System.Collections;

public class DestoryEndpoint : MonoBehaviour {
	public GameObject endpoint;

	public void end_game()
	{
		Destroy (endpoint);
		GameManager.s_Inst.reactivate_zombies ();
	}
}
