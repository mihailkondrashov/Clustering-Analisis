using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cluster_Analysis.CommonClasses;
using Cluster_Analysis.AlgoritmesOfClusterAnalysis.DistanceMetrics;
using Cluster_Analysis;
using System.Threading;

namespace TestApp
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Clustered_Data> datanew = new List<Clustered_Data>();
            List<K_Means<double>> k_meansList = new List<K_Means<double>>();
            List<List<Cluster>> clustersList = new List<List<Cluster>>();
            System.Diagnostics.Stopwatch myStopwatch = new System.Diagnostics.Stopwatch();

            datanew.Add(new Clustered_Data(1,1));
            datanew.Add(new Clustered_Data(1, 20));
            datanew.Add(new Clustered_Data(20, 1));
            datanew.Add(new Clustered_Data(20, 20));

            myStopwatch.Start(); //запуск
            for (var i = 0; i < 10000; i++)
            {
                K_Means<double> k_means = new K_Means<double>(2, 0.1);
                clustersList.Add(k_means.Clustering(datanew));
                k_meansList.Add(k_means);
                //Thread.Sleep(10);
            }
            myStopwatch.Stop();

            var fulltime = myStopwatch.ElapsedMilliseconds;
            double Onetime = myStopwatch.ElapsedMilliseconds/1000.0;

            int a = 0;
            int b = 0;
            int c = 0;
            int d = 0;
            int e = 0;

            foreach (var list in clustersList)
            {
                a += list.Count(q => q.Data.Count == 3);
                b += list.Count(q => q.Data.Count == 2);
                c += list.Count(q => q.Data.Count == 1);

                

            }
            b = b / 2;
            var ads = new Cluster(1, new Centroid(1, 10));
            ads.IntraClusterDistance();

            //// Поиск объектов класса Cluster в списке optimumClusters с максимальным внутрикластерным расcтоянием
            //var nonOptimumClusters = from list in clustersList // определяем каждый объект из clusters как cluster
            //                         where 
            //                         select cluster.IntraClusterDistance();// выбираем объект
           


        }
    }
}
