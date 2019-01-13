using System.Collections;
using System.Collections.Generic;
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

    }
}
