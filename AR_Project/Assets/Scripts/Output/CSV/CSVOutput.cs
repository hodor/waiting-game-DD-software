using System;
using System.Globalization;
using System.IO;
using AR_Project.DataClasses.NestedObjects;
using AR_Project.Savers;
using UnityEngine;

namespace Output.CSV
{
    public class CSVOutput : IOutput
    {
        private const string FileName = "Dados";
        private const int MaxNumberOfZeros = 3;
        private const string Extension = ".csv";
        private static readonly string DataDir = Application.dataPath + @"\Data";
        private string _imaginaryPath;
        private string _realPath;
        private string _patiencePath;
        private string[] _allPaths;

        private bool _sessionRunning;

        public void StartSession()
        {
            if (_sessionRunning) return;
            _imaginaryPath = GetNewDataFile(FileName + "_Imaginary");
            _realPath = GetNewDataFile(FileName + "_Real");
            _patiencePath = GetNewDataFile(FileName + "_Patience");
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

        public void EndSession()
        {
            CSVUtils.SetCurrentPath(null);
            _sessionRunning = false;
        }

        public void SaveUserData(PlayerPrefsSaver userData)
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

        public void SaveSelectedCharacter(PlayerPrefsSaver userData)
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

        public void StartExperiments(PlayerPrefsSaver userData)
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

        public void SaveExperimentData(Experiment experiment, int selectedValue, PlayerPrefsSaver userData,
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

        public void SaveTotalPoints(PlayerPrefsSaver userData)
        {
            var score = new[]
            {
                "Pontuação_Total", userData.totalPoints.ToString()
            };
            var path = GetCurrentPath(userData);
            CSVUtils.SetCurrentPath(path);
            CSVUtils.ReplaceLineThatContains("Pontuação_Total", score);
        }

        private static string GetNewDataFile(string Filename)
        {
            // Get the proper filename
            if (!Directory.Exists(DataDir))
                Directory.CreateDirectory(DataDir);

            var foundNextFile = false;
            var name = "";
            var count = 0;
            while (!foundNextFile)
            {
                name = DataDir + @"\" + Filename + "_" + GetSuffix(count) + Extension;
                if (!File.Exists(name)) foundNextFile = true;
                count++;
            }

            return name;
        }

        private static string GetSuffix(int number)
        {
            if (number == 0) return "000";

            var log = Math.Log10(number);
            var curNumZeros = Math.Floor(log) + 1;
            var prefix = "";
            while (curNumZeros < MaxNumberOfZeros)
            {
                prefix += "0";
                curNumZeros++;
            }

            return prefix + number;
        }
    }
}