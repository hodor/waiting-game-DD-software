using Newtonsoft.Json;

namespace AR_Project.DataClasses.NestedObjects
{
    [JsonObject]
    public class Prize
    {
        [JsonProperty(PropertyName = "id")] public int id;
        [JsonProperty(PropertyName = "value")] public float value;
    }
}