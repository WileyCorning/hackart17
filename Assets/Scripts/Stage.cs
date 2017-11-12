using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Stage {
	const float FADE_DURATION = 0.5f;

	public enum Phase {
		Initial,
		Starting,
		Activity,
		Concluding,
		Ended
	}

	IList<string> introTextLines;
	AudioClip narration;

	public Phase phase { get; protected set; } = Phase.Initial;

	public Stage(IList<string> introTextLines, AudioClip narration) {
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
//		FadePanel.singleton.Fade (1,0,0);
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
//		FadePanel.singleton.Fade (0,1, FADE_DURATION);
		yield return new WaitForSeconds (FADE_DURATION);
		ControllerManager.singleton.SetControllersVisible (false);
		if (narration != null) {
			AudioPlayer.singleton.PlaySound (narration);
			yield return new WaitForSeconds (narration.length);
		}
		phase = Phase.Ended;
	}
}
