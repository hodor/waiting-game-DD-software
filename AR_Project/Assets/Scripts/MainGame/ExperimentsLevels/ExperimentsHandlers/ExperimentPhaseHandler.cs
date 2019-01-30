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
            //TODO: Remove prefab second prize, we will get it dinamically later
            prefabReward = prefabSecondPrize;
            currentExperiments = experiments;
            finishLine = finishLineObj;
            isFakeExperiment = fake;
        }

        public void StartExperiment()
        {
            dataHandler = new ExperimentData(currentExperiments);
            currentPhase = dataHandler.currentExperiment();
            Debug.Log("Current experiment second prize timer: " + currentPhase.secondPrizeTimer);
            DoExperimentPhase();

        }
        void DoExperimentPhase()
        {
            var prizeButtons = gameObject.GetComponent<PrizeButtons>();
            var immediateButtonText = currentPhase.immediatePrizeValue + " moedas";
            var secondButtonText = currentPhase.secondPrizeValue + " moedas";

            Debug.Log("Experiment phase. Second timer is: " + currentPhase.secondPrizeTimer);
            if (currentPhase.isSecondPrizeAtRightButton)
            {
                prizeButtons.SetRightButton(secondButtonText, currentPhase.secondPrizeValue);
                prizeButtons.SetLeftButton(immediateButtonText, currentPhase.immediatePrizeValue);
                prizeButtons.ToggleRightButtonAvaiability(false);
                prizeButtons.ToggleLeftButtonAvaiability(true);
            }
            else 
            {
                prizeButtons.SetLeftButton(secondButtonText, currentPhase.secondPrizeValue);
                prizeButtons.SetRightButton(immediateButtonText, currentPhase.immediatePrizeValue);
                prizeButtons.ToggleRightButtonAvaiability(true);
                prizeButtons.ToggleLeftButtonAvaiability(false);
            }

            RespawnSecondPrize();
            RespawnImmediatePrize();
         

        }

        public bool IsSecondPrizeAtRightButton()
        {
            if (currentPhase.isSecondPrizeAtRightButton)
                return true;
            else
                return false;
        }

        //na logica dos botoes de premio, chamar essa função
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

            //TODO: pegar o prefab de acordo com o experimento atual
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
            //TODO: Add pontuação no playerprefs de acordo com a fase e o experimento
            if(immediatePrizeClicked)
            {
                var points = currentPhase.immediatePrizePoints;
                var key = "Fase " + dataHandler.GetExperimentIndex() + " do experimento";
                PlayerPrefsSaver.instance.AddExperimentPoints(key, points);
            }else
            {
                var points = currentPhase.secondPrizePoints;
                var key = "Fase " + dataHandler.GetExperimentIndex() + " do experimento";
                PlayerPrefsSaver.instance.AddExperimentPoints(key, points);
            }

            CleanScenario();
            NextPhase();
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