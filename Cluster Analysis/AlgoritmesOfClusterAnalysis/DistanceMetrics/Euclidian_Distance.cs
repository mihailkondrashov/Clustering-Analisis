using System;
using Cluster_Analysis.CommonClasses;
using System.Collections.Generic;
using Cluster_Analysis.Interfaces;

namespace Cluster_Analysis.AlgoritmesOfClusterAnalysis.DistanceMetrics
{
    public class Euclidian_Distance : IMetricDistance
    {
        public static double GetValueOfEuclidianDistance(Data dataOne, Data dataTwo)
        {
            double sumSquaredValue = Math.Pow((dataOne.X - dataTwo.X), 2) + Math.Pow((dataOne.Y - dataTwo.Y), 2);
            return Math.Sqrt(sumSquaredValue);
        }

        public double GetValueOfDistance(Data dataOne, Data dataTwo)
        {
            double sumSquaredValue = Math.Pow((dataOne.X - dataTwo.X), 2) + Math.Pow((dataOne.Y - dataTwo.Y), 2);
            return Math.Sqrt(sumSquaredValue);
        }
    }
}
