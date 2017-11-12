using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperienceManager : MonoBehaviour {
	public static ExperienceManager singleton;

	ControllerManager controllerManager;

	[SerializeField] Vector3 p2_translation;

	void Awake() {
		singleton = this;
		controllerManager = new ControllerManager ();
	}

	void Update() {
		if(Input.GetKeyDown(KeyCode.Alpha1)) {
			controllerManager.SetStrategyFactory (new IdentityOffsetStrategyFactory ());
		} 
		if(Input.GetKeyDown(KeyCode.Alpha2)) {
			controllerManager.SetStrategyFactory (new TranslationOffsetStrategyFactory (p2_translation));
		}
	}
}
