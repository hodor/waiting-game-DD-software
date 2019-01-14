using System.Collections;
using System.Collections.Generic;
using AR_Project.DataClasses.MainData;
using AR_Project.DataClasses.NestedObjects;
using AR_Project.MainGame.GameObjects;
using AR_Project.MainGame.Prize;
using UnityEngine;


namespace AR_Project.MainGame
{
    public class TeachTimers : MonoBehaviour
    {
        GameObject prefabReward;
        GameObject finishLine;
        List<GameObject> goList;

        public void StartTutorial(GameObject prefab, GameObject finish)
        {
            goList = new List<GameObject>();
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
            CleanScene();
            var mainGameScene = gameObject.GetComponent<MainGameScene>();
            mainGameScene.SetUIFakeExperiment();

        }
        void Respawn(int timer)
        {
            var respawnsScript = gameObject.GetComponent<Respawns>();
            var respawn = respawnsScript.GetRespawnByLane(timer);

            GameObject firstOne = (GameObject)Instantiate(prefabReward);
            firstOne.transform.position = respawn.transform.position;
            var objScript = firstOne.GetComponent<PrizeGO>();
            objScript.timer = timer;
            objScript.finalDestination = new Vector3(finishLine.transform.position.x,
                                                    firstOne.transform.position.y, 0);
            objScript.StartMoving();
            goList.Add(firstOne);

        }

        void CleanScene()
        {
            foreach (var gameObj in goList)
                Destroy(gameObj);
        }
    }

}
