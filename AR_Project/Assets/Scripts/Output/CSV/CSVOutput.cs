using System;
using System.Globalization;
using System.IO;
using AR_Project.DataClasses.NestedObjects;
using AR_Project.Savers;
using UnityEngine;

namespace Output.CSV
{
    public class CSVOutput : CSVBase
    {
        private const string FileName = "Dados";
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
                "Nome", userData.name
            };
            var birthday = new[]
            {
                "Nascimento", userData.birthday
            };
            var gender = new[]
            {
                "Gênero", userData.gender
            };
            var date = new[]
            {
                "Data_de_aplicação", DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture)
            };
            var character = new[]
            {
                "Personagem", ""
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
                "Personagem", userData.character.name
            };
            foreach (var path in _allPaths)
            {
                CSVUtils.SetCurrentPath(path);
                CSVUtils.ReplaceLineThatContains("Personagem", character);
            }
        }

        public override void StartExperiments(PlayerPrefsSaver userData)
        {
            var score = new[]
            {
                "Pontuação_Total", ""
            };
            var headers = new[]
            {
                "Tipo", "Trail", "Cluster_ID", "Recompensa_menor", "Tempo_assoc_rec_maior", "Recompensa_escolhida",
                "Tempo_de_escolha"
            };

            foreach (var path in _allPaths)
            {
                CSVUtils.SetCurrentPath(path);
                CSVUtils.WriteLineAtEnd(score, false);
                CSVUtils.WriteLineAtEnd(headers);
            }
        }

        public override void SaveExperimentData(Experiment experiment, int selectedValue, PlayerPrefsSaver userData,
            double timeToChooseInSeconds)
        {
            var clusterCode = (int) 'A';
            clusterCode += experiment.clusterId;
            var clusterLetter = (char) clusterCode;
            var cluster = clusterLetter.ToString();

            var values = new[]
            {
                userData.isTraining ? "Treino" : "Experimento",
                experiment.id.ToString(),
                cluster,
                experiment.immediatePrizeValue.ToString(),
                experiment.secondPrizeTimer.ToString(),
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
                "Pontuação_Total", userData.phasePoints[userData.gameType].ToString()
            };
            var path = GetCurrentPath(userData);
            CSVUtils.SetCurrentPath(path);
            CSVUtils.ReplaceLineThatContains("Pontuação_Total", score);
        }
    }
}