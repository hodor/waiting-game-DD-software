using System;
using AR_Project.DataClasses.JsonToClasses;
using UnityEngine;

namespace AR_Project.DataClasses.MainData
{
    public class MainData: MonoBehaviour
    {
        static public MainData instanceData = null;

        public Prizes prizes;
        public Config config;
        public RealExperiments realExperiments;
        public FakeExperiments fakeExperiments;
        public ContentConfig content;


        void Awake()
        {
            if (instanceData == null)
                instanceData = this;
            else if (instanceData != this)
                Destroy(gameObject);


            DontDestroyOnLoad(this);

            prizes = JsonReader.ReadPrizeConfig();
            config = JsonReader.ReadGameConfig();
            realExperiments = JsonReader.ReadRealExperiments();
            fakeExperiments = JsonReader.ReadFakeExperiments();
            content = JsonReader.ReadContentConfig();
        }
       
    }
}
