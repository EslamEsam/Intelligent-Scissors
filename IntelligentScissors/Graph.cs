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
    
    internal class Graph
    {
        public Dictionary<string, List<edge_weight>> graph = new Dictionary<string, List<edge_weight>>();
        List<edge_weight> edges = new List<edge_weight>();
        public void Add_vertices(RGBPixel[,] ImageMatrix)
        {
            for (int i = 0; i < ImageMatrix.GetLength(0); i++)
            {
                for (int j = 0; j < ImageMatrix.GetLength(1); j++)
                {
                    string key = i.ToString() + "," + j.ToString();
                    graph.Add(key, new List<edge_weight>());
                }
            }
        }

        public void Add_edges (RGBPixel[,] ImageMatrix)
        {
            for (int i = 0; i < ImageMatrix.GetLength(0); i++)
            {
                for (int j = 0; j < ImageMatrix.GetLength(1); j++)
                {
                    string key = i.ToString() + "," + j.ToString();
                    if (i == 0) 
                    {
                        if (j == 0)
                        {
                            graph[key].Add(new edge_weight { weight = 0, edge = (i + 1).ToString() + "," + j.ToString() });
                            graph[key].Add(new edge_weight { weight = 0, edge = (i).ToString() + "," + (j + 1).ToString() });

                        }
                        else if (j == (ImageMatrix.GetLength(1)-1))
                        {
                            graph[key].Add(new edge_weight { weight = 0, edge = (i).ToString() + "," + (j - 1).ToString() });
                            graph[key].Add(new edge_weight { weight = 0, edge = (i + 1).ToString() + "," + j.ToString() });
                        }
                        else
                        {
                            graph[key].Add(new edge_weight { weight = 0, edge = (i + 1).ToString() + "," + j.ToString() });
                            graph[key].Add(new edge_weight { weight = 0, edge = (i).ToString() + "," + (j + 1).ToString() });
                            graph[key].Add(new edge_weight { weight = 0, edge = (i).ToString() + "," + (j - 1).ToString() });
                        }

                    }

                    else if (i == (ImageMatrix.GetLength(0)-1))
                    {
                        if (j == 0)
                        {
                            graph[key].Add(new edge_weight { weight = 0, edge = (i-1).ToString() + "," + j.ToString() });
                            graph[key].Add(new edge_weight { weight = 0, edge = (i).ToString() + "," + (j + 1).ToString() });

                        }
                        else if (j == (ImageMatrix.GetLength(1)-1))
                        {
                            graph[key].Add(new edge_weight { weight = 0, edge = (i).ToString() + "," + (j - 1).ToString() });
                            graph[key].Add(new edge_weight { weight = 0, edge = (i - 1).ToString() + "," + j.ToString() });
                        }
                        else
                        {
                            graph[key].Add(new edge_weight { weight = 0, edge = (i).ToString() + "," + (j + 1).ToString() });
                            graph[key].Add(new edge_weight { weight = 0, edge = (i).ToString() + "," + (j - 1).ToString() });
                            graph[key].Add(new edge_weight { weight = 0, edge = (i - 1).ToString() + "," + j.ToString() });
                        }
                    }
                    else
                    {
                        if (j == 0)
                        {
                            graph[key].Add(new edge_weight { weight = 0, edge = (i-1).ToString() + "," + (j).ToString() });
                            graph[key].Add(new edge_weight { weight = 0, edge = (i).ToString() + "," + (j + 1).ToString() });
                            graph[key].Add(new edge_weight { weight = 0, edge = (i + 1).ToString() + "," + j.ToString() });
                        }
                        else if (j == (ImageMatrix.GetLength(1)-1))
                        {
                            graph[key].Add(new edge_weight { weight = 0, edge = (i).ToString() + "," + (j + 1).ToString() });
                            graph[key].Add(new edge_weight { weight = 0, edge = (i + 1).ToString() + "," + (j).ToString() });
                            graph[key].Add(new edge_weight { weight = 0, edge = (i - 1).ToString() + "," + j.ToString() });
                        }
                        else
                        {
                            graph[key].Add(new edge_weight { weight = 0, edge = (i + 1).ToString() + "," + j.ToString() });
                            graph[key].Add(new edge_weight { weight = 0, edge = (i).ToString() + "," + (j + 1).ToString() });
                            graph[key].Add(new edge_weight { weight = 0, edge = (i - 1).ToString() + "," + j.ToString() });
                            graph[key].Add(new edge_weight { weight = 0, edge = (i).ToString() + "," + (j - 1).ToString() });
                        }
                    }
                    
                }
            }
            
        }


    }
}
