using System;
using System.Globalization;
using System.IO;
using AR_Project.DataClasses.NestedObjects;
using AR_Project.Savers;
using AR_Project.DataClasses.MainData;
using UnityEngine;

namespace Output.CSV
{
    public class CSVOutput : CSVBase
    {
        private const string FileName = "Data";
        private string _imaginaryPath;
        private string _realPath;
        private string _patiencePath;
        private string[] _allPaths;

        private bool _sessionRunning;

        public override void StartSession()
        {
            if (_sessionRunning) return;
            _imaginaryPath = GetNewDataFile(FileName, "Imaginary");
            _realPath = GetNewDataFile(FileName, "Real");
            _patiencePath = GetNewDataFile(FileName, "Patience");
            _allPaths = new []{_imaginaryPath, _realPath, _patiencePath};
            _sessionRunning = true;
        }

        private string GetCurrentPath(PlayerPrefsSaver userData)
        {
            switch (userData.gameType)
            {
                case GameType.Imaginarium:
                    return _imaginaryPath;
                case GameType.Real:
                    return _realPath;
                case GameType.Patience:
                    return _patiencePath;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public override void EndSession()
        {
            CSVUtils.SetCurrentPath(null);
            _sessionRunning = false;
        }

        public override void SaveUserData(PlayerPrefsSaver userData)
        {
            var name = new[]
            {
                "Name", userData.name
            };
            var birthday = new[]
            {
                "Birth", userData.birthday
            };
            var gender = new[]
            {
                "Gender", userData.gender
            };
            var date = new[]
            {
                "Application_date", DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture)
            };
            var character = new[]
            {
                "Avatar", ""
            };

            foreach (var path in _allPaths)
            {
                CSVUtils.SetCurrentPath(path);
                CSVUtils.WriteLineAtEnd(date, false);
                CSVUtils.WriteLineAtEnd(name, false);
                CSVUtils.WriteLineAtEnd(birthday, false);
                CSVUtils.WriteLineAtEnd(gender, false);
                CSVUtils.WriteLineAtEnd(character);
            }
        }

        public override void SaveSelectedCharacter(PlayerPrefsSaver userData)
        {
            var character = new[]
            {
                "Avatar", userData.character.name
            };
            foreach (var path in _allPaths)
            {
                CSVUtils.SetCurrentPath(path);
                CSVUtils.ReplaceLineThatContains("Avatar", character);
            }
        }

        public override void StartExperiments(PlayerPrefsSaver userData)
        {
            var score = new[]
            {
                "Total_Score", ""
            };
            var headers = new[]
            {
                "Type", "Trial", "Cluster_ID", "Smallest_Reward", "Time_On_Biggest_Reward", "Chosen_Reward",
                "Choose_Time"
            };

            foreach (var path in _allPaths)
            {
                CSVUtils.SetCurrentPath(path);
                CSVUtils.WriteLineAtEnd(score, false);
                CSVUtils.WriteLineAtEnd(headers);
            }
        }

        private string GetTimeForLane(int selectedLane)
        {
            // We get the lane value out of the configuration files.
            // We first get all LaneTimes that are in the json file (which came from the spreadsheet) and we find the one with the selected lane
            // If the selected lane didn't exist there, we turn an error, or else we return the time associated with it.
            var laneTime = MainData.instanceData.config.laneTimes.Find(lt => lt.lane == selectedLane);
            return laneTime != null ? laneTime.time.ToString() : "Error";
        }

        public override void SaveExperimentData(Experiment experiment, float selectedValue, int biggestRewardLaneNumber, PlayerPrefsSaver userData,
            double timeToChooseInSeconds)
        {
            var clusterCode = (int) 'A';
            clusterCode += experiment.clusterId;
            var clusterLetter = (char) clusterCode;
            var cluster = clusterLetter.ToString();
            var laneTimeValue = GetTimeForLane(biggestRewardLaneNumber);

            var values = new[]
            {
                userData.isTraining ? "Training" : "Experiment",
                experiment.id.ToString(),
                cluster,
                experiment.GetImmediateRewardValue().ToString(),
                laneTimeValue,
                selectedValue.ToString(),
                timeToChooseInSeconds.ToString("0.00")
            };
            var path = GetCurrentPath(userData);
            CSVUtils.SetCurrentPath(path);
            CSVUtils.WriteLineAtEnd(values);
        }

        public override void SaveTotalPoints(PlayerPrefsSaver userData)
        {
            var score = new[]
            {
                "Total_Score", userData.phasePoints[userData.gameType].ToString()
            };
            var path = GetCurrentPath(userData);
            CSVUtils.SetCurrentPath(path);
            CSVUtils.ReplaceLineThatContains("Total_Score", score);
        }
    }
}