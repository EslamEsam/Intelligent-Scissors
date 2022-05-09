using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelligentScissors
{
    
    public struct edge_weight
    {
        public double weight;
        public string edge;
    }
    
    // graph making 
    internal class Graph
    {
        public Dictionary<string, List<edge_weight>> graph = new Dictionary<string, List<edge_weight>>();
        List<edge_weight> edges = new List<edge_weight>();
        public void Add_vertices(RGBPixel[,] ImageMatrix)
        {
            
            for (int i = 0; i < ImageMatrix.GetLength(1); i++)
            {
                for (int j = 0; j < ImageMatrix.GetLength(0); j++)
                {
                    string key = i.ToString() + "," + j.ToString();
                    graph.Add(key, new List<edge_weight>());
                }
            }
            
        }

        public void Add_edges(RGBPixel[,] ImageMatrix)
        {
            double[,] weight_down = new double[ImageMatrix.GetLength(1), ImageMatrix.GetLength(0)];
            double[,] weight_right = new double[ImageMatrix.GetLength(1), ImageMatrix.GetLength(0)];
            for (int i = 0; i < ImageMatrix.GetLength(1); i++)
            {
                for (int j = 0; j < ImageMatrix.GetLength(0); j++)
                {
                    string key = i.ToString() + "," + j.ToString();
                    
                    Vector2D weight = new Vector2D();
                    weight = ImageOperations.CalculatePixelEnergies(i, j, ImageMatrix);
                    weight.X = 1 / weight.X;
                    weight.Y = 1 / weight.Y;
                    if (i == 0)
                    {
                        

                        if (j == 0)
                        {

                            graph[key].Add(new edge_weight { weight = weight.X, edge = (i + 1).ToString() + "," + j.ToString() });
                            weight_down[i,j] = weight.Y;
                            graph[key].Add(new edge_weight { weight = weight.Y, edge = (i).ToString() + "," + (j + 1).ToString() });
                            weight_right[i,j] = weight.X;

                        }
                        else if (j == (ImageMatrix.GetLength(0) - 1))
                        {
                            graph[key].Add(new edge_weight { weight = weight_right[i,j-1], edge = (i).ToString() + "," + (j - 1).ToString() });
                            graph[key].Add(new edge_weight { weight = weight.Y, edge = (i + 1).ToString() + "," + j.ToString() });
                            weight_down[i, j] = weight.Y;
                        }
                        else
                        {
                            graph[key].Add(new edge_weight { weight = weight.Y, edge = (i + 1).ToString() + "," + j.ToString() });
                            weight_down[i, j] = weight.Y;
                            graph[key].Add(new edge_weight { weight = weight.X, edge = (i).ToString() + "," + (j + 1).ToString() });
                            weight_right[i, j] = weight.X;
                            graph[key].Add(new edge_weight { weight = weight_right[i,j-1], edge = (i).ToString() + "," + (j - 1).ToString() });
                        }

                    }

                    else if (i == (ImageMatrix.GetLength(1) - 1))
                    {
                        if (j == 0)
                        {
                            graph[key].Add(new edge_weight { weight =weight_down[i-1,j] , edge = (i - 1).ToString() + "," + j.ToString() });
                            
                            graph[key].Add(new edge_weight { weight = weight.X, edge = (i).ToString() + "," + (j + 1).ToString() });
                            weight_right[i, j] = weight.X;

                        }
                        else if (j == (ImageMatrix.GetLength(0) - 1))
                        {
                            graph[key].Add(new edge_weight { weight = weight_right[i,j-1], edge = (i).ToString() + "," + (j - 1).ToString() });
                            graph[key].Add(new edge_weight { weight = weight_down[i-1,j], edge = (i - 1).ToString() + "," + j.ToString() });
                        }
                        else
                        {
                            graph[key].Add(new edge_weight { weight = weight.X, edge = (i).ToString() + "," + (j + 1).ToString() });
                            weight_right[i, j] = weight.X;
                            graph[key].Add(new edge_weight { weight = weight_right[i,j-1], edge = (i).ToString() + "," + (j - 1).ToString() });
                            graph[key].Add(new edge_weight { weight = weight_down[i-1,j], edge = (i - 1).ToString() + "," + j.ToString() });
                        }
                    }
                    else
                    {
                        if (j == 0)
                        {
                            graph[key].Add(new edge_weight { weight = weight_down[i-1,j], edge = (i - 1).ToString() + "," + (j).ToString() });
                            graph[key].Add(new edge_weight { weight = weight.X, edge = (i).ToString() + "," + (j + 1).ToString() });
                            weight_right[i, j] = weight.X;
                            graph[key].Add(new edge_weight { weight = weight.Y, edge = (i + 1).ToString() + "," + j.ToString() });
                            weight_down[i, j] = weight.Y;
                        }
                        else if (j == (ImageMatrix.GetLength(0) - 1))
                        {
                            graph[key].Add(new edge_weight { weight = weight_right[i-1,j], edge = (i).ToString() + "," + (j - 1).ToString() });
                            graph[key].Add(new edge_weight { weight = weight.Y, edge = (i + 1).ToString() + "," + (j).ToString() });
                            weight_down[i, j] = weight.Y;
                            graph[key].Add(new edge_weight { weight = weight_down[i-1,j], edge = (i - 1).ToString() + "," + j.ToString() });
                        }
                        else
                        {
                            graph[key].Add(new edge_weight { weight = weight.Y, edge = (i + 1).ToString() + "," + j.ToString() });
                            weight_down[i, j] = weight.Y;
                            graph[key].Add(new edge_weight { weight = weight.X, edge = (i).ToString() + "," + (j + 1).ToString() });
                            weight_right[i, j] = weight.X;
                            graph[key].Add(new edge_weight { weight = weight_down[i-1,j], edge = (i - 1).ToString() + "," + j.ToString() });
                            graph[key].Add(new edge_weight { weight = weight_right[i,j-1], edge = (i).ToString() + "," + (j - 1).ToString() });
                        }
                    }

                }
            }

        }


        // add edges before weight 
        //public void Add_weight(RGBPixel[,] ImageMatrix)
        //{
        //    for (int i = 0; i < ImageMatrix.GetLength(0); i++)
        //    {
        //        for (int j = 0; j < ImageMatrix.GetLength(1); j++)
        //        {
        //            string key = i.ToString() + "," + j.ToString();
        //            
        //            if (i == 0)
        //            {
        //                

        //                if (j == 0)
        //                {

        //                    graph[key].Add(new edge_weight { weight = 0, edge = (i + 1).ToString() + "," + j.ToString() });
        //                    graph[key].Add(new edge_weight { weight = 0, edge = (i).ToString() + "," + (j + 1).ToString() });

        //                }
        //                else if (j == (ImageMatrix.GetLength(1) - 1))
        //                {
        //                    graph[key].Add(new edge_weight { weight = 0, edge = (i).ToString() + "," + (j - 1).ToString() });
        //                    graph[key].Add(new edge_weight { weight = weight.Y, edge = (i + 1).ToString() + "," + j.ToString() });
        //                }
        //                else
        //                {
        //                    graph[key].Add(new edge_weight { weight = 0, edge = (i + 1).ToString() + "," + j.ToString() });
        //                    graph[key].Add(new edge_weight { weight = 0, edge = (i).ToString() + "," + (j + 1).ToString() });
        //                    graph[key].Add(new edge_weight { weight = 0, edge = (i).ToString() + "," + (j - 1).ToString() });
        //                }

        //            }

        //            else if (i == (ImageMatrix.GetLength(0) - 1))
        //            {
        //                if (j == 0)
        //                {
        //                    graph[key].Add(new edge_weight { weight = 0, edge = (i - 1).ToString() + "," + j.ToString() });
        //                    graph[key].Add(new edge_weight { weight = 0, edge = (i).ToString() + "," + (j + 1).ToString() });

        //                }
        //                else if (j == (ImageMatrix.GetLength(1) - 1))
        //                {
        //                    graph[key].Add(new edge_weight { weight = 0, edge = (i).ToString() + "," + (j - 1).ToString() });
        //                    graph[key].Add(new edge_weight { weight = 0, edge = (i - 1).ToString() + "," + j.ToString() });
        //                }
        //                else
        //                {
        //                    graph[key].Add(new edge_weight { weight = 0, edge = (i).ToString() + "," + (j + 1).ToString() });
        //                    graph[key].Add(new edge_weight { weight = 0, edge = (i).ToString() + "," + (j - 1).ToString() });
        //                    graph[key].Add(new edge_weight { weight = 0, edge = (i - 1).ToString() + "," + j.ToString() });
        //                }
        //            }
        //            else
        //            {
        //                if (j == 0)
        //                {
        //                    graph[key].Add(new edge_weight { weight = 0, edge = (i - 1).ToString() + "," + (j).ToString() });
        //                    graph[key].Add(new edge_weight { weight = 0, edge = (i).ToString() + "," + (j + 1).ToString() });
        //                    graph[key].Add(new edge_weight { weight = 0, edge = (i + 1).ToString() + "," + j.ToString() });
        //                }
        //                else if (j == (ImageMatrix.GetLength(1) - 1))
        //                {
        //                    graph[key].Add(new edge_weight { weight = 0, edge = (i).ToString() + "," + (j + 1).ToString() });
        //                    graph[key].Add(new edge_weight { weight = 0, edge = (i + 1).ToString() + "," + (j).ToString() });
        //                    graph[key].Add(new edge_weight { weight = 0, edge = (i - 1).ToString() + "," + j.ToString() });
        //                }
        //                else
        //                {
        //                    graph[key].Add(new edge_weight { weight = 0, edge = (i + 1).ToString() + "," + j.ToString() });
        //                    graph[key].Add(new edge_weight { weight = 0, edge = (i).ToString() + "," + (j + 1).ToString() });
        //                    graph[key].Add(new edge_weight { weight = 0, edge = (i - 1).ToString() + "," + j.ToString() });
        //                    graph[key].Add(new edge_weight { weight = 0, edge = (i).ToString() + "," + (j - 1).ToString() });
        //                }
        //            }

        //        }
        //    }

        //}


    }
}
