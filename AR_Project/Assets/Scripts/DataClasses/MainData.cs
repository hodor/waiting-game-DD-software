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

            foreach (var exp in fakeExperiments.experiments)
                Debug.Log("scd prize timer: " + exp.secondPrizeTimer);

            //foreach (var lanes in config.gameSettings.timeLanes)
            //    Debug.Log("time lane: " + lanes);

            //foreach (var experiment in realExperiments.experiments)
            //    Debug.Log("immediate prize value: " + experiment.immediatePrizeValue);

            //foreach (var experiment in fakeExperiments.experiments)
                //Debug.Log("FAKE: immediate prize value: " + experiment.immediatePrizeValue);
        }
       
    }
}
