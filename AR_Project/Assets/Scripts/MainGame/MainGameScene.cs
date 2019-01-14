using System.Collections;
using System.Collections.Generic;
using AR_Project.DataClasses.MainData;
using AR_Project.DataClasses.NestedObjects;
using AR_Project.MainGame.UI;
using AR_Project.MainGame.ExperimentsLevels.ExperimentsHandlers;
using UnityEngine;

namespace AR_Project.MainGame
{
    public class MainGameScene : MonoBehaviour
    {
       
        public GameObject prefabReward;
        public GameObject finishLine;



        // Use this for initialization
        void Start()
        {
            //Tutorial();
            SetupFakeExperiment();
        }

        void Tutorial()
        {
            var teachTimer = gameObject.GetComponent<TeachTimers>();
            teachTimer.StartTutorial(prefabReward, finishLine);
        }

        void SetupFakeExperiment()
        {
            var fakeExperiments = MainData.instanceData.fakeExperiments.experiments;
            var experimentHandler = gameObject.GetComponent<ExperimentHandler>();
            experimentHandler.SetupExperiment(prefabReward, fakeExperiments, finishLine);
            experimentHandler.StartExperiment();

        }

        //void StartNewExperimentPhase()
        //{
        //    var prizeButtons = gameObject.GetComponent<PrizeButtons>();
        //    //todo: Pass the current experiment timer
        //    prizeButtons.timerSecondPrize = 5;
        //    prizeButtons.StartSecondPrizeTimer();
        //}
        void StopCurrentExperimentPhase()
        {
            var prizeButtons = gameObject.GetComponent<PrizeButtons>();
            prizeButtons.StopTimer();
        }


    }

}
