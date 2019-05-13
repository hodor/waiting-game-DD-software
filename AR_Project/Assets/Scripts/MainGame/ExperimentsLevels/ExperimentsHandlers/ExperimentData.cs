using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using AR_Project.DataClasses.NestedObjects;

namespace AR_Project.MainGame.ExperimentsLevels.ExperimentsHandlers
{
    public class ExperimentData : MonoBehaviour
    {
        public List<Experiment> experiments;

        int currentIndex = 0;

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