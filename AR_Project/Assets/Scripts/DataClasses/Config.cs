using System;
using System.Collections.Generic;
using System.Linq;
using AR_Project.DataClasses;
using AR_Project.DataClasses.NestedObjects;
using JetBrains.Annotations;
using Newtonsoft.Json;

[JsonObject]
public class Config
{
    [JsonProperty(PropertyName = "Experiments")]
    public List<Experiment> experiments;

    [JsonProperty(PropertyName = "Times")] public List<LaneTime> laneTimes;

    [JsonProperty(PropertyName = "Prizes")]
    public List<Prize> prizes;

    [JsonProperty(PropertyName = "Texts")] [CanBeNull]
    public List<Texts> allTexts;

    private Texts _selectedText = null;

    [JsonProperty(PropertyName = "Trainings")]
    public List<Experiment> trainings;
    
    [JsonProperty(PropertyName = "Debug")]
    public DebugConfig debug;

    public Texts GetTexts()
    {
        if (_selectedText != null) return _selectedText;
        var lang = debug.language;
        if (allTexts != null)
        {
            foreach (var text in allTexts)
            {
                if (text.language == lang)
                {
                    _selectedText = text;
                    return _selectedText;
                }
            }
            
            // Language not found - using the default
            _selectedText = allTexts[0];
        }

        return _selectedText;
    }

    public int GetPrize(int id)
    {
        foreach (var p in prizes)
        {
            if (p.Id == id)
            {
                return p.value;
            }
        }

        return -1;
    }
    
    public int GetMaxPrize()
    {
        int max = int.MinValue;
        foreach(var p in prizes)
        {
            if (p.value > max)
                max = p.value;
        }

        return max;
    }

    public int GetMinPrize()
    {
        int min = int.MaxValue;
        foreach (var p in prizes)
        {
            if (p.value < min)
                min = p.value;
        }

        return min;
    }

    public List<int> GetOrderedPrizeValues()
    {
        var orderedList = new List<int>();
        foreach (var p in prizes)
        {
            orderedList.Add(p.value);
        }

        orderedList.Sort();
        return orderedList;
    }
}