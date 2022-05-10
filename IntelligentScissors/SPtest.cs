using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace IntelligentScissors
{
    class SPtest
    { }
        //        public Dictionary<string, KeyValuePair<string, double>> calculateShortestPath(Point Anchor, Point Free,
        //            Dictionary<string,List<edge_weight>> graph)
        //        {
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
        //                priorityQueue.Enqueue(root);

        //                while (priorityQueue.Count() != 0)
        //                {
        //                    edge_weight vertex = priorityQueue.Dequeue();
        //                    reachedShortestPath.Add(vertex.edge, true);

        //                    foreach (var item in graph[vertex.edge])
        //                    {

        //                        if (!reachedShortestPath.ContainsKey(item.edge))
        //                        {

        //                            if (item.edge != root.edge && !priorityQueue.ContainsItem(item.edge))
        //                            {
        //                                edge_weight edge;
        //                                edge.edge = item.edge;
        //                                edge.weight = double.PositiveInfinity;
        //                                priorityQueue.Enqueue(edge);
        //                            }

        //                            if (item.weight == double.PositiveInfinity && !shortest_path.ContainsKey(item.edge))
        //                            {
        //                                shortest_path.Add(item.edge, new KeyValuePair<string, double>(vertex.edge, item.weight));
        //                            }

        //                            else
        //                            {
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
    }
