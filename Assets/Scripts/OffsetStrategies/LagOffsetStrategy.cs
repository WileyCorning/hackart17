using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LagOffsetStrategy : IOffsetStrategy {
	class TimePoint {
		public float t;
		public Vector3 position;
		public Quaternion rotation;
	}


	Transform visible;
	Transform actual;
	float delay;

	public LagOffsetStrategy(Transform visible, Transform actual, float delay) {
		this.visible = visible;
		this.actual = actual;
		this.delay = delay;
	}

	List<TimePoint> points = new List<TimePoint>();

	public void ApplyOffset () {
		Purge ();
		var now = new TimePoint () { t = Time.fixedTime, position = actual.position, rotation = actual.rotation };
		points.Add (now);
		var accurate = points [0];
		Debug.LogFormat ("{0} {1} {2} {3}", points.Count, Time.fixedTime, Time.fixedTime - delay, accurate.t);
		Debug.Log (accurate.position);
		visible.SetPositionAndRotation (accurate.position, accurate.rotation);
	}

	void Purge() {
		for (int i = points.Count - 1; i >= 0; i--) {
			if (points [i].t < Time.fixedTime - delay) {
				points.RemoveAt (i);
			}
		}
	}
	
}

public class LagOffsetStrategyFactory : IOffsetStrategyFactory {
	float delay;

	public LagOffsetStrategyFactory(float delay) {
		this.delay = delay;
	}

	public IOffsetStrategy Create(ControllerOffset controller) {
		return new LagOffsetStrategy (controller.transform, controller.actual, this.delay);
	}
}