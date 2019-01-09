using System;
using Newtonsoft.Json;
using AR_Project.DataClasses.NestedObjects;

namespace AR_Project.DataClasses
{
    [JsonObject]
    public class Config
    {
        [JsonProperty(PropertyName = "ConfiguracoesGerais")]
        public GameConfig gameSettings;
    }
}
