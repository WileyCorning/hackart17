using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperienceManager : MonoBehaviour {
	public static ExperienceManager singleton;

	ControllerManager controllerManager;

	[SerializeField] Vector3 gabe_translation;
	[SerializeField] Vector3 forward_translation;
	[SerializeField] float lagDelay;

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
		} else if (Input.GetKeyDown (KeyCode.Backspace)) {
			SetStageIndex (0);
			return;
		}

		var s = Input.inputString;

		if (s.Length == 1) {
			int index;
			if (int.TryParse (s, out index) && index >= 1 && index < stages.Count) {
				SetStageIndex (index-1);
			}
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

		controllerManager.SetPrefab (stages [i].prefab);

		IOffsetStrategyFactory factory;
		switch (stages [i].strategy) {
		case OffsetStrategy.Translation_Forward:
			factory = new TranslationOffsetStrategyFactory (forward_translation);
			break;
		case OffsetStrategy.Translation_Gabe:
			factory = new TranslationOffsetStrategyFactory (gabe_translation);
			break;
		case OffsetStrategy.Wobble:
			factory = new WobbleOffsetStrategyFactory();
			break;
		case OffsetStrategy.Lag:
			factory = new LagOffsetStrategyFactory (lagDelay);
			break;
		default:
			factory = new IdentityOffsetStrategyFactory ();
			break;
		}
		controllerManager.SetStrategyFactory (factory);

		currentStage.Begin ();
	}
}
