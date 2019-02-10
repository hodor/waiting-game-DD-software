using System;
using Newtonsoft.Json;


namespace AR_Project.DataClasses
{
    [JsonObject]
    public class ContentConfig
    {
        [JsonProperty(PropertyName = "tituloTelaDeCadastro")]
        public string titleRegistration;

        [JsonProperty(PropertyName = "tituloTelaDeIntroducao")]
        public string titleIntroduction;

        [JsonProperty(PropertyName = "tituloTelaDeApresentacaoDeRecompensa")]
        public string titleRewards;

        [JsonProperty(PropertyName = "tituloTelaDePersonagem")]
        public string titleCharacter;

        [JsonProperty(PropertyName = "tituloTelaDeApresentacaoDosTempos")]
        public string titleTimeInstructions;

        [JsonProperty(PropertyName = "tituloTelaDeTreino")]
        public string titleFakeExperiment;

        [JsonProperty(PropertyName = "tituloExperimentoReal")]
        public string titleRealExperiment;

    }
}
