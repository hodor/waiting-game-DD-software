﻿using System.Collections;
using System.Collections.Generic;
using AR_Project.DataClasses.MainData;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace AR_Project.Scenes.Reward
{
    public class RewardScene : MonoBehaviour
    {

        public Text[] rewardsText;

        void Start()
        {
            SetRewards();
        }

        void SetRewards() 
        {
            var rewards = MainData.instanceData.prizes.prizes;
            foreach (var r in rewards)
                Debug.Log("reward value: " + r.value);

            for (int i = 0; i < rewards.Count; i++)
            rewardsText[i].text = rewards[i].value + " moedas";

        }

        public void GoToCharacterScene()
        {
            SceneManager.LoadScene("Character");
        }

    }

}