using AR_Project.Savers;
using Output;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace AR_Project.Scenes.Final
{
    public class FinalScene : MonoBehaviour
    {
        public Text finalPoints;

        private void Start()
        {
            var points = PlayerPrefsSaver.instance.totalPoints;
            finalPoints.text = points + " pontos";
            Out.Instance.SaveTotalPoints(points);
            Out.Instance.EndSession();
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
            PlayerPrefsSaver.instance.totalPoints = 0;
        }
    }
}