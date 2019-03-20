using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cluster_Analysis.CommonClasses
{
    public abstract class Data
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
        /// <param name="values"></param>
        protected Data(double x, double y)
        {
            X = x;
            Y = y;
        }
        
    }
}
