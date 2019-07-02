namespace Cluster_Analysis.CommonClasses
{
    /// <summary>
    /// Abstract class of data
    /// </summary>
    public abstract class Data
    {
        /// <summary>
        /// Value of X Axes
        /// </summary>
        public double X { get; private set; }

        /// <summary>
        /// Value of Y Axes
        /// </summary>
        public double Y { get; private set; }

        /// <summary>
        /// Constructor of class Data
        /// </summary>
        /// <param name="x">Value of X Axes</param>
        /// <param name="y">Value of Y Axes</param>
        protected Data(double x, double y)
        {
            X = x;
            Y = y;
        }
        
    }
}
