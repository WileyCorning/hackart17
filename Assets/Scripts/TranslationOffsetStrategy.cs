using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TranslationOffsetStrategy : IOffsetStrategy {
	Transform pivot;
	Transform head;
	Vector3 translation;
	public TranslationOffsetStrategy(Transform pivot, Transform head, Vector3 translation) {
		this.pivot = pivot;
		this.head = head;
		this.translation = translation;
	}

	public void ApplyOffset() {
		pivot.localPosition = head.TransformVector (translation);
	}
}
public class TranslationOffsetStrategyFactory : IOffsetStrategyFactory {
	Vector3 translation;

	public TranslationOffsetStrategyFactory(Vector3 translation) {
		this.translation = translation;
	}

	public IOffsetStrategy Create(ControllerOffset controller) {
		return new TranslationOffsetStrategy (controller.pivot, controller.head, translation);
	}
}