using Newtonsoft.Json;

namespace AR_Project.DataClasses.NestedObjects
{
    [JsonObject]
    public class Experiment
    {
        public int clusterId;

        [JsonProperty(PropertyName = "id")] public int id;

        [JsonProperty(PropertyName = "immediateRewardValue")]
        public int immediateRewardId;

        [JsonProperty(PropertyName = "delayedRewardLane")]
        public int delayedRewardLane;

        [JsonProperty(PropertyName = "delayedRewardValue")]
        public int delayedRewardId;

        public float GetImmediateRewardValue()
        {
            return MainData.MainData.instanceData.config.GetPrize(immediateRewardId);
        }
        
        public float GetDelayedRewardValue()
        {
            return MainData.MainData.instanceData.config.GetPrize(delayedRewardId);
        }

        public int GetDelayedLaneTimer()
        {
            return MainData.MainData.instanceData.config.GetTimerFromLane(delayedRewardLane);
        }
    }
}