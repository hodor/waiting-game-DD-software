using System;
using System.Collections.Generic;
using AR_Project.DataClasses.MainData;
using AR_Project.DataClasses.NestedObjects;
using UnityEngine;

namespace AR_Project.Savers
{
    public enum GameType
    {
        None,
        Imaginarium,
        Real,
        Patience
    }
    public class PlayerPrefsSaver : MonoBehaviour
    {
        private const string LabelTotalPoints = "pontosTotais";
        private const string LabelName = "usuario";
        private const string LabelBirthday = "aniversario";
        private const string LabelGender = "genero";

        public static PlayerPrefsSaver instance;
        public GameObject character;

        public List<GameType> gameTypeOrder;
        //public bool hasDoneFakeFirst;
        //public bool isFirstExperiment;

        public GameType gameType;
        public bool isTraining;
        public bool isTutorial;

        public string name, birthday, gender;
        public Dictionary<GameType, int> phasePoints;
        private void Awake()
        {
            if (instance == null)
                instance = this;
            else if (instance != this)
                Destroy(gameObject);

            phasePoints = new Dictionary<GameType, int>();
            DontDestroyOnLoad(this);
        }

        public void AddExperimentPoints(int points)
        {
            if(!phasePoints.ContainsKey(gameType))
                phasePoints.Add(gameType, 0);
            phasePoints[gameType] += points;
        }

        public void SavePlayerPrefs()
        {
            //Save all the User info to playerprefs
            PlayerPrefs.SetString(LabelName, name);
            PlayerPrefs.SetString(LabelBirthday, birthday);
            PlayerPrefs.SetString(LabelGender, gender);
        }

        public bool ShouldMoveSlowly()
        {
            return !ARDebug.Debugging && gameType != GameType.Imaginarium;
        }
    }
}