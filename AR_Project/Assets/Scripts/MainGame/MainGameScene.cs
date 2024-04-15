using System;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
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

        public Text TimerLane01;
        public Text TimerLane02;
        public Text TimerLane03;
        public Text TimerLane04;
        public Text TimerLane05;

        public Text tutorialTitle;
        public GameObject tutorialUI;

        // Use this for initialization
        private void Start()
        {
            prefabChar = PlayerPrefsSaver.instance.character;
            SetupTimerLabels();
            SetTutorialUI();
        }

        public void SetFinalRoundScene()
        {
            ToggleGameUIObjects(false);
            finalRoundsScene.SetActive(true);
            var points = PlayerPrefsSaver.instance.phasePoints[PlayerPrefsSaver.instance.gameType];
            var begin = MainData.instanceData.config.GetTexts().taskScoreBegin;
            var end = MainData.instanceData.config.GetTexts().taskScoreEnd;
            finalRoundText.text = string.Format("{0} {1} {2}", begin, points, end);
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
            tutorialTitle.text = MainData.instanceData.config.GetTexts().timeInstructions;
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

        private void SkipAllExperiments(List<Experiment> experiments)
        {
            foreach (var experiment in experiments)
            {
                PlayerPrefsSaver.instance.AddExperimentPoints(0);
                Out.Instance.SaveExperimentData(experiment, 0, experiment.delayedRewardLane,
                    PlayerPrefsSaver.instance,0);
            }
        }

        private void SkipCurrentExperiment()
        {
            // Emulate the experiments
            if (PlayerPrefsSaver.instance.isTraining)
            {
                var trainings = ListShuffler.GetPseudoRandomExperiments(MainData.instanceData.config.trainings);
                SkipAllExperiments(trainings);
            }
            else
            {
                var experiments = ListShuffler.GetPseudoRandomExperiments(MainData.instanceData.config.experiments);
                SkipAllExperiments(experiments);
            }

            // Go to the next experiment
            CallbackFinishedExperiment();
        }

        public void SetupNextExperiment()
        {
            var training = PlayerPrefsSaver.instance.isTraining;
            var type = PlayerPrefsSaver.instance.gameType;
            var texts = MainData.instanceData.config.GetTexts();
            backgroundImg.sprite = bgExperiments;

            switch (type)
            {
                case GameType.Imaginarium:
                    if (!MainData.instanceData.config.debug.imaginaryGameEnabled)
                    {
                        SkipCurrentExperiment();
                        return;
                    }
                    fakeExperimentTitle.text = training ? texts.imaginaryGameTraining : texts.imaginaryGame;
                    ToggleGameUIObjects(false);
                    fakeExperimentUI.SetActive(true);
                    realExperimentUI.SetActive(false);
                    patienceExperimentUI.SetActive(false);
                    break;
                case GameType.Real:
                    if (!MainData.instanceData.config.debug.realGameEnabled)
                    {
                        SkipCurrentExperiment();
                        return;
                    }
                    realExperimentTitle.text = training ? texts.realGameTraining : texts.realGame;
                    ToggleGameUIObjects(false);
                    fakeExperimentUI.SetActive(false);
                    realExperimentUI.SetActive(true);
                    patienceExperimentUI.SetActive(false);
                    break;
                case GameType.Patience:
                    if (!MainData.instanceData.config.debug.patienceGameEnabled)
                    {
                        SkipCurrentExperiment();
                        return;
                    }
                    patienceExperimentTitle.text = training ? texts.patienceGameTraining : texts.patienceGame;
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

        //Get the timers from config and adding into the main scene
        private void SetupTimerLabels()
        {
            var laneTimes = MainData.instanceData.config.laneTimes;

            foreach (var lane in laneTimes)
            {
                var text = string.Format("{0} s", lane.time);
                switch (lane.lane)
                {
                    case 1:
                        TimerLane01.text = text;
                        break;
                    case 2:
                        TimerLane02.text = text;
                        break;
                    case 3:
                        TimerLane03.text = text;
                        break;
                    case 4:
                        TimerLane04.text = text;
                        break;
                    case 5:
                        TimerLane05.text = text;
                        break;
                }
            }
        }
    }
}