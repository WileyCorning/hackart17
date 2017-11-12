using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TranslationOffsetStrategy : IOffsetStrategy {
	Transform visible;
	Transform actual;
	Transform head;
	Vector3 translation;
	public TranslationOffsetStrategy(Transform visible, Transform actual, Transform head, Vector3 translation) {
		this.visible = visible;
		this.actual = actual;
		this.head = head;
		this.translation = translation;
	}

	public void ApplyOffset() {
		visible.transform.SetPositionAndRotation (actual.position + head.TransformVector (translation), actual.rotation);
	}
}
public class TranslationOffsetStrategyFactory : IOffsetStrategyFactory {
	Vector3 translation;

	public TranslationOffsetStrategyFactory(Vector3 translation) {
		this.translation = translation;
	}

	public IOffsetStrategy Create(ControllerOffset controller) {
		return new TranslationOffsetStrategy (controller.transform, controller.actual, controller.head, translation);
	}
}