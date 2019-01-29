using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AR_Project.Savers
{
    public class PlayerPrefsSaver : MonoBehaviour
    {

        static public PlayerPrefsSaver instance = null;
        public string name, birthday, gender;
        public GameObject character;
        public int totalPoints;
        public List<string> keysPhases;


        void Awake()
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
            PlayerPrefs.SetInt("pontosTotais", totalPoints);
        }

        public void SavePlayerPrefs()
        {
            //Save all the User info to playerprefs
            PlayerPrefs.SetString("usuario", name);
            PlayerPrefs.SetString("aniversario", birthday);
            PlayerPrefs.SetString("genero", gender);
        }

        public void LoadPlayerPrefs()
        {
            name = PlayerPrefs.GetString("usuario");
            birthday = PlayerPrefs.GetString("aniversario");
            gender = PlayerPrefs.GetString("genero");
            totalPoints = PlayerPrefs.GetInt("pontosTotais");

        }



    }

}
