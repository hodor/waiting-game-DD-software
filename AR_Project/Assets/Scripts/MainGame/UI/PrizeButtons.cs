using System.Collections;
using System.Collections.Generic;
using AR_Project.DataClasses.MainData;
using AR_Project.MainGame.ExperimentsLevels.ExperimentsHandlers;
using UnityEngine;
using UnityEngine.UI;

namespace AR_Project.MainGame.UI
{
    public class PrizeButtons: MonoBehaviour
    {
        public static PrizeButtons instance;

        List<Button> answersButtons;

        //public Button immediatePrizeButton;
        //public Button secondPrizeButton;
        public Image firstButtonImage;
        public Image secondButtonImage;

        public Button leftButton, rightButton;



        public List<Sprite> prizeImages;

        //public int timerSecondPrize;

        public void Awake()
        {
            if (instance == null)
                instance = this;
            else if (instance != this)
                Destroy(gameObject);
        }

        public void SetLeftButton(string text, int value)
        {
            leftButton.GetComponentInChildren<Text>().text = text;
            firstButtonImage.sprite = GetPrizeImage(value);
        }

        public void SetRightButton(string text, int value)
        {
            rightButton.GetComponentInChildren<Text>().text = text;
            secondButtonImage.sprite = GetPrizeImage(value);
        }

        public void ReleaseSecondPrizeButton()
        {
            ToggleLeftButtonAvaiability(true);
            ToggleRightButtonAvaiability(true);
        }

        public void ToggleLeftButtonAvaiability(bool avaiable)
        {
            if(avaiable)
                leftButton.interactable = true;
            else
                leftButton.interactable = false;
        }

        public void ToggleRightButtonAvaiability(bool avaiable)
        {
            if (avaiable)
                rightButton.interactable = true;
            else
                rightButton.interactable = false;
        }

        public void ClickedOnLeftButton()
        {
            var expHandler = gameObject.GetComponent<ExperimentPhaseHandler>();
            var isSecondPrizeAtRight = expHandler.IsSecondPrizeAtRightButton();
            if (isSecondPrizeAtRight)
                ClickedOnImmediatePrize();
            else
                ClickedOnSecondPrize();
        }

        public void ClickedOnRightButton()
        {
            var expHandler = gameObject.GetComponent<ExperimentPhaseHandler>();
            var isSecondPrizeAtRight = expHandler.IsSecondPrizeAtRightButton();
            if (isSecondPrizeAtRight)
                ClickedOnSecondPrize();
            else
                ClickedOnImmediatePrize();
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

        //public void SetButtons(string textImmediate, string textSecond, int firstPrizeValue, int secondPrizeValue)
        //{
        //    immediatePrizeButton.GetComponentInChildren<Text>().text = textImmediate;
        //    secondPrizeButton.GetComponentInChildren<Text>().text = textSecond;
        //    firstButtonImage.sprite = GetPrizeImage(firstPrizeValue);
        //    secondButtonImage.sprite = GetPrizeImage(secondPrizeValue);
        //}

        public Sprite GetPrizeImage(int prizeValue)
        {
            var prizes = MainData.instanceData.prizes.prizes;

            for (int i = 0; i < prizes.Count; i++)
            {
                if (prizes[i].value == prizeValue)
                {
                    return prizeImages[i];
                }
            }
            return null;
        }
    }
}
