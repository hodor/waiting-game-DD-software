using AR_Project.DataClasses.JsonToClasses;
using UnityEngine;

namespace AR_Project.DataClasses.MainData
{
    public class MainData : MonoBehaviour
    {
        public static MainData instanceData;

        public Config config;

        private void Awake()
        {
            if (instanceData == null)
                instanceData = this;
            else if (instanceData != this)
                Destroy(gameObject);

            DontDestroyOnLoad(this);
            config = JsonReader.ReadConfig();
        }
    }
}