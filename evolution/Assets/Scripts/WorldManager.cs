using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WorldManager : MonoBehaviour {

	public GameObject foodPrefab;
	public Transform topRightCorner;
	public Transform bottomLeftCorner;
	public Transform center;
	public List<Food> allFood;
	public List<Creature> allCreatures;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public Vector3 getRandomPositionInWorld(float range)
	{
		Vector3 position = RandomNavSphere(center.position, range, -1);
		return position;
	}

	public void PlaceFood(Vector3 position)
	{
		GameObject food = Instantiate(foodPrefab, position, Quaternion.identity);
		Food babyFood = food.GetComponent<Food>();
		allFood.Add(babyFood);
	}

	/*
	 * Find a random destination on the navmesh in range of the origin
	 * layermask = -1 for all
	*/
	public static Vector3 RandomNavSphere(Vector3 origin, float distance, int layermask)
	{
		Vector3 randomDirection = UnityEngine.Random.insideUnitSphere * distance;
		randomDirection += origin;
		NavMeshHit navHit;
		NavMesh.SamplePosition(randomDirection, out navHit, distance, layermask);

		return navHit.position;
	}
}
