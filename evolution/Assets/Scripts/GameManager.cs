using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public static GameManager instance;
	public WorldManager worldManager;
	public List<Creature> population;
	public int maxFoodCount;
	private float timer;
	private float foodTimer;

	// Use this for initialization
	void Start () {
		instance = this;
		maxFoodCount = 5;
		foodTimer = UnityEngine.Random.Range(5f, 10f);
		timer = foodTimer;
	}
	
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;
		if (worldManager.allFood.Count <= maxFoodCount)
		{
			AddFood();
		}
	}

	private void AddFood()
	{

		if (timer >= foodTimer)
		{
			timer = 0;
			AddFoodToRandomPoint();
		}
	}

	private void AddFoodToRandomPoint()
	{
		worldManager.PlaceFood(worldManager.getRandomPositionInWorld());
	}
}
