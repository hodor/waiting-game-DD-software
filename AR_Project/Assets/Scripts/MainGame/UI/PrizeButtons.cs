using System.Collections;
using System.Collections.Generic;
using AR_Project.DataClasses.MainData;
using AR_Project.MainGame.ExperimentsLevels.ExperimentsHandlers;
using UnityEngine;
using UnityEngine.UI;

namespace AR_Project.MainGame.UI
{
    public class PrizeButtons : MonoBehaviour
    {
        public static PrizeButtons instance;

        List<Button> answersButtons;

        //public Button immediatePrizeButton;
        //public Button secondPrizeButton;
        public Image firstButtonImage;
        public Image secondButtonImage;

        public Button leftButton, rightButton;

        public AudioSource audioSource;
        public AudioClip buttonSoud;

        public List<GameObject> laneButtons;


        public List<Sprite> prizeImages;

        //public int timerSecondPrize;
        int firstTimerSet, secondTimerSet;
        int[] timers;

        public void Awake()
        {
            if (instance == null)
                instance = this;
            else if (instance != this)
                Destroy(gameObject);
        }
        public void Start()
        {
            timers = MainData.instanceData.config.gameSettings.timeLanes;
        }

        public void SetupButtons(int firstTimer, int secondTimer)
        {
            firstTimerSet = firstTimer;
            secondTimerSet = secondTimer;
  
            for(int i=0; i < timers.Length; i++)
            {
                if(timers[i] == firstTimer)
                    laneButtons[i].SetActive(true);
                if(timers[i] == secondTimer)
                    laneButtons[i].SetActive(true);
                if (timers[i] != firstTimer && timers[i] != secondTimer)
                    laneButtons[i].SetActive(false);
            }

        }

        public void ClickedOnFirstLane()
        {
            var expHandler = gameObject.GetComponent<ExperimentPhaseHandler>();
            var timer = timers[0];
            expHandler.CallbackFromUIButtons(timer);
        }
        public void ClickedOnSecondLane()
        {
            var expHandler = gameObject.GetComponent<ExperimentPhaseHandler>();
            var timer = timers[1];
            expHandler.CallbackFromUIButtons(timer);
        }
        public void ClickedOnThirdLane()
        {
            var expHandler = gameObject.GetComponent<ExperimentPhaseHandler>();
            var timer = timers[2];
            expHandler.CallbackFromUIButtons(timer);
        }
        public void ClickedOnFourthLane()
        {
            var expHandler = gameObject.GetComponent<ExperimentPhaseHandler>();
            var timer = timers[3];
            expHandler.CallbackFromUIButtons(timer);
        }
        public void ClickedOnFivethLane()
        {
            var expHandler = gameObject.GetComponent<ExperimentPhaseHandler>();
            var timer = timers[4];
            expHandler.CallbackFromUIButtons(timer);
        }

        public void DisableButtons()
        {
            foreach (var button in laneButtons)
                button.SetActive(false);
        }




        //public void ClickedOnInvisible()
        //{
        //    Debug.Log("Clicked on invisible!");
        //}

        //public void SetLeftButton(string text, int value)
        //{
        //    leftButton.GetComponentInChildren<Text>().text = text;
        //    firstButtonImage.sprite = GetPrizeImage(value);
        //}

        //public void SetRightButton(string text, int value)
        //{
        //    rightButton.GetComponentInChildren<Text>().text = text;
        //    secondButtonImage.sprite = GetPrizeImage(value);
        //}

        //public void ToggleButtons(bool avaiable)
        //{
        //    if (avaiable)
        //    {
        //        leftButton.interactable = true;
        //        rightButton.interactable = true;
        //    }else{
        //        leftButton.interactable = false;
        //        rightButton.interactable = false;
        //    }
        //}

        //public void ReleaseSecondPrizeButton()
        //{
        //    ToggleLeftButtonAvaiability(true);
        //    ToggleRightButtonAvaiability(true);
        //}

        //public void ToggleLeftButtonAvaiability(bool avaiable)
        //{
        //    if(avaiable)
        //        leftButton.interactable = true;
        //    else
        //        leftButton.interactable = false;
        //}

        //public void ToggleRightButtonAvaiability(bool avaiable)
        //{
        //    if (avaiable)
        //        rightButton.interactable = true;
        //    else
        //        rightButton.interactable = false;
        //}

        //public void ClickedOnLeftButton()
        //{
        //    var expHandler = gameObject.GetComponent<ExperimentPhaseHandler>();
        //    var isSecondPrizeAtRight = expHandler.IsSecondPrizeAtRightButton();
        //    if (isSecondPrizeAtRight)
        //        ClickedOnImmediatePrize();
        //    else
        //        ClickedOnSecondPrize();

        //    PlaySound();
        //}

        //public void ClickedOnRightButton()
        //{
        //    var expHandler = gameObject.GetComponent<ExperimentPhaseHandler>();
        //    var isSecondPrizeAtRight = expHandler.IsSecondPrizeAtRightButton();
        //    if (isSecondPrizeAtRight)
        //        ClickedOnSecondPrize();
        //    else
        //        ClickedOnImmediatePrize();

        //    PlaySound();
        //}

        public void FinishedExperiment()
        {
            var expHandler = gameObject.GetComponent<ExperimentPhaseHandler>();
            var mainGameScene = gameObject.GetComponent<MainGameScene>();
            if(mainGameScene.finishedTutorial)
                expHandler.FinishedExperiment();
        }

        void PlaySound()
        {
            audioSource.PlayOneShot(buttonSoud);
        }

        //public void ClickedOnImmediatePrize()
        //{
        //    Debug.Log("CLICKED ON IMMEDIATE PRIZE");
        //    var expHandler = gameObject.GetComponent<ExperimentPhaseHandler>();
        //    expHandler.CallbackFromUIButtons(true);
        //}

        //public void ClickedOnSecondPrize()
        //{
        //    Debug.Log("CLICKED ON SECOND PRIZE");
        //    var expHandler = gameObject.GetComponent<ExperimentPhaseHandler>();
        //    expHandler.CallbackFromUIButtons(false);
        //}

        //public void SetButtons(string textImmediate, string textSecond, int firstPrizeValue, int secondPrizeValue)
        //{
        //    immediatePrizeButton.GetComponentInChildren<Text>().text = textImmediate;
        //    secondPrizeButton.GetComponentInChildren<Text>().text = textSecond;
        //    firstButtonImage.sprite = GetPrizeImage(firstPrizeValue);
        //    secondButtonImage.sprite = GetPrizeImage(secondPrizeValue);
        //}

        public Sprite GetPrizeImage(int prizeValue)
        {
            var prizes = MainData.instanceData.prizes.prizes;

            for (int i = 0; i < prizes.Count; i++)
            {
                if (prizes[i].value == prizeValue)
                {
                    return prizeImages[i];
                }
            }
            return null;
        }
    }
}
