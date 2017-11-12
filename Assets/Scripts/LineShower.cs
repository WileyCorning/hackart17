using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineShower : MonoBehaviour {
	[SerializeField] TextMesh text;
	[SerializeField] Vector3 offset;

	public static LineShower singleton;

	void Awake() {
		singleton = this;
	}

	public void Prepare() {
		text.text = "";
		var head = CameraRigEye.singleton.transform;
		transform.SetPositionAndRotation (head.position + head.TransformVector (offset), Quaternion.LookRotation (Vector3.ProjectOnPlane(head.forward,Vector3.up)));
	}

	public void SetLine(string line) {
		text.gameObject.SetActive (true);
		text.text = line;
	}

	public void Cleanup() {
		text.text = "";
		text.gameObject.SetActive (false);
	}
}
