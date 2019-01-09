using System;
using AR_Project.JsonToClasses;
using UnityEngine;

namespace AR_Project.DataClasses.MainData
{
    public class MainData: MonoBehaviour
    {
        public MainData instanceData;
        public Prizes prizes;
        public GameConfig config;
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

            foreach (var prize in prizes.prizes)
                Debug.Log("prize value: " + prize.value);
        }
    }
}
