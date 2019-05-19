using Newtonsoft.Json;

namespace AR_Project.DataClasses.NestedObjects
{
    [JsonObject]
    public class Prize
    {
        [JsonProperty(PropertyName = "valor")] public int value;
    }
}