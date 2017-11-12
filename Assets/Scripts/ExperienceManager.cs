using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperienceManager : MonoBehaviour {
	public static ExperienceManager singleton;

	ControllerManager controllerManager;

	[SerializeField] Vector3 gabe_translation;
	[SerializeField] Vector3 forward_translation;
	[SerializeField] float delay;

	[SerializeField] List<StageDescription> stages;

	Stage currentStage;
	int currentStageIndex = -1;

	void Awake() {
		singleton = this;
		controllerManager = new ControllerManager ();
	}

	void Update() {
		if (Input.GetKeyDown (KeyCode.Space)) {
			Advance ();
			return;
		}
		var s = Input.inputString;

		if (s.Length == 1) {
			int index;
			if (int.TryParse (s, out index) && index >= 0 && index < stages.Count) {
				SetStageIndex (index);
			}
		}

			

		if(Input.GetKeyDown(KeyCode.A)) {
			controllerManager.SetStrategyFactory (new IdentityOffsetStrategyFactory ());
		} 
		if(Input.GetKeyDown(KeyCode.B)) {
			controllerManager.SetStrategyFactory (new TranslationOffsetStrategyFactory (gabe_translation));
		}
		if(Input.GetKeyDown(KeyCode.C)) {
			controllerManager.SetStrategyFactory (new LagOffsetStrategyFactory (delay));
		}
		if(Input.GetKeyDown(KeyCode.D)) {
			controllerManager.SetStrategyFactory (new WobbleOffsetStrategyFactory ());
		}
	}

	void Advance() {
		
		if (currentStage == null) {
			NextStage ();
		} else {
			switch(currentStage.phase) {
			case Stage.Phase.Activity:
				currentStage.Conclude ();
				break;
			case Stage.Phase.Ended:
				NextStage ();
				break;
			}
		}
	}

	void NextStage() {
		var i = currentStageIndex + 1;
		i %= stages.Count;
		SetStageIndex (i);
	}

	void SetStageIndex(int i) {
		currentStageIndex = i;
		currentStage = stages [i].Create ();
		currentStage.Begin ();
	}
}
