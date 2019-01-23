using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace AR_Project.Scenes.Instructions
{
    public class InstructionsScene : MonoBehaviour
    {

        public void GoToNextScene()
        {
            SceneManager.LoadScene("Rewards");
        }
    }

}
