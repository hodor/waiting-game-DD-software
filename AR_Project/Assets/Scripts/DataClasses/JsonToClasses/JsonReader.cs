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
        public static Prizes ReadPrizeConfig()
        {
            var path = Application.dataPath + "/StreamingAssets" + "/ConfigJsons/configuracaoPremios.json";
            string outputJson = "";

            if (!File.Exists(path))
                return null;

            outputJson = File.ReadAllText(path);
            return JsonConvert.DeserializeObject<Prizes>(outputJson);
        }

        public static Config ReadGameConfig()
        {
            var path = Application.dataPath + "/StreamingAssets" + "/ConfigJsons/configuracoesGerais.json";
            string outputJson = "";

            if (!File.Exists(path))
                return null;

            outputJson = File.ReadAllText(path);
            return JsonConvert.DeserializeObject<Config>(outputJson);
        }

        public static RealExperiments ReadRealExperiments()
        {
            var path = Application.dataPath + "/StreamingAssets" + "/ConfigJsons/configuracaoEnsaiosReais.json";
            string outputJson = "";

            if (!File.Exists(path))
                return null;

            outputJson = File.ReadAllText(path);
            return JsonConvert.DeserializeObject<RealExperiments>(outputJson);
        }

        public static FakeExperiments ReadFakeExperiments()
        {
            var path = Application.dataPath + "/StreamingAssets" + "/ConfigJsons/configuracaoEnsaiosFicticios.json";
            string outputJson = "";

            if (!File.Exists(path))
                return null;

            outputJson = File.ReadAllText(path);
            return JsonConvert.DeserializeObject<FakeExperiments>(outputJson);
        }

        public static ContentConfig ReadContentConfig()
        {
            var path = Application.dataPath + "/StreamingAssets" + "/ConfigJsons/configuracoesDeTexto.json";
            string outputJson = "";

            if (!File.Exists(path))
                return null;

            outputJson = File.ReadAllText(path);
            return JsonConvert.DeserializeObject<ContentConfig>(outputJson);
        }
    }
}