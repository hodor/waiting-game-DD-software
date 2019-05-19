using Newtonsoft.Json;

namespace AR_Project.DataClasses
{
    [JsonObject]
    public class Texts
    {
        [JsonProperty(PropertyName = "personagem")]
        public string character;

        [JsonProperty(PropertyName = "introducao")]
        public string introduction;

        [JsonProperty(PropertyName = "apresentacaoDeRecompensa")]
        public string rewards;

        [JsonProperty(PropertyName = "apresentacaoDosTempos")]
        public string timeInstructions;

        [JsonProperty(PropertyName = "treinoImaginario")]
        public string trainingImaginarium;

        [JsonProperty(PropertyName = "experimentoImaginario")]
        public string experimentImaginarium;

        [JsonProperty(PropertyName = "treinoReal")]
        public string trainingReal;

        [JsonProperty(PropertyName = "experimentoReal")]
        public string experimentReal;

        [JsonProperty(PropertyName = "treinoPaciencia")]
        public string trainingPatience;

        [JsonProperty(PropertyName = "experimentoPaciencia")]
        public string experimentPatience;
    }
}