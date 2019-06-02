using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Creature : MonoBehaviour {

	public GameObject creaturePrefab;
	public Genome genome;
	public GameObject creatureGO;
	public Transform target;
	NavMeshAgent agent;

	#region basic traits
	private MovementSpeed movementSpeed;
	private ViewRadius viewRadius;
	private LifeSpan lifeSpan;
	private Acceleration acceleration;
	float size;
	#endregion

	#region Movement
	public float distance;
	public float wanderMaxTime;
	private float wanderTimer;
	private bool wander;
	private Vector3 destination;
	private bool reachedDestination;
	#endregion

	#region Life
	public float lifeSpanTimer;
	#endregion

	public Creature()
	{
	}

	public Creature(Genome genome, GameObject creatureGO)
	{
		this.genome = genome;
		this.creatureGO = creatureGO;
	}

	// Use this for initialization
	void Start()
	{
		GameManager.instance.worldManager.allCreatures.Add(this);
		size = 0.5f;
		if(genome == null){
			genome = new Genome();
			Colour color = new Colour(new Color32(50, 150, 122, 255));
			genome.Add(color);
			MovementSpeed movementSpeed = new MovementSpeed(10f);
			genome.Add(movementSpeed);
			ViewRadius viewRange = new ViewRadius(20f);
			genome.Add(viewRange);
			LifeSpan lifeSpan = new LifeSpan(40f);
			genome.Add(lifeSpan);
			Acceleration acceleration = new Acceleration(20f);
			genome.Add(acceleration);
		}
		Init();
	}

	void Init()
	{
		wander = true;
		reachedDestination = false;
		agent = GetComponent<NavMeshAgent>();
		//setup agent
		wanderMaxTime = UnityEngine.Random.Range(0f,2f);
		wanderTimer = 0f;
		#region SetTraits
		//set colour
		Colour colour = (Colour)genome.findGene("Colour");
		creatureGO.GetComponentInChildren<Renderer>().material.color = colour.value;
		//set movement speed
		movementSpeed = (MovementSpeed) genome.findGene("MovementSpeed");
		agent.speed = movementSpeed.value;
		//set acceleration
		acceleration = (Acceleration)genome.findGene("Acceleration");
		agent.acceleration = acceleration.value;
		//set view range
		viewRadius = (ViewRadius)genome.findGene("ViewRadius");
		creatureGO.GetComponent<SphereCollider>().radius = viewRadius.value;
		//set life span
		lifeSpan = (LifeSpan) genome.findGene("LifeSpan");
		lifeSpanTimer = 0;
		#endregion
	}
	
	// Update is called once per frame
	void Update () {
		//decide if it wants to wander or where to go
		if(Vector3.Distance(creatureGO.transform.position, destination) <= size)
		{
			reachedDestination = true;
		}

		if (!wander && reachedDestination)
		{
			wander = true;
			reachedDestination = false;
		}
		if (wander)
		{
			Wander();
		}
		else
		{
			Move(destination);
		}
		Age();
	}

	private void Age()
	{
		if (lifeSpan == null)
		{
			Debug.LogWarning("Lifespan not detected.");
			return;
		}
		lifeSpanTimer += Time.deltaTime;
		if (lifeSpanTimer >= lifeSpan.value)
		{
			Debug.Log("Death");
			lifeSpanTimer = 0;
			Destroy(this.gameObject);
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		//TODO: go to closer not further
		if (other.tag.Equals("Food"))
		{
			destination = other.transform.position;
			wander = false;
			reachedDestination = false;
		}
	}

	public void EatFood(Food food)
	{
		//TODO: consume the food
		if (food == null)
			return;
		if (lifeSpanTimer > (lifeSpan.value*3)/4)
		{
			lifeSpanTimer -= food.value;
			if (lifeSpanTimer < 0)
				lifeSpanTimer = 0;
		}
		else
		{
			Reproduce();
		}
		Destroy(food.gameObject);
	}

	#region Reproduction
	public void Reproduce()
	{
		//TODO: set mutation chance trait
		MultiplyWithMutation(0.1f);
	}

	public void MultiplyWithMutation(float mutationChance)
	{
		Vector3 position = WorldManager.RandomNavSphere(creatureGO.transform.position, 1.5f, -1);
		while (position.x == Mathf.Infinity || position.y == Mathf.Infinity || position.z == Mathf.Infinity||
			position.x == Mathf.NegativeInfinity || position.y == Mathf.NegativeInfinity || position.z == Mathf.NegativeInfinity)
		{
			//Debug.Log("while position == infinity");
			position = WorldManager.RandomNavSphere(creatureGO.transform.position, 1.5f, -1);
		}
		GameObject baby = Instantiate(creaturePrefab, position, Quaternion.identity);
		Creature babyCreature = baby.GetComponent<Creature>();
		babyCreature.creatureGO = baby;
		babyCreature.genome = this.genome.CopyGenome().TraitChange(mutationChance);
	}

	#endregion

	#region Movement
	void Move(Vector3 destination)
	{
		wanderTimer += Time.deltaTime;

		if (wanderTimer >= wanderMaxTime)
		{
			agent.SetDestination(destination);
			wanderTimer = 0;
			wanderMaxTime = UnityEngine.Random.Range(1f, 5f);
		}
	}

	void Wander()
	{
		wanderTimer += Time.deltaTime;

		if (wanderTimer >= wanderMaxTime)
		{
			agent.SetDestination(WorldManager.RandomNavSphere(transform.position, distance, -1));
			wanderTimer = 0;
			wanderMaxTime = UnityEngine.Random.Range(1f, 5f);
		}
	}

	#endregion

	private void OnDestroy()
	{
		GameManager.instance.worldManager.allCreatures.Remove(this);
		Destroy(this.gameObject);
	}
}
