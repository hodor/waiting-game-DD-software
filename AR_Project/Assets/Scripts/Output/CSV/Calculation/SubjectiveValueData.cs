using System;
using System.Collections.Generic;
using System.Globalization;
using AR_Project.DataClasses.MainData;

namespace Output.CSV.Calculation
{
    public class SubjectiveValueData : ICsvData
    {
        private List<float> values = new List<float>();

        private static float GetMaxPossibleValue()
        {
            var orderedPrizes = MainData.instanceData.config.GetOrderedPrizeValues();
            float maxPrize = orderedPrizes[orderedPrizes.Count - 1];
            float secondMaxPrize = orderedPrizes[orderedPrizes.Count - 2];
            return (maxPrize + secondMaxPrize) / 2.0f;
        }
        
        private static float GetMinPossibleValue()
        {
            var orderedPrizes = MainData.instanceData.config.GetOrderedPrizeValues();
            float smallestPrize = orderedPrizes[0];
            return smallestPrize / 2.0f;
        }

        public void Calculate(List<float> points)
        {
            int num_count = 9;
            var orderedPrizes = MainData.instanceData.config.GetOrderedPrizeValues();
            float maxPrize = orderedPrizes[orderedPrizes.Count - 1];
            float maxPossibleValue = GetMaxPossibleValue();
            float minPossibleValue = GetMinPossibleValue();
            float plausibleSubjectiveValue = maxPossibleValue - minPossibleValue;

            // Adding the max value for the 0 value
            values.Add(maxPrize);
//            if (points.Count % num_count != 0)
//            {
//                throw new Exception("Cannot calculate the subjective value data if it's not a multiple of 9");
//            }

            List<List<float>> grouped = new List<List<float>>();
            int index = 0;
            for (int i = 0; i < points.Count; i++)
            {
                if (i % num_count == 0 && i != 0) index++;
                if (grouped.Count <= index) grouped.Add(new List<float>());
                grouped[index].Add(points[i]);
            }

            foreach (var group in grouped)
            {
                int numLateChoices = 0;
                foreach (var p in group)
                {
                    const double tolerance = 0.000001;
                    if (System.Math.Abs(p - maxPrize) < tolerance) numLateChoices++;
                }

                float subjectiveValue = (((float) numLateChoices / (float) group.Count) * plausibleSubjectiveValue) +
                                        minPossibleValue;
                values.Add(subjectiveValue);
            }
        }

        public static float GetMaximumPossibleSV()
        {
            float maxPossibleValue = GetMaxPossibleValue();
            float minPossibleValue = GetMinPossibleValue();
            float plausibleSubjectiveValue = maxPossibleValue - minPossibleValue;

            return plausibleSubjectiveValue + minPossibleValue;
        }

        public List<string> ToList()
        {
            var usCulture = new CultureInfo("en-US");
            return values.ConvertAll<string>(x => x.ToString(OutputData.DecimalPrecision,usCulture).Replace(",",""));
        }

        public List<float> GetValues()
        {
            return values;
        }
    }
}