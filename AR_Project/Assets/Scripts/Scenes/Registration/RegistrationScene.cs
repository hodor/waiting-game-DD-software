using System;
using System.Collections.Generic;
using AR_Project.DataClasses.MainData;
using AR_Project.Savers;
using Output;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace AR_Project.Scenes.Registration
{
    public class RegistrationScene : MonoBehaviour
    {
        private Image _boyToggleImage;
        private bool _clickedOnGender;
        private Image _girlToggleImage;
        private Image _otherToggleImage;

        private Image _startBtnImage;

        public Text birthDay;
        public Text birthMonth;
        public Text birthYear;

        public Sprite boyOn, boyOff;

        public Button boyToggleBtn;
        public Sprite girlOn, girlOff;
        public Button girlToggleBtn;
        [FormerlySerializedAs("bday")] public InputField inputBDay;
        [FormerlySerializedAs("bmonth")] public InputField inputBMonth;
        [FormerlySerializedAs("byear")] public InputField inputBYear;
        [FormerlySerializedAs("name")] public InputField inputName;

        public bool isGirl = true;
        public bool isOther;
        public int maximumAge = 150;
        public Sprite otherOn, otherOff;
        public Button otherToggleBtn;
        public Button StartBtn;
        public Sprite startBtnDisabled;
        public Sprite startBtnEnabled;
        public Text username;
        public Text versionText;
        private bool ValidName, ValidDay, ValidMonth, ValidYear;

        public Text Label_Welcome;
        public Text Label_EnterName;
        public Text Label_DOBHeader;
        public Text Label_Day;
        public Text Label_Month;
        public Text Label_Year;
        public Text Label_GenderHeader;
        public Text Label_GenderOther;

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

            Label_Welcome.text = MainData.instanceData.config.GetTexts().welcome;
            Label_EnterName.text = MainData.instanceData.config.GetTexts().enterName;
            Label_DOBHeader.text = MainData.instanceData.config.GetTexts().dateOfBirthHeader;
            Label_Day.text = MainData.instanceData.config.GetTexts().day;
            Label_Month.text = MainData.instanceData.config.GetTexts().month;
            Label_Year.text = MainData.instanceData.config.GetTexts().year;
            Label_GenderHeader.text = MainData.instanceData.config.GetTexts().genderHeader;
            Label_GenderOther.text = MainData.instanceData.config.GetTexts().otherGender;
            
            versionText.text = "Version: " + ARDebug.Version;
        }

        public void OnNameChanged()
        {
            var name = inputName.text;
            name = name.Trim();
            inputName.text = name;
            SetDebugMode(name == "Debug");
            ValidName = !string.IsNullOrEmpty(name);
        }

        private void SetDebugMode(bool on)
        {
            ARDebug.Debugging = on;
            
            // ReSharper disable once InvertIf
            if (on)
            {
                // Auto fill
                inputBDay.text = "01";
                ValidDay = true;
                inputBMonth.text = "01";
                ValidMonth = true;
                inputBYear.text = "1999";
                ValidYear = true;
                OnClickedButtonOther();
            }
        }

        public void OnDayChanged()
        {
            var value = int.Parse(inputBDay.text);
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
            var value = int.Parse(inputBMonth.text);
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
            var value = int.Parse(inputBYear.text);
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
            if (_clickedOnGender && ValidName && ValidDay && ValidMonth && ValidYear)
            {
                _startBtnImage.sprite = startBtnEnabled;
                StartBtn.enabled = true;
            }
            else
            {
                _startBtnImage.sprite = startBtnDisabled;
                StartBtn.enabled = false;
            }

            if (Input.GetKeyUp(KeyCode.Tab))
            {
                if (inputName.isFocused)
                    inputBDay.Select();
                else if (inputBDay.isFocused)
                    inputBMonth.Select();
                else if (inputBMonth.isFocused) inputBYear.Select();
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
                if (string.IsNullOrEmpty(field.text)) field.placeholder.enabled = true;
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
            string[] bday = {birthDay.text, birthMonth.text, birthYear.text};
            var bd = string.Join("/", bday);

            string gender;
            if (isGirl) gender = "female";
            else if (isOther) gender = "other";
            else gender = "male";

            PlayerPrefsSaver.instance.name = userName;
            PlayerPrefsSaver.instance.birthday = bd;
            PlayerPrefsSaver.instance.gender = gender;
            var imaginaryFirst = MainData.instanceData.config.debug.imaginaryFirst;
            var gameOrder = new List<GameType> {GameType.Imaginarium, GameType.Real, GameType.Patience};
            gameOrder.Shuffle();

            if (imaginaryFirst)
            {
                if (gameOrder[0] != GameType.Imaginarium)
                {
                    var index = gameOrder.IndexOf(GameType.Imaginarium);
                    gameOrder[index] = gameOrder[0];
                    gameOrder[0] = GameType.Imaginarium;
                }
            }
            
            PlayerPrefsSaver.instance.gameType = gameOrder[0];
            PlayerPrefsSaver.instance.gameTypeOrder = gameOrder;
            PlayerPrefsSaver.instance.SavePlayerPrefs();
        }

        public void FinishedRegistration()
        {
            SaveUserData();
            GoToRewardScene();
        }

        private void GoToRewardScene()
        {
            Out.Instance.SaveUserData(PlayerPrefsSaver.instance);
            SceneManager.LoadScene("Instructions");
        }
    }
}