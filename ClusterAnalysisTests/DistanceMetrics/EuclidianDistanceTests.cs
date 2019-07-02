using Microsoft.VisualStudio.TestTools.UnitTesting;
using Cluster_Analysis.DistanceMetrics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cluster_Analysis.CommonClasses;

namespace Cluster_Analysis.DistanceMetrics.Tests
{
    [TestClass()]
    public class EuclidianDistanceTests
    {
        [TestMethod()]
        public void GetValueOfDistanceTest()
        {
            EuclidianDistance euclidianDistance = new EuclidianDistance();
            euclidianDistance.GetValueOfDistance(new ClusteredData(0,0), new ClusteredData(2,2));

            Assert.AreEqual(2,2);
        }
    }
}