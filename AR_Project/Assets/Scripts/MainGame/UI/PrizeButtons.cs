using System.Collections.Generic;
using AR_Project.DataClasses.MainData;
using AR_Project.DataClasses.NestedObjects;
using AR_Project.MainGame.ExperimentsLevels.ExperimentsHandlers;
using UnityEngine;

namespace AR_Project.MainGame.UI
{
    public class PrizeButtons : MonoBehaviour
    {
        public static PrizeButtons instance;
        private List<LaneTime> _settings;

        public AudioSource audioSource;
        public AudioClip buttonSoud;

        private int firstTimerSet, secondTimerSet;

        private bool IsHandlingLaneClick;

        public List<GameObject> laneButtons;

        public void Awake()
        {
            if (instance == null)
                instance = this;
            else if (instance != this)
                Destroy(gameObject);
        }

        public void Start()
        {
            _settings = MainData.instanceData.config.laneTimes;
        }

        public void SetupButtons(int firstLane, int secondLane)
        {
            firstTimerSet = firstLane;
            secondTimerSet = secondLane;

            for (var i = 0; i < _settings.Count; i++)
            {
                if (_settings[i].lane == firstLane)
                    laneButtons[i].SetActive(true);
                if (_settings[i].lane == secondLane)
                    laneButtons[i].SetActive(true);
                if (_settings[i].lane != firstLane && _settings[i].lane != secondLane)
                    laneButtons[i].SetActive(false);
            }
        }

        public void ClickedOnLane(int laneNumber)
        {
            if (IsHandlingLaneClick) return;
            IsHandlingLaneClick = true;
            var expHandler = gameObject.GetComponent<ExperimentPhaseHandler>();
            var timer = _settings[laneNumber].lane;
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
            if (mainGameScene.finishedTutorial)
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