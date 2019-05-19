using System;
using System.Collections;
using AR_Project.DataClasses.MainData;
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
            var points = PlayerPrefsSaver.instance.totalPoints;
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
            var teachTimer = gameObject.GetComponent<TeachTimers>();
            teachTimer.StartTutorial(prefabChar, finishLine);
        }

        public void ComeBackFromTutorial()
        {
            Out.Instance.StartExperiments();
            finishedTutorial = true;
            var randomizer = Random.Range(0, 9);
            var imaginaryFirst = randomizer % 2 == 0;
            if (ARDebug.Debugging && ARDebug.AlwaysImaginaryFirst)
                imaginaryFirst = true;

            if (imaginaryFirst)
            {
                PlayerPrefsSaver.instance.imaginariumFirst = true;
                PlayerPrefsSaver.instance.isTraining = true;
                PlayerPrefsSaver.instance.isImaginarium = true;
                SetupImaginariumExperiment();
            }
            else
            {
                PlayerPrefsSaver.instance.imaginariumFirst = false;
                PlayerPrefsSaver.instance.isTraining = true;
                PlayerPrefsSaver.instance.isImaginarium = false;
                SetupNotImaginariumExperiment();
            }
        }

        public void SetupImaginariumExperiment()
        {
            var training = PlayerPrefsSaver.instance.isTraining;
            if (training)
            {
                PlayerPrefsSaver.instance.totalPoints = 0;
                SetUIFakeExperiment();
            }
            else
            {
                SetUIRealExperiment();
            }
        }

        public void SetupNotImaginariumExperiment()
        {
            var training = PlayerPrefsSaver.instance.isTraining;
            if (training)
            {
                PlayerPrefsSaver.instance.totalPoints = 0;
                SetUIFakeExperiment();
            }
            else
            {
                SetUIRealExperiment();
            }
        }

        // ----- Fake Experiments -------- //
        public void SetUIFakeExperiment()
        {
            //mudar textos
            var isImaginarium = PlayerPrefsSaver.instance.isImaginarium;
            if (isImaginarium)
                fakeExperimentTitle.text = MainData.instanceData.config.texts.trainingImaginarium;
            else
                fakeExperimentTitle.text = MainData.instanceData.config.texts.trainingReal;

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

        private void SetupFakeExperiment()
        {
            var fakeExperiments = ListShuffler.GetPseudoRandomExperiments(MainData.instanceData.config.trainings);
            var experimentHandler = gameObject.GetComponent<ExperimentPhaseHandler>();
            experimentHandler.SetupExperiment(prefabChar, fakeExperiments, finishLine, true);
            experimentHandler.StartExperiment();
        }

        // ----- Real Experiments -------- //
        public void SetUIRealExperiment()
        {
            var isImaginarium = PlayerPrefsSaver.instance.isImaginarium;
            if (isImaginarium)
                realExperimentTitle.text = MainData.instanceData.config.texts.experimentImaginarium;
            else
                realExperimentTitle.text = MainData.instanceData.config.texts.experimentReal;

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

        private void SetupRealExperiment()
        {
            var realExperiments = ListShuffler.GetPseudoRandomExperiments(MainData.instanceData.config.experiments);
            var experimentHandler = gameObject.GetComponent<ExperimentPhaseHandler>();
            experimentHandler.SetupExperiment(prefabChar, realExperiments, finishLine, false);
            experimentHandler.StartExperiment();
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
            //(bool training, bool imaginariumFirst, bool isImaginarium
            var training = PlayerPrefsSaver.instance.isTraining;
            var imaginariumFirst = PlayerPrefsSaver.instance.imaginariumFirst;
            var isImaginarium = PlayerPrefsSaver.instance.isImaginarium;

            if (isImaginarium && training)
            {
                PlayerPrefsSaver.instance.isTraining = false;
                SetupImaginariumExperiment();
            }

            if (isImaginarium && !training)
            {
                if (imaginariumFirst)
                {
                    //setar o teste real
                    PlayerPrefsSaver.instance.isImaginarium = false;
                    PlayerPrefsSaver.instance.isTraining = true;
                    SetupNotImaginariumExperiment();
                }
                else
                {
                    //acabar o jogo
                    FinishedGame();
                }
            }

            if (!isImaginarium && training)
            {
                PlayerPrefsSaver.instance.isTraining = false;
                SetupNotImaginariumExperiment();
            }

            if (!isImaginarium && !training)
            {
                if (!imaginariumFirst)
                {
                    PlayerPrefsSaver.instance.isImaginarium = true;
                    PlayerPrefsSaver.instance.isTraining = true;
                    SetupImaginariumExperiment();
                }
                else
                {
                    FinishedGame();
                }
            }
        }

        // ----- Finished Game -------- //
        private void FinishedGame()
        {
            SceneManager.LoadScene("FinalScene");
        }
    }
}