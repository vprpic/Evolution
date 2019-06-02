using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementSpeed : Gene {

	public float value;

	public MovementSpeed(float value_)
	{
		name = "MovementSpeed";
		value = value_;
	}

	public void ChangeSlightly(float percentage)
	{
		Debug.Log("MovementSpeed " + percentage);
		float amount = value * percentage;

		float lowValue = value - amount;
		if (lowValue < 0)
			lowValue = 0;
		value = Random.Range(lowValue, value + amount);
	}
}
