using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdentityOffsetStrategy : IOffsetStrategy {
	Transform pivot;
	public IdentityOffsetStrategy(Transform pivot) {
		this.pivot = pivot;
	}

	public void ApplyOffset() {
		pivot.localPosition = Vector3.zero;
	}
}
public class IdentityOffsetStrategyFactory : IOffsetStrategyFactory {
	public IOffsetStrategy Create(ControllerOffset controller) {
		return new IdentityOffsetStrategy (controller.pivot);
	}
}