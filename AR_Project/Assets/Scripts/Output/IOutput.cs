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
        void StartExperiments(PlayerPrefsSaver userData);

        void SaveExperimentData(Experiment experiment, float selectedValue, int biggestRewardLaneNumber, PlayerPrefsSaver userData,
            double timeToChooseInSeconds);

        void SaveTotalPoints(PlayerPrefsSaver userData);
    }
}