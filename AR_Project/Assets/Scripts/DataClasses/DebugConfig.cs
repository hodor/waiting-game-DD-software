using Newtonsoft.Json;

[JsonObject]
public class DebugConfig
 {
     [JsonProperty(PropertyName = "alwaysImaginaryFirst")]
     public bool imaginaryFirst;
     
     [JsonProperty(PropertyName = "language")]
     public string language;
 }