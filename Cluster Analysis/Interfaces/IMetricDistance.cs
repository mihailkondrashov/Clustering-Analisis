using Cluster_Analysis.CommonClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cluster_Analysis.Interfaces
{
    public interface IMetricDistance
    {
        double GetValueOfDistance(Data One, Data Two);
    }
}
