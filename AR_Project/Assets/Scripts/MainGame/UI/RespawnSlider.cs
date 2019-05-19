using UnityEngine;
using UnityEngine.UI;

namespace AR_Project.MainGame.UI
{
    public class RespawnSlider : MonoBehaviour
    {
        private Slider _slider;

        private float fillTime = 1.0f;
        private bool started;

        public void StartSlider(float timeToFill)
        {
            _slider = GetComponent<Slider>();
            fillTime = timeToFill;
            Set();
        }

        public void ResetSlider()
        {
            _slider = GetComponent<Slider>();
            _slider.value = 0;
            started = false;
        }

        private void Set()
        {
            _slider.minValue = Time.time;
            _slider.maxValue = Time.time + fillTime;
            started = true;
        }

        private void Update()
        {
            if (started)
                _slider.value = Time.time;
        }
    }
}