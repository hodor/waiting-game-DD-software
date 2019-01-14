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

        public GameObject tutorialUI;
        public GameObject fakeExperimentUI;
        public GameObject realExperimentUI;
        public GameObject finishedGameUI;

        public GameObject GameObjects;
        public GameObject TimerLabels;
        public GameObject ButtonsUI;


        // Use this for initialization
        void Start()
        {
            SetTutorialUI();
            //SetUIRealExperiment();
        }

        // ----- Tutorial Timer -------- //
        public void SetTutorialUI()
        {
            tutorialUI.SetActive(true);
            ToggleGameUIObjects(false);
        }
        public void StartTutorial()
        {
            tutorialUI.SetActive(false);
            ButtonsUI.SetActive(false);
            TimerLabels.SetActive(true);
            GameObjects.SetActive(true);
            Tutorial();
        }

        void Tutorial()
        {
            var teachTimer = gameObject.GetComponent<TeachTimers>();
            teachTimer.StartTutorial(prefabReward, finishLine);
        }

        // ----- Fake Experiments -------- //
        public void SetUIFakeExperiment()
        {
            ToggleGameUIObjects(false);
            fakeExperimentUI.SetActive(true);
        }

        public void StartFakeExperiments()
        {
            ToggleGameUIObjects(true);
            fakeExperimentUI.SetActive(false);
            SetupFakeExperiment();
        }
        void SetupFakeExperiment()
        {
            var fakeExperiments = MainData.instanceData.fakeExperiments.experiments;
            var experimentHandler = gameObject.GetComponent<ExperimentPhaseHandler>();
            experimentHandler.SetupExperiment(prefabReward, fakeExperiments, finishLine, true);
            experimentHandler.StartExperiment();

        }

        // ----- Real Experiments -------- //
        public void SetUIRealExperiment()
        {
            ToggleGameUIObjects(false);
            realExperimentUI.SetActive(true);
        }

        public void StartRealExperiments()
        {
            ToggleGameUIObjects(true);
            realExperimentUI.SetActive(false);
            SetupRealExperiment();
        }
        void SetupRealExperiment()
        {
            var realExperiments = MainData.instanceData.realExperiments.experiments;
            var experimentHandler = gameObject.GetComponent<ExperimentPhaseHandler>();
            experimentHandler.SetupExperiment(prefabReward, realExperiments, finishLine, false);
            experimentHandler.StartExperiment();

        }


        // ----- Game Objects/UI -------- //
        void ToggleGameUIObjects(bool show)
        {
            if (show)
            {
                ButtonsUI.SetActive(true);
                GameObjects.SetActive(true);
                TimerLabels.SetActive(true);
            }else
            {
                ButtonsUI.SetActive(false);
                GameObjects.SetActive(false);
                TimerLabels.SetActive(false);
            }

        }

        public void CallbackFinishedExperiment(bool fake)
        {
            if (fake)
                SetUIRealExperiment();
            else
                FinishedGame();
        }

        // ----- Finished Game -------- //
        void FinishedGame()
        {
            finishedGameUI.SetActive(true);
            ToggleGameUIObjects(false);
        }
        public void CloseGame()
        {
            Application.Quit();
        }

    }

}
