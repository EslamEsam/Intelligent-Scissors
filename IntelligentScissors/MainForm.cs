using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace IntelligentScissors
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        RGBPixel[,] ImageMatrix;

        public void DrawCircl(Point index, Bitmap img, int Radius)
        {
            for (int i = 0; i < 360; i++)
            {
                double x = index.X - Radius * Math.Cos(2 * Math.PI / 360 * i);
                double y = index.Y - Radius * Math.Sin(2 * Math.PI / 360 * i);
                img.SetPixel((int)x, (int)y, Color.Red);
            }
        }
        string FilePath;
        Graph graph = new Graph();
        Bitmap global_img;

        private void btnOpen_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                //Open the browsed image and display it
                string OpenedFilePath = openFileDialog1.FileName;
                ImageMatrix = ImageOperations.OpenImage(OpenedFilePath);
                FilePath = OpenedFilePath;

                graph.Add_vertices(ImageMatrix);
                graph.Add_edges(ImageMatrix);

                ImageOperations.DisplayImage(ImageMatrix, pictureBox1);
            }
            txtWidth.Text = ImageOperations.GetWidth(ImageMatrix).ToString();
            txtHeight.Text = ImageOperations.GetHeight(ImageMatrix).ToString();
            Bitmap img = new Bitmap(FilePath);
            global_img = img;
        }

        static Point[] anchors = new Point[1000];
        static int anchorsCounter = 0, clicksCounter = 0;
        public List<string> path = new List<string>();
        bool draw_line = false;
        private void pictureBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            clicksCounter++;
            Bitmap img = new Bitmap(FilePath);
            Point cp = PointToClient(Cursor.Position);
            Point point = new Point(cp.X - 17, cp.Y - 17);
            //Console.WriteLine("Anchor X : " + point.X + "Anchor Y : " + point.Y);
            img.SetPixel((int)point.X, (int)point.Y, Color.Red);
            Graphics graphics = Graphics.FromImage((Image)global_img);
            Pen redPen = new Pen(Color.Red, 1);
            // Show the coordinates of the mouse click on the label, label1.
            Rectangle rect = new Rectangle(point.X, point.Y, 2, 2);
            anchors[anchorsCounter].X = point.X;
            anchors[anchorsCounter].Y = point.Y;
            // Draw the rectangle, starting with the given coordinates, on the picture box.
            graphics.DrawRectangle(redPen, rect);
            // draw a line from the center of the rectangle to the mouse
            pictureBox1.Image = global_img;
            pictureBox2.Image = global_img;
            Console.SetBufferSize(Int16.MaxValue - 1, Int16.MaxValue - 1);
            anchorsCounter++;
            draw_line = true;
            //if (clicksCounter >= 2)
            //{
            //    SPtest sp = new SPtest();
            //    Dictionary<string, KeyValuePair<string, double>> shortest_path = new Dictionary<string, KeyValuePair<string, double>>();
            //    shortest_path = sp.calculateShortestPath(anchors[anchorsCounter - 2], anchors[anchorsCounter - 1], graph.graph);
            //    graphics = Graphics.FromImage((Image)global_img);
            //    redPen = new Pen(Color.Blue, 1);

            //    int i_Free = anchors[anchorsCounter - 1].X;
            //    int j_Free = anchors[anchorsCounter - 1].Y;
            //    string FreePoint = i_Free + "," + j_Free;
            //    string LastPoint;
            //    if (shortest_path.ContainsKey(FreePoint))
            //    {
            //        LastPoint = shortest_path[FreePoint].Key;
            //        while (LastPoint != "null")
            //        {
            //            string si_Last = LastPoint.Substring(0, LastPoint.IndexOf(","));
            //            int i_Last = int.Parse(si_Last);
            //            string sj_Last = LastPoint.Substring(LastPoint.IndexOf(",") + 1);
            //            int j_Last = int.Parse(sj_Last);
            //            Point tempPoint = new Point(i_Last, j_Last);
            //            graphics.DrawLine(redPen, point, tempPoint);
            //            pictureBox1.Image = global_img;
            //            LastPoint = shortest_path[LastPoint].Key;
            //            point = tempPoint;

            //        }
            //    }

            //}
        }


        //edge_weight Last_Point = new edge_weight(startanchor , 0);
        // Dictionary<string, double> Last_point = new Dictionary<string, double>();

        static List<string> Last_Point = new List<string>();

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (draw_line == true)
            {

                Console.WriteLine("anchors X : " + anchors[0].X + "anchors Y : " + anchors[0].Y);
                Point cp = PointToClient(Cursor.Position);
                Point point = new Point(cp.X - 17, cp.Y - 17);
                Cursor_Y.Text = point.Y.ToString();
                Cursor_X.Text = point.X.ToString();
                // double total_distance = 0;
                //startanchor = anchors[0].X.ToString() + "," + anchors[0].Y.ToString();
                //Last_Point = new edge_weight(startanchor, 0);
                string startanchor = anchors[0].X.ToString() + "," + anchors[0].Y.ToString();

                //edge_weight Last_Point = new edge_weight(startanchor , 0);
                // Dictionary<string, double> Last_point = new Dictionary<string, double>();
                if (Last_Point.Count > 1)
                {
                    Console.WriteLine("  gfhrh  " + startanchor);

                    Console.WriteLine("  gfhrh  " + Last_Point.Count);

                    Console.WriteLine("  gfhrh  " + Last_Point[Last_Point.Count - 2]);
                }
                Last_Point.Add(startanchor);


                Console.WriteLine("  gfhrh  " + startanchor);
                Console.WriteLine("  gfhrh  " + Last_Point[anchorsCounter-1]);
                if (Last_Point.Count == 1)
                {
                    if (anchorsCounter >= 1)
                    {
                        string start = Last_Point[0];
                        Dictionary<string, double> point_distance = new Dictionary<string, double>();
                        if (graph.graph.ContainsKey(start))
                        {
                            foreach (var item in graph.graph[start])
                            {
                                point_distance.Add(item.edge, item.weight);

                            }
                            edge_weight min1 = new edge_weight();
                            min1.weight = double.PositiveInfinity;
                            edge_weight min2 = new edge_weight();
                            min2.weight = double.PositiveInfinity;
                            edge_weight min = new edge_weight();

                            foreach (var item in point_distance)
                            {
                                if (item.Value < min1.weight && item.Key != Last_Point[0])
                                {

                                    min1.edge = item.Key;
                                    min1.weight = item.Value;


                                }
                                if (min1.weight == item.Value && min1.edge != item.Key && item.Key != Last_Point[0])
                                {
                                    min2.edge = item.Key;
                                    min2.weight = item.Value;
                                }
                            }
                            if (min1.weight == min2.weight)
                            {

                                string si1 = min1.edge.Substring(0, min1.edge.IndexOf(","));
                                int i1 = int.Parse(si1);
                                string sj1 = min1.edge.Substring(min1.edge.IndexOf(",") + 1);
                                int j1 = int.Parse(sj1);
                                string si2 = min2.edge.Substring(0, min2.edge.IndexOf(","));
                                int i2 = int.Parse(si2);
                                string sj2 = min1.edge.Substring(min2.edge.IndexOf(",") + 1);
                                int j2 = int.Parse(sj2);
                                double distance1 = Math.Sqrt((Math.Pow((i1 - (int)point.X), 2)) + (Math.Pow((j1 - (int)point.Y), 2)));
                                double distance2 = Math.Sqrt((Math.Pow((i2 - (int)point.X), 2)) + (Math.Pow((j2 - (int)point.Y), 2)));
                                if (distance1 < distance2)
                                {
                                    min = min1;
                                }
                                else
                                    min = min2;
                            }
                            else
                                min = min1;
                            Console.WriteLine("min1 : " + min1.edge + " " + min1.weight);
                            Console.WriteLine("min2 : " + min2.edge + " " + min2.weight);
                            Console.WriteLine("min : " + min.edge + " " + min.weight);
                            Graphics graphics = Graphics.FromImage((Image)global_img);
                            Pen redPen = new Pen(Color.Red, 1);
                            string si_Last = min.edge.Substring(0, min.edge.IndexOf(","));
                            int i_Last = int.Parse(si_Last);
                            string sj_Last = min.edge.Substring(min.edge.IndexOf(",") + 1);
                            int j_Last = int.Parse(sj_Last);
                            Console.WriteLine("i_last = " + i_Last + "  ,  j_last = " + j_Last);

                            Point tempPoint = new Point(i_Last, j_Last);
                            string si = Last_Point[0].Substring(0, Last_Point[0].IndexOf(","));
                            int i = int.Parse(si);
                            string sj = Last_Point[0].Substring(Last_Point[0].IndexOf(",") + 1);
                            int j = int.Parse(sj);
                            Console.WriteLine("i = " + i + "  ,  j = " + j);

                            Point start_point = new Point(i, j);

                            graphics.DrawLine(redPen, start_point, tempPoint);
                            pictureBox1.Image = global_img;

                            Last_Point.Add(min.edge);
                        }
                    }
                }

                if (Last_Point.Count > 1)
                {
                    if (anchorsCounter >= 1)
                    {
                        string start = Last_Point[Last_Point.Count - 2];
                        Dictionary<string, double> point_distance = new Dictionary<string, double>();
                        if (graph.graph.ContainsKey(start))
                        {
                            foreach (var item in graph.graph[start])
                            {
                                point_distance.Add(item.edge, item.weight);

                            }
                            edge_weight min1 = new edge_weight();
                            min1.weight = double.PositiveInfinity;
                            edge_weight min2 = new edge_weight();
                            min2.weight = double.PositiveInfinity;
                            edge_weight min = new edge_weight();

                            foreach (var item in point_distance)
                            {
                                if (item.Value < min1.weight && item.Key != Last_Point[Last_Point.Count-2])
                                {
                                    Console.WriteLine("item.key = " + item.Key);
                                    Console.WriteLine("last point = " + Last_Point[Last_Point.Count - 2]);
                                    min1.edge = item.Key;
                                    min1.weight = item.Value;


                                }
                                if (min1.weight == item.Value && min1.edge != item.Key && item.Key != Last_Point[Last_Point.Count - 2])
                                {
                                    min2.edge = item.Key;
                                    min2.weight = item.Value;
                                }
                            }
                            if (min1.weight == min2.weight)
                            {

                                string si1 = min1.edge.Substring(0, min1.edge.IndexOf(","));
                                int i1 = int.Parse(si1);
                                string sj1 = min1.edge.Substring(min1.edge.IndexOf(",") + 1);
                                int j1 = int.Parse(sj1);
                                string si2 = min2.edge.Substring(0, min2.edge.IndexOf(","));
                                int i2 = int.Parse(si2);
                                string sj2 = min1.edge.Substring(min2.edge.IndexOf(",") + 1);
                                int j2 = int.Parse(sj2);
                                double distance1 = Math.Sqrt((Math.Pow((i1 - (int)point.X), 2)) + (Math.Pow((j1 - (int)point.Y), 2)));
                                double distance2 = Math.Sqrt((Math.Pow((i2 - (int)point.X), 2)) + (Math.Pow((j2 - (int)point.Y), 2)));
                                if (distance1 < distance2)
                                {
                                    min = min1;
                                }
                                else
                                    min = min2;
                            }
                            else
                                min = min1;
                            Console.WriteLine("min1 : " + min1.edge + " " + min1.weight);
                            Console.WriteLine("min2 : " + min2.edge + " " + min2.weight);
                            Console.WriteLine("min : " + min.edge + " " + min.weight);
                            Graphics graphics = Graphics.FromImage((Image)global_img);
                            Pen redPen = new Pen(Color.Red, 1);
                            string si_Last = min.edge.Substring(0, min.edge.IndexOf(","));
                            int i_Last = int.Parse(si_Last);
                            string sj_Last = min.edge.Substring(min.edge.IndexOf(",") + 1);
                            int j_Last = int.Parse(sj_Last);
                            Console.WriteLine("i_last = " + i_Last + "  ,  j_last = " + j_Last);

                            Point tempPoint = new Point(i_Last, j_Last);
                            string si = Last_Point[Last_Point.Count - 2].Substring(0, Last_Point[Last_Point.Count - 2].IndexOf(","));
                            int i = int.Parse(si);
                            string sj = Last_Point[Last_Point.Count - 2].Substring(Last_Point[Last_Point.Count - 2].IndexOf(",") + 1);
                            int j = int.Parse(sj);
                            Console.WriteLine("i = " + i + "  ,  j = " + j);

                            Point start_point = new Point(i, j);

                            graphics.DrawLine(redPen, start_point, tempPoint);
                            pictureBox1.Image = global_img;

                            Last_Point.Add(min.edge);
                        }
                    }
                }
            }
        }

        private void pictureBox1_CursorChanged(object sender, EventArgs e)
        {
        }

        private void FinishCropping_Click(object sender, EventArgs e)
        {
            //SPtest sp = new SPtest();
            //Dictionary<string, KeyValuePair<string, double>> shortest_path = new Dictionary<string, KeyValuePair<string, double>>();
            //shortest_path = sp.calculateShortestPath(anchors[0], anchors[anchorsCounter - 1], graph.graph);
            //Graphics graphics = Graphics.FromImage((Image)global_img);
            //Pen redPen = new Pen(Color.Red, 1);
            //Point point = anchors[anchorsCounter - 1];
            //int i_Free = anchors[anchorsCounter - 1].X;
            //int j_Free = anchors[anchorsCounter - 1].Y;
            //string FreePoint = i_Free + "," + j_Free;
            //string LastPoint;
            //if (shortest_path.ContainsKey(FreePoint))
            //{
            //    LastPoint = shortest_path[FreePoint].Key;
            //    while (LastPoint != "null")
            //    {
            //        string si_Last = LastPoint.Substring(0, LastPoint.IndexOf(","));
            //        int i_Last = int.Parse(si_Last);
            //        string sj_Last = LastPoint.Substring(LastPoint.IndexOf(",") + 1);
            //        int j_Last = int.Parse(sj_Last);
            //        Point tempPoint = new Point(i_Last, j_Last);
            //        graphics.DrawLine(redPen, point, tempPoint);
            //        pictureBox1.Image = global_img;
            //        LastPoint = shortest_path[LastPoint].Key;
            //        point = tempPoint;

            //    }
            //}
        }
    }
}