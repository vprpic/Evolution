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

	public override void ChangeSlightly(float percent)
	{
		float oldvalue = value;
		float amount = value * percent;

		float lowValue = value - amount;
		if (lowValue < 0)
			lowValue = 0;
		value = Random.Range(lowValue, value + amount);
		Debug.Log("MovementSpeed from: " + oldvalue + ", to: " + value);
	}

	public override Gene CopyGene()
	{
		Gene copied = new MovementSpeed(this.value);
		return copied;
	}
}
