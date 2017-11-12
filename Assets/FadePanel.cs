using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadePanel : MonoBehaviour {
	Coroutine fadeCoroutine;
	Material mat;
	[SerializeField] Renderer r;

	public static FadePanel singleton;

	void Awake() {
		singleton = this;
	}

	void Start() {
		mat = new Material (r.sharedMaterial);
		r.sharedMaterial = mat;
	}

	public void Fade(float from, float to, float duration) {
		if (fadeCoroutine != null) {
			StopCoroutine (fadeCoroutine);
		}
		fadeCoroutine = StartCoroutine (DoFade (from, to, duration));
	}

	IEnumerator DoFade(float from, float to, float duration) {
		float stop = Time.time + duration;
		while (Time.time < stop) {
			SetOpacity (Mathf.Lerp (to,from,(stop - Time.time)/duration));
			yield return new WaitForEndOfFrame ();
		}
		SetOpacity (to);
	}

	void SetOpacity(float a) {
		r.enabled = a > 0;
		mat.SetFloat ("_Opacity", a);
	}
	float GetOpacity() {
		return mat.color.a;
	}
}
