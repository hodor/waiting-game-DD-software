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
            int maxPrize = MainData.instanceData.config.GetMaxPrize();
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

                float subjectiveValue = (((float)numLateChoices / (float)group.Count) * 800.0f) + 50.0f;
                values.Add(subjectiveValue);
            }
            
        }
        
        public List<string> ToList()
        {
            
            return values.ConvertAll<string>(x => x.ToString(new CultureInfo("en-US")).Replace(",",""));
        }
    }
}