using Cluster_Analysis.CommonClasses;

namespace Cluster_Analysis.Interfaces
{
    /// <summary>
    /// Metric for calculating distances between points
    /// </summary>
    public interface IMetricDistance
    {
        /// <summary>
        /// Method of calculating distances between points
        /// </summary>
        /// <param name="one">Object of class Data</param>
        /// <param name="two">Object of class Data</param>
        /// <returns>Value of distance between points</returns>
        double GetValueOfDistance(Data one, Data two);
    }
}
