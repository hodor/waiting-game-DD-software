using Newtonsoft.Json;

namespace AR_Project.DataClasses.NestedObjects
{
    [JsonObject]
    public class Experiment
    {
        public int clusterId;

        [JsonProperty(PropertyName = "id")] public int id;

        [JsonProperty(PropertyName = "valorPremioImediato")]
        public int immediatePrizeValue;

        [JsonProperty(PropertyName = "tempoSegundoPremio")]
        public int secondPrizeTimer;

        [JsonProperty(PropertyName = "valorSegundoPremio")]
        public int secondPrizeValue;
    }
}