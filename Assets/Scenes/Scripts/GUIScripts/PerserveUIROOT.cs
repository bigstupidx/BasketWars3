using UnityEngine;
using System.Collections;

public class PerserveUIROOT : MonoBehaviour {

    public static PerserveUIROOT s_Inst;

    void Awake()
    {
        if (s_Inst != null && s_Inst != this)
        { 
            Destroy(this.gameObject);
        }
        if (s_Inst == null)
        {
            s_Inst = this;
        }
        GameObject.DontDestroyOnLoad(this.gameObject);
    }

        // Use this for initialization
        void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
