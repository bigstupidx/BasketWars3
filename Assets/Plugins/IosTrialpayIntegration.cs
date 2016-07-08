using UnityEngine;
using System.Runtime.InteropServices;
using System;

#if UNITY_IPHONE
public class IosTrialpayIntegration : ITrialpayIntegration
{
	
	[DllImport("__Internal")]
	private static extern string _getSdkVersion ();

	[DllImport("__Internal")]
	private static extern void  _setVerbose(bool verbose);

	[DllImport("__Internal")]
	private static extern void  _setSid (string sid);
	
	[DllImport("__Internal")]
	private static extern void  _registerVic (string touchpointName, string vic);
	
	[DllImport("__Internal")]
	private static extern void  _registerUnitySendMessage (string objectName, string methodName);
	
	[DllImport("__Internal")]
	private static extern void  _initiateBalanceChecks ();
	
	[DllImport("__Internal")]
	private static extern void  _startAvailabilityCheck (string touchpointName);
	
	[DllImport("__Internal")]
	private static extern bool  _isAvailable (string touchpointName);
	
	[DllImport("__Internal")]
	private static extern void  _open (string touchpointName);
	
	[DllImport("__Internal")]
	private static extern void  _openWithMode (string touchpointName, int mode);
	
	[DllImport("__Internal")]
	private static extern int  _withdrawBalance (string touchpointName);
	
	[DllImport("__Internal")]
	private static extern void _setAge(int age);
	
	[DllImport("__Internal")]
	private static extern void _setGender (char gender);
	
	[DllImport("__Internal")]
	private static extern void _updateLevel (int level);
	
	[DllImport("__Internal")]
	private static extern void _updateVcPurchaseInfo (string touchpointName, float dollarAmount, int vcAmount);
	
	[DllImport("__Internal")]
	private static extern void _updateVcBalance (string touchpointName, int vcAmount);
	
	[DllImport("__Internal")]
	private static extern void _setCustomParam (string paramName, string paramValue);
	
	[DllImport("__Internal")]
	private static extern void _clearCustomParam (string paramName);
	
	public IosTrialpayIntegration () {
		
	}
	
	Boolean initWithVic = false;
	Boolean enabledBalanceCheck = false;
	
	public string getSdkVersion() {
		return _getSdkVersion();
	}
	
	public void setVerbose(bool verbose) {
	       _setVerbose(verbose);
 	}

	public void setSid(string sid) {
		_setSid(sid);
	}
	
	public void registerVic(string touchpointName, string vic) {
		_registerVic(touchpointName, vic);
		initWithVic = true;
	}
	
	public void registerUnitySendMessage(string objectName, string methodName) {
		_registerUnitySendMessage(objectName, methodName);
	}
	
	public void open(string touchpointName) {
		if (initWithVic) {
			_open(touchpointName);
		} else {
			throw new Exception("VIC is not set in the Trialpay integration");
		}
	}
	
	public void open(string touchpointName, TrialpayViewMode mode) {
		if (initWithVic) {
			_openWithMode(touchpointName, (int)mode);
		} else {
			throw new Exception("VIC is not set in the Trialpay integration");
		}
	}
	
	public void initiateBalanceChecks() {
		if (initWithVic) {
			_initiateBalanceChecks();
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
		return _withdrawBalance(touchpointName);
	}
	
	public void startAvailabilityCheck(string touchpointName) {
		if (initWithVic) {
			_startAvailabilityCheck(touchpointName);
		} else {
			throw new Exception("No VIC is set in the Trialpay integration");
		}
	}

	
	public bool isAvailable(string touchpointName) {
		if (initWithVic) {
			return _isAvailable(touchpointName);
		} else {
			throw new Exception("No VIC is set in the Trialpay integration");
		}
	}
	
	public void openForTouchpoint(string touchpointName) {
		this.open(touchpointName);
	}
	
	public void openOfferwallForTouchpoint(string touchpointName) {
		this.openForTouchpoint(touchpointName);
	}
	
	public void openOfferwall(string vic, string sid) {
		this.setSid(sid);
		this.registerVic(vic, vic);
		this.openOfferwallForTouchpoint(vic);
	}
	
	public void setAge(int age) {
		_setAge(age);
	}
	
	public void setGender(char gender) {
		_setGender(gender);
	}
	
	public void updateLevel(int level) {
		_updateLevel(level);
	}
	
	public void setCustomParam(string paramName, string paramValue) {
		_setCustomParam(paramName, paramValue);
	}
	
	public void clearCustomParam(string paramName) {
		_clearCustomParam(paramName);
	}
	
	public void updateVcPurchaseInfo(string touchpointName, float dollarAmount, int vcAmount) {
		_updateVcPurchaseInfo(touchpointName, dollarAmount, vcAmount);
	}
	
	public void updateVcBalance(string touchpointName, int vcAmount) {
		_updateVcBalance(touchpointName, vcAmount);
	}
}
#endif
