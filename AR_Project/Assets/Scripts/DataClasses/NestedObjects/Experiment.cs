using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

namespace AR_Project.DataClasses.NestedObjects
{
    [JsonObject]
    public class Experiment
    {
        [JsonProperty(PropertyName = "valorPremioImediato")]
        public int immediatePrizeValue;

        [JsonProperty(PropertyName = "valorSegundoPremio")]
        public int secondPrizeValue;

        [JsonProperty(PropertyName = "tempoSegundoPremio")]
        public int secondPrizeTimer;

        [JsonProperty(PropertyName = "id")] public int id;

        public int clusterId;

    }

}
