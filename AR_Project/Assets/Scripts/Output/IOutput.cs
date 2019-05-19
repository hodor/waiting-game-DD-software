using AR_Project.DataClasses.NestedObjects;
using AR_Project.Savers;

namespace Output
{
    public interface IOutput
    {
        void StartSession();
        void EndSession();
        void SaveUserData(PlayerPrefsSaver userData);
        void SaveSelectedCharacter(PlayerPrefsSaver userData);
        void StartExperiments();

        void SaveExperimentData(Experiment experiment, int selectedValue, PlayerPrefsSaver userData,
            double timeToChooseInSeconds);

        void SaveTotalPoints(int points);
    }
}