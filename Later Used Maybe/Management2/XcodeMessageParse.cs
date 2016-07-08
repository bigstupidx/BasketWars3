using UnityEngine;
using System.Collections;

public class XcodeMessageParse : MonoBehaviour {

	public void RecievedMessageFromXcode(string message){
		if(message == "TestMessage"){
			Debug.Log("Got the message from Xcode");
		}
		else
			Debug.Log("Got a message but not what we expected!: " + message);
	}	
}
