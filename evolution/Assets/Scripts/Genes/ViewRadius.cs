using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewRadius : Gene {
	public float value;

	public ViewRadius(float value_)
	{
		name = "ViewRadius";
		value = value_;
	}

	public void ChangeSlightly(float percentage)
	{
		Debug.Log("ViewRadius " + percentage);
		float amount = value * percentage;
		float lowValue = value - amount;
		if (lowValue < 0)
			lowValue = 0;
		value = Random.Range(lowValue, value + amount);
	}
}
