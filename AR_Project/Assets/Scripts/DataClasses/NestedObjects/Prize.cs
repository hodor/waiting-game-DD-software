using Newtonsoft.Json;

namespace AR_Project.DataClasses.NestedObjects
{
    [JsonObject]
    public class Prize
    {
        [JsonProperty(PropertyName = "id")] public int Id;
        [JsonProperty(PropertyName = "value")] public int value;
    }
}