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
            string LastPoint = parents[i_Free, j_Free];  

            while ( !(LastPoint == "null") || !(string.IsNullOrEmpty(LastPoint)))
            {
                if (LastPoint == "null" || string.IsNullOrEmpty(LastPoint))
                {
                    break;
                }
                
                string si_Last = LastPoint.Substring(0, LastPoint.IndexOf(","));
                int i_Last = int.Parse(si_Last);
                string sj_Last = LastPoint.Substring(LastPoint.IndexOf(",") + 1);
                int j_Last = int.Parse(sj_Last);
                LastPoint = parents[i_Last, j_Last];
                backtrack_points.Add(LastPoint);
            }
            return backtrack_points;
        }
        
    }
}
