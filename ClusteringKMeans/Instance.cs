using System;
using System.Collections.Generic;

namespace ClusteringKMeans
{
    public class Instance
    {
        public List<Node> ListNodeRaw {get; set;}

        public Instance(){
            //Add nodes to the list of nodes in the Instance
            ListNodeRaw = new();
    
            Node n1 = new(0,65,220);
            Node n2 = new(1,73,160);
            Node n3 = new(2,59,110);
            Node n4 = new(3,61,120);
            Node n5 = new(4,75,150);
            Node n6 = new(5,67,240);
            Node n7 = new(6,68,230);
            Node n8 = new(7,70,220);
            Node n9 = new(8,62,130);
            Node n10 = new(9,66,210);
            Node n11 = new(10,77,190);
            Node n12 = new(11,75,180);
            Node n13 = new(12,74,170);
            Node n14 = new(13,70,210);
            Node n15 = new(14,61,110);
            Node n16 = new(15,58,100);
            Node n17 = new(16,66,230);
            Node n18 = new(17,59,120);
            Node n19 = new(18,68,210);
            Node n20 = new(19,61,130);

            ListNodeRaw.Add(n1);
            ListNodeRaw.Add(n2);
            ListNodeRaw.Add(n3);
            ListNodeRaw.Add(n4);
            ListNodeRaw.Add(n5); 
            ListNodeRaw.Add(n6); 
            ListNodeRaw.Add(n7); 
            ListNodeRaw.Add(n8); 
            ListNodeRaw.Add(n9); 
            ListNodeRaw.Add(n10); 
            ListNodeRaw.Add(n11); 
            ListNodeRaw.Add(n12); 
            ListNodeRaw.Add(n13); 
            ListNodeRaw.Add(n14); 
            ListNodeRaw.Add(n15); 
            ListNodeRaw.Add(n16); 
            ListNodeRaw.Add(n17);
            ListNodeRaw.Add(n18); 
            ListNodeRaw.Add(n19); 
            ListNodeRaw.Add(n20); 
    
        }
  
        public void Print()
        { 
            foreach(var n in ListNodeRaw){
                Console.WriteLine("Id:" + n.Id + " x:" + n.X + " y:" + n.Y); 
            }   
        }
    }
}