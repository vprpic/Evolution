using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class Genome {

	public List<Gene> genes;

	public Genome()
	{
		genes = new List<Gene>();
	}

	public Genome(List<Gene> genome)
	{
		this.genes = genome;
	}

	public Gene findGene(string lookFor)
	{
		Gene found;
		foreach (Gene gene in genes)
		{
			//Debug.Log("findGene: " + gene.name);
			if (gene.name.Equals(lookFor))
			{
				found = gene;
				return found;
			}
		}
		return null;
	}

	public void Add(Gene gene)
	{
		this.genes.Add(gene);
	}

	public Genome TraitChange(float mutationChance)
	{
		float mutationAmount = UnityEngine.Random.Range(0,mutationChance);
		int traitIndex = UnityEngine.Random.Range(0, genes.Count);
		Debug.Log("traitIndex: " + traitIndex);
		Debug.Log("trait: " + genes[traitIndex].name);
		Debug.Log("traitType: " + genes[traitIndex].GetType());

		Type type = genes[traitIndex].GetType().UnderlyingSystemType;
		//dynamic
		//dynamic objekt = Convert.ChangeType(genes[traitIndex], type);
		//objekt.ChangeSlightly(mutationChance);

		//reflection
		object[] mParam = new object[] { mutationAmount };
		MethodInfo myMethodInfo = type.GetMethod("ChangeSlightly");
		myMethodInfo.Invoke(genes[traitIndex],mParam);

		//TODO: change gene
		//((Colour)this.findGene("Colour")).ChangeSlightly(mutationAmount);
		//Debug.Log("MutationAmount" + mutationAmount);
		return this;
	}
}
