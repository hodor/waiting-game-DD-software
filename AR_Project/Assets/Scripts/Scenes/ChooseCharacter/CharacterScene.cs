using System.Collections;
using System.Collections.Generic;
using AR_Project.DataClasses.MainData;
using AR_Project.Savers;
using Output;
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
        public Text title;

        int currentIndex = 0;

        // Use this for initialization
        void Start()
        {
            title.text = MainData.instanceData.content.titleCharacter;
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
            GoToMainGameScene();
        }
        void GoToMainGameScene()
        {
            Out.Instance.SaveSelectedCharacter(PlayerPrefsSaver.instance);
            SceneManager.LoadScene("MainGame");
        }
    }

}
