using System;
using System.Collections.Generic;
using Cluster_Analysis.CommonClasses;
namespace Cluster_Analysis
{
    public class K_Means<T>:IClustering<T>
    {
        /// <summary>
        /// 
        /// </summary>
        public int CountOfClusters { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public event EventHandler EndClustering;

        /// <summary>
        /// 
        /// </summary>
        public Dictionary<string,Centroid<T>> StartingsCentroids { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public Dictionary<string, Centroid<T>> FinishesCentroids { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public K_Means(int countOfClusters)
        {
            CountOfClusters = countOfClusters;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<Cluster<T>> Clustering(List<double> clustered_Data)
        {
            throw new NotImplementedException();
        }
    }
}
