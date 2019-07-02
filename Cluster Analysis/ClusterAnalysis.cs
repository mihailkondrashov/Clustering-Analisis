using System.Collections.Generic;
using Cluster_Analysis.CommonClasses;
using Cluster_Analysis.Interfaces;

namespace Cluster_Analysis
{
    /// <summary>
    /// Perform a cluster analysis task
    /// </summary>
    public class ClusterAnalysis
    {
        /// <summary>
        /// Method of cluster analysis
        /// </summary>
        private IClusteringMethod ClusteringMethod { get;}

        /// <summary>
        /// Constructor of class ClusterAnalysis
        /// </summary>
        /// <param name="clusteringMethod">Method of cluster analysis</param>
        public ClusterAnalysis(IClusteringMethod clusteringMethod)
        {
            ClusteringMethod = clusteringMethod;
        }

        /// <summary>
        /// Perform a cluster analysis task
        /// </summary>
        /// <param name="clusteredData">List of source data for cluster analysis</param>
        /// <returns>List of calculated clusters</returns>
        public List<Cluster> Clustering(List<ClusteredData> clusteredData)
        {
            var clusters = ClusteringMethod.Clustering(clusteredData);
            return clusters;
        }
    }
}
