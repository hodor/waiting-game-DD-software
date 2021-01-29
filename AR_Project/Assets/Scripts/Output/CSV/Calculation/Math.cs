using System.Collections.Generic;
using AR_Project.DataClasses.MainData;

namespace Output.CSV.Calculation
{
    public static class Math
    {
        public static float GetAreaUnderCurve(List<float> points)
        {
            var timeLanes = MainData.instanceData.config.laneTimes;
            var time1 = timeLanes[1].time - timeLanes[0].time;
            var time2 = timeLanes[2].time - timeLanes[1].time;
            var time3 = timeLanes[3].time - timeLanes[2].time;
            var time4 = timeLanes[4].time - timeLanes[3].time;
            var ret = (time1 * ((points[0] + points[1])/2)) + 
                      (time2 * ((points[1] + points[2])/2)) +
                      (time3 * ((points[2] + points[3])/2)) +
                      (time4 * ((points[3] + points[4])/2)) ;
            return ret;
        }
        
        public static float GetMaxAreaUnderCurve()
        {
            var orderedPrizes = MainData.instanceData.config.GetOrderedPrizeValues();
            float maxPrize = orderedPrizes[orderedPrizes.Count - 1];
            var timeLanes = MainData.instanceData.config.laneTimes;
            var time1 = timeLanes[1].time - timeLanes[0].time;
            var time2 = timeLanes[2].time - timeLanes[1].time;
            var time3 = timeLanes[3].time - timeLanes[2].time;
            var time4 = timeLanes[4].time - timeLanes[3].time;
            float maxSubjectiveValue = SubjectiveValueData.GetMaximumPossibleSV();
            var ret = (time1 * ((maxPrize + maxSubjectiveValue)/2) + 
                      (time2 * maxSubjectiveValue) +
                      (time3 * maxSubjectiveValue) +
                      (time4 * maxSubjectiveValue));
            return ret;
        }

        public static List<float> GetNormalizedValues(SubjectiveValueData values)
        {
            var vals = values.GetValues();
            var max = vals[0];
            var ret = new List<float>();
            foreach (var v in vals)
            {
                ret.Add(v/max);
            }

            return ret;
        }
    }
}