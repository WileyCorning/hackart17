using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerManager {
	public static ControllerManager singleton;

	List<ControllerOffset> controllers = new List<ControllerOffset>();


	IOffsetStrategyFactory factory = new IdentityOffsetStrategyFactory();

	public ControllerManager () {
		singleton = this;
	}

	public void Register(ControllerOffset controller) {
		this.controllers.Add (controller);
		controller.SetStrategy (factory.Create (controller));
	}

	public void SetStrategyFactory(IOffsetStrategyFactory factory) {
		this.factory = factory;

		foreach (var controller in controllers) {
			controller.SetStrategy (factory.Create (controller));
		}
	}
}
