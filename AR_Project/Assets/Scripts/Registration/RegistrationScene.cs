using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
//using UnityEngine.Experimental.UIElements;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace AR_Project.Registration
{
    public class RegistrationScene : MonoBehaviour
    {

        public Text username;

        public Text birthDay;
        public Text birthMonth;
        public Text birthYear;

        public Toggle boyToggle;
        public Toggle girlToggle;

        public bool isGirl = true;


        public void ToggleValueChangedBoy(Toggle change)
        {
            if (change.isOn)
            {
                isGirl = false;
                boyToggle.isOn = true;
                girlToggle.isOn = false;
            }
            if (!change.isOn && !girlToggle.isOn)
                change.isOn = true;
        }
        public void ToggleValueChangedGirl(Toggle change)
        {
            if (change.isOn)
            {
                isGirl = true;
                girlToggle.isOn = true;
                boyToggle.isOn = false;
            }
            if (!change.isOn && !boyToggle.isOn)
                change.isOn = true;
        }

        void GetAllInformation()
        {
            var name = username.text;
            PlayerPrefs.SetString("usuario", name);

            string[] bday = { birthDay.text, birthMonth.text, birthYear.text };

            string bd = string.Join("/", bday);
            PlayerPrefs.SetString("aniversario", bd);

            string gender = "menina";
            if (!isGirl)
                gender = "menino";

            PlayerPrefs.SetString("genero", gender);
            Debug.Log("genero: " + gender);
        }

        public void FinishedRegistration()
        {
            GetAllInformation();
            GoToRewardScene();
        }

        void GoToRewardScene()
        {
            SceneManager.LoadScene("Rewards");
        }
    }

}
