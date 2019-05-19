using System;
using Newtonsoft.Json;


namespace AR_Project.DataClasses
{
    [JsonObject]
    public class Texts
    {
        [JsonProperty(PropertyName = "Introducao")]
        public string introduction;

        [JsonProperty(PropertyName = "ApresentacaoDeRecompensa")]
        public string rewards;

        [JsonProperty(PropertyName = "Personagem")]
        public string character;

        [JsonProperty(PropertyName = "ApresentacaoDosTempos")]
        public string timeInstructions;

        [JsonProperty(PropertyName = "TreinoImaginario")]
        public string trainingImaginarium;

        [JsonProperty(PropertyName = "ExperimentoImaginario")]
        public string experimentImaginarium;

        [JsonProperty(PropertyName = "TreinoReal")]
        public string trainingReal;

        [JsonProperty(PropertyName = "ExperimentoReal")]
        public string experimentReal;

    }
}
