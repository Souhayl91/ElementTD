using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneticAlgorithm : MonoBehaviour
{
    private int populationSize = 10;
    private float mutationChance = .8f;
    private float flipMutationChance = 0.03f;
    private float mutationMin = .1f;
    private float mutationMax = .3f;

    public List<BaseEnemy.Gene> genes = new List<BaseEnemy.Gene>();
    private List<BaseEnemy.Gene> _currentPopulationGenes = new List<BaseEnemy.Gene>();
    private BaseEnemy.Gene fittestGene = null;
    private BaseEnemy.Gene secondFittestGene = null;

    private List<BaseEnemy.Gene> _newGenes = new List<BaseEnemy.Gene>();

    public BaseEnemy.Gene _optimalGene = new BaseEnemy.Gene
    {
        fireResistance = 0.8f,
        natureResistance = 0.1f,
        waterResistance = 0.1f,
        distanceWalked = 0
    };

    public float bestFit = 1;
    private bool elitism = false;

    private int _debugMutateCount = 0;

    public void CreateNewRandomPop()
    {
        bestFit = 1;
        genes.Clear();
        fittestGene = null;
        secondFittestGene = null;
        for (int i = 0; i < populationSize; i++)
        {
            genes.Add(RandomGene());
        }
    }

    public void AddGene(BaseEnemy.Gene gene)
    {
        _currentPopulationGenes.Add(gene);
    }

    public void CreateNewGeneration()
    {
        SetFittestGenes();
        _currentPopulationGenes.Clear();
        _newGenes.Clear();
        for (int i = 0; i < populationSize; i++)
        {
            BaseEnemy.Gene newGene = Crossover();
            if (Random.Range(0, 0.99f) < mutationChance)
            {
                _debugMutateCount++;
                newGene = Mutate(newGene);
            }
            //Debug.Log("Gene number: " + i + " : " + newGene.ToString());
            _newGenes.Add(newGene);
        }
        //Debug.Log("Mutate count: " + _debugMutateCount);
        genes = _newGenes;
    }

    public void CreateNewGenerationWithOptimal()
    {
        SetFittestGenesWithOptimal();
        _newGenes.Clear();
        for (int i = 0; i < populationSize; i++)
        {
            BaseEnemy.Gene newGene = Crossover();
            if (Random.Range(0, 0.99f) < mutationChance)
            {
                _debugMutateCount++;
                newGene = Mutate(newGene);
            }
            Debug.Log("Gene number: " + i + " : " + newGene.ToString());
            _newGenes.Add(newGene);
        }
        //Debug.Log("Mutate count: " + _debugMutateCount);
        genes = _newGenes;
    }

    private void SetFittestGenes()
    {
        if (!elitism)
        {
            fittestGene = null;
            secondFittestGene = null;
        }
        foreach (var gene in _currentPopulationGenes)
        {
            if (fittestGene == null)
            {
                fittestGene = gene;
            }
            else if (gene.damageTaken > fittestGene.damageTaken)
            {
                secondFittestGene = fittestGene;
                fittestGene = gene;
            }
            else if (secondFittestGene == null)
            {
                secondFittestGene = gene;
            }
            else if (gene.damageTaken > secondFittestGene.damageTaken)
            {
                secondFittestGene = gene;
            }
        }
        //Debug.Log("*** Fittest gene : " + fittestGene.ToString());
        //Debug.Log("*** Second fittest gene : " + secondFittestGene.ToString());
    }

    private void SetFittestGenesWithOptimal()
    {
        if (!elitism)
        {
            fittestGene = null;
            secondFittestGene = null;
        }
        foreach (var gene in genes)
        {
            if (fittestGene == null)
            {
                fittestGene = gene;
            }
            else if (FitnessFunctionWithOptimal(gene) < FitnessFunctionWithOptimal(fittestGene))
            {
                secondFittestGene = fittestGene;
                fittestGene = gene;
            }
            else if (secondFittestGene == null)
            {
                secondFittestGene = gene;
            }
            else if (FitnessFunctionWithOptimal(gene) < FitnessFunctionWithOptimal(secondFittestGene))
            {
                secondFittestGene = gene;
            }
        }
        
        bestFit = FitnessFunctionWithOptimal(fittestGene);
        //Debug.Log("Fittest gene fitness value: " + bestFit);
        //Debug.Log("Fittest gene : " + fittestGene.ToString());
        //Debug.Log("Second fittest gene : " + secondFittestGene.ToString());
        //Debug.Log("----------------------------------");
    }

    private BaseEnemy.Gene Crossover()
    {
        BaseEnemy.Gene newGene = new BaseEnemy.Gene();

        if (Random.Range(0, 2) == 0)
        {
            newGene.fireResistance = fittestGene.fireResistance;
        }
        else
        {
            newGene.fireResistance = secondFittestGene.fireResistance;
        }

        if (Random.Range(0, 2) == 0)
        {
            newGene.natureResistance = fittestGene.natureResistance;
        }
        else
        {
            newGene.natureResistance = secondFittestGene.natureResistance;
        }

        if (Random.Range(0, 2) == 0)
        {
            newGene.waterResistance = fittestGene.waterResistance;
        }
        else
        {
            newGene.waterResistance = secondFittestGene.waterResistance;
        }

        newGene = BalanceGene(newGene);

        return newGene;
    }

    private BaseEnemy.Gene Mutate(BaseEnemy.Gene gene)
    {
        if (Random.Range(0, 0.99f) < flipMutationChance && (gene.fireResistance >= 0.8f || gene.waterResistance >= 0.8f || gene.natureResistance >= 0.8f))
        {
            if (gene.fireResistance >= 0.8f)
            {
                if (gene.waterResistance < gene.natureResistance)
                    gene.SwapFireWater();
                else
                    gene.SwapNatureFire();
            }
            else if (gene.waterResistance >= 0.8f)
            {
                if (gene.fireResistance < gene.natureResistance)
                {
                    gene.SwapFireWater();
                }
                else
                {
                    gene.SwapWaterNature();
                }
            }
            else if (gene.natureResistance >= 0.8f)
            {
                if (gene.waterResistance < gene.fireResistance)
                    gene.SwapWaterNature();
                else
                    gene.SwapNatureFire();
            }
            return gene;
        }
        int mutationType = Random.Range(0, 6);
        float change = 0;
        switch (mutationType)
        {
            case 0:
                change = MutatePlusMinus(gene.fireResistance, gene.natureResistance);
                gene.fireResistance += change;
                gene.natureResistance -= change;
                break;
            case 1:
                change = MutatePlusMinus(gene.fireResistance, gene.waterResistance);
                gene.fireResistance += change;
                gene.waterResistance -= change;
                break;
            case 2:
                change = MutatePlusMinus(gene.natureResistance, gene.fireResistance);
                gene.natureResistance += change;
                gene.fireResistance -= change;
                break;
            case 3:
                change = MutatePlusMinus(gene.natureResistance, gene.waterResistance);
                gene.natureResistance += change;
                gene.waterResistance -= change;
                break;
            case 4:
                change = MutatePlusMinus(gene.waterResistance, gene.fireResistance);
                gene.waterResistance += change;
                gene.fireResistance -= change;
                break;
            case 5:
                change = MutatePlusMinus(gene.waterResistance, gene.natureResistance);
                gene.waterResistance += change;
                gene.natureResistance -= change;
                break;
        }

        return gene;
    }

    private float MutatePlusMinus(float plus, float minus)
    {
        float change = (float)System.Math.Round(Random.Range(mutationMin, mutationMax), 2);

        if (1 - plus >= change && minus >= change)
        {
            return change;
        }
        else if (1 - plus >= minus)
        {
            return minus;
        }
        else
        {
            return 1 - plus;
        }
    }

    // Lower fitness score is better
    private float FitnessFunctionWithOptimal(BaseEnemy.Gene gene)
    {
        return (float)System.Math.Round(
        Mathf.Abs(gene.fireResistance - _optimalGene.fireResistance) +
        Mathf.Abs(gene.natureResistance - _optimalGene.natureResistance) +
        Mathf.Abs(gene.waterResistance - _optimalGene.waterResistance), 2);
    }

    private BaseEnemy.Gene RandomGene()
    {
        float random1 = Random.Range(0f, 1f);
        float random2 = Random.Range(0f, 1f);
        float random3 = Random.Range(0f, 1f);

        BaseEnemy.Gene randomGene = new BaseEnemy.Gene
        {
            fireResistance = random1,
            natureResistance = random2,
            waterResistance = random3,
            distanceWalked = 0
        };

        randomGene = BalanceGene(randomGene);

        return randomGene;
    }

    private BaseEnemy.Gene BalanceGene(BaseEnemy.Gene gene)
    {
        float fireResistance = gene.fireResistance;
        float natureResistance = gene.natureResistance;
        float waterResistance = gene.waterResistance;

        float total = fireResistance + natureResistance + waterResistance;
        if (total == 0)
        {
            fireResistance = 1 / 3;
            natureResistance = 1 / 3;
            waterResistance = 1 / 3;
        }
        else
        {
            fireResistance /= total;
            natureResistance /= total;
            waterResistance /= total;
        }

        fireResistance = (float)System.Math.Round(fireResistance, 2);
        natureResistance = (float)System.Math.Round(natureResistance, 2);
        waterResistance = (float)System.Math.Round(waterResistance, 2);

        total = fireResistance + natureResistance + waterResistance;

        while (total != 1)
        {
            fireResistance += 1 - total;
            total = fireResistance + natureResistance + waterResistance;
        }

        gene.fireResistance = fireResistance;
        gene.natureResistance = natureResistance;
        gene.waterResistance = waterResistance;

        return gene;
    }
}
