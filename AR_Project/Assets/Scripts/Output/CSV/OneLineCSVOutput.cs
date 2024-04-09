using System;
using System.Globalization;
using System.IO;
using AR_Project.DataClasses.NestedObjects;
using AR_Project.Savers;
using Output.CSV.Calculation;

namespace Output.CSV
{
    
    public class OneLineCSVOutput : CSVBase
    {
        private const string FileName = "Data_OneLine";
        private string _dataPath;
        private bool _sessionRunning;
        private OutputData _data;
        
        public override void StartSession()
        {
            if (_sessionRunning) return;
            _sessionRunning = true;
            _data = new OutputData();
            _dataPath = GetSingleDataFile(FileName);

            if (!File.Exists(_dataPath))
            {
                CSVUtils.SetCurrentPath(_dataPath);
                CSVUtils.WriteLineAtEnd(OutputData.Headers);
            }
        }

        public override void EndSession()
        {
            _data.date_application = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
            _data.total_points = _data.experimentData[GameType.Imaginarium].points.totalPoints +
                                 _data.experimentData[GameType.Patience].points.totalPoints +
                                 _data.experimentData[GameType.Real].points.totalPoints;
            
            CSVUtils.SetCurrentPath(_dataPath);
            CSVUtils.WriteLineAtEnd(_data.ToList().ToArray());
            CSVUtils.SetCurrentPath(null);
            _sessionRunning = false;
        }

        public override void SaveUserData(PlayerPrefsSaver userData)
        {
            _data.name = userData.name;
            _data.gender = userData.gender;
            _data.birth = userData.birthday;
            _data.game_order = userData.gameTypeOrder;
        }

        public override void SaveSelectedCharacter(PlayerPrefsSaver userData)
        {
            _data.avatar = userData.character.name;
        }

        public override void StartExperiments(PlayerPrefsSaver userData)
        {

        }

        public override void SaveExperimentData(Experiment experiment, float selectedValue, int biggestRewardLaneNumber, PlayerPrefsSaver userData,
            double timeToChooseInSeconds)
        {
            if (userData.isTraining) return;
            
            var clusterCode = (int) 'A';
            clusterCode += experiment.clusterId;
            var clusterLetter = (char) clusterCode;
            var cluster = clusterLetter.ToString();
            
            _data.experimentData[userData.gameType].points.sequenceOrder.Add(experiment.id.ToString());
            _data.experimentData[userData.gameType].points.AddPoint(cluster, selectedValue);
            _data.experimentData[userData.gameType].chooseTime.Add(timeToChooseInSeconds);
        }

        public override void SaveTotalPoints(PlayerPrefsSaver userData)
        {
            _data.experimentData[userData.gameType].points.totalPoints = userData.phasePoints[userData.gameType];
        }
    }
}