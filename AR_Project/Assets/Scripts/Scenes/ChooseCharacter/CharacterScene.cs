using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace AR_Project.Scenes.ChooseCharacter
{
    public class CharacterScene : MonoBehaviour
    {

        public List<GameObject> characterList;
        public GameObject respawnChar;
        public Image charBigImage;

        int currentIndex;

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void ClickedOnRightButton()
        {

        }

        public void ClickedOnLeftButton()
        {

        }



        public void SelectedCharacter()
        {

        }
        public void GoToMainGameScene()
        {
            SceneManager.LoadScene("MainGame");
        }
    }

}
