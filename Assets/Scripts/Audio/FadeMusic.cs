using UnityEngine;
using System.Collections;

public class FadeMusic : MonoBehaviour {

	float obj_volume;
	public float fade_speed = 1;
	public AudioSource audio_target;
	bool start_fade = false;

	void Start(){
		obj_volume = audio_target.volume;
	}

	void Update(){
		if(obj_volume > 0 && audio_target != null && start_fade == true)
		{
			obj_volume -= fade_speed * Time.deltaTime;
			audio_target.volume = obj_volume;
		}
	}

	public void fadeOutMusic() {
		start_fade = true;
	}

}
