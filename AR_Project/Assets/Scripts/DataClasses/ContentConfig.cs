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

        [JsonProperty(PropertyName = "tituloTelaDeTreinoImaginaria")]
        public string titleTrainingImaginarium;

        [JsonProperty(PropertyName = "tituloExperimentoImaginario")]
        public string titleExperimentImaginarium;

        [JsonProperty(PropertyName = "tituloTelaDeTreinoNaoImaginaria")]
        public string titleTrainingNonImaginarium;

        [JsonProperty(PropertyName = "tituloExperimentoNaoImaginario")]
        public string titleExperimentNonImaginarium;

    }
}
