using System.Collections;
using System.Collections.Generic;
using AR_Project.DataClasses.MainData;
using AR_Project.Savers;
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

            Debug.Log("Saving CSV File...");
            CSVSaver csv = new CSVSaver();
            csv.SaveCSV();
        }

        public void QuitGame()
        {
            Debug.Log("Quit Game...");
            Application.Quit();
        }

    }

}
