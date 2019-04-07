using Cluster_Analysis.CommonClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cluster_Analysis
{
    interface IClustering<T>
    {
        /// <summary>
        /// Количество вычисляемых кластеров
        /// </summary>
        int CountOfClusters { get; set; }

        /// <summary>
        /// Значение среднего внутрикластерного расстояния для всего решения 
        /// </summary>
        double AvarageIntraClustersDistances{ get; set; }

        /// <summary>
        /// Событие завершения расчета кластерного анализа 
        /// </summary>
        event EventHandler EndClustering;

        /// <summary>
        /// Метод кластеризации
        /// </summary>
        /// <param name="clustered_Data">Кластеризуемые данные</param>
        /// <returns>Список кластеров</returns>
        List<Cluster> Clustering(List<Clustered_Data> clustered_Data);
    }
}
