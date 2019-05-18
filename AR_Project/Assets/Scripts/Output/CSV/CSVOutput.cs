using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using AR_Project.DataClasses.NestedObjects;
using AR_Project.Savers;
using Boo.Lang.Environments;
using UnityEngine;

namespace Output.CSV
{
    public class CSVOutput : IOutput
    {
        private const string FileName = "Dados";
        private static readonly string DataDir = Application.dataPath + @"\Data";
        private const int MaxNumberOfZeros = 3;
        private const string Extension = ".csv";

        private bool _sessionRunning = false;
        private string _currentPath;

        public void StartSession()
        {
            if (_sessionRunning) return;
            _currentPath = GetNewDataFile();
            CSVUtils.SetCurrentPath(_currentPath);
            _sessionRunning = true;
        }

        public void EndSession()
        {
            CSVUtils.SetCurrentPath(null);
            _sessionRunning = false;
        }

        private static string GetNewDataFile()
        {
            // Get the proper filename
            if (!Directory.Exists(DataDir))
                Directory.CreateDirectory(DataDir);

            var foundNextFile = false;
            string name = "";
            int count = 0;
            while (!foundNextFile)
            {
                name = DataDir + @"\" + FileName + "_" + GetSuffix(count) + Extension;
                if (!File.Exists(name))
                {
                    foundNextFile = true;
                }
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

            return prefix + number.ToString();
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

            CSVUtils.WriteLineAtEnd(date, false);
            CSVUtils.WriteLineAtEnd(name, false);
            CSVUtils.WriteLineAtEnd(birthday, false);
            CSVUtils.WriteLineAtEnd(gender, false);
            CSVUtils.WriteLineAtEnd(character);
        }

        public void SaveSelectedCharacter(PlayerPrefsSaver userData)
        {
            var character = new[]
            {
                "Personagem", userData.character.name
            };
            CSVUtils.ReplaceLineThatContains("Personagem", character);
        }

        public void StartExperiments()
        {
            var score = new[]
            {
                "Pontuação_Total", ""
            };
            var headers = new[]
            {
                "Tarefa", "Trail", "Cluster_ID", "Recompensa_menor", "Tempo_assoc_rec_maior", "Recompensa_escolhida", "Tempo_de_escolha"
            };
            CSVUtils.WriteLineAtEnd(score, false);
            CSVUtils.WriteLineAtEnd(headers);
        }

        public void SaveExperimentData(Experiment experiment, int selectedValue, PlayerPrefsSaver userData, double timeToChooseInSeconds)
        {
            var clusterCode = (int) 'A';
            clusterCode += experiment.clusterId;
            var clusterLetter = (char) clusterCode;
            var cluster = clusterLetter.ToString();
            
            var values = new string[]
            {
                (userData.isTraining ? "Treino" : "Jogo_Tempo") + "_" + (userData.isImaginarium ? "Imaginário" : "Real"),
                experiment.id.ToString(), 
                cluster,
                experiment.immediatePrizeValue.ToString(),
                experiment.secondPrizeTimer.ToString(), 
                selectedValue.ToString(), 
                (timeToChooseInSeconds).ToString("0.00")
            };
            CSVUtils.WriteLineAtEnd(values);
        }

        public void SaveTotalPoints(int points)
        {
            var score = new[]
            {
                "Pontuação_Total", points.ToString()
            };
            CSVUtils.ReplaceLineThatContains("Pontuação_Total", score);
        }
    }
}