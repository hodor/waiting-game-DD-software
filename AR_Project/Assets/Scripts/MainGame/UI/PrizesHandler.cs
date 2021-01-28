using System.Collections.Generic;
using AR_Project.DataClasses.MainData;
using UnityEngine;
using UnityEngine.UI;

namespace AR_Project.MainGame.UI
{
    public class PrizesHandler : MonoBehaviour
    {
        public List<Text> prizesLabels;

        public void SetPrizesLabelsByLane(int firstLane, int secondLane, int firstPrizeId, int secondPrizeId)
        {
            var timers = MainData.instanceData.config.laneTimes;
            var firstPrize = MainData.instanceData.config.GetPrize(firstPrizeId);
            var secondPrize = MainData.instanceData.config.GetPrize(secondPrizeId);

            foreach (var prizeLabel in prizesLabels)
            {
                prizeLabel.text = "";
            }

            for (var i = 0; i < timers.Count; i++)
                if (timers[i].lane == firstLane)
                    prizesLabels[i].text = string.Format("{0} {1}", firstPrize, MainData.instanceData.config.GetTexts().pointsAbbreviated);
                else if (timers[i].lane == secondLane)
                    prizesLabels[i].text = string.Format("{0} {1}", secondPrize, MainData.instanceData.config.GetTexts().pointsAbbreviated);
                else
                    prizesLabels[i].text = "";
        }
    }
}