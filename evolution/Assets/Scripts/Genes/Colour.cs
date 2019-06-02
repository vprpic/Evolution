using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Colour : Gene {

	public Color32 value;
	
	public Colour(Color32 color_)
	{
		name = "Colour";
		value = color_;
	}

	public void ChangeSlightly(float percentage)
	{
		Debug.Log("Colour "+ percentage);
		float amount = 255 * percentage;
		byte r, g, b, a;
		r = (byte)Random.Range(Mathf.Clamp(value.r - amount, 0, 255), Mathf.Clamp(value.r + amount, 0, 255));
		g = (byte)Random.Range(Mathf.Clamp(value.g - amount, 0, 255), Mathf.Clamp(value.g + amount, 0, 255));
		b = (byte)Random.Range(Mathf.Clamp(value.b - amount, 0, 255), Mathf.Clamp(value.b + amount, 0, 255));
		a = 1;
		value = new Color32(r,g,b,a);
	}
}
