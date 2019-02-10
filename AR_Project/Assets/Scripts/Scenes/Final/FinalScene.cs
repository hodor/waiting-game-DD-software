using System.Collections;
using System.Collections.Generic;
using AR_Project.DataClasses.MainData;
using AR_Project.Savers;
using UnityEngine;
using UnityEngine.SceneManagement;
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

        public void ClickedOnRestartGame()
        {
            Debug.Log("Restart Game...");
            ResetGame();
            SceneManager.LoadScene("Registration");
        }

        void ResetGame()
        {
            PlayerPrefsSaver.instance.name = "";
            PlayerPrefsSaver.instance.birthday = "";
            PlayerPrefsSaver.instance.gender = "";
            PlayerPrefsSaver.instance.character = null;
            PlayerPrefsSaver.instance.totalPoints = 0;

        }

    }

}
