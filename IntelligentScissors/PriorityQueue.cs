using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelligentScissors
{
    class PriorityQueue
    {

        public List<edge_weight> vertices = new List<edge_weight>();

        public void Enqueue(edge_weight new_vertix)
        {
            vertices.Add(new_vertix);
            BuildMinHeap(vertices);
        }

        public void BuildMinHeap(List<edge_weight> vertices)
        {
            for (int i = (vertices.Count / 2); i >= 0; i--)
            {
                min_heap(vertices, i);
            }
        }
        public void min_heap(List<edge_weight> vertices, int i)
        {
            int left = 2 * i;
            int right = 2 * i + 1;

            int smallest;

            if (left < vertices.Count && vertices[left].weight < vertices[i].weight)
            {
                smallest = left;
            }
            else
            {
                smallest = i;
            }

            if (right < vertices.Count && vertices[right].weight < vertices[i].weight)
            {
                smallest = right;
            }

            if (smallest != i)
            {
                edge_weight vertix = vertices[i];
                vertices[i] = vertices[smallest];
                vertices[smallest] = vertix;
                min_heap(vertices, smallest);
            }
        }
        public void Update(string key, double New_Weight)
        {
            int i;
            for (i = 0; i < vertices.Count; i++)
            {
                if (key == vertices[i].edge)
                    break;
            }

            vertices.RemoveAt(i);
            edge_weight vertix = new edge_weight { edge = key, weight = New_Weight };
            Enqueue(vertix);
        }

        public edge_weight Dequeue()
        {
            edge_weight min_vertix = vertices[0];
            vertices[0] = vertices[vertices.Count - 1];
            vertices.RemoveAt(vertices.Count - 1);
            min_heap(vertices, 0);
            return min_vertix;
        }

        public int Count()
        {
            return vertices.Count();
        }

        public bool ContainsItem(string vertex)
        {
            for (int i = 0; i < vertices.Count; i++)
            {
                if (vertex == vertices[i].edge)
                {
                    return true;
                }
                    
            }
            return false;
        }
    }
}
