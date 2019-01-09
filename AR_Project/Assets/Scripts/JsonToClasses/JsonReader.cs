using System;
using System.IO;
using AR_Project.DataClasses;
using Newtonsoft.Json;

namespace AR_Project.JsonToClasses
{
    public static class JsonReader
    {
    
        public static Prizes ReadPrizeConfig(){

            var path = "./Assets/Resources/ConfigJsons/configuracaoPremios.json";
            string outputJson = "";

            if (!File.Exists(path))
                return null;

            outputJson = File.ReadAllText(path);
            return JsonConvert.DeserializeObject<Prizes>(outputJson);
        }

        public static Config ReadGameConfig()
        {
            var path = "./Assets/Resources/ConfigJsons/configuracoesGerais.json";
            string outputJson = "";

            if (!File.Exists(path))
                return null;

            outputJson = File.ReadAllText(path);
            return JsonConvert.DeserializeObject<Config>(outputJson);
        }


    }
}
