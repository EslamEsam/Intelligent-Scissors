using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace IntelligentScissors
{
    internal class ShortestPath
    {
        
        public string[,] calculateShortestPath(Point p , Dictionary<string,List<edge_weight>> graph , ref double[,] d)
        {
            string[,] parent = new string[d.GetLength(0), d.GetLength(1)];
            double[,] distance = new double[d.GetLength(0), d.GetLength(1)];
            bool[,] reachedShortestPath = new bool[d.GetLength(0), d.GetLength(1)];
            PriorityQueue priorityQueue = new PriorityQueue();
            string key = p.X.ToString() + "," + p.Y.ToString();
            Console.WriteLine("before adding to queue");
            if (graph.ContainsKey(key))
            {
                edge_weight root;
                root.edge = key;
                root.weight = 0;
                distance[p.X, p.Y] = 0;
                parent[p.X, p.Y] = "null";
                reachedShortestPath[p.X, p.Y] = true;
                foreach (var item in graph[key])
                {
                    priorityQueue.Enqueue(item);
                    string si = item.edge.Substring(0, item.edge.IndexOf(","));
                    int i = int.Parse(si);
                    string sj = item.edge.Substring(item.edge.IndexOf(",") + 1);
                    int j = int.Parse(sj);
                    Console.WriteLine("Value : " + item.edge);
                    Console.WriteLine("i = " + i + " j = " + j);
                    distance[i, j] = double.MaxValue;
                    parent[i, j] = root.edge;

                }
                while (priorityQueue.Count() != 0)
                {
                    edge_weight vertex = priorityQueue.Dequeue();
                    string si1 = vertex.edge.Substring(0, vertex.edge.IndexOf(","));
                    int i1 = int.Parse(si1);
                    string sj1 = vertex.edge.Substring(vertex.edge.IndexOf(",") + 1);
                    int j1 = int.Parse(sj1);
                    distance[i1,j1] = vertex.weight;
                    reachedShortestPath[i1, j1] = true;
                    foreach(var item in graph[vertex.edge])
                    {
                        string si2 = item.edge.Substring(0, item.edge.IndexOf(","));
                        int i2 = int.Parse(si2);
                        string sj2 = item.edge.Substring(item.edge.IndexOf(",") + 1);
                        int j2 = int.Parse(sj2);
                        if (!priorityQueue.ContainsItem(item))
                        {
                            distance[i2, j2] = double.MaxValue;
                        }
                       
                        
                        if (item.weight == double.MaxValue)
                        {
                            parent[i2, j2] = vertex.edge;
                        }
                        double newDistance = distance[i1, j1] + item.weight;
                        
                        if ((reachedShortestPath[i2,j2] == false)&&(newDistance < distance[i2,j2]))
                        {
                            if (!priorityQueue.ContainsItem(item))
                            {
                                priorityQueue.Enqueue(item);

                            }
                            else
                            {
                                priorityQueue.Update(item.edge,newDistance);
                            }
                            distance[i2, j2] = newDistance;
                            parent[i2, j2] = vertex.edge;
                            

                        }
                    }
                }
                
            }
            d = distance;
            return parent;
           
        }

       
    }
}
