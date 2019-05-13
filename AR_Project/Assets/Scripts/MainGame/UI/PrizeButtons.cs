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

        public AudioSource audioSource;
        public AudioClip buttonSoud;

        public List<GameObject> laneButtons;

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

        private bool IsHandlingLaneClick = false;
        public void ClickedOnLane(int laneNumber)
        {
            if (IsHandlingLaneClick) return;
            IsHandlingLaneClick = true;
            var expHandler = gameObject.GetComponent<ExperimentPhaseHandler>();
            var timer = timers[laneNumber];
            expHandler.CallbackFromUIButtons(timer);
            IsHandlingLaneClick = false;
        }

        public void DisableButtons()
        {
            foreach (var button in laneButtons)
                button.SetActive(false);
        }

        public void FinishedExperiment()
        {
            var expHandler = gameObject.GetComponent<ExperimentPhaseHandler>();
            var mainGameScene = gameObject.GetComponent<MainGameScene>();
            if(mainGameScene.finishedTutorial)
                expHandler.FinishedExperiment();
        }

        public void PlaySound()
        {
            audioSource.PlayOneShot(buttonSoud);
        }

        public void AnimateTotalPointsPoints() 
        {
            var expHandler = gameObject.GetComponent<ExperimentPhaseHandler>();
            PlaySound();
            expHandler.UpdateTotalPoints();
        }

    }
}
