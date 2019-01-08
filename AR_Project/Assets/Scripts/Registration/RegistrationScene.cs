using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
//using UnityEngine.Experimental.UIElements;
using UnityEngine.UI;


public class RegistrationScene : MonoBehaviour {

    public Text username;

    public Text birthDay;
    public Text birthMonth;
    public Text birthYear;

    public Toggle boyToggle;
    public Toggle girlToggle;

    public bool isGirl = false;


    public void ToggleValueChangedBoy(Toggle change)
    {
        if (change.isOn)
        {
            isGirl = false;
            girlToggle.isOn = false;
        }
    }
    public void ToggleValueChangedGirl(Toggle change)
    {
        if (change.isOn)
        {
            isGirl = true;
            boyToggle.isOn = false;
        }

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
	}

    void StartGame() 
    {
    
    }
}
