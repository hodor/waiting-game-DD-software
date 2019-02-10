using System.Collections;
using System.Collections.Generic;
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
            introductionText.text = MainData.instanceData.content.titleIntroduction;
        }

        public void GoToNextScene()
        {
            SceneManager.LoadScene("Rewards");
        }
    }

}
