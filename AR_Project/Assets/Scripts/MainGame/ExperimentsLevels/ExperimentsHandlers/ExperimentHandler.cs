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

        public ExperimentHandler(GameObject prefabSecondPrize, List<Experiment> experiments, GameObject finishLineObj)
        {
            prefabReward = prefabSecondPrize;
            currentExperiments = experiments;
            finishLine = finishLineObj;
        }

        public void StartExperiment()
        {
            dataHandler = new ExperimentData(currentExperiments);
            Experiment currentExperiment = dataHandler.currentExperiment();
            DoExperimentPhase(currentExperiment);

        }
        void DoExperimentPhase(Experiment currentPhase)
        {
            RespawnSecondPrize(currentPhase.secondPrizeTimer);
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



        void RespawnSecondPrize(int secondPrizeTimer)
        {
            var respawnsScript = gameObject.GetComponent<Respawns>();
            var respawn = respawnsScript.GetRespawnByLane(secondPrizeTimer);

            //TODO: pegar prefab de acordo com o experimento atual
            GameObject secondPrize = (GameObject)Instantiate(prefabReward);
            secondPrize.transform.position = respawn.transform.position;
            var objScript = secondPrize.GetComponent<PrizeGO>();
            objScript.timer = secondPrizeTimer;
            objScript.finalDestination = new Vector3(finishLine.transform.position.x,
                                                    secondPrize.transform.position.y, 0);
            objScript.StartMoving();
        }

        void RespawnImmediatePrize()
        {
            var respawnsScript = gameObject.GetComponent<Respawns>();
            var respawn = respawnsScript.GetRespawnByPosition(0);

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