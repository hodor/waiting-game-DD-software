using System.Collections;
using System.Collections.Generic;
using AR_Project.DataClasses.MainData;
using AR_Project.DataClasses.NestedObjects;
using UnityEngine;


namespace AR_Project.MainGame
{
    public class TeachTimers : MonoBehaviour
    {
        GameObject prefabReward;
        GameObject finishLine;

        public void StartTutorial(GameObject prefab, GameObject finish)
        {
            prefabReward = prefab;
            finishLine = finish;
            StartCoroutine(RespawnTutorial());
        }

        public IEnumerator RespawnTutorial()
        {
            var timeLanes = MainData.instanceData.config.gameSettings.timeLanes;
            foreach(var lane in timeLanes)
            {
                Respawn(lane);
                yield return new WaitForSeconds(lane);
            }

        }
        void Respawn(int timer)
        {
            var respawnsScript = gameObject.GetComponent<Respawns>();
            var respawn = respawnsScript.GetRespawnByLane(timer);

            GameObject firstOne = (GameObject)Instantiate(prefabReward);
            firstOne.transform.position = respawn.transform.position;
            var objScript = firstOne.GetComponent<Prize>();
            objScript.timer = timer;
            objScript.finalDestination = new Vector3(finishLine.transform.position.x,
                                                    firstOne.transform.position.y, 0);
            objScript.StartMoving();

        }
    }

}
