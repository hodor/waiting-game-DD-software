using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace AR_Project.Scenes.ChooseCharacter
{
    public class CharacterScene : MonoBehaviour
    {
        public List<GameObject> characters;    
        public List<Sprite> characterBigImage;
        public Image charBigImage;
        public Button leftButton, rightButton;

        int currentIndex = 0;

        // Use this for initialization
        void Start()
        {
            leftButton.enabled = false;
            UpdateScene(characterBigImage[currentIndex]);
        }

        void UpdateScene(Sprite bigChar)
        {
            charBigImage.sprite = bigChar;
        }

        public void ClickedOnRightButton()
        {
            if (currentIndex == characterBigImage.Count -1)
            {
                rightButton.enabled = false;
                leftButton.enabled = true;
            }
            else
            {
                currentIndex++;
                leftButton.enabled = true;
                rightButton.enabled = true;
            } 

            UpdateScene(characterBigImage[currentIndex]);
        }

        public void ClickedOnLeftButton()
        {
            if (currentIndex == 0)
            {
                leftButton.enabled = false;
                rightButton.enabled = true;
            }
            else
            {
                currentIndex--;
                leftButton.enabled = true;
                rightButton.enabled = true;
            }


            UpdateScene(characterBigImage[currentIndex]);
        }



        public void SelectedCharacter()
        {
            PlayerPrefsSaver.instance.character = characters[currentIndex];
            Debug.Log("Personagem escolhido: " + characters[currentIndex].name);
            GoToMainGameScene();
        }
        void GoToMainGameScene()
        {
            SceneManager.LoadScene("MainGame");
        }
    }

}
