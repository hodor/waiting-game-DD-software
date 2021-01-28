using Newtonsoft.Json;

namespace AR_Project.DataClasses.NestedObjects
{
    [JsonObject]
    public class Experiment
    {
        public int clusterId;

        [JsonProperty(PropertyName = "id")] public int id;

        [JsonProperty(PropertyName = "immediatePrizeValue")]
        public int immediatePrizeValue;

        [JsonProperty(PropertyName = "secondPrizeLane")]
        public int secondPrizeLane;

        [JsonProperty(PropertyName = "secondPrizeValue")]
        public int secondPrizeValue;
    }
}