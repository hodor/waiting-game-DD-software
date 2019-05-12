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
        void SaveTaskType(bool realTask);
        void SaveLevelData();
    }
}