using System;
using System.IO;
using AR_Project.Savers;
using UnityEngine;

namespace Output.Concrete
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
            _sessionRunning = true;
        }

        public void EndSession()
        {
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
            var writer = new StreamWriter(_currentPath, append:true);
            var headers = new string[6];
            headers[0] = "Nome";
            headers[1] = "Data de nascimento";
            headers[2] = "Genero";
            headers[3] = "Personagem";
            headers[4] = "Total de pontos";
            headers[5] = "Imaginária primeiro";
            var header = string.Join(",", headers);
            writer.WriteLine(header);

            var dataArr = new string[6];
            dataArr[0] = userData.name;
            dataArr[1] = userData.birthday;
            dataArr[2] = userData.gender;
            dataArr[3] = userData.character.name.ToString();
            dataArr[4] = userData.totalPoints.ToString();
            dataArr[5] = userData.imaginariumFirst.ToString();
            var data = string.Join(",", dataArr);
            writer.WriteLine(data);
            writer.Close();
        }

        public void SaveLevelData()
        {
            throw new System.NotImplementedException();
        }
    }
}