using System.Collections.Generic;

namespace Output.CSV.Calculation
{
    public interface ICsvData
    {
        List<string> ToList();
    }
}