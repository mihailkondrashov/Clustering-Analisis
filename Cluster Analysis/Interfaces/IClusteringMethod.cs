using System;
using System.Collections.Generic;
using Cluster_Analysis.CommonClasses;

namespace Cluster_Analysis.Interfaces
{
    public interface IClusteringMethod
    {
        /// <summary>
        /// Count of clusters required
        /// </summary>
        int CountOfClusters { get; set; }

        /// <summary>
        /// The value of the average intracluster distance for the entire solution
        /// </summary>
        double AvarageIntraclusterDistances { get; set; }

        /// <summary>
        /// The event of the completion of the calculation of cluster analysis
        /// </summary>
        event EventHandler EndClustering;

        /// <summary>
        /// Clustering
        /// </summary>
        /// <param name="clusteredData">List of source data for cluster analysis</param>
        /// <returns>List of calculated clusters</returns>
        List<Cluster> Clustering(List<ClusteredData> clusteredData);
    }
}
