using AR_Project.Savers;

namespace Output
{
    public interface IOutput
    {
        void StartSession();
        void EndSession();
        void SaveUserData(PlayerPrefsSaver userData);
        void SaveLevelData();
    }
}