using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

namespace AR_Project.DataClasses
{
    [JsonObject]
    public class RealExperiments
    {
        [JsonProperty(PropertyName = "EnsaiosReais")]
        public List<Experiment> experiments;
    }

}
