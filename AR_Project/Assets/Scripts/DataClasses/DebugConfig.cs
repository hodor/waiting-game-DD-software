using Newtonsoft.Json;

[JsonObject]
public class DebugConfig
 {
     [JsonProperty(PropertyName = "alwaysImaginaryFirst")]
     public bool imaginaryFirst;
     
     [JsonProperty(PropertyName = "language")]
     public string language;
     
     [JsonProperty(PropertyName = "imaginaryGameEnabled")]
     public bool imaginaryGameEnabled;
     
     [JsonProperty(PropertyName = "realGameEnabled")]
     public bool realGameEnabled;
     
     [JsonProperty(PropertyName = "patienceGameEnabled")]
     public bool patienceGameEnabled;
 }