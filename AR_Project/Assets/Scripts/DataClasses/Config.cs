using System.Collections.Generic;
using AR_Project.DataClasses;
using AR_Project.DataClasses.NestedObjects;
using Newtonsoft.Json;

[JsonObject]
public class Config
{
    [JsonProperty(PropertyName = "Ensaios")]
    public List<Experiment> experiments;

    [JsonProperty(PropertyName = "Tempo")] public List<GameSetting> gameSettings;

    [JsonProperty(PropertyName = "Premios")]
    public List<Prize> prizes;

    [JsonProperty(PropertyName = "Textos")]
    public Texts texts;

    [JsonProperty(PropertyName = "Treinos")]
    public List<Experiment> trainings;
    
    [JsonProperty(PropertyName = "Debug")]
    public DebugConfig debug;
}