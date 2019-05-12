using Output.CSV;

namespace Output
{
    public static class OutputFactory
    {
        public static IOutput GetOutputStrategy()
        {
            return new CSVOutput();
        }
    }
}