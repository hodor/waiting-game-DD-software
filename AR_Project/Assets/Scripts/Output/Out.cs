namespace Output
{
    public static class Out
    {
        private static IOutput _instance;

        public static IOutput Instance
        {
            get { return _instance ?? (_instance = OutputFactory.GetOutputStrategy()); }
        }
    }
}