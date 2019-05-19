using System.Collections.Generic;
using UnityEngine;

namespace AR_Project.Savers
{
    public class PlayerPrefsSaver : MonoBehaviour
    {
        private const string LabelTotalPoints = "pontosTotais";
        private const string LabelName = "usuario";
        private const string LabelBirthday = "aniversario";
        private const string LabelGender = "genero";

        public static PlayerPrefsSaver instance;
        public GameObject character;

        public bool imaginariumFirst;
        //public bool hasDoneFakeFirst;
        //public bool isFirstExperiment;

        public bool isImaginarium;
        public bool isTraining;
        public List<string> keysPhases;
        public string name, birthday, gender;
        public int totalPoints;


        private void Awake()
        {
            if (instance == null)
                instance = this;
            else if (instance != this)
                Destroy(gameObject);

            DontDestroyOnLoad(this);
        }

        public void AddExperimentPoints(string key, int points)
        {
            totalPoints += points;
            keysPhases.Add(key);
            PlayerPrefs.SetInt(key, points);
            PlayerPrefs.SetInt(LabelTotalPoints, totalPoints);
        }

        public void SavePlayerPrefs()
        {
            //Save all the User info to playerprefs
            PlayerPrefs.SetString(LabelName, name);
            PlayerPrefs.SetString(LabelBirthday, birthday);
            PlayerPrefs.SetString(LabelGender, gender);
        }

        public void LoadPlayerPrefs()
        {
            name = PlayerPrefs.GetString(LabelName);
            birthday = PlayerPrefs.GetString(LabelBirthday);
            gender = PlayerPrefs.GetString(LabelGender);
            totalPoints = PlayerPrefs.GetInt(LabelTotalPoints);
        }
    }
}