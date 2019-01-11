using System.Collections;
using System.Collections.Generic;
using AR_Project.DataClasses.MainData;
using AR_Project.DataClasses.NestedObjects;
using UnityEngine;

namespace AR_Project.MainGame
{
    public class Respawns : MonoBehaviour
    {
        public GameObject[] respawns;

        public GameObject CheckRespawn(Experiment experiment)
        {
            var timerLanes = MainData.instanceData.config.gameSettings.timeLanes;
            foreach (var tl in timerLanes)
                Debug.Log("tl: " + tl.ToString());

            for (int i = 0; i < timerLanes.Length; i++)
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