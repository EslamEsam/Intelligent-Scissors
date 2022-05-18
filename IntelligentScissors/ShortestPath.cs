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
            , nodes[,] graph, int width, int height, Bitmap global_img)
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

            if (graph[AnchorPoint.Y, AnchorPoint.X].weight_right == graph[AnchorPoint.Y, AnchorPoint.X].weight_down
               && graph[AnchorPoint.Y, AnchorPoint.X].weight_down == graph[AnchorPoint.Y, AnchorPoint.X].weight_up
               && graph[AnchorPoint.Y, AnchorPoint.X].weight_up == graph[AnchorPoint.Y, AnchorPoint.X].weight_left)
            {
                if (graph[FreePoint.Y, FreePoint.X].weight_right == graph[FreePoint.Y, FreePoint.X].weight_down
            && graph[FreePoint.Y, FreePoint.X].weight_down == graph[FreePoint.Y, FreePoint.X].weight_up
            && graph[FreePoint.Y, FreePoint.X].weight_up == graph[FreePoint.Y, FreePoint.X].weight_left)

                    return Handle_calculateShortestPath(AnchorPoint, FreePoint, graph, width, height, global_img);
            }

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
                            AddToPriority(neighbor_right, root, ref priorityQueue, vertex, ref shortest_path , graph);
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
                            AddToPriority(neighbor_down, root, ref priorityQueue, vertex, ref shortest_path , graph);
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
                            AddToPriority(neighbor_left, root, ref priorityQueue, vertex, ref shortest_path , graph);
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
                            AddToPriority(neighbor_up, root, ref priorityQueue, vertex, ref shortest_path , graph);
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
            , edge_weight vertex, ref Dictionary<string, KeyValuePair<string, double>> shortest_path, nodes[,] graph)
        {
            string si1 = item.edge.Substring(0, item.edge.IndexOf(","));
            string sj1 = item.edge.Substring(item.edge.IndexOf(",") + 1);
            int i_item = int.Parse(si1);
            int j_item = int.Parse(sj1);

            if (graph[j_item, i_item].weight_right != graph[j_item, i_item].weight_left ||
                   graph[j_item, i_item].weight_left != graph[j_item, i_item].weight_up ||
                   graph[j_item, i_item].weight_up != graph[j_item, i_item].weight_down)
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

        public Dictionary<string, KeyValuePair<string, double>> Handle_calculateShortestPath( Point AnchorPoint, Point FreePoint
                    , nodes[,] graph, int width, int height, Bitmap global_img)
        {
            Dictionary<string, bool> reachedShortestPath = new Dictionary<string, bool>();
            Dictionary<string, KeyValuePair<string, double>> shortest_path = new Dictionary<string, KeyValuePair<string, double>>();
            PriorityQueue priorityQueue = new PriorityQueue();
            Graphics graphics = Graphics.FromImage((Image)global_img);
            Pen redPen = new Pen(Color.Red, 2);

            if (graph[AnchorPoint.Y, AnchorPoint.X].weight_right == graph[AnchorPoint.Y, AnchorPoint.X].weight_down
                && graph[AnchorPoint.Y, AnchorPoint.X].weight_down == graph[AnchorPoint.Y, AnchorPoint.X].weight_up
                && graph[AnchorPoint.Y, AnchorPoint.X].weight_up == graph[AnchorPoint.Y, AnchorPoint.X].weight_left)
            {


                bool stopper = false;
                while (true)
                {
                    int i = AnchorPoint.Y, j = AnchorPoint.X;

                    while (i != height - 1)
                    {

                        if (graph[i, j].weight_down != graph[i + 1, j].weight_down)
                        {
                            Point tempPoint = new Point(j, i + 2);
                            graphics.DrawLine(redPen, AnchorPoint, tempPoint);
                            AnchorPoint.Y = i + 2;
                            stopper = true;
                            break;
                        }
                        else
                            i++;
                    }
                    if (stopper == true)
                        break;

                    while (j != width - 1)
                    {
                        //if (graph[i, j].weight_right != graph[i + 1, j].weight_down)
                        //{
                        //    Point tempPoint = new Point(j, i + 2);
                        //    graphics.DrawLine(redPen, AnchorPoint, tempPoint);
                        //    AnchorPoint.Y = i + 2;
                        //    stopper = true;
                        //    break;
                        //}
                        //else
                        //    i++;

                    }
                    if (stopper == true)
                        break;

                    while (j != 0)
                    {


                    }
                    if (stopper == true)
                        break;

                    while (i != 0)
                    {

                    }
                    if (stopper == true)
                        break;


                }
            }
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

                if (j_vertex == FreePoint.X)
                {
                    return shortest_path;
                }
                if (j_vertex >= height || i_vertex >= width)
                    continue;
                neighbors = graph[j_vertex, i_vertex];

                if (j_vertex != width - 1)
                {
                    neighbor_right.edge = (i_vertex).ToString() + "," + (j_vertex + 1).ToString();
                    neighbor_right.weight = neighbors.weight_right;
                    if (neighbors.weight_right != 0)
                    {
                        if (!reachedShortestPath.ContainsKey(neighbor_right.edge))
                        {
                            AddToPriority(neighbor_right, root, ref priorityQueue, vertex, ref shortest_path, graph);
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
                            AddToPriority(neighbor_down, root, ref priorityQueue, vertex, ref shortest_path, graph);
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
                            AddToPriority(neighbor_left, root, ref priorityQueue, vertex, ref shortest_path, graph);
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
                            AddToPriority(neighbor_up, root, ref priorityQueue, vertex, ref shortest_path, graph);
                        }
                    }
                }

                string free_point = FreePoint.X.ToString() + "," + FreePoint.Y.ToString();
                //Console.WriteLine("j_vertex : " + j_vertex + " " + "i_vertex : " + i_vertex);
                //Console.WriteLine("FreePoint X  : " + FreePoint.X + " " + "FreePoint Y : " + FreePoint.Y + "\n");
               
                    if (j_vertex < FreePoint.X + 4 && j_vertex > FreePoint.X - 4)
                    {
                        Console.WriteLine("Entered");
                        Point tempPoint = new Point(j_vertex, i_vertex);
                        //graphics.DrawLine(redPen, AnchorPoint, tempPoint);
                        //shortest_path.Clear();
                        //shortest_path = Handle_calculateShortestPath(AnchorPoint, tempPoint, graph, width, height, global_img);
                        graphics.DrawLine(redPen, tempPoint, FreePoint);
                        FreePoint = tempPoint;
                        break;

                    }
                
                if (vertex.edge == free_point)
                    break;
                //Console.WriteLine(shortest_path[vertex.edge].Key);

            }
            return shortest_path;

        }




    }

}



/*
 * 
 * public void AddToPriority(edge_weight item, edge_weight root, ref PriorityQueue priorityQueue
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
            
        }*/




