using System.Collections.Generic;

namespace Output.CSV.Calculation
{
    public class PointsData : ICsvData
    {
        public int totalPoints;
        public List<string> sequenceOrder = new List<string>();
        private SortedDictionary<string, List<int>> sequencePointsInCluster = new SortedDictionary<string, List<int>>();

        public void AddPoint(string cluster, int point)
        {
            if (!sequencePointsInCluster.ContainsKey(cluster))
            {
                sequencePointsInCluster.Add(cluster, new List<int>());
            }
            
            sequencePointsInCluster[cluster].Add(point);
        }
        
        public List<int> GetSequencePoints()
        {
            var list = new List<int>();
            foreach (var v in sequencePointsInCluster.Values)
            {
                list.AddRange(v);
            }

            return list;
        }
        public List<string> ToList()
        {
            var sequence = string.Join("_", sequenceOrder.ToArray());
            var list = new List<string> {totalPoints.ToString(), sequence};
            list.AddRange(GetSequencePoints().ConvertAll(x => x.ToString()));
            return list;
        }
    }
}