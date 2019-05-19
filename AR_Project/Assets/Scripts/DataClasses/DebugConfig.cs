using Newtonsoft.Json;

[JsonObject]
public class DebugConfig
 {
     [JsonProperty(PropertyName = "alwaysImaginaryFirst")]
     public bool imaginaryFirst;
 }