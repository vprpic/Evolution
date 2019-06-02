using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Colour : Gene {

	public Color32 value;
	
	public Colour(Color32 color_)
	{
		name = "Colour";
		this.value = color_;
	}

	public override void ChangeSlightly(float percentage)
	{
		float amount = 255 * percentage;
		byte r, g, b, a;
		r = (byte)Random.Range(Mathf.Clamp(value.r - amount, 0, 255), Mathf.Clamp(value.r + amount, 0, 255));
		g = (byte)Random.Range(Mathf.Clamp(value.g - amount, 0, 255), Mathf.Clamp(value.g + amount, 0, 255));
		b = (byte)Random.Range(Mathf.Clamp(value.b - amount, 0, 255), Mathf.Clamp(value.b + amount, 0, 255));
		a = 1;
		value = new Color32(r,g,b,a);
	}

	public override Gene CopyGene()
	{
		Gene copied = new Colour(this.value);
		return copied;
	}
}
