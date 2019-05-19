using System;
using AR_Project.DataClasses.JsonToClasses;
using UnityEngine;
using UnityEngine.Serialization;

namespace AR_Project.DataClasses.MainData
{
    public class MainData : MonoBehaviour
    {
        static public MainData instanceData = null;

        public Config config;

        void Awake()
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