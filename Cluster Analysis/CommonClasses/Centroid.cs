using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cluster_Analysis
{
    public class Centroid
    {
        /// <summary>
        /// 
        /// </summary>
        public double X { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public double Y { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public Centroid(double x, double y)
        {
            Y = y;
            X = x;
        }

    }
}
