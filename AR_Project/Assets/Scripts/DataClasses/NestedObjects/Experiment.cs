using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

namespace AR_Project.DataClasses.NestedObjects
{
    [JsonObject]
    public class Experiment
    {
        [JsonProperty(PropertyName = "maiorPremioEstaNoBotaoDireito")]
        public bool isSecondPrizeAtRightButton;

        [JsonProperty(PropertyName = "valorPremioImediato")]
        public int immediatePrizeValue;

        [JsonProperty(PropertyName = "pontuacaoPremioImediato")]
        public int immediatePrizePoints;

        [JsonProperty(PropertyName = "valorSegundoPremio")]
        public int secondPrizeValue;

        [JsonProperty(PropertyName = "tempoSegundoPremio")]
        public int secondPrizeTimer;

        [JsonProperty(PropertyName = "pontuacaoSegundoPremio")]
        public int secondPrizePoints;

        [JsonProperty(PropertyName = "id")] public int id;

    }

}
