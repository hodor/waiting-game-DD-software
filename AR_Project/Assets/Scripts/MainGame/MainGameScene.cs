using System.Collections;
using System.Collections.Generic;
using AR_Project.DataClasses.MainData;
using AR_Project.DataClasses.NestedObjects;
using AR_Project.MainGame.UI;
using AR_Project.MainGame.ExperimentsLevels.ExperimentsHandlers;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using AR_Project.Savers;

namespace AR_Project.MainGame
{
    public class MainGameScene : MonoBehaviour
    {
       
        public GameObject prefabChar;
        public GameObject finishLine;

        public GameObject tutorialUI;
        public GameObject fakeExperimentUI;
        public GameObject realExperimentUI;

        public GameObject SliderLanes;
        public Image backgroundImg;

        public Sprite bgTutorial, bgExperiments;

        public GameObject GameObjects;
        public GameObject TimerLabels;
        public GameObject ButtonsUI;


        // Use this for initialization
        void Start()
        {
            prefabChar = PlayerPrefsSaver.instance.character;
            SetTutorialUI();
        }

        // ----- Tutorial Timer -------- //
        public void SetTutorialUI()
        {
            backgroundImg.sprite = bgTutorial;
            tutorialUI.SetActive(true);
            ToggleGameUIObjects(false);
        }
        public void StartTutorial()
        {
            tutorialUI.SetActive(false);
            ButtonsUI.SetActive(false);
            TimerLabels.SetActive(true);
            GameObjects.SetActive(true);
            SliderLanes.SetActive(true);
            Tutorial();
        }

        void Tutorial()
        {
            var teachTimer = gameObject.GetComponent<TeachTimers>();
            teachTimer.StartTutorial(prefabChar, finishLine);
        }

        // ----- Fake Experiments -------- //
        public void SetUIFakeExperiment()
        {
            backgroundImg.sprite = bgExperiments;
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
            experimentHandler.SetupExperiment(prefabChar, fakeExperiments, finishLine, true);
            experimentHandler.StartExperiment();

        }

        // ----- Real Experiments -------- //
        public void SetUIRealExperiment()
        {
            backgroundImg.sprite = bgExperiments;
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
            experimentHandler.SetupExperiment(prefabChar, realExperiments, finishLine, false);
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
                SliderLanes.SetActive(true);
            }else
            {
                ButtonsUI.SetActive(false);
                GameObjects.SetActive(false);
                TimerLabels.SetActive(false);
                SliderLanes.SetActive(false);
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
            SceneManager.LoadScene("FinalScene");
        }

    }

}
