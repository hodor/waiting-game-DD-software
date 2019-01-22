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

        public Button StartBtn;
        public Sprite startBtnEnabled;
        public Sprite startBtnDisabled;

        public Button boyToggleBtn;
        public Button girlToggleBtn;

        public Sprite boyOn, boyOff;
        public Sprite girlOn, girlOff;

        public bool isGirl = true;
        bool clickedOnGender = false;


        private void Update()
        {
            if(clickedOnGender == true && birthDay.text != null && 
            birthMonth != null && birthYear != null) 
            {
                StartBtn.GetComponent<Image>().sprite = startBtnEnabled;
                StartBtn.enabled = true;
            }else
            {
                StartBtn.GetComponent<Image>().sprite = startBtnDisabled;
                StartBtn.enabled = false;
            }
        }
        public void OnClickedButtonGirl()
        {
            clickedOnGender = true;
            isGirl = true;
            boyToggleBtn.GetComponent<Image>().sprite = boyOff;
            girlToggleBtn.GetComponent<Image>().sprite = girlOn;
        }

        public void OnClickedButtonBoy()
        {
            clickedOnGender = true;
            isGirl = false;
            girlToggleBtn.GetComponent<Image>().sprite = girlOff;
            boyToggleBtn.GetComponent<Image>().sprite = boyOn;
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
            SceneManager.LoadScene("Instructions");
        }
    }

}
