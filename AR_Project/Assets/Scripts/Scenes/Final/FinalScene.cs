using System.Collections;
using System.Collections.Generic;
using AR_Project.DataClasses.MainData;
using UnityEngine;
using UnityEngine.UI;

namespace AR_Project.Scenes.Final
{
    public class FinalScene : MonoBehaviour
    {
        public Text finalPoints;

        void Start()
        {
            var points = PlayerPrefsSaver.instance.totalPoints;
            finalPoints.text = points + " pontos";
        }

        public void QuitGame()
        {
            //TODO: Save all data in excel file
            Application.Quit();
        }

    }

}
