using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace IntelligentScissors
{
    class Backtrack
    {
        public static List<string> backtrack(string [,] parents , Point free_point)
        {
            List<string> backtrack_points = new List<string>();
            int i_Free = free_point.X;
            int j_Free = free_point.Y;
            string FreePoint = i_Free + "," + j_Free;
            backtrack_points.Add(FreePoint);
            string LastPoint = parents[i_Free, j_Free];   //first it would be the parnet of the freepoint and it will be updated
           // string AnchorPoint = anchor_point.X.ToString() + "," + anchor_point.Y.ToString();

            while (LastPoint != "null")
            {
                string si_Last = LastPoint.Substring(0, LastPoint.IndexOf(","));
                int i_Last = int.Parse(si_Last);
                Console.WriteLine(i_Last);
                string sj_Last = LastPoint.Substring(LastPoint.IndexOf(",") + 1);
                int j_Last = int.Parse(sj_Last);
                Console.WriteLine(j_Last);
                LastPoint = parents[i_Last, j_Last];
                backtrack_points.Add(LastPoint);
            }
            return backtrack_points;
        }


        
        List<string> backtrack_points = new List<string>();
        public string backtrack_recursion(string[,] parents, Point free_point , ref List<string> points)
        {
                     
            int i_Free = free_point.X;
            int j_Free = free_point.Y;
            string FreePoint = i_Free + "," + j_Free;
            backtrack_points.Add(FreePoint);
            string LastPoint = parents[i_Free, j_Free];   //first it would be the parnet of the freepoint and it will be updated
           // string AnchorPoint = anchor_point.X.ToString() + "," + anchor_point.Y.ToString();
            if (LastPoint == null)
            {
                points = backtrack_points;
                return null;
            }
            string si_Last = LastPoint.Substring(0, LastPoint.IndexOf(","));
            int i_Last = int.Parse(si_Last);
            string sj_Last = LastPoint.Substring(LastPoint.IndexOf(",") + 1);
            int j_Last = int.Parse(sj_Last);

            Point last = new Point(i_Last, j_Last);
            LastPoint = backtrack_recursion(parents, last , ref backtrack_points);
            
            return null;
        }
    }
}
