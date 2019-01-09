﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

namespace AR_Project.DataClasses
{
    [JsonObject]
    public class Experiment
    {
        [JsonProperty(PropertyName = "valorPremioImediato")]
        public int immediatePrizeValue;

        [JsonProperty(PropertyName = "pontuacaoPremioImediato")]
        public int immediatePrizePoints;

        [JsonProperty(PropertyName = "valorSegundoPremio")]
        public int secondPrizeValue;

        [JsonProperty(PropertyName = "tempoSegundoPremio")]
        public int secondPrizePoints;

        [JsonProperty(PropertyName = "pontuacaoSegundoPremio")]
        public int secondPrizeTimer;

    }

}
