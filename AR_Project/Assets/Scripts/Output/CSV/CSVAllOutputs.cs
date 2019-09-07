using AR_Project.DataClasses.NestedObjects;
using AR_Project.Savers;

namespace Output.CSV
{
    public class CSVAllOutputs : IOutput
    {
        private CSVOutput OldOutput = new CSVOutput();
        private OneLineCSVOutput NewOutput = new OneLineCSVOutput();
        public void StartSession()
        {
            OldOutput.StartSession();
            NewOutput.StartSession();
        }

        public void EndSession()
        {
            OldOutput.EndSession();
            NewOutput.EndSession();
        }

        public void SaveUserData(PlayerPrefsSaver userData)
        {
            OldOutput.SaveUserData(userData);
            NewOutput.SaveUserData(userData);
        }

        public void SaveSelectedCharacter(PlayerPrefsSaver userData)
        {
            OldOutput.SaveSelectedCharacter(userData);
            NewOutput.SaveSelectedCharacter(userData);
        }

        public void StartExperiments(PlayerPrefsSaver userData)
        {
            OldOutput.StartExperiments(userData);
            NewOutput.StartExperiments(userData);
        }

        public void SaveExperimentData(Experiment experiment, int selectedValue, PlayerPrefsSaver userData,
            double timeToChooseInSeconds)
        {
            OldOutput.SaveExperimentData(experiment, selectedValue, userData, timeToChooseInSeconds);
            NewOutput.SaveExperimentData(experiment, selectedValue, userData, timeToChooseInSeconds);
        }

        public void SaveTotalPoints(PlayerPrefsSaver userData)
        {
            OldOutput.SaveTotalPoints(userData);
            NewOutput.SaveTotalPoints(userData);
        }
    }
}