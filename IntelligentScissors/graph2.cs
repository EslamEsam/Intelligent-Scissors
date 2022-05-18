using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelligentScissors
{

    struct nodes
    {
        public double weight_down;
        public double weight_up;
        public double weight_right;
        public double weight_left;
    }
    internal class graph2
    {

        public nodes[,] Add_vertices(RGBPixel[,] ImageMatrix)
        {
            int width = ImageMatrix.GetLength(1);
            int height = ImageMatrix.GetLength(0);
            nodes[,] graph = new nodes[height, width];
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {

                    Vector2D weight = new Vector2D();
                    weight = ImageOperations.CalculatePixelEnergies(j, i, ImageMatrix);
                    weight.X = 1 / weight.X;
                    weight.Y = 1 / weight.Y;

                    if (j != width - 1)
                    {   
                        graph[i, j].weight_right = weight.X;
                    }

                    if (i != height - 1)
                    {
                        graph[i, j].weight_down = weight.Y;
                    }

                    if (j != 0)
                    {
                        weight = ImageOperations.CalculatePixelEnergies(j - 1 , i , ImageMatrix);
                        weight.X = 1 / weight.X;
                        graph[i, j].weight_left = weight.X;
                    }

                    if (i != 0)
                    {
                        weight = ImageOperations.CalculatePixelEnergies( j, i - 1, ImageMatrix);
                        weight.Y = 1 / weight.Y;
                        graph[i, j].weight_up = weight.Y;
                    }

                }
            }

            return graph;
        }
    }
}
