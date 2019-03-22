using System;
using System.Collections.Generic;
using Cluster_Analysis.AlgoritmesOfClusterAnalysis.DistanceMetrics;
using System.Linq;
using Cluster_Analysis.Interfaces;

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
        /// Событие изменения кластерного центроида
        /// </summary>
        public event EventHandler ChangeCentroid;

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
        public double IntraClusterDistance(IMetricDistance metricDistance)
        {
            double intraClusterDistance = 0.0;
            //Недопущение деления на ноль
            if (Data.Count == 0)
            {
                intraClusterDistance = 0;
            }
            else
            {
                //intraClusterDistance = Data.Average(a => Euclidian_Distance.GetValueOfEuclidianDistance(ClustersCendroid, a));
                intraClusterDistance = Data.Average(a => metricDistance.GetValueOfDistance(ClustersCendroid, a));
            }
            return Math.Round(intraClusterDistance, 3);
        }

        /// <summary>
        /// 
        /// </summary>
        public void GetGravityCenter(IMetricDistance metricDistance)
        {
            if (Data.Count != 0)
            {
                //Расчет центра тяжести кластера
                Centroid newCentroid = new Centroid(Data.Average(a => a.X), Data.Average(a => a.Y));
                //Изменение кластерного центроида
                if (metricDistance.GetValueOfDistance(newCentroid, ClustersCendroid) > 0.5)
                {
                    ClustersCendroid = newCentroid;
                    ChangeCentroid?.Invoke(this, null);
                }
            }
            else
            {
                ClustersCendroid = ClustersCendroid;
            }
        }
    }
}
