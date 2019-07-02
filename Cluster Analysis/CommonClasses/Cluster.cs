using System;
using System.Collections.Generic;
using System.Linq;
using Cluster_Analysis.Interfaces;

namespace Cluster_Analysis.CommonClasses
{
    public class Cluster
    {
        /// <summary>
        /// Id cluster
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Clusters data
        /// </summary>
        public List<ClusteredData> Data { get; private set; }

        /// <summary>
        /// Object of class cendroid for cluster
        /// </summary>
        public Centroid ClustersCendroid { get; private set; }

        /// <summary>
        /// The Event changing the cluster centroid
        /// </summary>
        public event EventHandler ChangeCentroid;

        /// <summary>
        /// Constructor of class Cluster
        /// </summary>
        /// <param name="id">Id</param>
        /// <param name="data">Clusters data</param>
        /// <param name="cendroid">Object of class cendroid for cluster</param>
        public Cluster(int id, List<ClusteredData> data, Centroid cendroid)
        {
            Id = id;
            Data = data;
            ClustersCendroid = cendroid;
        }

        /// <summary>
        /// Constructor of class Cluster
        /// </summary>
        /// <param name="id">Id</param>
        /// <param name="cendroid">Object of class cendroid for cluster</param>
        public Cluster(int id, Centroid cendroid)
        {
            Id = id;
            Data = new List<ClusteredData>();
            ClustersCendroid = cendroid;
        }

        /// <summary>
        /// The calculation of the intracluster distances
        /// </summary>
        /// <param name="metricDistance">Method of calculating intracluster distance</param>
        /// <returns>Value of intracluster distance </returns>
        public double IntraClusterDistance(IMetricDistance metricDistance)
        {
            double intraClusterDistance = 0.0;
            //Avoiding division by zero
            if (Data.Count == 0)
            {
                intraClusterDistance = 0;
            }
            else
            {
                intraClusterDistance = Data.Average(a => metricDistance.GetValueOfDistance(ClustersCendroid, a));
            }
            return Math.Round(intraClusterDistance, 3);
        }

        /// <summary>
        /// Redefining the cluster centroid according to the center of gravity
        /// </summary>
        /// <param name="metricDistance">Method of calculating distance</param>
        public void SetCentroidLikeGravityCenter(IMetricDistance metricDistance)
        {
            if (Data.Count != 0)
            {
                if (metricDistance.GetValueOfDistance(GetGravityCenter(), ClustersCendroid) > 0.01)
                {
                    ClustersCendroid = new Centroid(GetGravityCenter().X, GetGravityCenter().Y);
                    ChangeCentroid?.Invoke(this, null);
                }
            }
            else
            {
                ClustersCendroid = ClustersCendroid;
            }
        }

        /// <summary>
        /// Calculating the center of gravity for a cluster
        /// </summary>
        /// <returns>Object of class cendroid for cluster</returns>
        public Centroid GetGravityCenter()
        {
             Centroid newCentroid = new Centroid(Data.Average(a => a.X), Data.Average(a => a.Y));
             return newCentroid;
        }
    }
}
