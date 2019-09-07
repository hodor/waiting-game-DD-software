using System.Collections.Generic;
using System.Globalization;

namespace Output.CSV.Calculation
{
    public class SubjectiveValueData : ICsvData
    {
        private List<float> values = new List<float>();

        public void Calculate(List<int> points)
        {
            values.Add(1000);
        }
        
        public List<string> ToList()
        {
            return values.ConvertAll<string>(x => x.ToString(CultureInfo.InvariantCulture));
        }
    }
}