using Newtonsoft.Json;

namespace AR_Project.DataClasses.NestedObjects
{
    [JsonObject]
    public class Experiment
    {
        public int clusterId;

        [JsonProperty(PropertyName = "id")] public int id;

        [JsonProperty(PropertyName = "immediatePrizeValue")]
        public int immediatePrizeId;

        [JsonProperty(PropertyName = "secondPrizeLane")]
        public int secondPrizeLane;

        [JsonProperty(PropertyName = "secondPrizeValue")]
        public int secondPrizeId;

        public int GetImmediatePrizeValue()
        {
            return MainData.MainData.instanceData.config.GetPrize(immediatePrizeId);
        }
        
        public int GetSecondPrizeValue()
        {
            return MainData.MainData.instanceData.config.GetPrize(secondPrizeId);
        }

        public int GetSecondLaneTimer()
        {
            return MainData.MainData.instanceData.config.GetTimerFromLane(secondPrizeLane);
        }
    }
}