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

        public GameObject finalRoundsScene;
        public Text finalRoundText;

        public GameObject SliderLanes;
        public Image backgroundImg;

        public Sprite bgTutorial, bgExperiments;

        public GameObject GameObjects;
        public GameObject TimerLabels;  

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

        public void SetFinalRoundScene()
        {
            ToggleGameUIObjects(false);
            finalRoundsScene.SetActive(true);
            var points = PlayerPrefsSaver.instance.totalPoints;
            finalRoundText.text = "Parabéns, você ganhou " + points +  " pontos nessa tarefa!";
            StartCoroutine("StartNewRound");
        }

        IEnumerator StartNewRound()
        {
            yield return new WaitForSeconds(2f);
            finalRoundsScene.SetActive(false);
            CallbackFinishedExperiment();
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
            finishedTutorial = true;
            int randomizer = Random.Range(0, 10);
            bool willBeImaginariumFirst = randomizer % 2 == 0 ? true : false;

            if (willBeImaginariumFirst)
            {
                Debug.Log("IMAGINARIUMMMM FIRST");
                PlayerPrefsSaver.instance.imaginariumFirst = true;
                PlayerPrefsSaver.instance.isTraining = true;
                PlayerPrefsSaver.instance.isImaginarium = true;
                SetupImaginariumExperiment();
            }else
            {
                Debug.Log("NON IMAGINARIUM FIRST");
                PlayerPrefsSaver.instance.imaginariumFirst = false;
                PlayerPrefsSaver.instance.isTraining = true;
                PlayerPrefsSaver.instance.isImaginarium = false;
                SetupNotImaginariumExperiment();
            }

        }

        public void SetupImaginariumExperiment()
        {
            Debug.Log("SETUP IMAGINARIUM EXPERIMENT");
            var training = PlayerPrefsSaver.instance.isTraining;
            //var imaginariumFirst = PlayerPrefsSaver.instance.imaginariumFirst;
            //var isImaginarium = PlayerPrefsSaver.instance.isImaginarium;
            if (training) {
                PlayerPrefsSaver.instance.totalPoints = 0;
                Debug.Log("IS TRAINING");
                SetUIFakeExperiment();
            }else
            {
                Debug.Log("IS NOT TRAINING");
                SetUIRealExperiment();
            }


        }

        public void SetupNotImaginariumExperiment()
        {
            Debug.Log("SETUP NON IMAGINARIUM EXPERIMENT");
            var training = PlayerPrefsSaver.instance.isTraining;
            if (training)
            {
                PlayerPrefsSaver.instance.totalPoints = 0;
                Debug.Log("IS TRAINING");
                SetUIFakeExperiment();
            }
            else
            {
                Debug.Log("IS NOT TRAINING");
                SetUIRealExperiment();
            }
        }

        // ----- Fake Experiments -------- //
        public void SetUIFakeExperiment()
        {
            //mudar textos
            var isImaginarium = PlayerPrefsSaver.instance.isImaginarium;
            if (isImaginarium)
                fakeExperimentTitle.text = MainData.instanceData.content.titleTrainingImaginarium;
            else
                fakeExperimentTitle.text = MainData.instanceData.content.titleTrainingNonImaginarium;

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
            var isImaginarium = PlayerPrefsSaver.instance.isImaginarium;
            if (isImaginarium)
                realExperimentTitle.text = MainData.instanceData.content.titleExperimentImaginarium;
            else
                realExperimentTitle.text = MainData.instanceData.content.titleExperimentNonImaginarium;

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
            var isImaginarium = PlayerPrefsSaver.instance.isImaginarium;
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

        public void CallbackFinishedExperiment ()
        {
            //(bool training, bool imaginariumFirst, bool isImaginarium
            var training = PlayerPrefsSaver.instance.isTraining;
            var imaginariumFirst = PlayerPrefsSaver.instance.imaginariumFirst;
            var isImaginarium = PlayerPrefsSaver.instance.isImaginarium;

            Debug.Log("Is training? " + training);
            Debug.Log("Is imaginariumFirst? " + imaginariumFirst);
            Debug.Log("isImaginarium? " + isImaginarium);

            if (isImaginarium && training)
            {
                PlayerPrefsSaver.instance.isTraining = false;
                SetupImaginariumExperiment();
            }
            if(isImaginarium && !training)
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

            if(!isImaginarium && training)
            {
                PlayerPrefsSaver.instance.isTraining = false;
                SetupNotImaginariumExperiment();
            }

            if(!isImaginarium && !training)
            {
                if (!imaginariumFirst)
                {
                    PlayerPrefsSaver.instance.isImaginarium = true;
                    PlayerPrefsSaver.instance.isTraining = true;
                    SetupImaginariumExperiment();
                }else
                {
                    FinishedGame();
                }
            }
        }

        // ----- Finished Game -------- //
        void FinishedGame()
        {
            SceneManager.LoadScene("FinalScene");
        }

    }

}
