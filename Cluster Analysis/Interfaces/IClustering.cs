using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cluster_Analysis
{
    interface IClustering
    {
        /// <summary>
        /// 
        /// </summary>
        int[] ClusteringsMatrix { get; set; }

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
        int[] Clustering(List<double> clustered_Data);
    }
}
