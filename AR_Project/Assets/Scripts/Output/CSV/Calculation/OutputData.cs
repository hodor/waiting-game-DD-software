using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
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
            var age = DateTime.Today - birthdate;
            return ((float)age.Days) / 365.25f;
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
            var usCulture = new CultureInfo("en-US");
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
                age_year().ToString("0.##", usCulture).Replace(",",""),
                age_month().ToString("0.##", usCulture).Replace(",",""),
                gender,
                avatar,
                sequenceTasks
            };
            
            var orderedData = experimentData.Values.ToList();

            foreach(var data in orderedData)
                list.AddRange(data.points.ToList());
            
            list.Add(total_points.ToString(usCulture).Replace(",",""));

            foreach(var data in orderedData)
                data.subjectiveValueData.Calculate(data.points.GetSequencePoints());
            foreach(var data in orderedData)
                list.AddRange(data.subjectiveValueData.ToList());
            //AUC
            List<float> aucValues = new List<float>();
            float maxAUC = Math.GetMaxAreaUnderCurve();
            foreach (var data in orderedData)
            {
                var auc = Math.GetAreaUnderCurve(data.subjectiveValueData.GetValues());
                aucValues.Add(auc);
                list.Add(auc.ToString("0.##", usCulture).Replace(",", ""));
            }

            // Subjective Values normalized
            foreach (var data in orderedData)
            {
                list.AddRange(Math.GetNormalizedValues(data.subjectiveValueData).ConvertAll<string>
                    (x => x.ToString("0.###",usCulture).Replace(",","")));
            }
            //AUC normalized
            foreach (var auc in aucValues)
            {
                list.Add((auc / maxAUC).ToString("0.##", usCulture).Replace(",", ""));
            }
            
            foreach (var data in orderedData)
                
            {
                //We need sort the choose time using the sequence order
                var sequenceOrder = data.points.sequenceOrder;
                Dictionary<int, double> orderedChooseTime = new Dictionary<int, double>();
                for (int i = 0; i < sequenceOrder.Count; i++)
                {
                    string strOrder = sequenceOrder[i];
                    int order = Int32.Parse(strOrder);
                    double time = data.chooseTime[i];
                    orderedChooseTime.Add(order - 1, time);
                }

                for (int i = 0; i < orderedChooseTime.Count; i++)
                {
                    list.Add(orderedChooseTime[i].ToString("0.##", usCulture).Replace(",", ""));
                }
            }

            return list;
        }
        public static readonly string[] Headers = new[]
        {
            "Name", "Date_application", "Birth", "Age_year", "Age_month", "Gender", "Avatar", "Task_sequence", 
            "Imaginary_points", "Imaginary_trail_sequence", 
            "IM01_D1R1", "IM02_D1R1", "IM03_D1R1", "IM04_D1R2", "IM05_D1R2", "IM06_D1R2", "IM07_D1R3", "IM08_D1R3", 
            "IM09_D1R3", "IM10_D2R1", "IM11_D2R1", "IM12_D2R1", "IM13_D2R2", "IM14_D2R2", "IM15_D2R2", "IM16_D2R3", 
            "IM17_D2R3", "IM18_D2R3", "IM19_D3R1", "IM20_D3R1", "IM21_D3R1", "IM22_D3R2", "IM23_D3R2", "IM24_D3R2", 
            "IM25_D3R3", "IM26_D3R3", "IM27_D3R3", "IM28_D4R1", "IM29_D4R1", "IM30_D4R1", "IM31_D4R2", "IM32_D4R2", 
            "IM33_D4R2", "IM34_D4R3", "IM35_D4R3", "IM36_D4R3",
            "Patience_points", "Patience_trail_sequence", 
            "PT01_D1R1", "PT02_D1R1", "PT03_D1R1", "PT04_D1R2", "PT05_D1R2", "PT06_D1R2", "PT07_D1R3", "PT08_D1R3", 
            "PT09_D1R3", "PT10_D2R1", "PT11_D2R1", "PT12_D2R1", "PT13_D2R2", "PT14_D2R2", "PT15_D2R2", "PT16_D2R3", 
            "PT17_D2R3", "PT18_D2R3", "PT19_D3R1", "PT20_D3R1", "PT21_D3R1", "PT22_D3R2", "PT23_D3R2", "PT24_D3R2", 
            "PT25_D3R3", "PT26_D3R3", "PT27_D3R3", "PT28_D4R1", "PT29_D4R1", "PT30_D4R1", "PT31_D4R2", "PT32_D4R2", 
            "PT33_D4R2", "PT34_D4R3", "PT35_D4R3", "PT36_D4R3",
            "Real_points", "Real_trail_sequence", 
            "RL01_D1R1", "RL02_D1R1", "RL03_D1R1", "RL04_D1R2", "RL05_D1R2", "RL06_D1R2", "RL07_D1R3", "RL08_D1R3", 
            "RL09_D1R3", "RL10_D2R1", "RL11_D2R1", "RL12_D2R1", "RL13_D2R2", "RL14_D2R2", "RL15_D2R2", "RL16_D2R3", 
            "RL17_D2R3", "RL18_D2R3", "RL19_D3R1", "RL20_D3R1", "RL21_D3R1", "RL22_D3R2", "RL23_D3R2", "RL24_D3R2", 
            "RL25_D3R3", "RL26_D3R3", "RL27_D3R3", "RL28_D4R1", "RL29_D4R1", "RL30_D4R1", "RL31_D4R2", "RL32_D4R2", 
            "RL33_D4R2", "RL34_D4R3", "RL35_D4R3", "RL36_D4R3",
            "total_points", 
            "IMSV0", "IMSV1", "IMSV2", "IMSV3", "IMSV4", 
            "PTSV0", "PTSV1", "PTSV2", "PTSV3", "PTSV4", 
            "RLSV0", "RLSV1", "RLSV2", "RLSV3", "RLSV4", 
            "IMAUC", "PTAUC", "RLAUC",
            "IMSV_N0", "IMSV_N1", "IMSV_N2", "IMSV_N3", "IMSV_N4", 
            "PTSV_N0", "PTSV_N1", "PTSV_N2", "PTSV_N3", "PTSV_N4", 
            "RLSV_N0", "RLSV_N1", "RLSV_N2", "RLSV_N3", "RLSV_N4", 
            "IMAUC_N", "PTAUC_N", "RLAUC_N",
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