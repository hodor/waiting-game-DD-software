using System.Collections;
using System.Collections.Generic;
using AR_Project.DataClasses.MainData;
using AR_Project.DataClasses.NestedObjects;
using AR_Project.MainGame.GameObjects;
using AR_Project.MainGame.Prize;
using AR_Project.MainGame.UI;
using UnityEngine;


namespace AR_Project.MainGame
{
    public class TeachTimers : MonoBehaviour
    {
        GameObject finishLine;
        List<GameObject> goList;
        int currentIndex = 0;
        GameObject prefabReward;
        
        private MainGameScene _mainGameScene;
        private Respawns _respawnsScript;
        private SlidersHandler _slider;

        private void Start()
        {
            _slider = gameObject.GetComponent<SlidersHandler>();
            _respawnsScript = gameObject.GetComponent<Respawns>();
            _mainGameScene = gameObject.GetComponent<MainGameScene>();
        }


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
            _mainGameScene.ComeBackFromTutorial();

        }
        void Respawn(int timer)
        {
            var respawn = _respawnsScript.GetRespawnByLane(timer);

            GameObject firstOne = (GameObject)Instantiate(prefabReward);
            firstOne.transform.position = respawn.transform.position;
            var objScript = firstOne.GetComponent<PrizeGO>();
            objScript.timer = timer;
            objScript.finalDestination = new Vector3(finishLine.transform.position.x,
                                                    firstOne.transform.position.y, 0);
            objScript.StartMoving(true);
            _slider.SetAndStartSingleSlider(currentIndex, timer);
            currentIndex++;

            goList.Add(firstOne);

        }

        void CleanScene()
        {
            _slider.ResetSliders();

            foreach (var gameObj in goList)
                Destroy(gameObj);
        }
    }

}
