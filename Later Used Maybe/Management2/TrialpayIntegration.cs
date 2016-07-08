using UnityEngine;
using System.Runtime.InteropServices;
using System;

/*!
    MonoBehaviour to create appropriate native package (Android/iOS).
 */
public class TrialpayIntegration: MonoBehaviour {

	private static ITrialpayIntegration instance = null;
	public static string m_vic = "d0aaa792e2818dc6b183cde2d0747995";

/*!
     Get the Trialpay Manager instance.
     @returns The Trialpay Manager instance.
 */
	public static ITrialpayIntegration getInstance() {
		if (null == instance) {
			#if UNITY_IPHONE
				instance = new IosTrialpayIntegration();
			#elif UNITY_ANDROID
				instance = new AndroidTrialpayIntegration();
			#else
				throw new Exception ("Trialpay Integration Supports Android and iOS only");
			#endif
		}
		return instance;
	}

	public static void Init(string userid){
		getInstance().setSid(userid);	
		getInstance().registerVic("Coins",m_vic);
		getInstance().enableBalanceCheck();
		getInstance().registerUnitySendMessage("MessageReciever","TrialpayMessage");
		SaveLoadManager.m_save_info.m_coins += instance.withdrawBalance("Coins");
	}

/*!
    Open the Trialpay Offer Wall for a given touchpoint.
    @param vic The campaign identifier.
    @param sid The device user identifier.
    @deprecated use ITrialpayIntegration.openForTouchpoint()
*/
	public static void openOfferwall(string vic, string sid) {
		ITrialpayIntegration trialpayIntegration = TrialpayIntegration.getInstance();
		trialpayIntegration.openOfferwall(vic, sid);
	}

	public void StartOfferWall(){
		#if UNITY_IPHONE
		instance.openOfferwallForTouchpoint("Coins");
		#endif
	}

	public void TrialpayMessage(string message){
		Debug.Log("Message recieved: " + message);
		switch (message) {
		case "offerwall_close":
			Debug.Log("Closed the offer wall");
			break;
		case "balance_update": // Set this only if you set your product to work with the TrialPay's Balance solution.
			Debug.Log("Balance update");
			GameManager.s_Inst.AddCoins(instance.withdrawBalance("Coins")); // Use the same currency name you used in registerVic
			// Store credit and display message
			break;			
		}
	}

}
