using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class PlayAudioClip : MonoBehaviour {
	AudioClip m_first_clip;
	public AudioClip m_alt_clip;
	float m_first_volume;
	public float m_alt_volume;

	void Start(){
		m_first_clip = GetComponent<AudioSource>().clip;
		m_first_volume = GetComponent<AudioSource>().volume;
	}

	public void PlayAudio(){
		GetComponent<AudioSource>().clip = m_first_clip;
		GetComponent<AudioSource>().volume = m_first_volume;
		gameObject.GetComponent<AudioSource>().Play();
	}

	public void PlayAltAudio(){
		GetComponent<AudioSource>().clip = m_alt_clip;
		GetComponent<AudioSource>().volume = m_alt_volume;
		gameObject.GetComponent<AudioSource>().Play();
	}
}
