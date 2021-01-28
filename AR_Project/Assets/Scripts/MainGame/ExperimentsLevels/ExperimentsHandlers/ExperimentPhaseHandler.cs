using System;
using System.Collections;
using System.Collections.Generic;
using AR_Project.DataClasses.MainData;
using AR_Project.DataClasses.NestedObjects;
using AR_Project.MainGame.GameObjects;
using AR_Project.MainGame.Prize;
using AR_Project.MainGame.UI;
using AR_Project.Savers;
using Output;
using UnityEngine;
using UnityEngine.UI;

namespace AR_Project.MainGame.ExperimentsLevels.ExperimentsHandlers
{
    public class ExperimentPhaseHandler : MonoBehaviour
    {
        private List<Experiment> currentExperiments;
        private Experiment currentPhase;
        private ExperimentData dataHandler;
        private GameObject finishLine;
        private List<GameObject> goList;
        private GameObject immediatePrize;
        private bool isFakeExperiment;
        public GameObject points;

        private GameObject prefabReward;
        private GameObject secondPrize;
        public Text totalPoints;

        public void SetupExperiment(GameObject prefabSecondPrize, List<Experiment> experiments,
            GameObject finishLineObj, bool fake)
        {
            goList = new List<GameObject>();
            prefabReward = prefabSecondPrize;
            currentExperiments = experiments;
            finishLine = finishLineObj;
            isFakeExperiment = fake;

            points.SetActive(true);
            totalPoints.text = string.Format("{0}: {1}", MainData.instanceData.config.GetTexts().score, 0);
            // Reset points on training
            if (!PlayerPrefsSaver.instance.isTraining)
            {
                if (!PlayerPrefsSaver.instance.phasePoints.ContainsKey(PlayerPrefsSaver.instance.gameType))
                    PlayerPrefsSaver.instance.phasePoints.Add(PlayerPrefsSaver.instance.gameType, 0);

                PlayerPrefsSaver.instance.phasePoints[PlayerPrefsSaver.instance.gameType] = 0;
            }
        }

        public void UpdateTotalPoints()
        {
            totalPoints.text = string.Format("{0}: {1}", MainData.instanceData.config.GetTexts().score,
                PlayerPrefsSaver.instance.phasePoints[PlayerPrefsSaver.instance.gameType]);
            StartCoroutine("BlinkAnimationPoints");
        }

        private IEnumerator BlinkAnimationPoints()
        {
            totalPoints.color = Color.yellow;
            yield return new WaitForSeconds(0.5f);
            totalPoints.color = Color.white;
        }

        public void StartExperiment()
        {
            dataHandler = new ExperimentData(currentExperiments);
            currentPhase = dataHandler.currentExperiment();
            totalPoints.text = string.Format("{0}: {1}", MainData.instanceData.config.GetTexts().score, 0);
            DoExperimentPhase();
        }

        private void DoExperimentPhase()
        {
            var prizeButtons = gameObject.GetComponent<PrizeButtons>();
            var prizeLabels = gameObject.GetComponent<PrizesHandler>();
            var sliderHandler = gameObject.GetComponent<SlidersHandler>();

            prizeLabels.SetPrizesLabelsByLane(1, currentPhase.secondPrizeLane,
                currentPhase.immediatePrizeId, currentPhase.secondPrizeId);
            sliderHandler.DisableOtherSliders(1, currentPhase.secondPrizeLane);
            prizeButtons.SetupButtons(1, currentPhase.secondPrizeLane);
            MainGameScene.ExperimentStartDT = DateTime.Now;
        }

        private void NextPhase()
        {
            currentPhase = dataHandler.NextExperiment();
            if (currentPhase != null)
            {
                DoExperimentPhase();
            }
            else
            {
                var mainGameScene = gameObject.GetComponent<MainGameScene>();
                mainGameScene.SetFinalRoundScene();
            }
        }

        private void RespawnSecondPrize()
        {
            var shouldMoveSlowly = PlayerPrefsSaver.instance.ShouldMoveSlowly();
            var respawnsScript = gameObject.GetComponent<Respawns>();
            var respawn = respawnsScript.GetRespawnByLane(currentPhase.secondPrizeLane);
            var slider = gameObject.GetComponent<SlidersHandler>();

            //TODO: pegar prefab de acordo com o experimento atual
            secondPrize = Instantiate(prefabReward);
            secondPrize.SetActive(true);
            secondPrize.transform.position = respawn.transform.position;
            var objScript = secondPrize.GetComponent<PrizeGO>();
            objScript.timer = currentPhase.GetSecondLaneTimer();
            objScript.finalDestination = new Vector3(finishLine.transform.position.x,
                secondPrize.transform.position.y, 0);
            objScript.StartMoving(false);

            if (shouldMoveSlowly)
                slider.SetAndStartSliderByTimer(currentPhase.GetSecondLaneTimer());
        }

        private void RespawnImmediatePrize()
        {
            var shouldMoveSlowly = PlayerPrefsSaver.instance.ShouldMoveSlowly();
            var respawnsScript = gameObject.GetComponent<Respawns>();
            var respawn = respawnsScript.GetRespawnByPosition(0);
            var slider = gameObject.GetComponent<SlidersHandler>();

            immediatePrize = Instantiate(prefabReward);
            immediatePrize.SetActive(true);
            immediatePrize.transform.position = respawn.transform.position;
            var objScript = immediatePrize.GetComponent<PrizeGO>();
            objScript.timer = 0;
            objScript.finalDestination = new Vector3(finishLine.transform.position.x,
                immediatePrize.transform.position.y, 0);
            objScript.StartMoving(false);

            if (shouldMoveSlowly)
                slider.SetAndStartSingleSlider(0, 0);
        }

        public void CallbackFromUIButtons(int laneClicked)
        {
            if (currentPhase == null) return;
            var timeDiff = DateTime.Now - MainGameScene.ExperimentStartDT;
            PrizeButtons.instance.DisableButtons();
            if (laneClicked == 1)
            {
                var prizeValue = currentPhase.GetImmediatePrizeValue();

                PlayerPrefsSaver.instance.AddExperimentPoints(prizeValue);
                Out.Instance.SaveExperimentData(currentPhase, prizeValue,
                    PlayerPrefsSaver.instance,
                    timeDiff.TotalSeconds);
                RespawnImmediatePrize();
            }
            else if (laneClicked == currentPhase.secondPrizeLane)
            {
                if (currentPhase == null) return;
                var prizeValue = currentPhase.GetSecondPrizeValue();

                PlayerPrefsSaver.instance.AddExperimentPoints(prizeValue);
                Out.Instance.SaveExperimentData(currentPhase, prizeValue, PlayerPrefsSaver.instance,
                    timeDiff.TotalSeconds);
                RespawnSecondPrize();
            }
        }

        public void FinishedExperiment()
        {
            CleanScenario();
            NextPhase();
            //PrizeButtons.instance.ToggleButtons(true);
        }

        private void CleanScenario()
        {
            var slider = gameObject.GetComponent<SlidersHandler>();
            slider.ResetSliders();

            Destroy(immediatePrize);
            Destroy(secondPrize);
        }
    }
}