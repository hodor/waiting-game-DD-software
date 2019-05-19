using System;
using System.Collections;
using System.Collections.Generic;
using AR_Project.DataClasses.MainData;
using AR_Project.MainGame.GameObjects;
using AR_Project.MainGame.Prize;
using AR_Project.MainGame.UI;
using UnityEngine;

namespace AR_Project.MainGame
{
    public class TeachTimers : MonoBehaviour
    {
        private MainGameScene _mainGameScene;
        private Respawns _respawnsScript;
        private SlidersHandler _slider;
        private int currentIndex;
        private GameObject finishLine;
        private List<GameObject> goList;
        private GameObject prefabReward;

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
            var timeLanes = MainData.instanceData.config.gameSettings;
            foreach (var setting in timeLanes)
            {
                var time = setting.time;
                if (ARDebug.Debugging) time = (int) Math.Ceiling(ARDebug.TimeToFill);
                Respawn(setting.time);
                yield return new WaitForSeconds(time);
            }

            CleanScene();
            _mainGameScene.ComeBackFromTutorial();
        }

        private void Respawn(int timer)
        {
            var respawn = _respawnsScript.GetRespawnByLane(timer);

            var firstOne = Instantiate(prefabReward);
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

        private void CleanScene()
        {
            _slider.ResetSliders();

            foreach (var gameObj in goList)
                Destroy(gameObj);
        }
    }
}