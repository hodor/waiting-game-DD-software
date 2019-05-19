using System;
using System.Collections.Generic;
using AR_Project.DataClasses.NestedObjects;
using UnityEngine;

namespace AR_Project.MainGame.ExperimentsLevels.ExperimentsHandlers
{
    public class ExperimentData : MonoBehaviour
    {
        private int currentIndex;
        public List<Experiment> experiments;

        public ExperimentData(List<Experiment> currentExperimentList)
        {
            experiments = currentExperimentList;
        }

        public Experiment currentExperiment()
        {
            return experiments[currentIndex];
        }

        public Experiment NextExperiment()
        {
            currentIndex++;
            try
            {
                return experiments[currentIndex];
            }
            catch (Exception e)
            {
                currentIndex--;
                return null;
            }
        }

        public int GetExperimentIndex()
        {
            return currentIndex;
        }
    }
}