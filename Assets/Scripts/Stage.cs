using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "StageDescriptor")]
public class StageDescription : ScriptableObject {
	[SerializeField] List<string> introTextLines;
	[SerializeField] AudioClip narration;

	public Stage Create() {
		return new Stage (introTextLines, narration);
	}
}


public class Stage {
	const float FADE_DURATION = 0.5f;

	public enum Phase {
		Initial,
		Starting,
		Activity,
		Concluding,
		Ended
	}

	List<string> introTextLines;
	AudioClip narration;

	public Phase phase { get; protected set; } = Phase.Initial;

	public Stage(List<string> introTextLines, AudioClip narration) {
		this.introTextLines = introTextLines;
		this.narration = narration;
	}

	public void Begin() {
		phase = Phase.Starting;
		ControllerManager.singleton.SetControllersVisible (false);
		ExperienceManager.singleton.StartCoroutine (DoIntroText (StartActivity));
	}

	IEnumerator DoIntroText(Action callback) {
		LineShower.singleton.Prepare ();
		SteamVR_Fade.Start(Color.clear, 0);
		foreach (var line in introTextLines) {
			LineShower.singleton.SetLine (line);
			yield return new WaitForSeconds (LineShower.singleton.lineDuration);
		}
		LineShower.singleton.Cleanup();
		callback();
	}

	void StartActivity() {
		phase = Phase.Activity;
		ControllerManager.singleton.SetControllersVisible (true);
	}

	public void Conclude() {
		phase = Phase.Concluding;
		ExperienceManager.singleton.StartCoroutine (DoConcludeActivity());

	}
	IEnumerator DoConcludeActivity() {
		SteamVR_Fade.Start (Color.black, FADE_DURATION);
		yield return new WaitForSeconds (FADE_DURATION);
		ControllerManager.singleton.SetControllersVisible (false);
		if (narration != null) {
			AudioPlayer.singleton.PlaySound (narration);
			yield return new WaitForSeconds (narration.length);
		}
		phase = Phase.Ended;
	}
}
