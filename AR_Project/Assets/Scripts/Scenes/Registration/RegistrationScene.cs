using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
//using UnityEngine.Experimental.UIElements;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using AR_Project.Savers;

namespace AR_Project.Scenes.Registration
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
        public Button otherToggleBtn;

        public Sprite boyOn, boyOff;
        public Sprite girlOn, girlOff;
        public Sprite otherOn, otherOff;

        public bool isGirl = true;
        public bool isOther = false;
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
            isOther = false;
            boyToggleBtn.GetComponent<Image>().sprite = boyOff;
            girlToggleBtn.GetComponent<Image>().sprite = girlOn;
            otherToggleBtn.GetComponent<Image>().sprite = otherOff;
        }

        public void OnClickedButtonBoy()
        {
            clickedOnGender = true;
            isGirl = false;
            isOther = false;
            girlToggleBtn.GetComponent<Image>().sprite = girlOff;
            boyToggleBtn.GetComponent<Image>().sprite = boyOn;
            otherToggleBtn.GetComponent<Image>().sprite = otherOff;
        }
        public void OnClickedButtonOther()
        {
            clickedOnGender = true;
            isGirl = false;
            isOther = true;
            otherToggleBtn.GetComponent<Image>().sprite = otherOn;
            girlToggleBtn.GetComponent<Image>().sprite = girlOff;
            boyToggleBtn.GetComponent<Image>().sprite = boyOff;
        }

        void GetAllInformation()
        {
            var name = username.text;
            string[] bday = { birthDay.text, birthMonth.text, birthYear.text };
            string bd = string.Join("/", bday);
            string gender = "menina";
            if (!isGirl && !isOther)
                gender = "menino";
            else
                gender = "outro";

            PlayerPrefsSaver.instance.name = name;
            PlayerPrefsSaver.instance.birthday = bd;
            PlayerPrefsSaver.instance.gender = gender;
            PlayerPrefsSaver.instance.SavePlayerPrefs();

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
