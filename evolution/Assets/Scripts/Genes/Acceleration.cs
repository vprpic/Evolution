using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Acceleration : Gene
{
	public float value;

	public Acceleration(float value_)
	{
		name = "Acceleration";
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
		Debug.Log("Acceleration from: " + oldvalue + ", to: " + value);
	}

	public override Gene CopyGene()
	{
		Gene copied = new Acceleration(this.value);
		return copied;
	}
}
