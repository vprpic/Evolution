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
			if (gene.name.Equals(lookFor))
			{
				found = gene;
				return found;
			}
		}
		Debug.LogWarning("Genome '"+lookFor+"' not found.");
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

		genes[traitIndex].ChangeSlightly(mutationAmount);

		//Type type = genes[traitIndex].GetType().UnderlyingSystemType;
		//object[] mParam = new object[] { mutationAmount };
		//MethodInfo myMethodInfo = type.GetMethod("ChangeSlightly");
		//myMethodInfo.Invoke(genes[traitIndex],mParam);
		return this;
	}

	public Genome CopyGenome()
	{
		Genome copied = new Genome();
		foreach(Gene gene in this.genes)
		{
			copied.Add(gene.CopyGene());
		}
		return copied;
	}
}
