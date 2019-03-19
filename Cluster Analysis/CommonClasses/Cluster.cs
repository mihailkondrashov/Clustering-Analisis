using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cluster_Analysis.CommonClasses
{
    public class Cluster<T>
    {
        /// <summary>
        /// 
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public List<Clustered_Data<T>> Data { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public Centroid<T> Cendroid { get; private set; }

        /// <summary>
        /// Конструктор класса Cluster
        /// </summary>
        /// <param name="id">Идентификатор</param>
        /// <param name="data">Данные, относящиеся к кластеру</param>
        /// <param name="cendroid">Центроид кластера</param>
        public Cluster(int id, List<Clustered_Data<T>> data, Centroid<T> cendroid)
        {
            Id = id;
            Data = data;
            Cendroid = cendroid;
        }

        /// <summary>
        /// Расчет внутрикластерного расстояния
        /// </summary>
        public double IntraClusterDistance()
        {
            //TODO: Реализовать
            double value = default(double);
            return value;
        }
    }
}
