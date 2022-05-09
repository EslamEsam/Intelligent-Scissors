using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace IntelligentScissors
{
    class ShortestPath
    {
        public string[,] calculateShortestPath(Point p, Dictionary<string, List<edge_weight>> graph, ref double[,] d)
        {
            string[,] parent = new string[d.GetLength(0), d.GetLength(1)];
            double[,] distance = new double[d.GetLength(0), d.GetLength(1)];
            bool[,] reachedShortestPath = new bool[d.GetLength(0), d.GetLength(1)];

            PriorityQueue priorityQueue = new PriorityQueue();

            string start = p.X.ToString() + "," + p.Y.ToString();
            if (graph.ContainsKey(start))
            {
                edge_weight root;
                root.edge = start;
                root.weight = 0;
                distance[p.X, p.Y] = 0;
                parent[p.X, p.Y] = "null";
                priorityQueue.Enqueue(root);
                //foreach (var vertix in graph)
                //{
                //    if (vertix.Key != root.edge)
                //    {
                //        edge_weight edge;
                //        edge.edge = vertix.Key;
                //        edge.weight = double.PositiveInfinity;
                //        string si = edge.edge.Substring(0, edge.edge.IndexOf(","));
                //        int i = int.Parse(si);
                //        string sj = edge.edge.Substring(edge.edge.IndexOf(",") + 1);
                //        int j = int.Parse(sj);
                //        distance[i, j] = double.PositiveInfinity;
                //        priorityQueue.Enqueue(edge);
                //    }
                //}

                while (priorityQueue.Count() != 0)
                {
                    edge_weight vertex = priorityQueue.Dequeue();
                    string si1 = vertex.edge.Substring(0, vertex.edge.IndexOf(","));
                    int i1 = int.Parse(si1);
                    string sj1 = vertex.edge.Substring(vertex.edge.IndexOf(",") + 1);
                    int j1 = int.Parse(sj1);
                    reachedShortestPath[i1, j1] = true;
                    foreach (var item in graph[vertex.edge])
                    {
                        string si2 = item.edge.Substring(0, item.edge.IndexOf(","));
                        int i2 = int.Parse(si2);
                        string sj2 = item.edge.Substring(item.edge.IndexOf(",") + 1);
                        int j2 = int.Parse(sj2);

                        if (!reachedShortestPath[i2, j2])
                        {

                            if (item.edge != root.edge && !priorityQueue.ContainsItem(item.edge))
                            {
                                edge_weight edge;
                                edge.edge = item.edge;
                                edge.weight = double.PositiveInfinity;
                                distance[i2, j2] = double.PositiveInfinity;
                                priorityQueue.Enqueue(edge);
                            }

                            if (item.weight == double.PositiveInfinity && parent[i2, j2] == null)
                                parent[i2, j2] = vertex.edge;


                            else
                            {
                                double newDistance = distance[i1, j1] + item.weight;
                                if ((newDistance < distance[i2, j2]))
                                {

                                    priorityQueue.Update(item.edge, newDistance);
                                    distance[i2, j2] = newDistance;
                                    parent[i2, j2] = vertex.edge;
                                }
                            }
                            if (parent[i2, j2] == null)
                            {
                                parent[i2, j2] = vertex.edge;
                            }
                        }

                    }

                }
                //foreach (var item in graph)
                //{
                //    string test = item.Key;
                //    string si2 = test.Substring(0, test.IndexOf(","));
                //    int i2 = int.Parse(si2);
                //    string sj2 = test.Substring(test.IndexOf(",") + 1);
                //    int j2 = int.Parse(sj2);
                //    if (reachedShortestPath[i2, j2] && parent[i2, j2] == null)
                //    {
                //        Console.WriteLine("hafdlshdfljhsdflksdhflksdhflksdhflksdhfklsdhflksdh");
                //        Console.WriteLine("i = " + i2);
                //        Console.WriteLine("j = " + j2);
                //    }
                //}
            }

            d = distance;
            return parent;

        }

    }
}
