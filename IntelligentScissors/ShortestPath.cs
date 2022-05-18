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
        public Dictionary<string, KeyValuePair<string, double>> calculateShortestPath(Point AnchorPoint, Point FreePoint
            , nodes[,] graph, int width, int height)
        {
            Dictionary<string, bool> reachedShortestPath = new Dictionary<string, bool>();
            Dictionary<string, KeyValuePair<string, double>> shortest_path = new Dictionary<string, KeyValuePair<string, double>>();
            PriorityQueue priorityQueue = new PriorityQueue();
            string start = AnchorPoint.X.ToString() + "," + AnchorPoint.Y.ToString();
            edge_weight root;
            root.edge = start;
            root.weight = 0;
            priorityQueue.Enqueue(root);
            shortest_path.Add(root.edge, new KeyValuePair<string, double>("null", 0));
            nodes neighbors;
            edge_weight neighbor_right;
            edge_weight neighbor_down;
            edge_weight neighbor_left;
            edge_weight neighbor_up;


            while (priorityQueue.Count() != 0)
            {
                edge_weight vertex = priorityQueue.Dequeue();
                reachedShortestPath.Add(vertex.edge, true);

                string si1 = vertex.edge.Substring(0, vertex.edge.IndexOf(","));
                string sj1 = vertex.edge.Substring(vertex.edge.IndexOf(",") + 1);
                int i_vertex = int.Parse(si1);
                int j_vertex = int.Parse(sj1);

                if (j_vertex >= height || i_vertex >= width)
                    continue;
                neighbors = graph[j_vertex , i_vertex];

                if (j_vertex != width - 1)
                {
                    neighbor_right.edge = (i_vertex).ToString() + "," + (j_vertex + 1).ToString();
                    neighbor_right.weight = neighbors.weight_right;
                    if (neighbors.weight_right != 0)
                    {
                        if (!reachedShortestPath.ContainsKey(neighbor_right.edge))
                        {
                            AddToPriority(neighbor_right, root, ref priorityQueue, vertex, ref shortest_path);
                        }
                    }

                }

                if (i_vertex != height - 1)
                {
                    neighbor_down.edge = (i_vertex + 1).ToString() + "," + (j_vertex).ToString();
                    neighbor_down.weight = neighbors.weight_down;
                    if (neighbors.weight_down != 0)
                    {

                        if (!reachedShortestPath.ContainsKey(neighbor_down.edge))
                        {
                            AddToPriority(neighbor_down, root, ref priorityQueue, vertex, ref shortest_path);
                        }
                    }
                }

                if (j_vertex != 0)
                {
                    neighbor_left.edge = (i_vertex).ToString() + "," + (j_vertex - 1).ToString(); ;
                    neighbor_left.weight = neighbors.weight_left;
                    if (neighbors.weight_left != 0)
                    {

                        if (!reachedShortestPath.ContainsKey(neighbor_left.edge))
                        {
                            AddToPriority(neighbor_left, root, ref priorityQueue, vertex, ref shortest_path);
                        }
                    }

                }

                if (i_vertex != 0)
                {
                    neighbor_up.edge = (i_vertex - 1).ToString() + "," + (j_vertex).ToString();
                    neighbor_up.weight = neighbors.weight_up;
                    if (neighbors.weight_up != 0)
                    {

                        if (!reachedShortestPath.ContainsKey(neighbor_up.edge))
                        {
                            AddToPriority(neighbor_up, root, ref priorityQueue, vertex, ref shortest_path);
                        }
                    }
                }
                string free_point = FreePoint.X.ToString() + "," + FreePoint.Y.ToString();
                if (vertex.edge == free_point)
                    break;

            }
            return shortest_path;

        }

        public void AddToPriority(edge_weight item, edge_weight root, ref PriorityQueue priorityQueue
            , edge_weight vertex, ref Dictionary<string, KeyValuePair<string, double>> shortest_path)
        {

            if (item.edge != root.edge && !priorityQueue.ContainsItem(item.edge))
            {
                edge_weight edge;
                edge.edge = item.edge;
                edge.weight = double.PositiveInfinity;
                priorityQueue.Enqueue(edge);
            }
                double newDistance = shortest_path[vertex.edge].Value + item.weight;
                if (!shortest_path.ContainsKey(item.edge))
                {
                    shortest_path.Add(item.edge, new KeyValuePair<string, double>(vertex.edge, newDistance));
                    priorityQueue.Update(item.edge, newDistance);

                }
                else if ((newDistance < shortest_path[item.edge].Value))
                {

                    priorityQueue.Update(item.edge, newDistance);
                    shortest_path.Remove(item.edge);
                    shortest_path.Add(item.edge, new KeyValuePair<string, double>(vertex.edge, newDistance));

                }
            
        }

    }

}



