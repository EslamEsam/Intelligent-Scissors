using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace IntelligentScissors
{
    class SPtest
    {

    }
}
//        public Dictionary<string, KeyValuePair<string, double>> calculateShortestPath(Point Anchor, Point Free, Dictionary<string,
//            List<edge_weight>> graph , int width , int height)
//        {
//            //string[,] parent = new string[d.GetLength(0), d.GetLength(1)];
//            //double[,] distance = new double[d.GetLength(0), d.GetLength(1)];
//            //bool[,] reachedShortestPath = new bool[width , height];
//            Dictionary<string,bool> reachedShortestPath = new Dictionary<string,bool>();
//            Dictionary<string, KeyValuePair<string, double>> shortest_path = new Dictionary<string, KeyValuePair<string, double>>(); 
//            PriorityQueue priorityQueue = new PriorityQueue();

//            string start = Anchor.X.ToString() + "," + Anchor.Y.ToString();
//            if (graph.ContainsKey(start))
//            {
//                edge_weight root;
//                root.edge = start;
//                root.weight = 0;

//                shortest_path.Add(start, new KeyValuePair<string, double>("null", 0));
//                //distance[Anchor.X, Anchor.Y] = 0;
//                //parent[Anchor.X, Anchor.Y] = "null";
//                priorityQueue.Enqueue(root);

//                while (priorityQueue.Count() != 0)
//                {
//                    edge_weight vertex = priorityQueue.Dequeue();
//                    //string si1 = vertex.edge.Substring(0, vertex.edge.IndexOf(","));
//                    //int i1 = int.Parse(si1);
//                    //string sj1 = vertex.edge.Substring(vertex.edge.IndexOf(",") + 1);
//                    //int j1 = int.Parse(sj1);
//                    reachedShortestPath.Add(vertex.edge, true);

//                    foreach (var item in graph[vertex.edge])
//                    {
//                        //string si2 = item.edge.Substring(0, item.edge.IndexOf(","));
//                        //int i2 = int.Parse(si2);
//                        //string sj2 = item.edge.Substring(item.edge.IndexOf(",") + 1);
//                        //int j2 = int.Parse(sj2);

//                        if (!reachedShortestPath.ContainsKey(item.edge))
//                        {

//                            if (item.edge != root.edge && !priorityQueue.ContainsItem(item.edge))
//                            {
//                                edge_weight edge;
//                                edge.edge = item.edge;
//                                edge.weight = double.PositiveInfinity;
//                                //distance[i2, j2] = double.PositiveInfinity;
//                                priorityQueue.Enqueue(edge);
//                            }

//                            if (item.weight == double.PositiveInfinity && !shortest_path.ContainsKey(item.edge))
//                            {
//                                shortest_path.Add(item.edge, new KeyValuePair<string, double>(vertex.edge, item.weight));
//                            }

//                            else
//                            {
//                                //double t = shortest_path[vertex.edge].Value;
//                                double newDistance = shortest_path[vertex.edge].Value + item.weight;
//                                if(!shortest_path.ContainsKey(item.edge))
//                                {
//                                    shortest_path.Add(item.edge, new KeyValuePair<string, double>(vertex.edge, newDistance));
//                                    priorityQueue.Update(item.edge, newDistance);

//                                }
//                                else if ((newDistance < shortest_path[item.edge].Value))
//                                {

//                                    priorityQueue.Update(item.edge, newDistance);
//                                    shortest_path.Remove(item.edge);
//                                    shortest_path.Add(item.edge, new KeyValuePair<string, double>(vertex.edge, newDistance));

//                                }
//                            }
//                            //if (parent[i2, j2] == null)
//                            //{
//                            //    parent[i2, j2] = vertex.edge;
//                            //}
//                        }

//                    }
//                    string free_point = Free.X.ToString() + "," + Free.Y.ToString();
//                    if (vertex.edge == free_point)
//                        break;

//                }

//            }


//            return shortest_path;

//        }

//    }
//}
