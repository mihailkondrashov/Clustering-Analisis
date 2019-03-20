using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cluster_Analysis.CommonClasses;
using Cluster_Analysis.AlgoritmesOfClusterAnalysis.DistanceMetrics;
using Cluster_Analysis;

namespace TestApp
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Clustered_Data> datanew = new List<Clustered_Data>();

            datanew.Add(new Clustered_Data(1,1));
            datanew.Add(new Clustered_Data(1, 20));
            datanew.Add(new Clustered_Data(20, 1));
            datanew.Add(new Clustered_Data(20, 20));

            Euclidian_Distance.GetValueOfEuclidianDistance(datanew[0], datanew[1]);


            K_Means<double> k_means = new K_Means<double>(4, 0.1);
            k_means.Clustering(datanew);

            var clusters = new List<Cluster>();

            clusters.Add(new Cluster (1,new Centroid (10,10)));
            clusters.Add(new Cluster(2, new Centroid(10, 10)));
            clusters.Add(new Cluster(3, new Centroid(9, 10)));

            k_means.UpdateClustering(datanew, clusters);



        }
    }
}
