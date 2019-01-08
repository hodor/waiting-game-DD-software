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

    public Toggle boy;
    public Toggle girl;

    // Use this for initialization
    void Start () {
		
	}
	
    void GetAllInformation()
    {
        var name = username.text;
        PlayerPrefs.SetString("username", name);

        string[] bday = { birthDay.text, birthMonth.text, birthYear.text }; 

        string bd = string.Join("/", bday);
        PlayerPrefs.SetString("birthday", bd);

    }

    public void FinishedRegistration()
    {
        GetAllInformation();
	}

    void StartGame() 
    {
    
    }
}
