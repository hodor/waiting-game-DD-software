using System;
using System.Collections.Generic;
using System.Globalization;
using AR_Project.Savers;

namespace Output.CSV.Calculation
{
    public class OutputData : ICsvData
    {
        public string name;
        public string date_application;
        public string birth;
        public string gender;
        public string avatar;
        public List<GameType> game_order;

        public float age_year()
        {
            var birthdate = DateTime.ParseExact(birth, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            float years = DateTime.Now.Year - birthdate.Year;
            float months = DateTime.Now.Month - birthdate.Month;
            return years + (months/12);
        }

        public int age_month()
        {
            var birthdate = DateTime.ParseExact(birth, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            return ((DateTime.Now.Year - birthdate.Year) * 12) + DateTime.Now.Month - birthdate.Month;
        }

        public Dictionary<GameType, ExperimentData> experimentData = new Dictionary<GameType, ExperimentData>
        {
            {GameType.Imaginarium, new ExperimentData()},
            {GameType.Patience, new ExperimentData()},
            {GameType.Real, new ExperimentData()}
        };
        
        public int total_points;

        public List<string> ToList()
        {
            var sequenceTasks = "";
            foreach (var gt in game_order)
            {
                var str = "";
                switch (gt)
                {
                    case GameType.Imaginarium:
                        str = "I";
                        break;
                    case GameType.Patience:
                        str = "P";
                        break;
                    case GameType.Real:
                        str = "R";
                        break;
                    case GameType.None:
                        break;
                }

                sequenceTasks += str;
            }

            var list = new List<string>
            {
                name,
                date_application,
                birth,
                age_year().ToString(CultureInfo.InvariantCulture),
                age_month().ToString(),
                gender,
                avatar,
                sequenceTasks
            };
            list.AddRange(experimentData[GameType.Imaginarium].points.ToList());
            list.AddRange(experimentData[GameType.Patience].points.ToList());
            list.AddRange(experimentData[GameType.Real].points.ToList());
            list.Add(total_points.ToString());
            experimentData[GameType.Imaginarium].subjectiveValueData.Calculate(experimentData[GameType.Imaginarium].points.GetSequencePoints());
            experimentData[GameType.Patience].subjectiveValueData.Calculate(experimentData[GameType.Patience].points.GetSequencePoints());
            experimentData[GameType.Real].subjectiveValueData.Calculate(experimentData[GameType.Real].points.GetSequencePoints());
            list.AddRange(experimentData[GameType.Imaginarium].subjectiveValueData.ToList());
            list.AddRange(experimentData[GameType.Patience].subjectiveValueData.ToList());
            list.AddRange(experimentData[GameType.Real].subjectiveValueData.ToList());
            list.Add(Math.GetAreaUnderCurve(experimentData[GameType.Imaginarium].points.GetSequencePoints()).
                ToString(CultureInfo.InvariantCulture));
            list.Add(Math.GetAreaUnderCurve(experimentData[GameType.Patience].points.GetSequencePoints()).
                ToString(CultureInfo.InvariantCulture));
            list.Add(Math.GetAreaUnderCurve(experimentData[GameType.Real].points.GetSequencePoints()).
                ToString(CultureInfo.InvariantCulture));
            list.AddRange(experimentData[GameType.Imaginarium].chooseTime.ConvertAll(x => x.ToString(CultureInfo.InvariantCulture)));
            list.AddRange(experimentData[GameType.Patience].chooseTime.ConvertAll(x => x.ToString(CultureInfo.InvariantCulture)));
            list.AddRange(experimentData[GameType.Real].chooseTime.ConvertAll(x => x.ToString(CultureInfo.InvariantCulture)));
            return list;
        }
        
        public static readonly string[] Headers = new[]
        {
            "Name", "Date_application", "Birth", "Age_year", "Age_month", "Gender", "Avatar", "Sequence_tasks", 
            "Imaginary_points", "Imaginary_trail_sequence", 
            "IMA01", "IMA02", "IMA03", "IMB04", "IMB05", "IMB06", "IMC07", "IMC08", "IMC09", "IMD10", "IMD11", "IMD12", 
            "IME13", "IME14", "IME15", "IMF16", "IMF17", "IMF18", "IMG19", "IMG20", "IMG21", "IMH22", "IMH23", "IMH24", 
            "IMI25", "IMI26", "IMI27", "IMJ28", "IMJ29", "IMJ30", "IMK31", "IMK32", "IMK33", "IML34", "IML35", "IML36", 
            "Patience_points", "Patience_trail_sequence", 
            "PTA01", "PTA02", "PTA03", "PTB04", "PTB05", "PTB06", "PTC07", "PTC08", "PTC09", "PTD10", "PTD11", "PTD12", 
            "PTE13", "PTE14", "PTE15", "PTF16", "PTF17", "PTF18", "PTG19", "PTG20", "PTG21", "PTH22", "PTH23", "PTH24", 
            "PTI25", "PTI26", "PTI27", "PTJ28", "PTJ29", "PTJ30", "PTK31", "PTK32", "PTK33", "PTL34", "PTL35", "PTL36", 
            "Real_points", "Real_trail_sequence", 
            "RLA01", "RLA02", "RLA03", "RLB04", "RLB05", "RLB06", "RLC07", "RLC08", "RLC09", "RLD10", "RLD11", "RLD12", 
            "RLE13", "RLE14", "RLE15", "RLF16", "RLF17", "RLF18", "RLG19", "RLG20", "RLG21", "RLH22", "RLH23", "RLH24", 
            "RLI25", "RLI26", "RLI27", "RLJ28", "RLJ29", "RLJ30", "RLK31", "RLK32", "RLK33", "RLL34", "RLL35", "RLL36", 
            "total_points", 
            "IMSV0", "IMSV7", "IMSV15", "IMSV30", "IMSV60", "PTSV0", "PTSV7", "PTSV15", "PTSV30", "PTSV60", "RLSV0", 
            "RLSV7", "RLSV15", "RLSV30", "RLSV60", 
            "IMAUC", "PTAUC", "RLAUC", 
            "IM_time01", "IM_time02", "IM_time03", "IM_time04", "IM_time05", "IM_time06", "IM_time07", "IM_time08", 
            "IM_time09", "IM_time10", "IM_time11", "IM_time12", "IM_time13", "IM_time14", "IM_time15", "IM_time16", 
            "IM_time17", "IM_time18", "IM_time19", "IM_time20", "IM_time21", "IM_time22", "IM_time23", "IM_time24", 
            "IM_time25", "IM_time26", "IM_time27", "IM_time28", "IM_time29", "IM_time30", "IM_time31", "IM_time32", 
            "IM_time33", "IM_time34", "IM_time35", "IM_time36", 
            "PT_time01", "PT_time02", "PT_time03", "PT_time04", "PT_time05", "PT_time06", "PT_time07", "PT_time08", 
            "PT_time09", "PT_time10", "PT_time11", "PT_time12", "PT_time13", "PT_time14", "PT_time15", "PT_time16", 
            "PT_time17", "PT_time18", "PT_time19", "PT_time20", "PT_time21", "PT_time22", "PT_time23", "PT_time24", 
            "PT_time25", "PT_time26", "PT_time27", "PT_time28", "PT_time29", "PT_time30", "PT_time31", "PT_time32", 
            "PT_time33", "PT_time34", "PT_time35", "PT_time36", 
            "RL_time01", "RL_time02", "RL_time03", "RL_time04", "RL_time05", "RL_time06", "RL_time07", "RL_time08", 
            "RL_time09", "RL_time10", "RL_time11", "RL_time12", "RL_time13", "RL_time14", "RL_time15", "RL_time16", 
            "RL_time17", "RL_time18", "RL_time19", "RL_time20", "RL_time21", "RL_time22", "RL_time23", "RL_time24", 
            "RL_time25", "RL_time26", "RL_time27", "RL_time28", "RL_time29", "RL_time30", "RL_time31", "RL_time32", 
            "RL_time33", "RL_time34", "RL_time35", "RL_time36"
        };
    }
}