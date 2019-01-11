using System.Collections;
using System.Collections.Generic;
using AR_Project.DataClasses.MainData;
using AR_Project.DataClasses.NestedObjects;
using UnityEngine;

namespace AR_Project.MainGame
{
    public class MainGameScene : MonoBehaviour
    {
        public GameObject[] respawns;
        public GameObject prefabReward;

        GameObject currentRespawn;



        // Use this for initialization
        void Start()
        {
            InstantiateTest();
        }

        // Update is called once per frame
        void Update()
        {

        }

        void InstantiateTest()
        {
            Experiment firstExperiment = MainData.instanceData.fakeExperiments.experiments[0];
            var respawn = CheckRespawn(firstExperiment);
            Debug.Log("Respawn: " + respawn.ToString());
            Debug.Log("Respawn position: " + respawn.transform.position.ToString());
            GameObject firstOne = (GameObject)Instantiate(prefabReward);
            firstOne.transform.position = respawn.transform.position;
            Debug.Log("Firstone position: " + firstOne.transform.position.ToString());
            Debug.Log("Firstone timer: " + firstExperiment.secondPrizeTimer.ToString());
            firstOne.GetComponent<Prize>().timer = firstExperiment.secondPrizeTimer;
            firstOne.GetComponent<Prize>().StartMoving();

            //debugar o respawn

        }

        GameObject CheckRespawn(Experiment experiment)
        {
            var timerLanes = MainData.instanceData.config.gameSettings.timeLanes;
            foreach(var tl in timerLanes)
                Debug.Log("tl: " + tl.ToString());

            for (int i=0; i < timerLanes.Length; i++)
            {
                if (experiment.secondPrizeTimer == timerLanes[i])
                {
                    Debug.Log("Returned respawn: " + respawns[i]);
                    Debug.Log("Returned respawn position: " + respawns[i].transform.position);
                    Debug.Log("i: " + i);

                    Debug.Log("time lane right: " + timerLanes[i]);
                    return respawns[i];
                }
            }

            return null;
        }


    }

}
