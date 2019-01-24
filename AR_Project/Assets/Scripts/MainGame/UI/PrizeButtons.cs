using System.Collections;
using System.Collections.Generic;
using AR_Project.MainGame.ExperimentsLevels.ExperimentsHandlers;
using UnityEngine;
using UnityEngine.UI;

namespace AR_Project.MainGame.UI
{
    public class PrizeButtons: MonoBehaviour
    {
        public Button immediatePrizeButton;
        public Button secondPrizeButton;

        public int timerSecondPrize;

        public void StartSecondPrizeTimer()
        {
            StartCoroutine(TimerSecondPrizeButton());
        }
        IEnumerator TimerSecondPrizeButton()
        {
            secondPrizeButton.interactable = false;
            yield return new WaitForSeconds(timerSecondPrize);
            secondPrizeButton.interactable = true;
        }

        public void StopTimer()
        {
            StopCoroutine(TimerSecondPrizeButton());
        }

        public void ClickedOnImmediatePrize()
        {
            Debug.Log("CLICKED ON IMMEDIATE PRIZE");
            var expHandler = gameObject.GetComponent<ExperimentPhaseHandler>();
            expHandler.CallbackFromUIButtons(true);
        }

        public void ClickedOnSecondPrize()
        {
            Debug.Log("CLICKED ON SECOND PRIZE");
            var expHandler = gameObject.GetComponent<ExperimentPhaseHandler>();
            expHandler.CallbackFromUIButtons(false);
        }

        public void SetButtonsText(string textImmediate, string textSecond)
        {
            immediatePrizeButton.GetComponentInChildren<Text>().text = textImmediate;
            secondPrizeButton.GetComponentInChildren<Text>().text = textSecond;

        }
    }
}
