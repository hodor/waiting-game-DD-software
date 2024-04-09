using Newtonsoft.Json;

namespace AR_Project.DataClasses.NestedObjects
{
    [JsonObject]
    public class LaneTime
    {
        [JsonProperty(PropertyName = "lane")] public int lane;
        [JsonProperty(PropertyName = "time")] public int time;
    }
}