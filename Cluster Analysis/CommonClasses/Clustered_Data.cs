using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cluster_Analysis.CommonClasses
{
    public class Clustered_Data<T>:Data<T>
    {
        public Clustered_Data(T x, T y) : base(x, y)
        {

        }
    }
}
