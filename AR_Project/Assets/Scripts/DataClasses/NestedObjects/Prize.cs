using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AR_Project.DataClasses.Helper;
using Newtonsoft.Json;


namespace AR_Project.DataClasses.NestedObjects
{
    [JsonObject]
    public class Prize
    {
        [JsonProperty(PropertyName = "valor")]
        public int value;

        [JsonProperty(PropertyName = "pontuacaoDoTimer")]
        public int[] timerPoints;
    }

}
