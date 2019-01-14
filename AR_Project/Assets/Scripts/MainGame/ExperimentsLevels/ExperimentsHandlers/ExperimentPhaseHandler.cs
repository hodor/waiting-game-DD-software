using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using AR_Project.DataClasses.NestedObjects;
using AR_Project.MainGame.GameObjects;
using AR_Project.MainGame.Prize;
using AR_Project.MainGame.UI;

namespace AR_Project.MainGame.ExperimentsLevels.ExperimentsHandlers
{
    public class ExperimentPhaseHandler : MonoBehaviour
    {
        //Respawn prizes
        //Cuidar dos botões
        //Main Loop do game
        GameObject prefabReward;
        GameObject finishLine;
        List<Experiment> currentExperiments;
        Experiment currentPhase;
        ExperimentData dataHandler;
        GameObject secondPrize;
        GameObject immediatePrize;

        public void SetupExperiment(GameObject prefabSecondPrize, List<Experiment> experiments, GameObject finishLineObj)
        {
            //TODO: Remove prefab second prize, we will get it dinamically later
            prefabReward = prefabSecondPrize;
            currentExperiments = experiments;
            finishLine = finishLineObj;
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

            prizeButtons.timerSecondPrize = currentPhase.secondPrizeTimer;
            prizeButtons.SetButtonsText(immediateButtonText, secondButtonText);

            RespawnSecondPrize();
            prizeButtons.StartSecondPrizeTimer();
            RespawnImmediatePrize();
            //TODO: Setar os botões de acordo com a fase atual (info de valor)
         

        }

        //na logica dos botoes de premio, chamar essa função
        void NextPhase()
        {
            if (dataHandler.CheckIfThereIsMoreExperiments())
            {
                currentPhase = dataHandler.NextExperiment();
                Debug.Log("Phase " + dataHandler.GetExperimentIndex());
                Debug.Log("Timer second " + currentPhase.secondPrizeTimer);
                Debug.Log("Coins immediate " + currentPhase.immediatePrizeValue);
                Debug.Log("Coins second " + currentPhase.secondPrizeValue);
                DoExperimentPhase();
            }else
            {
                //TODO: finish the game here
                Debug.Log("Finished the game");
            }

        }



        void RespawnSecondPrize()
        {
            var respawnsScript = gameObject.GetComponent<Respawns>();
            var respawn = respawnsScript.GetRespawnByLane(currentPhase.secondPrizeTimer);

            //TODO: pegar prefab de acordo com o experimento atual
            secondPrize = (GameObject)Instantiate(prefabReward);
            secondPrize.SetActive(true);
            secondPrize.transform.position = respawn.transform.position;
            var objScript = secondPrize.GetComponent<PrizeGO>();
            objScript.timer = currentPhase.secondPrizeTimer;
            objScript.finalDestination = new Vector3(finishLine.transform.position.x,
                                                    secondPrize.transform.position.y, 0);
            objScript.StartMoving();

         
        }

        void RespawnImmediatePrize()
        {
            var respawnsScript = gameObject.GetComponent<Respawns>();
            var respawn = respawnsScript.GetRespawnByPosition(0);

            //TODO: pegar o prefab de acordo com o experimento atual
            immediatePrize = (GameObject)Instantiate(prefabReward);
            immediatePrize.SetActive(true);
            immediatePrize.transform.position = respawn.transform.position;
            var objScript = immediatePrize.GetComponent<PrizeGO>();
            objScript.timer = 0;
            objScript.finalDestination = new Vector3(finishLine.transform.position.x,
                                                    immediatePrize.transform.position.y, 0);
            objScript.StartMoving();
        }

        public void CallbackFromUIButtons(bool immediatePrizeClicked)
        {
            //TODO: Add pontuação no playerprefs de acordo com a fase e o experimento
            if(immediatePrizeClicked)
            {
                var points = currentPhase.immediatePrizePoints;
                var key = "Fase " + dataHandler.GetExperimentIndex() + " do experimento";
                PlayerPrefs.SetInt(key, points);
            }else
            {
                var points = currentPhase.secondPrizePoints;
                var key = "Fase " + dataHandler.GetExperimentIndex() + " do experimento";
                PlayerPrefs.SetInt(key, points);
            }

            CleanScenario();
            NextPhase();
        }

        void CleanScenario()
        {
            var prizeButtons = gameObject.GetComponent<PrizeButtons>();
            prizeButtons.StopTimer();
            immediatePrize.SetActive(false);
            secondPrize.SetActive(false);

        }

    }
}