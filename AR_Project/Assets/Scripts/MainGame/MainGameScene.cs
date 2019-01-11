using System.Collections;
using System.Collections.Generic;
using AR_Project.DataClasses.MainData;
using AR_Project.DataClasses.NestedObjects;
using UnityEngine;

namespace AR_Project.MainGame
{
    public class MainGameScene : MonoBehaviour
    {
       
        public GameObject prefabReward;
        public GameObject finishLine;

        // Use this for initialization
        void Start()
        {
            InstantiateTest();
        }

        void InstantiateTest()
        {
            Experiment firstExperiment = MainData.instanceData.fakeExperiments.experiments[0];
            var respawnsScript = gameObject.GetComponent<Respawns>();
            var respawn = respawnsScript.CheckRespawn(firstExperiment);

            GameObject firstOne = (GameObject)Instantiate(prefabReward);
            firstOne.transform.position = respawn.transform.position;
            var objScript = firstOne.GetComponent<Prize>();
            objScript.timer = firstExperiment.secondPrizeTimer;
            objScript.finalDestination = new Vector3(finishLine.transform.position.x,
                                                    firstOne.transform.position.y, 0);
            objScript.StartMoving();

        }




    }

}
