using AR_Project.DataClasses.MainData;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace AR_Project.Scenes.Instructions
{
    public class InstructionsScene : MonoBehaviour
    {
        public Text introductionText;


        private void Start()
        {
            introductionText.text = MainData.instanceData.config.texts.introduction;
        }

        public void GoToNextScene()
        {
            SceneManager.LoadScene("Rewards");
        }
    }
}