using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerOffset : MonoBehaviour {
	[SerializeField] Transform forkTip;
	GameObject currentFood;

	public Transform head { get { return CameraRigEye.singleton.transform; } }
	public Transform pivot { get { return transform; } }

	IOffsetStrategy strategy;

	void Start() {
		this.strategy = new IdentityOffsetStrategy (transform);
		ControllerManager.singleton.Register (this);
	}

	void FixedUpdate() {
		strategy?.ApplyOffset ();
	}

	public void SetStrategy(IOffsetStrategy strategy) {
		this.strategy = strategy;
	}

	public void SetFoodPrefab(GameObject prefab) {
		if (currentFood != null) {
			Destroy (currentFood);
		}
		currentFood = Instantiate (prefab, forkTip);
	}

	public void ClearFoodPrefab() {
		if (currentFood != null) {
			Destroy (currentFood);
			currentFood = null;
		}
	}
}
