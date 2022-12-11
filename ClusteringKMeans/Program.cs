using System;
using System.Collections.Generic;

namespace ClusteringKMeans
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            Instance I = new();
            I.Print();
            ClusteringKMeansAlgorithm CKMA = new();
            CKMA.DoClustering();
            CKMA.PrintDataAfterClustering();
        }
    }
}