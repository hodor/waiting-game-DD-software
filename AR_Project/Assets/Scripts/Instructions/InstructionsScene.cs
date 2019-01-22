using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InstructionsScene : MonoBehaviour {

    public GameObject Wave;

	// Use this for initialization
	void Start () {

        //Call animation waves
        Wave.GetComponent<Animation>().Play("SoundwavesAnim");
	}
	
	public void GoToNextScene()
    {
        SceneManager.LoadScene("Rewards");
    }
}
