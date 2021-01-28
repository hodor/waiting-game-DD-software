using System.Collections.Generic;

namespace Output.CSV.Calculation
{
    public static class Math
    {
        public static float GetAreaUnderCurve(List<int> points)
        {
            return 0.0f;
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