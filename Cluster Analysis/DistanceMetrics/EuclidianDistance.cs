using System;
using Cluster_Analysis.CommonClasses;
using Cluster_Analysis.Interfaces;

namespace Cluster_Analysis.DistanceMetrics
{
    /// <summary>
    ///  The "ordinary" straight-line distance between two points in Euclidean space
    /// </summary>
    public class EuclidianDistance : IMetricDistance
    {
        /// <summary>
        /// Method of calculating Euclidian distance between points
        /// </summary>
        /// <param name="dataOne">Object of class Data</param>
        /// <param name="dataTwo">Object of class Data</param>
        /// <returns>Value of distance between points</returns>
        public double GetValueOfDistance(Data dataOne, Data dataTwo)
        {
            double sumSquaredValue = Math.Pow((dataOne.X - dataTwo.X), 2) + Math.Pow((dataOne.Y - dataTwo.Y), 2);
            return Math.Sqrt(sumSquaredValue);
        }
    }
}
