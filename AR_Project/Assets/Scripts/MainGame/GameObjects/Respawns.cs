using AR_Project.DataClasses.MainData;
using AR_Project.DataClasses.NestedObjects;
using UnityEngine;

namespace AR_Project.MainGame.GameObjects
{
    public class Respawns : MonoBehaviour
    {
        public GameObject[] respawns;

        public GameObject CheckRespawnByExperiment(Experiment experiment)
        {
            var timerLanes = MainData.instanceData.config.gameSettings;

            for (var i = 0; i < timerLanes.Count; i++)
                if (experiment.secondPrizeTimer == timerLanes[i].time)
                    return respawns[i];

            return null;
        }

        public GameObject GetRespawnByPosition(int position)
        {
            return respawns[position];
        }

        public GameObject GetRespawnByLane(int lane)
        {
            var timerLanes = MainData.instanceData.config.gameSettings;
            for (var i = 0; i < timerLanes.Count; i++)
                if (lane == timerLanes[i].time)
                    return respawns[i];
            return null;
        }
    }
}