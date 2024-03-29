﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewRadius : Gene {
	public float value;

	public ViewRadius(float value_)
	{
		name = "ViewRadius";
		value = value_;
	}

	public override void ChangeSlightly(float percentage)
	{
		float oldvalue = value;
		float amount = value * percentage;
		float lowValue = value - amount;
		if (lowValue < 0)
			lowValue = 0;
		value = Random.Range(lowValue, value + amount);
		Debug.Log("ViewRadius from: " + oldvalue + ", to: " + value);
	}

	public override Gene CopyGene()
	{
		Gene copied = new ViewRadius(this.value);
		return copied;
	}
}
