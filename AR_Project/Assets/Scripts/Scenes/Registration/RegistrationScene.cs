using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Xml.Schema;
using UnityEngine;
using UnityEditor;
//using UnityEngine.Experimental.UIElements;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using AR_Project.Savers;
using Output;
using UnityEngine.Experimental.UIElements;
using UnityEngine.Serialization;
using Button = UnityEngine.UI.Button;
using Image = UnityEngine.UI.Image;

namespace AR_Project.Scenes.Registration
{
    public class RegistrationScene : MonoBehaviour
    {
        [FormerlySerializedAs("name")] public InputField inputName;
        [FormerlySerializedAs("bday")] public InputField inputBDay;
        [FormerlySerializedAs("bmonth")] public InputField inputBMonth;
        [FormerlySerializedAs("byear")] public InputField inputBYear;
        public Text username;

        public Text birthDay;
        public Text birthMonth;
        public Text birthYear;
        public int maximumAge = 150;
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
        private bool _clickedOnGender = false;
        
        private Image _startBtnImage;
        private Image _boyToggleImage;
        private Image _girlToggleImage;
        private Image _otherToggleImage;
        private bool ValidName = false, ValidDay = false, ValidMonth = false, ValidYear = false;

        private void Awake()
        {
            // Initialize output, we'll save as new data comes in
            Out.Instance.StartSession();
        }
        

        private void Start()
        {
            _startBtnImage = StartBtn.GetComponent<Image>();
            _boyToggleImage = boyToggleBtn.GetComponent<Image>();
            _girlToggleImage = girlToggleBtn.GetComponent<Image>();
            _otherToggleImage = otherToggleBtn.GetComponent<Image>();
            inputName.characterLimit = 30;
            inputBDay.characterLimit = 2;
            inputBMonth.characterLimit = 2;
            inputBYear.characterLimit = 4;
        }

        public void OnNameChanged()
        {
            var name = inputName.text;
            name = name.Trim();
            inputName.text = name;
            ValidName = !string.IsNullOrEmpty(name);
        }

        public void OnDayChanged()
        {
            int value = int.Parse(inputBDay.text);
            inputBDay.text = value.ToString("00");
            if (value > 31 || value <= 0)
            {
                inputBDay.textComponent.color = Color.red;
                ValidDay = false;
            }
            else
            {
                inputBDay.textComponent.color = Color.black;
                ValidDay = true;
            }
        }

        public void OnMonthChanged()
        {
            int value = int.Parse(inputBMonth.text);
            inputBMonth.text = value.ToString("00");
            if (value > 12 || value <= 0)
            {
                inputBMonth.textComponent.color = Color.red;
                ValidMonth = false;
            }
            else
            {
                inputBMonth.textComponent.color = Color.black;
                ValidMonth = true;
            }
        }

        public void OnYearChanged()
        {
            int value = int.Parse(inputBYear.text);
            inputBYear.text = value.ToString("0000");
            if (DateTime.Now.Year - value > maximumAge)
            {
                inputBYear.textComponent.color = Color.red;
                ValidYear = false;
            }
            else
            {
                inputBYear.textComponent.color = Color.black;
                ValidYear = true;
            }
        }
        
        private void Update()
        {
            if(_clickedOnGender == true && ValidName && ValidDay && ValidMonth && ValidYear) 
            {
                _startBtnImage.sprite = startBtnEnabled;
                StartBtn.enabled = true;
            } else
            {
                _startBtnImage.sprite = startBtnDisabled;
                StartBtn.enabled = false;
            }

            if (Input.GetKeyUp(KeyCode.Tab))
            {
                if (inputName.isFocused)
                {
                    inputBDay.Select();
                } else if (inputBDay.isFocused)
                {
                    inputBMonth.Select();
                } else if (inputBMonth.isFocused)
                {
                    inputBYear.Select();
                }
            }

            HandleFocusedInput(inputName);
            HandleFocusedInput(inputBMonth);
            HandleFocusedInput(inputBDay);
            HandleFocusedInput(inputBYear);
        }

        private void HandleFocusedInput(InputField field)
        {
            if (field.isFocused)
            {
                // Hide the placeholder
                field.placeholder.enabled = false;
            }
            else
            {
                if (string.IsNullOrEmpty(field.text))
                {
                    field.placeholder.enabled = true;
                }
            }
        }
        public void OnClickedButtonGirl()
        {
            _clickedOnGender = true;
            isGirl = true;
            isOther = false;
            _boyToggleImage.sprite = boyOff;
            _girlToggleImage.sprite = girlOn;
            _otherToggleImage.sprite = otherOff;
        }

        public void OnClickedButtonBoy()
        {
            _clickedOnGender = true;
            isGirl = false;
            isOther = false;
            _girlToggleImage.sprite = girlOff;
            _boyToggleImage.sprite = boyOn;
            _otherToggleImage.sprite = otherOff;
        }
        public void OnClickedButtonOther()
        {
            _clickedOnGender = true;
            isGirl = false;
            isOther = true;
            _otherToggleImage.sprite = otherOn;
            _girlToggleImage.sprite = girlOff;
            _boyToggleImage.sprite = boyOff;
        }

        private void SaveUserData()
        {
            var userName = username.text;
            string[] bday = { birthDay.text, birthMonth.text, birthYear.text };
            var bd = string.Join("/", bday);
            
            string gender;
            if (isGirl) gender = "femenino";
            else if (isOther) gender = "outro";
            else gender = "masculino";

            PlayerPrefsSaver.instance.name = userName;
            PlayerPrefsSaver.instance.birthday = bd;
            PlayerPrefsSaver.instance.gender = gender;
            PlayerPrefsSaver.instance.SavePlayerPrefs();
        }

        public void FinishedRegistration()
        {
            SaveUserData();
            GoToRewardScene();
        }

        void GoToRewardScene()
        {
            Out.Instance.SaveUserData(PlayerPrefsSaver.instance);
            SceneManager.LoadScene("Instructions");
        }
    }

}
