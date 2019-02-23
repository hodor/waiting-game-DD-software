using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using AR_Project.DataClasses.NestedObjects;
using AR_Project.MainGame.GameObjects;
using AR_Project.MainGame.Prize;
using AR_Project.MainGame.UI;
using AR_Project.Savers;

namespace AR_Project.MainGame.ExperimentsLevels.ExperimentsHandlers
{
    public class ExperimentPhaseHandler : MonoBehaviour
    {
        public Text totalPoints;

        GameObject prefabReward;
        GameObject finishLine;
        List<Experiment> currentExperiments;
        Experiment currentPhase;
        ExperimentData dataHandler;
        GameObject secondPrize;
        GameObject immediatePrize;
        List<GameObject> goList;
        bool isFakeExperiment;

        public void SetupExperiment(GameObject prefabSecondPrize, List<Experiment> experiments, GameObject finishLineObj, bool fake)
        {
            currentExperiments = new List<Experiment>();
            goList = new List<GameObject>();
            prefabReward = prefabSecondPrize;
            currentExperiments = experiments;
            finishLine = finishLineObj;
            isFakeExperiment = fake;
            //totalPoints.text = "Pontos: 0";
            totalPoints.text = "Pontos: " + PlayerPrefsSaver.instance.totalPoints;
        }

        public void UpdateTotalPoints()
        {
            totalPoints.text = "Pontos: " + PlayerPrefsSaver.instance.totalPoints.ToString();
            //totalPoints.GetComponent<Animator>().Play("ScoreAnimation");
            StartCoroutine("BlinkAnimationPoints");

        }

        IEnumerator BlinkAnimationPoints()
        {
            totalPoints.color = Color.yellow;
            yield return new WaitForSeconds(0.5f);
            totalPoints.color = Color.black;
        }

        public void StartExperiment()
        {
            dataHandler = new ExperimentData(currentExperiments);
            currentPhase = dataHandler.currentExperiment();
            PlayerPrefsSaver.instance.totalPoints = 0;
            totalPoints.text = "Pontos: " + PlayerPrefsSaver.instance.totalPoints.ToString();
            DoExperimentPhase();

        }
        void DoExperimentPhase()
        {
            var prizeButtons = gameObject.GetComponent<PrizeButtons>();
            var prizeLabels = gameObject.GetComponent<PrizesHandler>();
            var sliderHandler = gameObject.GetComponent<SlidersHandler>();

            var immediateButtonText = currentPhase.immediatePrizeValue + " pontos em 0 segundos";
            var secondButtonText = currentPhase.secondPrizeValue + " pontos em " 
            + currentPhase.secondPrizeTimer + " s";
                
            prizeLabels.SetPrizesLabelsByTimer(0, currentPhase.secondPrizeTimer,
            currentPhase.immediatePrizeValue, currentPhase.secondPrizeValue);
            sliderHandler.DisableOtherSliders(0, currentPhase.secondPrizeTimer);

            Debug.Log("Experiment phase. Second timer is: " + currentPhase.secondPrizeTimer);
            if (currentPhase.isSecondPrizeAtRightButton)
            {
                prizeButtons.SetRightButton(secondButtonText, currentPhase.secondPrizeValue);
                prizeButtons.SetLeftButton(immediateButtonText, currentPhase.immediatePrizeValue);
            }
            else 
            {
                prizeButtons.SetLeftButton(secondButtonText, currentPhase.secondPrizeValue);
                prizeButtons.SetRightButton(immediateButtonText, currentPhase.immediatePrizeValue);
            }

        }

        public bool IsSecondPrizeAtRightButton()
        {
            if (currentPhase.isSecondPrizeAtRightButton)
                return true;
            else
                return false;
        }

        void NextPhase()
        {
            if (dataHandler.CheckIfThereIsMoreExperiments())
            {
                currentPhase = dataHandler.NextExperiment();
                DoExperimentPhase();
            }else
            {
                Debug.Log("Finished the game");
                var mainGameScene = gameObject.GetComponent<MainGameScene>();
                mainGameScene.CallbackFinishedExperiment(isFakeExperiment);
            }

        }



        void RespawnSecondPrize()
        {
            var respawnsScript = gameObject.GetComponent<Respawns>();
            var respawn = respawnsScript.GetRespawnByLane(currentPhase.secondPrizeTimer);
            var slider = gameObject.GetComponent<SlidersHandler>();

            //TODO: pegar prefab de acordo com o experimento atual
            secondPrize = (GameObject)Instantiate(prefabReward);
            secondPrize.SetActive(true);
            secondPrize.transform.position = respawn.transform.position;
            var objScript = secondPrize.GetComponent<PrizeGO>();
            objScript.timer = currentPhase.secondPrizeTimer;
            objScript.finalDestination = new Vector3(finishLine.transform.position.x,
                                                    secondPrize.transform.position.y, 0);
            objScript.StartMoving();
            slider.SetAndStartSliderByTimer(currentPhase.secondPrizeTimer);

        }

        void RespawnImmediatePrize()
        {
            var respawnsScript = gameObject.GetComponent<Respawns>();
            var respawn = respawnsScript.GetRespawnByPosition(0);
            var slider = gameObject.GetComponent<SlidersHandler>();

            immediatePrize = (GameObject)Instantiate(prefabReward);
            immediatePrize.SetActive(true);
            immediatePrize.transform.position = respawn.transform.position;
            var objScript = immediatePrize.GetComponent<PrizeGO>();
            objScript.timer = 0;
            objScript.finalDestination = new Vector3(finishLine.transform.position.x,
                                                    immediatePrize.transform.position.y, 0);
            objScript.StartMoving();
            slider.SetAndStartSingleSlider(0, 0);
        }

        public void CallbackFromUIButtons(bool immediatePrizeClicked)
        {
            PrizeButtons.instance.ToggleButtons(false);

            if (immediatePrizeClicked)
            {
                RespawnImmediatePrize();
                var points = currentPhase.immediatePrizePoints;
                var key = "Fase " + dataHandler.GetExperimentIndex() + " do experimento";
                PlayerPrefsSaver.instance.AddExperimentPoints(key, points);
                UpdateTotalPoints();
                //call animation for the first buttin
            }
            else{
                //call animation fro the second btn
                RespawnSecondPrize();
                var points = currentPhase.secondPrizePoints;
                var key = "Fase " + dataHandler.GetExperimentIndex() + " do experimento";
                PlayerPrefsSaver.instance.AddExperimentPoints(key, points);
                UpdateTotalPoints();
            }

        }

        public void FinishedExperiment() 
        {   
            CleanScenario();
            NextPhase();
            PrizeButtons.instance.ToggleButtons(true);
        }

        void CleanScenario()
        {
            var slider = gameObject.GetComponent<SlidersHandler>();
            slider.ResetSliders();

            Destroy(immediatePrize);
            Destroy(secondPrize);

        }

    }
}