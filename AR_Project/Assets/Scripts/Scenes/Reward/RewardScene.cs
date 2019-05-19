using AR_Project.DataClasses.MainData;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace AR_Project.Scenes.Reward
{
    public class RewardScene : MonoBehaviour
    {
        public Text[] rewardsText;
        public Text title;

        void Start()
        {
            title.text = MainData.instanceData.config.texts.rewards;
            SetRewards();
        }

        void SetRewards()
        {
            var rewards = MainData.instanceData.config.prizes;
            for (int i = 0; i < rewards.Count; i++)
                rewardsText[i].text = rewards[i].value + " Pts";
        }

        public void GoToCharacterScene()
        {
            SceneManager.LoadScene("Character");
        }
    }
}