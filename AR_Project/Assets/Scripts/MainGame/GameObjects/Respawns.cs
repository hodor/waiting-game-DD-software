using System.Collections;
using System.Collections.Generic;
using AR_Project.DataClasses.MainData;
using AR_Project.DataClasses.NestedObjects;
using UnityEngine;
using AR_Project.MainGame.GameObjects;

namespace AR_Project.MainGame.GameObjects
{
    public class Respawns : MonoBehaviour
    {
        public GameObject[] respawns;

        public GameObject CheckRespawnByExperiment(Experiment experiment)
        {
            var timerLanes = MainData.instanceData.config.gameSettings.timeLanes;

            for (int i = 0; i < timerLanes.Length; i++)
            {
                if (experiment.secondPrizeTimer == timerLanes[i])
                {
                    return respawns[i];
                }
            }

            return null;
        }

        public GameObject GetRespawnByPosition(int position)
        {
            return respawns[position];
        }

        public GameObject GetRespawnByLane(int lane)
        {
            var timerLanes = MainData.instanceData.config.gameSettings.timeLanes;
            for (int i = 0; i < timerLanes.Length; i++)
            {
                if (lane == timerLanes[i])
                {
                    return respawns[i];
                }
            }
            return null;

        }
    }
}