using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerManager {
	public static ControllerManager singleton;

	bool controllersVisible = false;

	List<ControllerOffset> controllers = new List<ControllerOffset>();

	IOffsetStrategyFactory factory = new IdentityOffsetStrategyFactory();
	GameObject currentPrefab;

	public ControllerManager () {
		singleton = this;
	}



	public void SetPrefab(GameObject prefab) {
		currentPrefab = prefab;
		foreach (var controller in controllers) {
			controller.SetFoodPrefab (prefab);
		}
	}

	public void Register(ControllerOffset controller) {
		this.controllers.Add (controller);
		controller.SetStrategy (factory.Create (controller));
		controller.SetVisible (controllersVisible);
		if (currentPrefab != null) {
			controller.SetFoodPrefab (currentPrefab);
		}
	}

	public void SetStrategyFactory(IOffsetStrategyFactory factory) {
		this.factory = factory;

		foreach (var controller in controllers) {
			controller.SetStrategy (factory.Create (controller));
		}
	}

	public void SetControllersVisible(bool value) {
		controllersVisible = value;
		foreach (var controller in controllers) {
			controller.SetVisible (value);
		}
	}
}
