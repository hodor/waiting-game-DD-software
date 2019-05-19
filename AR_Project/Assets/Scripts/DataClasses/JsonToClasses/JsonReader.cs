using System.IO;
using Newtonsoft.Json;
using UnityEngine;

namespace AR_Project.DataClasses.JsonToClasses
{
    public static class JsonReader
    {
        public static Config ReadConfig()
        {
            var path = Application.dataPath + "/StreamingAssets/config.json";
            var outputJson = "";

            if (!File.Exists(path))
                return null;

            outputJson = File.ReadAllText(path);
            return JsonConvert.DeserializeObject<Config>(outputJson);
        }
    }
}