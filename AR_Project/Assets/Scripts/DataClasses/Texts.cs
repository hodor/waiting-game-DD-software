using Newtonsoft.Json;

namespace AR_Project.DataClasses
{
    [JsonObject]
    public class Texts
    {
        [JsonProperty(PropertyName = "Personagem")]
        public string character;

        [JsonProperty(PropertyName = "ExperimentoImaginario")]
        public string experimentImaginarium;

        [JsonProperty(PropertyName = "ExperimentoReal")]
        public string experimentReal;

        [JsonProperty(PropertyName = "Introducao")]
        public string introduction;

        [JsonProperty(PropertyName = "ApresentacaoDeRecompensa")]
        public string rewards;

        [JsonProperty(PropertyName = "ApresentacaoDosTempos")]
        public string timeInstructions;

        [JsonProperty(PropertyName = "TreinoImaginario")]
        public string trainingImaginarium;

        [JsonProperty(PropertyName = "TreinoReal")]
        public string trainingReal;
    }
}