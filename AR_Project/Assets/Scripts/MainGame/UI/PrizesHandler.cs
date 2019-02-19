using System.Collections;
using System.Collections.Generic;
using AR_Project.DataClasses.MainData;
using UnityEngine;
using UnityEngine.UI;

namespace AR_Project.MainGame.UI
{
    public class PrizesHandler : MonoBehaviour
    {
        public List<Text> prizesLabels;

        public void SetPrizesLabelsByTimer (int firstTimer, int secondTimer, int firstPrize, int secondPrize)
        {
            var timers = MainData.instanceData.config.gameSettings.timeLanes;

            foreach (var prizeLabel in prizesLabels)
                prizeLabel.text = "";
                
            for (int i=0; i< timers.Length; i++)
            {
                if(timers[i] == firstTimer)
                {
                    prizesLabels[i].text = firstPrize + " pts";

                }else if(timers[i] == secondTimer)
                {
                    prizesLabels[i].text = secondPrize + " pts";
                }else
                {
                    prizesLabels[i].text = "";
                }

            }

        }

    }
}
