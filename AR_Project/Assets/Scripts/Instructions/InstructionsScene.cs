using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InstructionsScene : MonoBehaviour {

	public void GoToNextScene()
    {
        SceneManager.LoadScene("Rewards");
    }
}
