using Output.Concrete;

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