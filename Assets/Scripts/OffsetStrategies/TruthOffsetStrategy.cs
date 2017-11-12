using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TruthOffsetStrategy : IOffsetStrategy {
	Transform visible;
	Transform actual;
	public TruthOffsetStrategy(Transform visible, Transform actual) {
		this.visible = visible;
		this.actual = actual;
	}

	public void ApplyOffset() {
		visible.SetPositionAndRotation (actual.position, actual.rotation);
	}
}
public class IdentityOffsetStrategyFactory : IOffsetStrategyFactory {
	public IOffsetStrategy Create(ControllerOffset controller) {
		return new TruthOffsetStrategy (controller.transform, controller.actual);
	}
}