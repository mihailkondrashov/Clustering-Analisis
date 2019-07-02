using System.Collections.Generic;
using System.Linq;
using Cluster_Analysis.AlgoritmesOfClusterAnalysis;
using Cluster_Analysis.CommonClasses;
using Cluster_Analysis.DistanceMetrics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Cluster_Analysis.Tests
{
    [TestClass()]
    public class ClusterAnalysisTests
    {
        [TestMethod()]
        public void ClusteringTest()
        {
            List<ClusteredData> datanew = new List<ClusteredData>();
            EuclidianDistance distance = new EuclidianDistance();
            K_Means k_means = new K_Means(2, 0.1, distance);
            ClusterAnalysis clusterAnalysis = new ClusterAnalysis(k_means);

            datanew.Add(new ClusteredData(0, 0));
            datanew.Add(new ClusteredData(0, 20));
            datanew.Add(new ClusteredData(20, 0));
            datanew.Add(new ClusteredData(20, 20));

            var list = clusterAnalysis.Clustering(datanew);

            Assert.AreEqual(2, list.Count);

            var flag = from value in k_means.FinishesCentroids.Values
                where (value.X == 0 && value.Y == 0) ||
                      (value.X == 0 && value.Y == 20) ||
                      (value.X == 20 && value.Y == 20) ||
                      (value.X == 20 && value.Y == 0) ||
                      (value.X == 10 && value.Y == 20) ||
                      (value.X == 0 && value.Y == 10) ||
                      (value.X == 20 && value.Y == 10) ||
                      (value.X == 0 && value.Y == 10)
                select value;
            Assert.AreEqual(0.1, k_means.CoefficientTaboo);
            Assert.AreEqual(2, k_means.CountOfClusters);
            Assert.IsNotNull(flag);

        }
    }
}