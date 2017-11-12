using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineShower : MonoBehaviour {
	[SerializeField] TextMesh text;
	[SerializeField] float distance;
	[SerializeField] AudioClip boom;
	public float lineDuration = 0.5f;

	public static LineShower singleton;

	void Awake() {
		singleton = this;
	}

	public void Prepare() {
		text.text = "";
		var head = CameraRigEye.singleton.transform;
		Vector3 forward = Vector3.ProjectOnPlane (head.forward, Vector3.up).normalized;


		transform.SetPositionAndRotation (head.position + forward*distance, Quaternion.LookRotation (forward));
	}

	public void SetLine(string line) {
		text.gameObject.SetActive (true);
		AudioPlayer.singleton.PlaySound (boom);
		text.text = line;
	}

	public void Cleanup() {
		text.text = "";
		text.gameObject.SetActive (false);
	}
}
