using System;
using System.Globalization;
using System.IO;
using System.Text;
using AR_Project.Savers;
using Boo.Lang.Environments;
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

            var w = WriteLine(date);
            WriteLine(name, w);
            WriteLine(birthday, w);
            WriteLine(gender, w, close: true);
        }

        public void SaveSelectedCharacter(PlayerPrefsSaver userData)
        {
            var character = new[]
            {
                "Personagem", userData.character.name
            };
            WriteLine(character, close: true);
        }

        public void SaveLevelData()
        {
            throw new System.NotImplementedException();
        }

        private StreamWriter GetWriter()
        {
            return new StreamWriter(new FileStream(_currentPath, FileMode.Append, FileAccess.Write), Encoding.UTF8);
        }

        private string Cols(string[] arr)
        {
            return string.Join(",", arr);
        }

        private StreamWriter WriteLine(string[] arr, StreamWriter writer = null, bool close = false)
        {
            if (writer == null) writer = GetWriter();   
            var cols = Cols(arr);
            writer.WriteLine(cols);
            if(close)
                writer.Close();
            return writer;
        }
    }
}