using System;
using System.Collections.Generic;
using Cluster_Analysis.AlgoritmesOfClusterAnalysis.DistanceMetrics;

namespace Cluster_Analysis.CommonClasses
{
    public class Cluster
    {
        /// <summary>
        /// Идентификатор кластера
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Данные входящие в кластер
        /// </summary>
        public List<Clustered_Data> Data { get; private set; }

        /// <summary>
        /// Центроид кластера
        /// </summary>
        public Centroid ClustersCendroid { get; private set; }

        /// <summary>
        /// Конструктор класса Cluster
        /// </summary>
        /// <param name="id">Идентификатор</param>
        /// <param name="data">Данные, относящиеся к кластеру</param>
        /// <param name="cendroid">Центроид кластера</param>
        public Cluster(int id, List<Clustered_Data> data, Centroid cendroid)
        {
            Id = id;
            Data = data;
            ClustersCendroid = cendroid;
        }

        /// <summary>
        /// Конструктор класса Cluster
        /// </summary>
        /// <param name="id">Идентификатор</param>
        /// <param name="cendroid">Центроид кластера</param>
        public Cluster(int id, Centroid cendroid)
        {
            Id = id;
            Data = new List<Clustered_Data>();
            ClustersCendroid = cendroid;
        }

        /// <summary>
        /// Расчет внутрикластерного расстояния
        /// </summary>
        public double IntraClusterDistance()
        {
            double intraClusterDistance = 0.0;

            for (var i = 0; i < Data.Count; i++)
            {
                intraClusterDistance += Euclidian_Distance.GetValueOfEuclidianDistance(ClustersCendroid, Data[i]);
            }

            //Недопущение деления на ноль
            if (intraClusterDistance == 0)
            {
                intraClusterDistance = 0;
            }
            else
            {
                intraClusterDistance = intraClusterDistance / Data.Count;
            }
            return Math.Round(intraClusterDistance, 3);
        }
    }
}
