using AR_Project.DataClasses.MainData;
using AR_Project.Savers;
using Output;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace AR_Project.Scenes.Final
{
    public class FinalScene : MonoBehaviour
    {
        public Text firstText;
        public Text secondText;
        public Text finalPoints;
        public Text realPoints;
        
        
        private void Start()
        {
            Out.Instance.EndSession();
            firstText.text = MainData.instanceData.config.GetTexts().finalPoints;
            secondText.text = MainData.instanceData.config.GetTexts().realPoints;
            var total = PlayerPrefsSaver.instance.phasePoints[GameType.Real] + 
                        PlayerPrefsSaver.instance.phasePoints[GameType.Patience] + 
                        PlayerPrefsSaver.instance.phasePoints[GameType.Imaginarium];
            var real = PlayerPrefsSaver.instance.phasePoints[GameType.Real];
            finalPoints.text = string.Format("{0} {1}", total, MainData.instanceData.config.GetTexts().points);
            realPoints.text = string.Format("{0} {1}", real, MainData.instanceData.config.GetTexts().points);
        }

        public void ClickedOnRestartGame()
        {
            ResetGame();
            SceneManager.LoadScene("Registration");
        }

        private void ResetGame()
        {
            PlayerPrefsSaver.instance.name = "";
            PlayerPrefsSaver.instance.birthday = "";
            PlayerPrefsSaver.instance.gender = "";
            PlayerPrefsSaver.instance.character = null;
            PlayerPrefsSaver.instance.phasePoints.Clear();
        }
    }
}