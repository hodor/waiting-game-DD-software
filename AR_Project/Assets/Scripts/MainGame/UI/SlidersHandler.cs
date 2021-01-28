using System;
using System.Collections.Generic;
using AR_Project.DataClasses.MainData;
using UnityEngine;
using UnityEngine.UI;

namespace AR_Project.MainGame.UI
{
    public class SlidersHandler : MonoBehaviour
    {
        private const float TOLERANCE = 0.001f;
        public List<Slider> sliders;
        public List<Image> slidersColors;

        public void SetAndStartSingleSlider(int index, float timeToFill)
        {
            if (ARDebug.Debugging) timeToFill = ARDebug.TimeToFill;
            var sliderScript = sliders[index].GetComponent<RespawnSlider>();
            sliderScript.StartSlider(Math.Abs(timeToFill) < TOLERANCE ? 0.5f : timeToFill);
        }

        public void SetAndStartSliderByTimer(float timeToFill)
        {
            if (ARDebug.Debugging) timeToFill = ARDebug.TimeToFill;
            var timers = MainData.instanceData.config.laneTimes;
            for (var i = 0; i < timers.Count; i++)
                if (Math.Abs(timers[i].time - timeToFill) < TOLERANCE)
                {
                    var sliderScript = sliders[i].GetComponent<RespawnSlider>();
                    sliderScript.StartSlider(Math.Abs(timeToFill) < TOLERANCE ? 0.5f : timeToFill);
                }
        }

        public void DisableOtherSliders(int firstLaneToFill, int secondLaneToFill)
        {
            var timers = MainData.instanceData.config.laneTimes;
            for (var i = 0; i < timers.Count; i++)
                if (timers[i].lane != firstLaneToFill && timers[i].lane != secondLaneToFill)
                    DisableSlider(sliders[i]);
                else
                    EnableSlider(sliders[i]);
        }

        public void EnableAllSliders()
        {
            foreach (var slider in sliders)
                EnableSlider(slider);
        }

        public void ResetSliders()
        {
            foreach (var slider in sliders)
                slider.GetComponent<RespawnSlider>().ResetSlider();
        }

        private void DisableSlider(Slider slider)
        {
            var images = slider.GetComponentsInChildren<Image>();
            foreach (var image in images)
            {
                var tempColor = image.color;
                tempColor.a = 0.10f;
                image.color = tempColor;
            }
        }

        private void EnableSlider(Slider slider)
        {
            var images = slider.GetComponentsInChildren<Image>();
            foreach (var image in images)
            {
                var tempColor = image.color;
                tempColor.a = 1.0f;
                image.color = tempColor;
            }
        }
    }
}