using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WobbleOffsetStrategy : IOffsetStrategy {
	Transform visible;
	Transform actual;
	Transform head;
	Transform fork;

	float rapidity = 5;
	float range = 0.5f;
	float maxAngle = 60;

	public WobbleOffsetStrategy(Transform visible, Transform actual, Transform head, Transform fork) {
		this.visible = visible;
		this.actual = actual;
		this.head = head;
		this.fork = fork;
	}

	public void ApplyOffset() {
		Quaternion r = actual.rotation;

		float distance = Vector3.Distance (fork.position, head.position);


		float closeness = Mathf.Pow(1-Mathf.Clamp01(distance/range),1);

		float pitch = -maxAngle * Mathf.Cos (distance*rapidity) * closeness;
		float yaw = maxAngle * Mathf.Cos (distance*rapidity) * closeness;

		visible.transform.SetPositionAndRotation (actual.position, r * Quaternion.AngleAxis (yaw, Vector3.up) * Quaternion.AngleAxis (pitch, Vector3.right));
	}
}
public class WobbleOffsetStrategyFactory : IOffsetStrategyFactory {

	public IOffsetStrategy Create(ControllerOffset controller) {
		return new WobbleOffsetStrategy (controller.transform, controller.actual, controller.head, controller.fork);
	}
}