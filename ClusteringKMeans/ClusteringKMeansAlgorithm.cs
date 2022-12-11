using System;
using System.Collections.Generic;
using System.Linq;

namespace ClusteringKMeans
{
    public class ClusteringKMeansAlgorithm
    {
        public int NClusters { get; set; }
        public Instance Data { get; set; }
        public List<Node> ListNodesNormalized { get; set; }
        private List<Tuple<int,double, double>> ListKMeans { get; set; }
        
        public ClusteringKMeansAlgorithm()
        {
            this.NClusters = 3;
            this.Data = new();
            this.ListNodesNormalized = new();
        }

        public void DoClustering()
        {
            //Normalize data (coordinates x & y) for each node
            ListNodesNormalized = NormalizeData(Data.ListNodeRaw);
            
            //Assign initial clusters at random
            InitClustering();
            
            bool canUpdateMeans = true, canUpdateCluster = true;
            int iteration = 0, maxCountIteration = ListNodesNormalized.Count * 10;
            
            while (canUpdateMeans && canUpdateCluster && iteration < maxCountIteration)
            {
                iteration++;
                canUpdateMeans = CalculateKMeans();
                canUpdateCluster = UpdateClustering();
            }
        }   //END CLUSTERING ALGORITHM
        
        public List<Node> NormalizeData(List<Node> listNodesRaw)
        {
            List<Node> listNormalizedNodes = new();
            //Gaussian Normalization
            var meanX = listNodesRaw.Select(n => n.X).DefaultIfEmpty(0).Average();
            var sdX = listNodesRaw.Select(n => n.X).DefaultIfEmpty(0).Average(z => z * z) -
                      Math.Pow(listNodesRaw.Select(n => n.X).DefaultIfEmpty(0).Average(), 2);

            var meanY = listNodesRaw.Select(n => n.Y).DefaultIfEmpty(0).Average();
            var sdY = listNodesRaw.Select(n => n.Y).DefaultIfEmpty(0).Average(z => z * z) -
                      Math.Pow(listNodesRaw.Select(n => n.Y).DefaultIfEmpty(0).Average(), 2);

            int i = 0;
            foreach (var node in listNodesRaw)
            {
                Node normalizedNode = new(i, (node.X - meanX) / sdX, (node.Y - meanY) / sdY);
                listNormalizedNodes.Add(normalizedNode);
                i++;
            }

            return listNormalizedNodes;
        }
        public void InitClustering()
        {
            //Asigna a cada nodo un cluster aleatorio
            var random = new Random(0);
            ListNodesNormalized[0].ClusterId = 0;
            ListNodesNormalized[1].ClusterId = 1;
            ListNodesNormalized[2].ClusterId = 2;
            foreach (var n in ListNodesNormalized)
            {
                if (n.Id==0||n.Id==1||n.Id==2)
                {
                    continue;
                }
                int indexCluster = random.Next(NClusters);
                n.ClusterId = indexCluster;
            }
        }
        public bool CalculateKMeans()
        {
            //Update means only if all clusters have nodes
            bool canUpdate = CanUpdate();
            if (!canUpdate)
            {
                return false;
            }

            ListKMeans = new();
            
            for (int i = 0; i < NClusters; i++)
            {
                List<Node>listNodesInCluster = ListNodesNormalized.FindAll(n => n.ClusterId.Equals(i));
                
                //Mean of x values
                var xmean = listNodesInCluster.Select(n => n.X).DefaultIfEmpty(0).Average();
                //Mean of y values
                var ymean = listNodesInCluster.Select(n => n.Y).DefaultIfEmpty(0).Average();
                ListKMeans.Add(Tuple.Create(i,xmean,ymean));
            }
            
            return true;
        }
        private bool CanUpdate()
        {
            //Check if all clusters have elements
            for (int i = 0; i < NClusters; i++)
            {
                List<Node>listNodesInCluster = ListNodesNormalized.FindAll(n => n.ClusterId.Equals(i));
                
                if (!listNodesInCluster.Any()) //Check If there is no node in the Cluster
                {
                    return false;
                }
            }
            return true;
        }
        public bool UpdateClustering()
        {
            bool changedCluster = false;
            List<Node> listNodesBeforeUpdating = new();
            List<Node> listNodesNormalizedAux = new List<Node>(ListNodesNormalized);
            for (int i = 0; i < NClusters; i++)
            {
                List<Node>listNodesInCluster = listNodesNormalizedAux.FindAll(n => n.ClusterId.Equals(i));
                foreach (var n in listNodesInCluster)
                {
                    List<double> distances = new();
                    for (int j = 0; j < NClusters; j++)
                    {
                        // compute distances from curr tuple to all k means
                        double euclideanDistance = Math.Sqrt(Math.Pow(n.X - ListKMeans[j].Item2, 2) + Math.Pow(n.Y - ListKMeans[j].Item3, 2));
                        distances.Add(euclideanDistance);
                    }
                    
                    Node nodeBeforeChange = new Node(n.Id, n.X, n.Y);
                    nodeBeforeChange.ClusterId = n.ClusterId;
                    listNodesBeforeUpdating.Add(nodeBeforeChange);
                    
                    int newClusterId = distances.IndexOf(distances.Min());
                    if (newClusterId != n.ClusterId)
                    {
                        //Updating process
                        changedCluster = true;
                        int indexNode = ListNodesNormalized.IndexOf(n);
                        ListNodesNormalized[indexNode].ClusterId = newClusterId; //Set the new cluster to the node
                    }
                    listNodesNormalizedAux.Remove(n);
                }
            }

            if (changedCluster == false)
            {
                return false;
            }

            if (!CanUpdate())
            {
                ListNodesNormalized = listNodesBeforeUpdating;
                return false;
            }
            
            return true;
        }

        public void PrintDataAfterClustering()
        {
            Console.WriteLine("Results from Clustering Method:\n");
            Console.WriteLine("Raw data by cluster:\n");
            for (int k = 0; k < NClusters; ++k)
            {
                Console.WriteLine("===================");
                foreach (var n in ListNodesNormalized)
                {
                    if (n.ClusterId==k)
                    {
                        Console.WriteLine(n.Id);
                    }
                }
            }
        }
    }
}

//https://www.google.com/url?sa=t&rct=j&q=&esrc=s&source=web&cd=&cad=rja&uact=8&ved=2ahUKEwj0xMHjn977AhX9nWoFHfuhC3wQFnoECBAQAQ&url=https%3A%2F%2Fvisualstudiomagazine.com%2Farticles%2F2013%2F12%2F01%2Fk-means-data-clustering-using-c.aspx%3Fm%3D2&usg=AOvVaw2a2wRbaN6ypyfSXLIYZS5U
