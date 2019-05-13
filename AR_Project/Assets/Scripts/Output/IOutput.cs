using AR_Project.DataClasses.NestedObjects;
using AR_Project.Savers;
using UnityEngine;

namespace Output
{
    public interface IOutput
    {
        void StartSession();
        void EndSession();
        void SaveUserData(PlayerPrefsSaver userData);
        void SaveSelectedCharacter(PlayerPrefsSaver userData);
        void SaveExperimentData(Experiment experiment, int selectedValue, long timeToChooseInMS);
    }
}