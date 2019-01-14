using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using AR_Project.DataClasses.NestedObjects;
using AR_Project.MainGame.GameObjects;
using AR_Project.MainGame.Prize;

namespace AR_Project.MainGame.ExperimentsLevels.ExperimentsHandlers
{
    public class ExperimentHandler : MonoBehaviour
    {
        //Respawn prizes
        //Cuidar dos botões
        //Main Loop do game
        GameObject prefabReward;
        GameObject finishLine;
        List<Experiment> currentExperiments;
        ExperimentData dataHandler;

        //public ExperimentHandler(GameObject prefabSecondPrize, List<Experiment> experiments, GameObject finishLineObj)
        //{
        //    //TODO: Remove prefab second prize, we will get it dinamically later
        //    prefabReward = prefabSecondPrize;
        //    currentExperiments = experiments;
        //    finishLine = finishLineObj;
        //}
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
            Experiment currentExperiment = dataHandler.currentExperiment();
            Debug.Log("Current experiment second prize timer: " + currentExperiment.secondPrizeTimer);
            DoExperimentPhase(currentExperiment);

        }
        void DoExperimentPhase(Experiment currentPhase)
        {
            RespawnSecondPrize(currentPhase);
            RespawnImmediatePrize(currentPhase);
            //TODO: Setar os botões de acordo com a fase atual
        }

        //na logica dos botoes de premio, chamar essa função
        void NextPhase()
        {
            if (dataHandler.CheckIfThereIsMoreExperiments())
            {
                Experiment current = dataHandler.NextExperiment();
                DoExperimentPhase(current);
            }else
            {
                //TODO: finish the game here
            }

        }



        void RespawnSecondPrize(Experiment currentExperiment)
        {
            var respawnsScript = gameObject.GetComponent<Respawns>();
            var respawn = respawnsScript.GetRespawnByLane(currentExperiment.secondPrizeTimer);

            //TODO: pegar prefab de acordo com o experimento atual
            GameObject secondPrize = (GameObject)Instantiate(prefabReward);
            secondPrize.transform.position = respawn.transform.position;
            var objScript = secondPrize.GetComponent<PrizeGO>();
            objScript.timer = currentExperiment.secondPrizeTimer;
            objScript.finalDestination = new Vector3(finishLine.transform.position.x,
                                                    secondPrize.transform.position.y, 0);
            objScript.StartMoving();
        }

        void RespawnImmediatePrize(Experiment currentExperiment)
        {
            var respawnsScript = gameObject.GetComponent<Respawns>();
            var respawn = respawnsScript.GetRespawnByPosition(0);

            //TODO: pegar o prefab de acordo com o experimento atual
            GameObject immediatePrize = (GameObject)Instantiate(prefabReward);
            immediatePrize.transform.position = respawn.transform.position;
            var objScript = immediatePrize.GetComponent<PrizeGO>();
            objScript.timer = 0;
            objScript.finalDestination = new Vector3(finishLine.transform.position.x,
                                                    immediatePrize.transform.position.y, 0);
            objScript.StartMoving();
        }

    }
}