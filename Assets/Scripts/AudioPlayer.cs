using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour {
	public static AudioPlayer singleton;
	AudioSource source;

	void Awake() {
		singleton = this;
		source = GetComponent<AudioSource> ();
	}

	public void PlaySound(AudioClip clip) {
		source.clip = clip;
		source.Play ();
	}
}
