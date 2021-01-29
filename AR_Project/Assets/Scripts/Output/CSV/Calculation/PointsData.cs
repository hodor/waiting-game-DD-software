using System.Collections.Generic;

namespace Output.CSV.Calculation
{
    public class PointsData : ICsvData
    {
        public float totalPoints;
        public List<string> sequenceOrder = new List<string>();
        private SortedDictionary<string, List<float>> sequencePointsInCluster = new SortedDictionary<string, List<float>>();

        public void AddPoint(string cluster, float point)
        {
            if (!sequencePointsInCluster.ContainsKey(cluster))
            {
                sequencePointsInCluster.Add(cluster, new List<float>());
            }
            
            sequencePointsInCluster[cluster].Add(point);
        }
        
        public List<float> GetSequencePoints()
        {
            var list = new List<float>();
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