using System;
using System.Collections.Generic;
using System.Globalization;
using AR_Project.DataClasses.MainData;

namespace Output.CSV.Calculation
{
    public class SubjectiveValueData : ICsvData
    {
        private List<float> values = new List<float>();

        public void Calculate(List<int> points)
        {
            int num_count = 9;
            var orderedPrizes = MainData.instanceData.config.GetOrderedPrizeValues();
            int maxPrize = orderedPrizes[orderedPrizes.Count - 1];
            int secondMaxPrize = orderedPrizes[orderedPrizes.Count - 2];
            int smallestPrize = orderedPrizes[0];
            float maxPossibleValue = ((float)maxPrize + (float)secondMaxPrize) / 2.0f;
            float minPossibleValue = (float) smallestPrize / 2.0f;
            float plausibleSubjectiveValue = maxPossibleValue - minPossibleValue;
            
            // Adding the max value for the 0 value
            values.Add(maxPrize);
//            if (points.Count % num_count != 0)
//            {
//                throw new Exception("Cannot calculate the subjective value data if it's not a multiple of 9");
//            }
            
            List<List<int>> grouped = new List<List<int>>();
            int index = 0;
            for(int i = 0; i < points.Count; i++)
            {
                if (i % num_count == 0 && i != 0) index++;
                if(grouped.Count <= index) grouped.Add(new List<int>());
                grouped[index].Add(points[i]);
            }

            foreach (var group in grouped)
            {
                int numLateChoices = 0;
                foreach (var p in group)
                {
                    if (p == maxPrize) numLateChoices++;
                }

                float subjectiveValue = (((float)numLateChoices / (float)group.Count) * plausibleSubjectiveValue) + minPossibleValue;
                values.Add(subjectiveValue);
            }
            
        }
        
        public List<string> ToList()
        {
            var usCulture = new CultureInfo("en-US");
            return values.ConvertAll<string>(x => x.ToString("0.###",usCulture).Replace(",",""));
        }

        public List<float> GetValues()
        {
            return values;
        }
    }
}