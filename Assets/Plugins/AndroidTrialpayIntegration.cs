using UnityEngine;
using System.Runtime.InteropServices;
using System;

#if UNITY_ANDROID
public class AndroidTrialpayIntegration : ITrialpayIntegration {
	 
	AndroidJavaClass trialpayManager = null;
	public AndroidTrialpayIntegration() {
		trialpayManager = new AndroidJavaClass("com.trialpay.android.unity.TrialpayManager"); 
	}
	
	Boolean initWithVic = false;
	Boolean enabledBalanceCheck = false;
	
	public string getSdkVersion() {
		return trialpayManager.CallStatic<string>("staticGetSdkVersion");
	}
	
	public void setVerbose(bool verbosity) {
		// do nothing on Android
	}

	public void setSid(string sid) {
		trialpayManager.CallStatic("staticSetSid", sid);
	}
	
	public void registerVic(string touchpointName, string vic) {
		trialpayManager.CallStatic("staticRegisterVic", touchpointName, vic);
		initWithVic = true;
	}
	
	public void registerUnitySendMessage(string objectName, string methodName) {
		trialpayManager.CallStatic("staticRegisterUnitySendMessage", objectName, methodName);
	}
	

	public void open(string touchpointName) {
		if (initWithVic) {
			trialpayManager.CallStatic("staticOpen", touchpointName);
		} else {
			throw new Exception("vic is not set in the Trialpay integration");
		}
	}

	public void open(string touchpointName, TrialpayViewMode mode) {
		if (initWithVic) {
			trialpayManager.CallStatic("staticOpen", touchpointName, (int)mode);
		} else {
			throw new Exception("vic is not set in the Trialpay integration");
		}
	}

	public void initiateBalanceChecks() {
		if (initWithVic) {
			trialpayManager.CallStatic("staticInitiateBalanceChecks");
			enabledBalanceCheck = true;
		} else {
			throw new Exception("VIC is not set in the Trialpay integration");
		}
	}

	public void enableBalanceCheck() {
		this.initiateBalanceChecks();
	}

	public int withdrawBalance(string touchpointName) {
		if (!enabledBalanceCheck) enableBalanceCheck();
		return trialpayManager.CallStatic<int>("staticWithdrawBalance", touchpointName);
	}
	
	public void startAvailabilityCheck(string touchpointName) {
		if (initWithVic) {
			trialpayManager.CallStatic("staticStartAvailabilityCheck", touchpointName);
		} else {
			throw new Exception("No VIC is set in the Trialpay integration");
		}
	}
	
	public bool isAvailable(string touchpointName) {
		if (initWithVic) {
			return trialpayManager.CallStatic<bool>("staticIsAvailable", touchpointName);
		} else {
			throw new Exception("No VIC is set in the Trialpay integration");
		}
	}
	
	public void openForTouchpoint(string touchpointName) {
		this.open(touchpointName);
	}
	
	public void openOfferwallForTouchpoint(string touchpointName) {
		this.open(touchpointName);
	}
	
	public void openOfferwall(string vic, string sid) {
		this.setSid(sid);
		this.registerVic(vic, vic);
		this.openOfferwallForTouchpoint(vic);
	}
	
	public void setAge(int age) {
		trialpayManager.CallStatic("staticSetAge", age);
	}
	
	public void setGender(char gender) {
		trialpayManager.CallStatic("staticSetGender", gender);
	}
	
	public void updateLevel(int level) {
		trialpayManager.CallStatic("staticUpdateLevel", level);
	}
	
	public void setCustomParam(string paramName, string paramValue) {
		trialpayManager.CallStatic("staticSetCustomParam", paramName, paramValue);
	}
	
	public void clearCustomParam(string paramName) {
		trialpayManager.CallStatic("staticClearCustomParam", paramName);
	}
	
	public void updateVcPurchaseInfo(string touchpointName, float dollarAmount, int vcAmount) {
		trialpayManager.CallStatic("staticUpdateVcPurchaseInfo", touchpointName, dollarAmount, vcAmount);
	}
	
	public void updateVcBalance(string touchpointName, int vcAmount) {
		trialpayManager.CallStatic("staticUpdateVcBalance", touchpointName, vcAmount);
	}
}

#endif
