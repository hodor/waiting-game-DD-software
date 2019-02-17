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

        public void SetPrizesLabelsByValues (int firstValue, int secondValue)
        {
            var prizes = MainData.instanceData.prizes.prizes;

            foreach (var prizeLabel in prizesLabels)
                prizeLabel.text = "";


            for (int i=0; i< prizes.Count; i++)
            {
                if(prizes[i].value == firstValue)
                {
                    prizesLabels[i].text = firstValue + " pts";
                }
                if(prizes[i].value == secondValue)
                {
                    prizesLabels[i].text = secondValue + " pts";
                }
                if (prizes[i].value != firstValue && prizes[i].value != secondValue)
                {
                    prizesLabels[i].text = "";
                }
            }

        }

    }
}
