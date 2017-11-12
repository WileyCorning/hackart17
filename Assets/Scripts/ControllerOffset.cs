using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerOffset : MonoBehaviour {
	public enum Handedness { None, Left, Right }

	[SerializeField] Handedness handednessIndicator;
	public Handedness handedness { get { return handednessIndicator; } }

	public Transform head { get { return CameraRigEye.singleton.transform; } }

	[SerializeField] Transform controllerActual;
	public Transform actual { get { return controllerActual; } }

	[SerializeField] SteamVR_RenderModel renderModel;
	[SerializeField] Transform forkTip;
	public Transform fork { get { return forkTip; } }

	[SerializeField] Transform modelAttach;

	GameObject currentFood;


	IOffsetStrategy strategy;

	IEnumerator Start() {
		this.strategy = new TruthOffsetStrategy (transform,actual);
		ControllerManager.singleton.Register (this);

		// Wait for rendermodel to populate
		while (renderModel.transform.Find ("body") == null) {
			yield return new WaitForEndOfFrame ();
		}

		// Move rendermodel here
		renderModel.transform.SetParent (modelAttach);
		renderModel.transform.localPosition = Vector3.zero;
		renderModel.transform.localRotation = Quaternion.identity;
	}

	void FixedUpdate() {
		if (!actual.gameObject.activeSelf) {
			transform.position = new Vector3 (0, -10, 0);
		} else {
			strategy?.ApplyOffset ();
		}
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

	public void SetVisible(bool value) {
		modelAttach.gameObject.SetActive (value);
	}
}