/*
 * 
            Dictionary<int, bool> reachedShortestPath = new Dictionary<int, bool>();
            Dictionary<int, KeyValuePair<int, double>> shortest_path = new Dictionary<int, KeyValuePair<int, double>>();
            PriorityQueue priorityQueue = new PriorityQueue();
            nodes neighbors = graph[AnchorPoint.X, AnchorPoint.Y];
            edge_weight root;
            root.edge = AnchorPoint.X * width + AnchorPoint.Y;
            root.weight = 0;
            priorityQueue.Enqueue(root);
            shortest_path.Add(root.edge, new KeyValuePair<int, double>(-1, 0));


            while (priorityQueue.Count() != 0)
            {
                edge_weight vertex = priorityQueue.Dequeue();
                reachedShortestPath.Add(vertex.edge, true);

                int i = vertex.edge / width;
                int j = vertex.edge % width;

                edge_weight neighbor_right;
                edge_weight neighbor_down;
                edge_weight neighbor_left;
                edge_weight neighbor_up;

                if (j != width - 1)
                {
                    neighbor_right.edge = vertex.edge + 1;
                    neighbor_right.weight = neighbors.weight_right;
                    if(!reachedShortestPath.ContainsKey(neighbor_right.edge))
                    {
                        AddToPriority(neighbor_right, root, ref priorityQueue, vertex, ref shortest_path);
                    }

                }

                if (i != height - 1)
                {
                    neighbor_down.edge = vertex.edge + width;
                    neighbor_down.weight = neighbors.weight_down;
                    if (!reachedShortestPath.ContainsKey(neighbor_down.edge))
                    {
                        AddToPriority(neighbor_down, root, ref priorityQueue, vertex, ref shortest_path);
                    }
                }

                if (j != 0)
                {
                    neighbor_left.edge = vertex.edge - 1;
                    neighbor_left.weight = neighbors.weight_left;
                    if (!reachedShortestPath.ContainsKey(neighbor_left.edge))
                    {
                        AddToPriority(neighbor_left, root, ref priorityQueue, vertex, ref shortest_path);
                    }

                }

                if (i != 0)
                {
                    neighbor_up.edge = vertex.edge - width;
                    neighbor_up.weight = neighbors.weight_up;
                    if (!reachedShortestPath.ContainsKey(neighbor_up.edge))
                    {
                        AddToPriority(neighbor_up, root, ref priorityQueue, vertex, ref shortest_path);
                    }
                }
                int free_point = FreePoint.X * width + FreePoint.Y;
                if (vertex.edge == free_point)
                    break;

            }
            return shortest_path;

        }

        public void AddToPriority(edge_weight item, edge_weight root, ref PriorityQueue priorityQueue
            , edge_weight vertex, ref Dictionary<int, KeyValuePair<int, double>> shortest_path)
        {

            if (item.edge != root.edge && !priorityQueue.ContainsItem(item.edge))
            {
                edge_weight edge;
                edge.edge = item.edge;
                edge.weight = double.PositiveInfinity;
                priorityQueue.Enqueue(edge);
            }

            //if (item.weight == double.PositiveInfinity && !shortest_path.ContainsKey(item.edge))
            //{
            //    shortest_path.Add(item.edge, new KeyValuePair<int, double>(vertex.edge, item.weight));
            //}

            //else
            //{
                //double t = shortest_path[vertex.edge].Value;
                double newDistance = shortest_path[vertex.edge].Value + item.weight;
                if (!shortest_path.ContainsKey(item.edge))
                {
                    shortest_path.Add(item.edge, new KeyValuePair<int, double>(vertex.edge, newDistance));
                    priorityQueue.Update(item.edge, newDistance);

                }
                else if ((newDistance < shortest_path[item.edge].Value))
                {

                    priorityQueue.Update(item.edge, newDistance);
                    shortest_path.Remove(item.edge);
                    shortest_path.Add(item.edge, new KeyValuePair<int, double>(vertex.edge, newDistance));

                }
            //}
        }

    }
*/




