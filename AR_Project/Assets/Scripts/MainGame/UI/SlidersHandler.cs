using System.Collections;
using System.Collections.Generic;
using AR_Project.DataClasses.MainData;
using UnityEngine;

namespace AR_Project.MainGame.UI
{
    public class SlidersHandler : MonoBehaviour
    {

        public List<GameObject> sliders;

        public void SetAndStartSingleSlider(int index, float timeToFill)
        {
            var sliderScript = sliders[index].GetComponent<RespawnSlider>();
            if(timeToFill == 0.0f)
                sliderScript.StartSlider(0.5f);
            else
                sliderScript.StartSlider(timeToFill);
        }

        public void SetAndStartSliderByTimer(int timeToFill)
        {
            var timers = MainData.instanceData.config.gameSettings.timeLanes;
            for(int i=0; i < timers.Length; i++)
            {
                if(timers[i] == timeToFill)
                {
                    var sliderScript = sliders[i].GetComponent<RespawnSlider>();
                    if (timeToFill == 0.0f)
                        sliderScript.StartSlider(0.5f);
                    else
                        sliderScript.StartSlider(timeToFill);
                }
            }

        }

        public void ResetSliders()
        {
            foreach (var slider in sliders)
                slider.GetComponent<RespawnSlider>().ResetSlider();
        }
    }
}