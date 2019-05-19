using Newtonsoft.Json;

namespace AR_Project.DataClasses.NestedObjects
{
    [JsonObject]
    public class GameSetting
    {
        [JsonProperty(PropertyName = "pista")] public int lane;
        [JsonProperty(PropertyName = "tempo")] public int time;
    }
}