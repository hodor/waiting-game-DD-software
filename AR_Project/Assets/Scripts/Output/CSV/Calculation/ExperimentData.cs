using System.Collections.Generic;

namespace Output.CSV.Calculation
{
    public class ExperimentData
    {
        public PointsData points = new PointsData();
        public SubjectiveValueData subjectiveValueData = new SubjectiveValueData();
        public List<double> chooseTime = new List<double>();
    }
}