using System;
using System.Collections.Generic;
using System.Linq;
using AR_Project.DataClasses.NestedObjects;
using UnityEngine;
using Random = System.Random;

public class ClusterValue
{
    public int SecondPrizeTimer { get; set; }
    public int FirstPrizeValue { get; set; }

    public override bool Equals(object obj)
    {
        var other = obj as ClusterValue;
        if (other == null) return false;
        return SecondPrizeTimer == other.SecondPrizeTimer && FirstPrizeValue == other.FirstPrizeValue;
    }

    public override int GetHashCode()
    {
        int hash = 13;
        hash = (hash * 7) + SecondPrizeTimer.GetHashCode();
        hash = (hash * 7) + FirstPrizeValue.GetHashCode();
        return hash;
    }
}
public static class ListShuffler
{
    private static Random rng = new Random((int) DateTime.Now.Ticks);

    public static void Shuffle<T>(this IList<T> list)  
    {  
        int n = list.Count;  
        while (n > 1) {  
            n--;  
            int k = rng.Next(n + 1);  
            T value = list[k];  
            list[k] = list[n];  
            list[n] = value;  
        }  
    }

    public static List<List<Experiment>> GetClusters(List<Experiment> experiments)
    {
        var clusterDict = new Dictionary<ClusterValue, List<Experiment>>();
        var ret = new List<List<Experiment>>();
        int minPoints = 0;
        foreach (var experiment in experiments)
        {
            minPoints += experiment.immediatePrizePoints;
            var key = new ClusterValue()
            {
                FirstPrizeValue = experiment.immediatePrizeValue, 
                SecondPrizeTimer = experiment.secondPrizeTimer
            };
            if (!clusterDict.ContainsKey(key))
            {
                clusterDict.Add(key, new List<Experiment>());
            }

            int curIndex = 0;
            foreach (var kvp in clusterDict)
            {
                if (kvp.Key.Equals(key))
                {
                    experiment.clusterId = curIndex;
                    break;
                }

                curIndex++;
            }
            clusterDict[key].Add(experiment);
        }
        
        Debug.Log("This task minimum points is: "+minPoints);

        foreach (var cluster in clusterDict.Values)
        {
            var sortedCluster = cluster.OrderBy(list => list.id).ToList();
            ret.Add(sortedCluster);
        }
        return ret;
    }

    public static List<Experiment> GetPseudoRandomExperiments(List<Experiment> experiments)
    {
        var clusters = GetClusters(experiments);
        var numClusters = clusters.Count;
        var pseudoList = new List<Experiment>();
        
        var allElementsOutOfClusters = false;
        var validIndices = new List<int>();
        for (var i = 0; i < numClusters; i++)
        {
            validIndices.Add(i);
        }
        while (validIndices.Count > 0)
        {
            int randomIndex = validIndices.Count == 1 ? 0 : rng.Next(0, validIndices.Count);
            int validIndex = validIndices[randomIndex];
            var experiment = clusters[validIndex].First();
            pseudoList.Add(experiment);
            
            clusters[validIndex].RemoveAt(0);
            if (clusters[validIndex].Count == 0)
            {
                validIndices.Remove(validIndex);
            }
        }

        return pseudoList;
    }
}