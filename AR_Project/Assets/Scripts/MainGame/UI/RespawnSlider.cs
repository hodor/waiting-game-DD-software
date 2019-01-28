using UnityEngine;
using UnityEngine.UI;

namespace AR_Project.MainGame.UI
{
    public class RespawnSlider : MonoBehaviour
    {

        float fillTime = 1.0f;
        private Slider _slider;
        bool started;

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

        void Set()
        {
            _slider.minValue = Time.time;
            _slider.maxValue = Time.time + fillTime;
            started = true;
        }
        void Update()
        {
            if (started)
                _slider.value = Time.time;
        }
    }

}
