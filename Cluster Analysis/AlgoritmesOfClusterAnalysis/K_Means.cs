using System;
using System.Collections.Generic;
using System.Linq;
using Cluster_Analysis.CommonClasses;
using Cluster_Analysis.DistanceMetrics;
using Cluster_Analysis.Interfaces;

namespace Cluster_Analysis.AlgoritmesOfClusterAnalysis
{
    public class K_Means:IClusteringMethod
    {
        /// <summary>
        /// Количество вычисляемых кластеров
        /// </summary>
        public int CountOfClusters { get; set; }

        /// <summary>
        /// Коэффициент, отпределяющий радус запретной зоны генерации начальных центроидов
        /// </summary>
        public double CoefficientTaboo { get; set; }

        /// <summary>
        /// Событие завершения расчета кластерного анализа 
        /// </summary>
        public event EventHandler EndClustering;

        /// <summary>
        /// Массив начальных центроидов
        /// </summary>
        public Dictionary<string,Centroid> StartingsCentroids { get; private set; }

        /// <summary>
        /// Массив конечных центроидов
        /// </summary>
        public Dictionary<string, Centroid> FinishesCentroids { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public double AvarageIntraclusterDistances { get; set; }

        /// <summary>
        /// 
        /// </summary>
        private bool _changedCentroids = true;

        /// <summary>
        /// 
        /// </summary>
        private IMetricDistance _metricDistance;

        /// <summary>
        /// Стандартный конструктор класса K_Means
        /// </summary>
        public K_Means()
        {
            //Количество кластеров по умолчанию
            CountOfClusters = 2;
            CoefficientTaboo = 0;
            _metricDistance = new EuclidianDistance();
        }

        /// <summary>
        /// Расширенный конструктор класса K_Means
        /// </summary>
        /// <param name="countOfClusters">Количество кластеров</param>
        /// <param name="coefficientTaboo"></param>
        /// <param name="metricDistance"></param>
        public K_Means(int countOfClusters, double coefficientTaboo, IMetricDistance metricDistance)
        {
            CountOfClusters = countOfClusters;
            CoefficientTaboo = coefficientTaboo;
            _metricDistance = metricDistance;
        }

        /// <summary>
        /// Метод кластеризации
        /// </summary>
        /// <param name="clusteredData">Кластеризуемые данные</param>
        /// <returns>Список кластеров</returns>
        public List<Cluster> Clustering(List<ClusteredData> clusteredData)
        {
            //Создание объекта списка кластеров
            var clusters = new List<Cluster>();
            FinishesCentroids = new Dictionary<string, Centroid>();
            //
            var changedClustering = true;
            _changedCentroids = true;
            //Получение начальных кластеров
            GetStartingCentroid(clusteredData);

            for (var i = 0; i < CountOfClusters;i++)
            {
                clusters.Add(new Cluster(i+1,StartingsCentroids[$"Кластер - {i + 1}"]));
            }

            while (changedClustering && _changedCentroids)
            {
                UpdateClustering(clusteredData, clusters);
                UpdateCentroids(clusters);
            }
            foreach (var cluster in clusters)
            {
                FinishesCentroids.Add($"Кластер - {cluster.Id}", cluster.ClustersCendroid);
            }

            AvarageIntraclusterDistances = clusters.Average(a => a.IntraClusterDistance(_metricDistance));

            EndClustering?.Invoke(this, null);

            return clusters;
        }

        /// <summary>
        /// Получение начальных центроидов кластера
        /// </summary>
        /// <param name="clusteredData">Кластеризуемые данные</param>
        private void GetStartingCentroid(List<ClusteredData> clusteredData)
        {
            //Инициализация массива начальных центроидов
            StartingsCentroids = new Dictionary<string, Centroid>();
            //Объект класса Random, для случайной генерации центроидов
            Random randomCentroid = new Random();

            for (int i = 0; i < CountOfClusters; ++i)
            {
                //Получение случайных значений координат центроида в диапазоне исходных данных
                StartingsCentroids.Add($"Кластер - {i + 1}", new Centroid(randomCentroid.Next((int)clusteredData.Min(a => a.X), (int)clusteredData.Max(a => a.X) + 1),
                    randomCentroid.Next((int)clusteredData.Min(a => a.Y), (int)clusteredData.Max(a => a.Y) + 1)));

                //Проверка на генерацию начальных центроидов вне запретной зоны
                CheckStartingCentroidPosition(clusteredData, randomCentroid);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="clusteredData"></param>
        /// <param name="clusters"></param>
        private bool UpdateClustering (List<ClusteredData> clusteredData, List<Cluster> clusters)
        {
            bool changedClustering = false;

            foreach (var data in clusteredData) 
            {
                // Поиск объектов класса Cluster до центроидов которых минимальное растояние от data
                var optimumClusters = (from cluster in clusters // определяем каждый объект из clusters как cluster
                                       where _metricDistance.GetValueOfDistance(cluster.ClustersCendroid, data) ==
                                             clusters.Min(a => _metricDistance.GetValueOfDistance(a.ClustersCendroid, data)) //Проверка условия поиска соответсвий
                                       select cluster);// выбираем объект


                //Поиск объектов класса Cluster, которые имеют в данных кластера значение data
                var clustersList = from cluster in clusters // определяем каждый объект из clusters как cluster
                                   where cluster.Data.Any(a => a == data) //Проверка условия поиска соответсвий
                                   select cluster;// выбираем объект

                //У всех объектов класса Cluster в списке clustersList удаляем значение data
                foreach (Cluster cluster in clustersList)
                {
                    cluster.Data.Remove(data);
                }
                //Всем объектам класса Cluster в списке optimumClusters добавляем значение data
                foreach (Cluster cluster in optimumClusters)
                {
                    cluster.Data.Add(data);
                }
                // Поиск объектов класса Cluster в списке optimumClusters с максимальным внутрикластерным расcтоянием
                var nonOptimumClusters = from cluster in optimumClusters // определяем каждый объект из clusters как cluster
                                          where cluster.IntraClusterDistance(_metricDistance) != optimumClusters.Min(a => a.IntraClusterDistance(_metricDistance))//Проверка условия поиска соответсвий
                                          select cluster;// выбираем объект

                // Производим удаление значения data у объектов класса Cluster в списке nonOptimumClusters
                foreach (Cluster cluster in nonOptimumClusters)
                {
                    cluster.Data.Remove(data);
                }

                //Проверка наличия объекта data только у одного кластера
                var checkList = from cluster in clusters // определяем каждый объект из clusters как cluster
                    where cluster.Data.Any(a => a == data) //Проверка условия поиска соответсвий
                    select cluster;// выбираем объект
   

                if (checkList.Count() > 1)
                {
                    checkList.ToList()[0].Data.Remove(data);
                }

                if ((!(optimumClusters.Count() == 1) && !(clustersList.Count() == 1) && !(optimumClusters == clustersList)) || !(optimumClusters.Count() == 1) && !(clustersList.Count() == 0))
                {
                    changedClustering = true;
                }
            }

            return changedClustering;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="clusters"></param>
        private void UpdateCentroids (List<Cluster> clusters)
        {
            _changedCentroids = false;

            foreach(var cluster in clusters)
            {
                cluster.ChangeCentroid += Cluster_ChangeCentroid;
                cluster.SetCentroidLikeGravityCenter(_metricDistance);
            } 
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Cluster_ChangeCentroid(object sender, EventArgs e)
        {
            _changedCentroids = true;
        }

        /// <summary>
        /// Проверка на генерацию начальных центроидов вне запретной зоны
        /// </summary>
        /// <param name="clusteredData">Кластеризуемые данные</param>
        /// <param name="randomCentroid"></param>
        private void CheckStartingCentroidPosition(List<ClusteredData> clusteredData, Random randomCentroid)
        {
            for (var j = 0; j < StartingsCentroids.Count - 1; j++)
            {
                while (_metricDistance.GetValueOfDistance(StartingsCentroids[$"Кластер - {j + 1}"], StartingsCentroids[$"Кластер - {StartingsCentroids.Count}"]) <
                                    CoefficientTaboo * Math.Min(clusteredData.Max(a => a.X) - clusteredData.Min(a => a.X), clusteredData.Max(a => a.Y) - clusteredData.Min(a => a.Y)))
                {
                    StartingsCentroids[$"Кластер - {StartingsCentroids.Count}"] = new Centroid(randomCentroid.Next((int)clusteredData.Min(a => a.X), (int)clusteredData.Max(a => a.X) + 1),
                        randomCentroid.Next((int)clusteredData.Min(a => a.Y), (int)clusteredData.Max(a => a.Y) + 1));
                }
            }
        }
    }
}
