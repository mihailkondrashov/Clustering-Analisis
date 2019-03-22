using System;
using System.Collections.Generic;
using Cluster_Analysis.CommonClasses;
using System.Linq;
using Cluster_Analysis.AlgoritmesOfClusterAnalysis.DistanceMetrics;

namespace Cluster_Analysis
{
    public class K_Means<T>:IClustering<T>
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
        private bool changedCentroids = true;

        /// <summary>
        /// Стандартный конструктор класса K_Means
        /// </summary>
        public K_Means()
        {
            //Количество кластеров по умолчанию
            CountOfClusters = 2;
            CoefficientTaboo = 0;
        }

        /// <summary>
        /// Расширенный конструктор класса K_Means
        /// </summary>
        /// <param name="countOfClusters">Количество кластеров</param>
        public K_Means(int countOfClusters, double coefficientTaboo)
        {
            CountOfClusters = countOfClusters;
            CoefficientTaboo = coefficientTaboo;
        }

        /// <summary>
        /// Метод кластеризации
        /// </summary>
        /// <param name="clustered_Data">Кластеризуемые данные</param>
        /// <returns>Список кластеров</returns>
        public List<Cluster> Clustering(List<Clustered_Data> clustered_Data)
        {
            //Создание объекта списка кластеров
            List<Cluster> clusters = new List<Cluster>();
            FinishesCentroids = new Dictionary<string, Centroid>();
            //
            var changedClustering = true;
            changedCentroids = true;
            //Получение начальных кластеров
            GetStartingCentroid(clustered_Data);

            for (var i = 0; i < CountOfClusters;i++)
            {
                clusters.Add(new Cluster(i+1,StartingsCentroids[$"Кластер - {i + 1}"]));
            }

            while (changedClustering && changedCentroids)
            {
                UpdateClustering(clustered_Data, clusters);
                UpdateCentroids(clusters);
            }

            foreach (var cluster in clusters)
            {
                FinishesCentroids.Add($"Кластер - {cluster.Id}", cluster.ClustersCendroid);
            }
            if (true)
            {
                EndClustering?.Invoke(this,null);
            }

            return clusters;
        }

        /// <summary>
        /// 
        /// </summary>
        public void AvarageIntraClustersDistances()
        {

        }

        /// <summary>
        /// Получение начальных центроидов кластера
        /// </summary>
        /// <param name="clustered_Data">Кластеризуемые данные</param>
        private void GetStartingCentroid(List<Clustered_Data> clustered_Data)
        {
            //Инициализация массива начальных центроидов
            StartingsCentroids = new Dictionary<string, Centroid>();
            //Объект класса Random, для случайной генерации центроидов
            Random randomCentroid = new Random();

            for (int i = 0; i < CountOfClusters; ++i)
            {
                //Получение случайных значений координат центроида в диапазоне исходных данных
                StartingsCentroids.Add($"Кластер - {i + 1}", new Centroid(randomCentroid.Next((int)clustered_Data.Min(a => a.X), (int)clustered_Data.Max(a => a.X) + 1),
                    randomCentroid.Next((int)clustered_Data.Min(a => a.Y), (int)clustered_Data.Max(a => a.Y) + 1)));

                //Проверка на генерацию начальных центроидов вне запретной зоны
                CheckStartingCentroidPosition(clustered_Data, randomCentroid);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="clustered_Data"></param>
        /// <param name="clusters"></param>
        private bool UpdateClustering (List<Clustered_Data> clustered_Data, List<Cluster> clusters)
        {
            bool changedClustering = false;

            foreach (var data in clustered_Data) 
            {
                // Поиск объектов класса Cluster до центроидов которых минимальное растояние от data
                var optimumClusters = (from cluster in clusters // определяем каждый объект из clusters как cluster
                                      where Euclidian_Distance.GetValueOfEuclidianDistance(cluster.ClustersCendroid, data) ==
                                            clusters.Min(a => Euclidian_Distance.GetValueOfEuclidianDistance(a.ClustersCendroid, data)) //Проверка условия поиска соответсвий
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
                                          where cluster.IntraClusterDistance() != optimumClusters.Min(a => a.IntraClusterDistance())//Проверка условия поиска соответсвий
                                          select cluster;// выбираем объект

                // Производим удаление значения data у объектов класса Cluster в списке nonOptimumClusters
                foreach (Cluster cluster in nonOptimumClusters)
                {
                    cluster.Data.Remove(data);
                }

                if ((!(optimumClusters.Count() == 1) && !(clustersList.Count() == 1) && !(optimumClusters == clustersList)) || !(optimumClusters.Count() == 1) && !(clustersList.Count() == 0))
                {
                    changedClustering = true;
                }
            }

            return changedClustering;
        }

        private void UpdateCentroids (List<Cluster> clusters)
        {
            changedCentroids = false;

            foreach(var cluster in clusters)
            {
                cluster.ChangeCentroid += Cluster_ChangeCentroid;
                cluster.GetGravityCenter();
            } 
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Cluster_ChangeCentroid(object sender, EventArgs e)
        {
            changedCentroids = true;
        }

        /// <summary>
        /// Проверка на генерацию начальных центроидов вне запретной зоны
        /// </summary>
        /// <param name="clustered_Data">Кластеризуемые данные</param>
        private void CheckStartingCentroidPosition(List<Clustered_Data> clustered_Data, Random randomCentroid)
        {
            for (var j = 0; j < StartingsCentroids.Count - 1; j++)
            {
                while (Euclidian_Distance.GetValueOfEuclidianDistance(StartingsCentroids[$"Кластер - {j + 1}"], StartingsCentroids[$"Кластер - {StartingsCentroids.Count}"]) <
                                    CoefficientTaboo * Math.Min(clustered_Data.Max(a => a.X) - clustered_Data.Min(a => a.X), clustered_Data.Max(a => a.Y) - clustered_Data.Min(a => a.Y)))
                {
                    StartingsCentroids[$"Кластер - {StartingsCentroids.Count}"] = new Centroid(randomCentroid.Next((int)clustered_Data.Min(a => a.X), (int)clustered_Data.Max(a => a.X) + 1),
                        randomCentroid.Next((int)clustered_Data.Min(a => a.Y), (int)clustered_Data.Max(a => a.Y) + 1));
                }
            }
        }




    }
}
