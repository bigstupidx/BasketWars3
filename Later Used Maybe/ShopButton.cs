using UnityEngine;
using System.Collections;

public class ShopButton : MonoBehaviour {

	public enum ButtonType{
		Coins,
		Nuke,
		Armor,
		Arc,
		Focus
	};
	
	public ButtonType m_button_type;
	public int m_cost;
	public int m_value;

	void OnClick(){
		if(m_button_type == ButtonType.Arc)
			AddArc(m_cost);
		else if(m_button_type == ButtonType.Armor)
			AddArmor(m_cost);
		else if(m_button_type == ButtonType.Nuke)
			AddNuke(m_cost);
		else if(m_button_type == ButtonType.Focus)
			AddFocus(m_cost);
		else if(m_button_type == ButtonType.Coins)
			GameManager.s_Inst.gameObject.GetComponent<ShopMenu>().BuyCoinBundle(m_value); //special case to but coins. Need to pass what # bundle they want.
	}

	bool CheckCoins(int coins){
		if(coins <= SaveLoadManager.m_save_info.m_coins){
			return true;
		}
		else{
			//Pop Up coin shop
			return false;
		}
	}

	void RemoveCoins(int coins){
		GameManager.s_Inst.RemoveCoins(coins);
	}
	
	void AddNuke(int cost){
		if(CheckCoins(cost)){
			GameManager.s_Inst.GetComponent<ShopMenu>().WithdrawCoins();
			GameObject.Find("MessageSuccess").GetComponent<TweenPosition>().ResetToBeginning();
			GameObject.Find("MessageSuccess").GetComponent<TweenPosition>().PlayForward();
			SaveLoadManager.m_save_info.m_nuke_powerup++;
			RemoveCoins(cost);
			GameManager.s_Inst.gameObject.GetComponent<ShopMenu>().UpdateLabels();
			PowerupEquipper.Init();
		}
		else{
			GameManager.s_Inst.gameObject.GetComponent<ShopMenu>().MoveInCoinsPanel();
			GameObject.Find("MessageFailed").GetComponent<TweenPosition>().ResetToBeginning();
			GameObject.Find("MessageFailed").GetComponent<TweenPosition>().PlayForward();
		}	
	}
	
	void AddArmor(int cost){
		if(CheckCoins(cost)){
			GameManager.s_Inst.GetComponent<ShopMenu>().WithdrawCoins();
			GameObject.Find("MessageSuccess").GetComponent<TweenPosition>().ResetToBeginning();
			GameObject.Find("MessageSuccess").GetComponent<TweenPosition>().PlayForward();
			SaveLoadManager.m_save_info.m_armor_powerup++;
			RemoveCoins(cost);
			GameManager.s_Inst.gameObject.GetComponent<ShopMenu>().UpdateLabels();
			PowerupEquipper.Init();
		}
		else{
			GameManager.s_Inst.gameObject.GetComponent<ShopMenu>().MoveInCoinsPanel();
			GameObject.Find("MessageFailed").GetComponent<TweenPosition>().ResetToBeginning();
			GameObject.Find("MessageFailed").GetComponent<TweenPosition>().PlayForward();
		}
	}

	void AddArc(int cost){
		if(CheckCoins(cost)){
			GameManager.s_Inst.GetComponent<ShopMenu>().WithdrawCoins();
			GameObject.Find("MessageSuccess").GetComponent<TweenPosition>().ResetToBeginning();
			GameObject.Find("MessageSuccess").GetComponent<TweenPosition>().PlayForward();
			SaveLoadManager.m_save_info.m_guide_powerup++;
			RemoveCoins(cost);
			GameManager.s_Inst.gameObject.GetComponent<ShopMenu>().UpdateLabels();
			PowerupEquipper.Init();
		}
		else{
			GameManager.s_Inst.gameObject.GetComponent<ShopMenu>().MoveInCoinsPanel();
			GameObject.Find("MessageFailed").GetComponent<TweenPosition>().ResetToBeginning();
			GameObject.Find("MessageFailed").GetComponent<TweenPosition>().PlayForward();
		}
	}

	void AddFocus(int cost){
		if(CheckCoins(cost)){
			GameManager.s_Inst.GetComponent<ShopMenu>().WithdrawCoins();
			GameObject.Find("MessageSuccess").GetComponent<TweenPosition>().ResetToBeginning();
			GameObject.Find("MessageSuccess").GetComponent<TweenPosition>().PlayForward();
			SaveLoadManager.m_save_info.m_focus_powerup++;
			RemoveCoins(cost);
			GameManager.s_Inst.gameObject.GetComponent<ShopMenu>().UpdateLabels();
			PowerupEquipper.Init();
		}
		else{
			GameManager.s_Inst.gameObject.GetComponent<ShopMenu>().MoveInCoinsPanel();
			GameObject.Find("MessageFailed").GetComponent<TweenPosition>().ResetToBeginning();
			GameObject.Find("MessageFailed").GetComponent<TweenPosition>().PlayForward();
		}
	}
}
