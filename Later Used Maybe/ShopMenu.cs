using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ShopMenu : MonoBehaviour{
	public UIPanel m_coins_panel;
	public UIPanel m_message_panel;
	public UILabel m_coins_label;
	public UIButton m_paid_refill;
	public UIButton m_free_refill;
	static bool m_can_make_payments;
	public AudioClip m_deposit_coins_sound;
	public AudioClip m_withdraw_coins_sound;
	string[] m_product_ids = {"coin_bundle_1","coin_bundle_2","coin_bundle_3","coin_bundle_4","coin_bundle_5","coin_bundle_6","coin_bundle_7","stamina_refill1","unlock_Stalingrad","unlock_Kursk01","unlock_Normandy","unlimited_stamina"};
	List<StoreKitTransaction> m_trasnactions_list = new List<StoreKitTransaction>();

	void Start(){
		m_can_make_payments = StoreKitBinding.canMakePayments();
		StoreKitBinding.requestProductData(m_product_ids);
	}
	
	//Coins Panel
	public void MoveInCoinsPanel(){
		m_coins_panel.GetComponent<TweenPosition>().PlayForward();
	}
	
	public void MoveOutCoinsPanel(){
		m_coins_panel.GetComponent<TweenPosition>().PlayReverse();
	}
	
#region IAP
	public void BuyCoinBundle(int bundle_num){
		if(m_can_make_payments){
			if(bundle_num == 1){
				StoreKitBinding.purchaseProduct("coin_bundle_1",1);
				m_message_panel.GetComponent<TweenPosition>().PlayForward();
			}
			if(bundle_num == 2){
				StoreKitBinding.purchaseProduct("coin_bundle_2",1);
				m_message_panel.GetComponent<TweenPosition>().PlayForward();
			}
			if(bundle_num == 3){
				StoreKitBinding.purchaseProduct("coin_bundle_3",1);
				m_message_panel.GetComponent<TweenPosition>().PlayForward();
			}
			if(bundle_num == 4){
				StoreKitBinding.purchaseProduct("coin_bundle_4",1);
				m_message_panel.GetComponent<TweenPosition>().PlayForward();
			}
			if(bundle_num == 5){
				StoreKitBinding.purchaseProduct("coin_bundle_5",1);
				m_message_panel.GetComponent<TweenPosition>().PlayForward();
			}
			if(bundle_num == 6){
				StoreKitBinding.purchaseProduct("coin_bundle_6",1);
				m_message_panel.GetComponent<TweenPosition>().PlayForward();
			}
			if(bundle_num == 7){
				StoreKitBinding.purchaseProduct("coin_bundle_7",1);
				m_message_panel.GetComponent<TweenPosition>().PlayForward();
			}
		}
	}


	public void BuyStaminaRefill(){
		StoreKitBinding.purchaseProduct("stamina_refill1",1);
		m_message_panel.GetComponent<TweenPosition>().PlayForward();
	}

	public void BuyUnlimitedStamina(){
		StoreKitBinding.purchaseProduct("unlimited_stamina",1);
		m_message_panel.GetComponent<TweenPosition>().PlayForward();
	}

	public void BuyStalingradUnlock(){
		StoreKitBinding.purchaseProduct("unlock_Stalingrad",1);
		m_message_panel.GetComponent<TweenPosition>().PlayForward();
	}

	public void BuyKurskUnlock(){
		StoreKitBinding.purchaseProduct("unlock_Kursk01",1);
		m_message_panel.GetComponent<TweenPosition>().PlayForward();
	}

	public void BuyNormandyUnlock(){
		StoreKitBinding.purchaseProduct("unlock_Normandy",1);
		m_message_panel.GetComponent<TweenPosition>().PlayForward();
	}

	public void BuyStaminaRefillCoins(){
		if(SaveLoadManager.m_save_info.m_coins >= 1000){
			GameManager.s_Inst.RemoveCoins(1000);
			GameManager.s_Inst.gameObject.GetComponent<StaminaGuage>().IncreaseStamina(10);
			if(GameManager.s_Inst.m_waiting_for_stamina_refill){
				GameManager.s_Inst.m_waiting_for_stamina_refill = false;
				GameManager.s_Inst.m_level_name_to_load = Application.loadedLevelName;
				Application.LoadLevelAsync("LevelLoader");
			}
		}
		else
			GameManager.s_Inst.gameObject.GetComponent<ShopMenu>().MoveInCoinsPanel();
	}

	public void AddTransaction(StoreKitTransaction trans){
		m_trasnactions_list.Add(trans);
	}

	public void ConfirmPurchase(StoreKitTransaction trans){
		StoreKitTransaction temp = new StoreKitTransaction();
		int i = 0;
		foreach(StoreKitTransaction t in m_trasnactions_list)
		{
			if(t.transactionIdentifier == trans.transactionIdentifier){
				temp = t;
				continue;
			}
			i++;
		}
		if(temp.transactionIdentifier != "" && temp.transactionIdentifier != null){
			ProcessCompleteTransaction(temp.productIdentifier);
			m_trasnactions_list.RemoveAt(i);
		}
	}

	public void PurchaseFailed()
	{
		GameObject.Find("MessageFailed").GetComponent<TweenPosition>().ResetToBeginning();
		GameObject.Find("MessageFailed").GetComponent<TweenPosition>().PlayForward();
	}

	public void ProcessCompleteTransaction(string productID){
		Debug.Log("Move On");
		GameObject.Find("MessageSuccess").GetComponent<TweenPosition>().ResetToBeginning();
		GameObject.Find("MessageSuccess").GetComponent<TweenPosition>().PlayForward();
		if(productID == "coin_bundle_1" || productID == "coin_bundle_11"){
			GameManager.s_Inst.AddCoins(1000);
		}
		else if(productID == "coin_bundle_2" || productID == "coin_bundle_22"){
			GameManager.s_Inst.AddCoins(2250);
		}
		else if(productID == "coin_bundle_3" || productID == "coin_bundle_33"){
			GameManager.s_Inst.AddCoins(5750);
		}
		else if(productID == "coin_bundle_4" || productID == "coin_bundle_44"){
			GameManager.s_Inst.AddCoins(8250);
		}
		else if(productID == "coin_bundle_5" || productID == "coin_bundle_55"){
			GameManager.s_Inst.AddCoins(12000);
		}
		else if(productID == "coin_bundle_6" || productID == "coin_bundle_66"){
			GameManager.s_Inst.AddCoins(20000);
		}
		else if(productID == "coin_bundle_7" || productID == "coin_bundle_77"){
			GameManager.s_Inst.AddCoins(28000);
		}
		else if(productID == "stamina_refill" || productID == "stamina_refill1"){
			GameManager.s_Inst.gameObject.GetComponent<StaminaGuage>().IncreaseStamina(10);
			if(GameManager.s_Inst.m_waiting_for_stamina_refill){
				GameManager.s_Inst.m_waiting_for_stamina_refill = false;
				GameManager.s_Inst.m_level_name_to_load = Application.loadedLevelName;
				Application.LoadLevelAsync("LevelLoader");
			}
		}
		else if(productID == "unlimited_stamina"){
			Adjust.trackEvent("an0ioz");
			FlurryAnalytics.logEvent("Purchased unlimited stamina",false);
			GameManager.s_Inst.m_unlimited_stamina = true;
		}
		else if(productID == "unlock_Stalingrad" || productID == "unlock_Stalingrad1"){
			GameManager.s_Inst.gameObject.GetComponent<StageUnlocker>().m_Stalingrad_purchased = true;
			GameManager.s_Inst.gameObject.GetComponent<StageUnlocker>().ShowStalingrad();
			GameManager.s_Inst.SaveIAPItems();
			GameObject.Find("UnlockLevel").GetComponent<TweenPosition>().PlayReverse();
			GameObject.Find("BlackoutPanel").GetComponent<BlackoutPanel>().MoveOut();
		}
		else if(productID == "unlock_Kursk01" || productID == "unlock_Kursk1"){
			GameManager.s_Inst.gameObject.GetComponent<StageUnlocker>().m_Kursk_purchased = true;
			GameManager.s_Inst.gameObject.GetComponent<StageUnlocker>().ShowKursk();
			GameManager.s_Inst.SaveIAPItems();	
			GameObject.Find("UnlockLevel").GetComponent<TweenPosition>().PlayReverse();
			GameObject.Find("BlackoutPanel").GetComponent<BlackoutPanel>().MoveOut();
		}
		else if(productID == "unlock_Normandy" || productID == "unlock_Normandy1"){
			GameManager.s_Inst.gameObject.GetComponent<StageUnlocker>().m_Normandy_purchased = true;
			GameManager.s_Inst.gameObject.GetComponent<StageUnlocker>().ShowNormandy();
			GameManager.s_Inst.SaveIAPItems();
			GameObject.Find("UnlockLevel").GetComponent<TweenPosition>().PlayReverse();
			GameObject.Find("BlackoutPanel").GetComponent<BlackoutPanel>().MoveOut();
		}
		m_message_panel.GetComponent<TweenPosition>().PlayReverse();
		GetComponent<AudioSource>().clip = m_deposit_coins_sound;
		GetComponent<AudioSource>().Play();
	}

	public void WithdrawCoins(){
		GetComponent<AudioSource>().clip = m_withdraw_coins_sound;
		GetComponent<AudioSource>().Play();	
	}

#endregion
	
	public void UpdateLabels()
	{
		if(m_coins_label == null)
			m_coins_label = GameObject.Find("SecondaryCoinLabel").GetComponent<UILabel>();
		m_coins_label.text = string.Format("{0:n0}", SaveLoadManager.m_save_info.m_coins);
	}
}
