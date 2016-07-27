using UnityEngine;
using System.Collections;

public class AlignJeepStop : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Transform soldier = GameObject.Find("Soldier").transform;
		Transform jeep = GameObject.Find("jeep_frame").transform;
		if(Application.loadedLevelName == "Stalingrad08" || Application.loadedLevelName == "Stalingrad09" || Application.loadedLevelName == "Stalingrad10"){ // Need special logic to get the jeep to not look like its floating!
			transform.position = new Vector3(soldier.position.x + 0.75f,soldier.position.y,0);
			jeep.position = new Vector3(soldier.position.x - 7,soldier.position.y, 0);
			if(!(Application.loadedLevelName == "Stalingrad10")) //Don't want to parent the jeep on level 10 but we still want it to be at the correct height.
				jeep.GetComponent<Reenforcments>().m_is_on_building = true;
		}else{
			transform.position = new Vector3(soldier.position.x + 0.75f,soldier.position.y,0);
			jeep.position = new Vector3(soldier.position.x - 7,soldier.position.y + 0.5f, 0);
		}
	}
}
