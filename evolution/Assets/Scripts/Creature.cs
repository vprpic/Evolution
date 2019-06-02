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
	MovementSpeed movementSpeed;
	ViewRadius viewRadius;
	float size;
	#endregion

	#region Movement
	public float distance;
	public float wanderTimer;
	private float timer;
	private bool wander;
	private Vector3 destination;
	private bool reachedDestination;
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
		size = 0.5f;
		if(genome == null){
			genome = new Genome();
			Colour color = new Colour(new Color32(50, 150, 122, 255));
			genome.Add((Gene)color);
			MovementSpeed movementSpeed = new MovementSpeed(10f);
			genome.Add((Gene)movementSpeed);
			ViewRadius viewRange = new ViewRadius(10f);
			genome.Add((Gene)viewRange);
		}
		Init();
	}

	void Init()
	{
		wander = true;
		reachedDestination = false;
		agent = GetComponent<NavMeshAgent>();
		//set colour
		Colour colour = (Colour)genome.findGene("Colour");
		creatureGO.GetComponentInChildren<Renderer>().material.color = colour.value;
		//set movement speed
		movementSpeed = (MovementSpeed) genome.findGene("MovementSpeed");
		agent.speed = movementSpeed.value;
		//set view range
		viewRadius = (ViewRadius)genome.findGene("ViewRadius");
		creatureGO.GetComponent<SphereCollider>().radius = viewRadius.value;

		//setup agent
		wanderTimer = Random.Range(1f,5f);
		timer = wanderTimer;
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
	}

	private void OnTriggerEnter(Collider other)
	{
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
		//TODO: remove reproduce
		Reproduce();
		Destroy(food.gameObject);
	}

	#region Reproduction
	public void Reproduce()
	{
		//TODO: set mutation chance trait
		MultiplyWithMutation(1f);
	}

	public void MultiplyWithMutation(float mutationChance)
	{
		Vector3 position = WorldManager.RandomNavSphere(creatureGO.transform.position, 1.5f, -1);
		while (position.x == Mathf.Infinity || position.y == Mathf.Infinity || position.z == Mathf.Infinity||
			position.x == Mathf.NegativeInfinity || position.y == Mathf.NegativeInfinity || position.z == Mathf.NegativeInfinity)
		{
			Debug.Log("while position == infinity");
			position = WorldManager.RandomNavSphere(creatureGO.transform.position, 1.5f, -1);
		}
		GameObject baby = Instantiate(creaturePrefab, position, Quaternion.identity);
		Creature babyCreature = baby.GetComponent<Creature>();
		babyCreature.creatureGO = baby;
		babyCreature.genome = this.genome.TraitChange(mutationChance);
	}

	#endregion

	#region Movement
	void Move(Vector3 destination)
	{
		timer += Time.deltaTime;

		if (timer >= wanderTimer)
		{
			agent.SetDestination(destination);
			timer = 0;
			wanderTimer = Random.Range(1f, 5f);
		}
	}

	void Wander()
	{
		timer += Time.deltaTime;

		if (timer >= wanderTimer)
		{
			agent.SetDestination(WorldManager.RandomNavSphere(transform.position, distance, -1));
			timer = 0;
			wanderTimer = Random.Range(1f, 5f);
		}
	}
	
	
	#endregion

	
}
