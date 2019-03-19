using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cluster_Analysis.CommonClasses
{
    public abstract class Data<T>
    {
        /// <summary>
        /// 
        /// </summary>
        protected T X { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        protected T Y { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        protected Data(T x, T y)
        {
            Y = y;
            X = x;
        }

    }
}
