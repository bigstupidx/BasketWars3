using UnityEngine;
using System.Collections;

public class SlideSelectItem : MonoBehaviour 
{
	public bool move_front = true;

	public void FlipBool()
	{
		move_front = !move_front;
	}
}
