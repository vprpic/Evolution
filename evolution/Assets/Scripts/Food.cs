using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour {

	public float value;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.tag.Equals("CreatureBody"))
		{
			Creature creature = other.gameObject.transform.parent.gameObject.GetComponent<Creature>();
			creature.EatFood(this);
		}
	}
}
