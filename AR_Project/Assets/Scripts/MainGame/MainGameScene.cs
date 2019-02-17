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

        public GameObject pointsUI;
        public GameObject tutorialUI;
        public GameObject fakeExperimentUI;
        public GameObject realExperimentUI;

        public GameObject SliderLanes;
        public Image backgroundImg;

        public Sprite bgTutorial, bgExperiments;

        public GameObject GameObjects;
        public GameObject TimerLabels;
        public GameObject ButtonsUI;

        public GameObject prizeLabels;

        public Text tutorialTitle;
        public Text fakeExperimentTitle;
        public Text realExperimentTitle;

        public bool finishedTutorial;

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
            tutorialTitle.text = MainData.instanceData.content.titleTimeInstructions;
            prizeLabels.SetActive(false);
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

        public void ComeBackFromTutorial()
        {
            var hasDoneFakeFirst = PlayerPrefsSaver.instance.hasDoneFakeFirst;
            if (hasDoneFakeFirst)
            {
                SetUIRealExperiment();
                PlayerPrefsSaver.instance.isFirstExperiment = true;
            }else{
                SetUIFakeExperiment();
                PlayerPrefsSaver.instance.isFirstExperiment = true;
            }

            finishedTutorial = true;

        }

        // ----- Fake Experiments -------- //
        public void SetUIFakeExperiment()
        {
            fakeExperimentTitle.text = MainData.instanceData.content.titleFakeExperiment;
            backgroundImg.sprite = bgExperiments;
            ToggleGameUIObjects(false);
            fakeExperimentUI.SetActive(true);
        }

        public void StartFakeExperiments()
        {
            ToggleGameUIObjects(true);
            fakeExperimentUI.SetActive(false);
            prizeLabels.SetActive(true);
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
            realExperimentTitle.text = MainData.instanceData.content.titleRealExperiment;
            backgroundImg.sprite = bgExperiments;
            ToggleGameUIObjects(false);

            realExperimentUI.SetActive(true);
        }

        public void StartRealExperiments()
        {
            ToggleGameUIObjects(true);
            realExperimentUI.SetActive(false);
            prizeLabels.SetActive(true);
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
                pointsUI.SetActive(true);
            }else
            {
                ButtonsUI.SetActive(false);
                GameObjects.SetActive(false);
                TimerLabels.SetActive(false);
                SliderLanes.SetActive(false);
                pointsUI.SetActive(false);
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
