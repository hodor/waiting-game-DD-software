using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

namespace AR_Project.DataClasses.NestedObjects
{
    [JsonObject]
    public class GameSetting
    {
        [JsonProperty(PropertyName = "tempo")] public int time;
        [JsonProperty(PropertyName = "pista")] public int lane;
    }
}
