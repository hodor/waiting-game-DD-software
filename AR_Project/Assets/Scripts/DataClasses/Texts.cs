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

        [JsonProperty(PropertyName = "imaginaryGameTraining")]
        public string imaginaryGameTraining;

        [JsonProperty(PropertyName = "imaginaryGame")]
        public string imaginaryGame;

        [JsonProperty(PropertyName = "realGameTraining")]
        public string realGameTraining;

        [JsonProperty(PropertyName = "realGame")]
        public string realGame;

        [JsonProperty(PropertyName = "patienceGameTraining")]
        public string patienceGameTraining;

        [JsonProperty(PropertyName = "patienceGame")]
        public string patienceGame;
        
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