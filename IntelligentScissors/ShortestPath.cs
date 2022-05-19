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
        public Dictionary<string, KeyValuePair<string, double>> calculateShortestPath(ref Point AnchorPoint, ref Point FreePoint
            , nodes[,] graph, int width, int height, Bitmap global_img)
        {
            Dictionary<string, bool> reachedShortestPath = new Dictionary<string, bool>();
            Dictionary<string, KeyValuePair<string, double>> shortest_path = new Dictionary<string, KeyValuePair<string, double>>();
            PriorityQueue priorityQueue = new PriorityQueue();

            if (graph[AnchorPoint.Y, AnchorPoint.X].weight_right == graph[AnchorPoint.Y, AnchorPoint.X].weight_down
               && graph[AnchorPoint.Y, AnchorPoint.X].weight_down == graph[AnchorPoint.Y, AnchorPoint.X].weight_up
               && graph[AnchorPoint.Y, AnchorPoint.X].weight_up == graph[AnchorPoint.Y, AnchorPoint.X].weight_left
               && graph[FreePoint.Y, FreePoint.X].weight_right == graph[FreePoint.Y, FreePoint.X].weight_down
                && graph[FreePoint.Y, FreePoint.X].weight_down == graph[FreePoint.Y, FreePoint.X].weight_up
                && graph[FreePoint.Y, FreePoint.X].weight_up == graph[FreePoint.Y, FreePoint.X].weight_left)
            {
                Handle_calculateShortestPath(ref AnchorPoint, ref FreePoint, graph, width, height, global_img); //O(N)
            }
            string start = AnchorPoint.X.ToString() + "," + AnchorPoint.Y.ToString();
            edge_weight root;
            root.edge = start;
            root.weight = 0;
            priorityQueue.Enqueue(root); // O(N)
            shortest_path.Add(root.edge, new KeyValuePair<string, double>("null", 0)); // O(1)
            nodes neighbors;
            edge_weight neighbor_right;
            edge_weight neighbor_down;
            edge_weight neighbor_left;
            edge_weight neighbor_up;


            while (priorityQueue.Count() != 0) // O(V)
            {
                edge_weight vertex = priorityQueue.Dequeue();
                reachedShortestPath.Add(vertex.edge, true);

                string si1 = vertex.edge.Substring(0, vertex.edge.IndexOf(","));
                string sj1 = vertex.edge.Substring(vertex.edge.IndexOf(",") + 1);
                int i_vertex = int.Parse(si1);
                int j_vertex = int.Parse(sj1);

                // checks if it's within graph boundries
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
                            AddToPriority(neighbor_right, root, ref priorityQueue, vertex,
                                ref shortest_path, graph, height, width);
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
                            AddToPriority(neighbor_down, root, ref priorityQueue, vertex,
                                ref shortest_path, graph, height, width);
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
                            AddToPriority(neighbor_left, root, ref priorityQueue, vertex,
                                ref shortest_path, graph, height, width);
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
                            AddToPriority(neighbor_up, root, ref priorityQueue, vertex,
                                ref shortest_path, graph, height, width);
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
            , edge_weight vertex, ref Dictionary<string, KeyValuePair<string, double>> shortest_path, nodes[,] graph
            , int height, int width) // O(V)
        {
            string si1 = item.edge.Substring(0, item.edge.IndexOf(","));
            string sj1 = item.edge.Substring(item.edge.IndexOf(",") + 1);
            int i_item = int.Parse(si1);
            int j_item = int.Parse(sj1);
            if (j_item < height && i_item < width)
            {
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
        }


        public void Handle_calculateShortestPath(ref Point AnchorPoint, ref Point FreePoint
                    , nodes[,] graph, int width, int height, Bitmap global_img) // O(N)
        {
            Graphics graphics = Graphics.FromImage((Image)global_img);
            Pen redPen = new Pen(Color.Red, 2);
            bool stopper = false;
            int i, j;

            //check down the anchor point
            if (!stopper)
            {
                i = AnchorPoint.Y;
                j = AnchorPoint.X;

                while (i < height - 2)
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
            }

            //check right the anchor point
            if (!stopper)
            {
                i = AnchorPoint.Y;
                j = AnchorPoint.X;
                while (j < width - 2)
                {
                    if (graph[i, j].weight_right != graph[i, j + 1].weight_right)
                    {
                        Point tempPoint = new Point(j + 2, i);
                        graphics.DrawLine(redPen, AnchorPoint, tempPoint);
                        AnchorPoint.X = j + 2;
                        stopper = true;
                        break;
                    }
                    else
                        j++;

                }

            }

            //check left the anchor point
            if (!stopper)
            {
                i = AnchorPoint.Y;
                j = AnchorPoint.X;
                while (j > 1)
                {
                    if (graph[i, j].weight_left != graph[i, j - 1].weight_left)
                    {
                        Point tempPoint = new Point(j - 2, i);
                        graphics.DrawLine(redPen, AnchorPoint, tempPoint);
                        AnchorPoint.X = j - 2;
                        stopper = true;
                        break;
                    }
                    else
                        j--;

                }

            }

            //check up the anchor point
            if (!stopper)
            {
                i = AnchorPoint.Y;
                j = AnchorPoint.X;
                while (i > 1)
                {
                    if (graph[i, j].weight_up != graph[i - 1, j].weight_up)
                    {
                        Point tempPoint = new Point(j, i - 2);
                        graphics.DrawLine(redPen, AnchorPoint, tempPoint);
                        AnchorPoint.Y = i - 2;
                        stopper = true;
                        break;
                    }
                    else
                        i--;
                }
            }

            //check free points 
            stopper = false;

            //check down the free point
            if (!stopper)
            {
                i = FreePoint.Y;
                j = FreePoint.X;
                while (i < height - 2)
                {

                    if (graph[i, j].weight_down != graph[i + 1, j].weight_down)
                    {
                        Point tempPoint = new Point(j, i + 2);
                        graphics.DrawLine(redPen, FreePoint, tempPoint);
                        FreePoint.Y = i + 2;
                        stopper = true;
                        break;
                    }
                    else
                        i++;
                }
            }

            //check right the free point
            if (!stopper)
            {
                i = FreePoint.Y;
                j = FreePoint.X;
                while (j < width - 2)
                {
                    if (graph[i, j].weight_right != graph[i, j + 1].weight_right)
                    {
                        Point tempPoint = new Point(j + 2, i);
                        graphics.DrawLine(redPen, FreePoint, tempPoint);
                        FreePoint.X = j + 2;
                        stopper = true;
                        break;
                    }
                    else
                        j++;

                }

            }


            //check left the free point
            if (!stopper)
            {
                i = FreePoint.Y;
                j = FreePoint.X;
                while (j > 1)
                {

                    if (graph[i, j].weight_left != graph[i, j - 1].weight_left)
                    {
                        Point tempPoint = new Point(j - 2, i);
                        graphics.DrawLine(redPen, FreePoint, tempPoint);
                        FreePoint.X = j - 2;
                        stopper = true;
                        break;
                    }
                    else
                        j--;
                }

            }

            //check up the free point
            if (!stopper)
            {
                i = FreePoint.Y;
                j = FreePoint.X;
                while (i > 1)
                {

                    if (graph[i, j].weight_up != graph[i - 1, j].weight_up)
                    {
                        Point tempPoint = new Point(j, i - 2);
                        graphics.DrawLine(redPen, FreePoint, tempPoint);
                        FreePoint.Y = i - 2;
                        stopper = true;
                        break;
                    }
                    else
                        i--;
                }


            }



        }


    }

}