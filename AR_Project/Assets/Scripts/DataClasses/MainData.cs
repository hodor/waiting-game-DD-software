using AR_Project.DataClasses.JsonToClasses;
using UnityEngine;

namespace AR_Project.DataClasses.MainData
{
    public sealed class MainData
    {
        private static readonly MainData instance = new MainData();
        public Config config;
        static MainData()
        {

        }

        private MainData()
        {
            config = JsonReader.ReadConfig();
        }

        public static MainData instanceData
        {
            get
            {
                return instance;
            }
        }
    }
}