using System;
using System.IO;
using AR_Project.DataClasses;
using AR_Project.DataClasses.NestedObjects;
using Newtonsoft.Json;
using UnityEngine;

namespace AR_Project.DataClasses.JsonToClasses
{
    public static class JsonReader
    {
    
        public static Prizes ReadPrizeConfig(){
#if UNITY_EDITOR
            var path = "./Assets/Resources/ConfigJsons/configuracaoPremios.json";
#elif PLATFORM_STANDALONE_WIN
            var path = Application.dataPath + "./Data/ConfigJsons/configuracaoPremios.json";
#endif
            string outputJson = "";

            if (!File.Exists(path))
                return null;

            outputJson = File.ReadAllText(path);
            return JsonConvert.DeserializeObject<Prizes>(outputJson);
        }

        public static Config ReadGameConfig()
        {
#if UNITY_EDITOR
            var path = "./Assets/Resources/ConfigJsons/configuracoesGerais.json";
#elif PLATFORM_STANDALONE_WIN
            var path = Application.dataPath + "./Data/ConfigJsons/configuracoesGerais.json";
#endif

            string outputJson = "";

            if (!File.Exists(path))
                return null;

            outputJson = File.ReadAllText(path);
            return JsonConvert.DeserializeObject<Config>(outputJson);
        }

        public static RealExperiments ReadRealExperiments() 
        {
#if UNITY_EDITOR
            var path = "./Assets/Resources/ConfigJsons/configuracaoEnsaiosReais.json";
#elif PLATFORM_STANDALONE_WIN
            var path = Application.dataPath + "./Data/ConfigJsons/configuracaoEnsaiosReais.json";
#endif

            string outputJson = "";

            if (!File.Exists(path))
                return null;

            outputJson = File.ReadAllText(path);
            return JsonConvert.DeserializeObject<RealExperiments>(outputJson);
        }

        public static FakeExperiments ReadFakeExperiments()
        {
#if UNITY_EDITOR
            var path = "./Assets/Resources/ConfigJsons/configuracaoEnsaiosFicticios.json";
#elif PLATFORM_STANDALONE_WIN
            var path = Application.dataPath + "./Data/ConfigJsons/configuracaoEnsaiosFicticios.json";
#endif

            string outputJson = "";

            if (!File.Exists(path))
                return null;

            outputJson = File.ReadAllText(path);
            return JsonConvert.DeserializeObject<FakeExperiments>(outputJson);
        }

        public static ContentConfig ReadContentConfig()
        {
#if UNITY_EDITOR
            var path = "./Assets/Resources/ConfigJsons/configuracoesDeTexto.json";
#elif PLATFORM_STANDALONE_WIN
            var path = Application.dataPath + "./Data/ConfigJsons/configuracoesDeTexto.json";
#endif

            string outputJson = "";

            if (!File.Exists(path))
                return null;

            outputJson = File.ReadAllText(path);
            return JsonConvert.DeserializeObject<ContentConfig>(outputJson);
        }



    }
}
