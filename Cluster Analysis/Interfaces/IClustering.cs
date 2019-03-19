using Cluster_Analysis.CommonClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cluster_Analysis
{
    interface IClustering<T>
    {
        /// <summary>
        /// 
        /// </summary>
        int CountOfClusters { get; set; }

        /// <summary>
        /// 
        /// </summary>
        event EventHandler EndClustering;

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        List<Cluster<T>> Clustering(List<double> clustered_Data);
    }
}
