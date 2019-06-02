using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeSpan : Gene
{
	public float value;

	public LifeSpan(float value_)
	{
		name = "LifeSpan";
		this.value = value_;
	}

	public override void ChangeSlightly(float percent)
	{
		float oldvalue = value;
		float amount = value * percent;
		float lowValue = value - amount;
		if (lowValue < 0)
			lowValue = 0;
		value = Random.Range(lowValue, value + amount);
		Debug.Log("LifeSpan from: " + oldvalue + ", to: " + value);
	}

	public override Gene CopyGene()
	{
		Gene copied = new LifeSpan(this.value);
		return copied;
	}
}
