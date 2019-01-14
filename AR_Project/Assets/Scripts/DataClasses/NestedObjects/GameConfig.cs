﻿using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

namespace AR_Project.DataClasses.NestedObjects
{
    [JsonObject]
    public class GameConfig
    {
        [JsonProperty(PropertyName = "tempo")]
        public int[] timeLanes;
    }

}