using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;
using AR_Project.DataClasses.NestedObjects;

namespace AR_Project.DataClasses
{
    [JsonObject]
    public class RealExperiments
    {
        [JsonProperty(PropertyName = "EnsaiosReais")]
        public List<Experiment> experiments;
    }
}