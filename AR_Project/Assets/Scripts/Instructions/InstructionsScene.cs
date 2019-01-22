using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InstructionsScene : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
        //Call animation waves	
	}
	
	public void GoToNextScene()
    {
        SceneManager.LoadScene("Rewards");
    }
}
