using System;
using System.Collections.Generic;
using System.Linq;
using AR_Project.DataClasses.NestedObjects;

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
        var clusterDict = new Dictionary<int, List<Experiment>>();
        var ret = new List<List<Experiment>>();
        foreach (var experiment in experiments)
        {
            var biggestTimer = experiment.secondPrizeTimer;
            if (!clusterDict.ContainsKey(biggestTimer))
            {
                clusterDict.Add(biggestTimer, new List<Experiment>());
            }
            clusterDict[biggestTimer].Add(experiment);
        }

        foreach (var cluster in clusterDict.Values)
        {
            var sortedCluster = cluster.OrderBy(list => list.immediatePrizePoints).ToList();
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
            var randomIndex = rng.Next(0, validIndices.Count - 1);

            var experiment = clusters[validIndices[randomIndex]][0];
            clusters[validIndices[randomIndex]].RemoveAt(0);
            if (clusters[validIndices[randomIndex]].Count == 0)
            {
                validIndices.RemoveAt(randomIndex);
            }
            pseudoList.Add(experiment);
        }

        return pseudoList;
    }
}