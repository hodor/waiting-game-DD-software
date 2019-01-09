using System.Collections;
using System.Collections.Generic;
using AR_Project.DataClasses.NestedObjects;
using Newtonsoft.Json;
using UnityEngine;

namespace AR_Project.DataClasses
{
    [JsonObject]
    public class FakeExperiments
    {
            [JsonProperty(PropertyName = "EnsaiosFicticios")]
            public List<Experiment> experiments;
    }

}
