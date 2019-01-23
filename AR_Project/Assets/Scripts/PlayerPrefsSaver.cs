using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefsSaver : MonoBehaviour {

    static public PlayerPrefsSaver instance = null;
    public string name, birthday, gender;
    public GameObject character;
    public int totalPoints;


	void Awake () {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(this);
    }
	
    public void AddExperimentPoints(string key, int points)
    {
        totalPoints += points;
        PlayerPrefs.SetInt(key, points);
    }

    public void SavePlayerPrefs(){
        //Save all the User info to playerprefs
        PlayerPrefs.SetString("usuario", name);
        PlayerPrefs.SetString("aniversario", birthday);
        PlayerPrefs.SetString("genero", gender);
    }



}
