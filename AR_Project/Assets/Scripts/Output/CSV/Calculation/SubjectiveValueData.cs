using System;
using System.Collections.Generic;
using System.Globalization;

namespace Output.CSV.Calculation
{
    public class SubjectiveValueData : ICsvData
    {
        private List<float> values = new List<float>();

        public void Calculate(List<int> points)
        {
            int num_count = 9;
            values.Add(1000);
            if (points.Count % num_count != 0)
            {
                throw new Exception("Cannot calculate the subjective value data if it's not a multiple of 7");
            }
            
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
                int num_late_choices = 0;
                foreach (var p in group)
                {
                    if (p == 1000) num_late_choices++;
                }

                values.Add((num_late_choices / group.Count) * 800 + 50);
            }
            
        }
        
        public List<string> ToList()
        {
            return values.ConvertAll<string>(x => x.ToString(CultureInfo.CurrentCulture));
        }
    }
}