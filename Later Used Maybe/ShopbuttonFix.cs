using UnityEngine;
using System.Collections;

public class ShopbuttonFix : MonoBehaviour {	
	void OnLevelWasLoaded()
	{
		gameObject.GetComponent<UIButton>().onClick.Clear();
		gameObject.GetComponent<UIButton>().onClick.Add(new EventDelegate(GameManager.s_Inst.gameObject.GetComponent<ShopMenu>(),"MoveInMainshopPanel"));

	}
}
