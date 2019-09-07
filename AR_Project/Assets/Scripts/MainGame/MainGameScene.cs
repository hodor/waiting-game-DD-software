using System;
using System.Collections;
using System.Collections.Generic;
using AR_Project.DataClasses.MainData;
using AR_Project.DataClasses.NestedObjects;
using AR_Project.MainGame.ExperimentsLevels.ExperimentsHandlers;
using AR_Project.Savers;
using Output;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace AR_Project.MainGame
{
    public class MainGameScene : MonoBehaviour
    {
        public static DateTime ExperimentStartDT;
        public Image backgroundImg;

        public Sprite bgTutorial, bgExperiments;
        public Text fakeExperimentTitle;
        public GameObject fakeExperimentUI;

        public GameObject finalRoundsScene;
        public Text finalRoundText;

        public bool finishedTutorial;
        public GameObject finishLine;

        public GameObject GameObjects;

        public GameObject pointsUI;
        public GameObject prefabChar;

        public GameObject prizeLabels;
        public Text realExperimentTitle;
        public GameObject realExperimentUI;
        public Text patienceExperimentTitle;
        public GameObject patienceExperimentUI;

        public GameObject SliderLanes;
        public GameObject TimerLabels;

        public Text tutorialTitle;
        public GameObject tutorialUI;

        // Use this for initialization
        private void Start()
        {
            prefabChar = PlayerPrefsSaver.instance.character;
            SetTutorialUI();
        }

        public void SetFinalRoundScene()
        {
            ToggleGameUIObjects(false);
            finalRoundsScene.SetActive(true);
            var points = PlayerPrefsSaver.instance.phasePoints[PlayerPrefsSaver.instance.gameType];
            finalRoundText.text = "Parabéns, você ganhou " + points + " pontos nessa tarefa!";
            StartCoroutine("StartNewRound");
        }

        private IEnumerator StartNewRound()
        {
            yield return new WaitForSeconds(2f);
            finalRoundsScene.SetActive(false);
            CallbackFinishedExperiment();
        }


        // ----- Tutorial Timer -------- //
        public void SetTutorialUI()
        {
            backgroundImg.sprite = bgTutorial;
            tutorialTitle.text = MainData.instanceData.config.texts.timeInstructions;
            prizeLabels.SetActive(false);
            tutorialUI.SetActive(true);
            ToggleGameUIObjects(false);
        }

        public void StartTutorial()
        {
            tutorialUI.SetActive(false);
            TimerLabels.SetActive(true);
            GameObjects.SetActive(true);
            SliderLanes.SetActive(true);
            Tutorial();
        }

        private void Tutorial()
        {
            PlayerPrefsSaver.instance.isTutorial = true;
            var teachTimer = gameObject.GetComponent<TeachTimers>();
            teachTimer.StartTutorial(prefabChar, finishLine);
        }

        public void ComeBackFromTutorial()
        {
            PlayerPrefsSaver.instance.isTutorial = false;

            Out.Instance.StartExperiments(PlayerPrefsSaver.instance);
            finishedTutorial = true;
            PlayerPrefsSaver.instance.isTraining = true;

            SetupNextExperiment();
        }

        public void SetupNextExperiment()
        {
            var training = PlayerPrefsSaver.instance.isTraining;
            var type = PlayerPrefsSaver.instance.gameType;
            var texts = MainData.instanceData.config.texts;
            backgroundImg.sprite = bgExperiments;

            switch (type)
            {
                case GameType.Imaginarium:
                    fakeExperimentTitle.text = training ? texts.trainingImaginarium : texts.experimentImaginarium;
                    ToggleGameUIObjects(false);
                    fakeExperimentUI.SetActive(true);
                    realExperimentUI.SetActive(false);
                    patienceExperimentUI.SetActive(false);
                    break;
                case GameType.Real:
                    realExperimentTitle.text = training ? texts.trainingReal : texts.experimentReal;
                    ToggleGameUIObjects(false);
                    fakeExperimentUI.SetActive(false);
                    realExperimentUI.SetActive(true);
                    patienceExperimentUI.SetActive(false);
                    break;
                case GameType.Patience:
                    patienceExperimentTitle.text = training ? texts.trainingPatience : texts.experimentPatience;
                    ToggleGameUIObjects(false);
                    fakeExperimentUI.SetActive(false);
                    realExperimentUI.SetActive(false);
                    patienceExperimentUI.SetActive(true);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public void HandleClickedStart()
        {
            var training = PlayerPrefsSaver.instance.isTraining;
            ToggleGameUIObjects(true);
            fakeExperimentUI.SetActive(false);
            realExperimentUI.SetActive(false);
            patienceExperimentUI.SetActive(false);
            prizeLabels.SetActive(true);
            if (training)
            {
                SetupTraining();
            }
            else
            {
                SetupExperiment();
            }
        }

        private void Start(List<Experiment> data)
        {
            var experimentHandler = gameObject.GetComponent<ExperimentPhaseHandler>();
            experimentHandler.SetupExperiment(prefabChar, data, finishLine, true);
            experimentHandler.StartExperiment();
        }

        private void SetupTraining()
        {
            var trainings = ListShuffler.GetPseudoRandomExperiments(MainData.instanceData.config.trainings);
            Start(trainings);
        }

        private void SetupExperiment()
        {
            var experiments = ListShuffler.GetPseudoRandomExperiments(MainData.instanceData.config.experiments);
            Start(experiments);
        }

        // ----- Game Objects/UI -------- //
        private void ToggleGameUIObjects(bool show)
        {
            if (show)
            {
                GameObjects.SetActive(true);
                TimerLabels.SetActive(true);
                SliderLanes.SetActive(true);
                pointsUI.SetActive(true);
                prizeLabels.SetActive(true);
            }
            else
            {
                GameObjects.SetActive(false);
                TimerLabels.SetActive(false);
                SliderLanes.SetActive(false);
                pointsUI.SetActive(false);
                prizeLabels.SetActive(false);
            }
        }

        public void CallbackFinishedExperiment()
        {
            var training = PlayerPrefsSaver.instance.isTraining;
            var gameType = PlayerPrefsSaver.instance.gameType;
            
            if(!training)
                Out.Instance.SaveTotalPoints(PlayerPrefsSaver.instance);
            
            PlayerPrefsSaver.instance.isTraining = !PlayerPrefsSaver.instance.isTraining;
            
            var gameIndex = PlayerPrefsSaver.instance.gameTypeOrder.IndexOf(gameType);
            // If we're at the last index
            if (!training && gameIndex == PlayerPrefsSaver.instance.gameTypeOrder.Count - 1)
            {
                FinishedGame();
            }
            else
            {
                if(!training)
                    PlayerPrefsSaver.instance.gameType = PlayerPrefsSaver.instance.gameTypeOrder[gameIndex + 1];
                SetupNextExperiment();
            }
        }

        // ----- Finished Game -------- //
        private void FinishedGame()
        {
            SceneManager.LoadScene("FinalScene");
        }
    }
}