using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IOffsetStrategy {
	void ApplyOffset();
}


public interface IOffsetStrategyFactory {
	IOffsetStrategy Create (ControllerOffset controller);
}