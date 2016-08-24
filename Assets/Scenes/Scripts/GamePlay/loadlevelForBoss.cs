using UnityEngine;
using System.Collections;

public class loadlevelForBoss : MonoBehaviour {

	public void LoadBritainBoss(){
		Application.LoadLevel("Britain11");	
	}

	public void LoadStalingradBoss(){
		Application.LoadLevel("Stalingrad11");	
	}

	public void LoadKurskBoss(){
		Application.LoadLevel("Kursk11");	
	}

	public void LoadNormandyBoss(){
		Application.LoadLevel("Normandy11");	
	}
}
