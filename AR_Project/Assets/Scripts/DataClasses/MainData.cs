using System;
using AR_Project.JsonToClasses;
using UnityEngine;
using AR_Project.DataClasses.NestedObjects;

namespace AR_Project.DataClasses.MainData
{
    public class MainData: MonoBehaviour
    {
        public MainData instanceData;

        public Prizes prizes;
        public Config config;
        public RealExperiments realExperiments;
        public FakeExperiments fakeExperiments;


        void Awake()
        {
            if (instanceData != null)
                Destroy(instanceData);
            else
                instanceData = this;
        }
        void Start()
        {
            prizes = JsonReader.ReadPrizeConfig();
            config = JsonReader.ReadGameConfig();
            realExperiments = JsonReader.ReadRealExperiments();
            //fakeExperiments = JsonReader.ReadFakeExperiments();


            foreach (var prize in prizes.prizes)
                Debug.Log("prize value: " + prize.value);

            foreach (var lanes in config.gameSettings.timeLanes)
                Debug.Log("time lane: " + lanes);

            foreach (var experiment in realExperiments.experiments)
                Debug.Log("immediate prize value: " + experiment.immediatePrizeValue);
        }
    }
}
