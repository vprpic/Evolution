using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Gene {

	public string name;

	abstract public void ChangeSlightly(float percent);
	abstract public Gene CopyGene();
}
