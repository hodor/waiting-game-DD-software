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
    [JsonProperty(PropertyName = "Tasks")]
    public List<Experiment> experiments;

    [JsonProperty(PropertyName = "Delays")] public List<LaneTime> laneTimes;

    [JsonProperty(PropertyName = "Rewards")]
    public List<Prize> prizes;

    [JsonProperty(PropertyName = "Texts")] [CanBeNull]
    public List<Texts> allTexts;

    private Texts _selectedText = null;

    [JsonProperty(PropertyName = "Practices")]
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

    public float GetPrize(int id)
    {
        foreach (var p in prizes)
        {
            if (p.id == id)
            {
                return p.value;
            }
        }

        return -1;
    }

    public int GetTimerFromLane(int lane)
    {
        foreach (var laneTime in laneTimes)
        {
            if (laneTime.lane == lane)
            {
                return laneTime.time;
            }
        }

        return -1;
    }
    
    public float GetMaxPrize()
    {
        float max = float.MinValue;
        foreach(var p in prizes)
        {
            if (p.value > max)
                max = p.value;
        }

        return max;
    }

    public float GetMinPrize()
    {
        float min = float.MaxValue;
        foreach (var p in prizes)
        {
            if (p.value < min)
                min = p.value;
        }

        return min;
    }

    public List<float> GetOrderedPrizeValues()
    {
        var orderedList = new List<float>();
        foreach (var p in prizes)
        {
            orderedList.Add(p.value);
        }

        orderedList.Sort();
        return orderedList;
    }
    
    
}