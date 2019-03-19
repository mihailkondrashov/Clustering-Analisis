using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cluster_Analysis
{
    public class K_Means:IClustering
    {
        /// <summary>
        /// 
        /// </summary>
        public int[] ClusteringsMatrix { get; set; }

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
        public Dictionary<string,Centroid> StartingsCentroids { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public Dictionary<string, Centroid> FinishesCentroids { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public int[] Clustering(List<double> clustered_Data)
        {
            throw new NotImplementedException();
        }
    }
}
