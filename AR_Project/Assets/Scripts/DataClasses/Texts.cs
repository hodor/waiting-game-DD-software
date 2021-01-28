using Newtonsoft.Json;

namespace AR_Project.DataClasses
{
    [JsonObject]
    public class Texts
    {
        
        [JsonProperty(PropertyName = "language")]
        public string language;
        
        [JsonProperty(PropertyName = "welcome")]
        public string welcome;
        
        [JsonProperty(PropertyName = "enterName")]
        public string enterName;
        
        [JsonProperty(PropertyName = "dateOfBirthHeader")]
        public string dateOfBirthHeader;
        
        [JsonProperty(PropertyName = "day")]
        public string day;
        
        [JsonProperty(PropertyName = "month")]
        public string month;
        
        [JsonProperty(PropertyName = "year")]
        public string year;
        
        [JsonProperty(PropertyName = "genderHeader")]
        public string genderHeader;

        [JsonProperty(PropertyName = "otherGender")]
        public string otherGender;

        [JsonProperty(PropertyName = "chooseCharacter")]
        public string character;

        [JsonProperty(PropertyName = "introduction")]
        public string introduction;

        [JsonProperty(PropertyName = "rewardInstructions")]
        public string rewards;

        [JsonProperty(PropertyName = "timeInstructions")]
        public string timeInstructions;

        [JsonProperty(PropertyName = "trainingImaginarium")]
        public string trainingImaginarium;

        [JsonProperty(PropertyName = "experimentImaginarium")]
        public string experimentImaginarium;

        [JsonProperty(PropertyName = "realTraining")]
        public string trainingReal;

        [JsonProperty(PropertyName = "realExperiment")]
        public string experimentReal;

        [JsonProperty(PropertyName = "trainingPatience")]
        public string trainingPatience;

        [JsonProperty(PropertyName = "experimentPatience")]
        public string experimentPatience;
        
        [JsonProperty(PropertyName = "finalScore")]
        public string finalPoints;
        
        [JsonProperty(PropertyName = "realFinalScore")]
        public string realPoints;
        
        [JsonProperty(PropertyName = "taskScoreBegin")]
        public string taskScoreBegin;
        
        [JsonProperty(PropertyName = "taskScoreEnd")]
        public string taskScoreEnd;
        
        [JsonProperty(PropertyName = "score")]
        public string score;
        
        [JsonProperty(PropertyName = "points")]
        public string points;
        
        [JsonProperty(PropertyName = "pointsAbbreviated")]
        public string pointsAbbreviated;
    }
}