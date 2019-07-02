namespace Cluster_Analysis.CommonClasses
{
    /// <summary>
    /// Source data for cluster analysis
    /// </summary>
    public class ClusteredData:Data
    {
        /// <summary>
        /// Constructor of class Clustered_Data
        /// </summary>
        /// <param name="x">Value of X Axes</param>
        /// <param name="y">Value of Y Axes</param>
        public ClusteredData(double x, double y) : base(x, y) { }
    }
}
