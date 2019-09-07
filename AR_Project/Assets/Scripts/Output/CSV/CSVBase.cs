using System;
using System.IO;
using AR_Project.DataClasses.NestedObjects;
using AR_Project.Savers;
using UnityEngine;

namespace Output.CSV
{
    public abstract class CSVBase : IOutput
    {
        protected const string Extension = ".csv";
        protected static readonly string DataDir = Application.dataPath + @"\Data";
        protected const int MaxNumberOfZeros = 3;
        
        protected static string GetSingleDataFile(string filename)
        {
            return DataDir + @"\" + filename + Extension;
        }
        
        protected static string GetNewDataFile(string folderName, string fileName)
        {

            var foundNextFile = false;
            var name = "";
            var count = 0;
            while (!foundNextFile)
            {
                var numberSuffix = GetSuffix(count);
                var dir = DataDir + @"\" + folderName + "_" + numberSuffix;
                
                // Get the proper filename
                if (!Directory.Exists(dir))
                    Directory.CreateDirectory(dir);
                
                name = dir + @"\" + fileName + Extension;
                if (!File.Exists(name)) foundNextFile = true;
                count++;
            }

            return name;
        }

        protected static string GetSuffix(int number)
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
        
        

        public abstract void StartSession();
        public abstract void EndSession();
        public abstract void SaveUserData(PlayerPrefsSaver userData);
        public abstract void SaveSelectedCharacter(PlayerPrefsSaver userData);
        public abstract void StartExperiments(PlayerPrefsSaver userData);

        public abstract void SaveExperimentData(Experiment experiment, int selectedValue, PlayerPrefsSaver userData,
            double timeToChooseInSeconds);

        public abstract void SaveTotalPoints(PlayerPrefsSaver userData);
    }
}