using System;
using Cluster_Analysis.CommonClasses;
using System.Collections.Generic;

namespace Cluster_Analysis.AlgoritmesOfClusterAnalysis.DistanceMetrics
{
    public static class Euclidian_Distance
    {
        public static double GetValueOfEuclidianDistance(Data dataOne, Data dataTwo)
        {
            double sumSquaredValue = Math.Pow((dataOne.X - dataTwo.X), 2) + Math.Pow((dataOne.Y - dataTwo.Y), 2);
            return Math.Sqrt(sumSquaredValue);
        }
    }
}
