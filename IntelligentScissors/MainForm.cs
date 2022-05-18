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
        nodes[,] nodes;

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
        Graph graph;
        graph2 graph2 = new graph2();
        Bitmap global_img;
        Point[] anchors = new Point[1000];
        int anchorsCounter = 0;
        public List<string> path = new List<string>();

        private void btnOpen_Click(object sender, EventArgs e)
        {
            FilePath = "";
            graph = new Graph();
            pictureBox1.Image = null;
            pictureBox2.Image = null;
            anchorsCounter = 0;
            Array.Clear(anchors, 0, anchors.Length);

            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                //Open the browsed image and display it
                string OpenedFilePath = openFileDialog1.FileName;
                ImageMatrix = ImageOperations.OpenImage(OpenedFilePath);
                FilePath = OpenedFilePath;

                Console.SetBufferSize(Int16.MaxValue - 1, Int16.MaxValue - 1);
                var timer = new System.Diagnostics.Stopwatch();
                // if (!bigger_graph) { 
                timer.Start();
                nodes = graph2.Add_vertices(ImageMatrix);
                //graph.Add_vertices(ImageMatrix);
                //graph.Add_edges(ImageMatrix);
                timer.Stop();
                //int samar = 0;
                //for (int i = 0; i < ImageMatrix.GetLength(0); i++)
                //{
                //    for (int j = 0; j < ImageMatrix.GetLength(1); j++)
                //    {
                //        Console.WriteLine("Node : " + samar);
                //        samar++;
                //        Console.WriteLine("right weight : " + nodes[i, j].weight_right);
                //        Console.WriteLine("down weight : " + nodes[i, j].weight_down);
                //        Console.WriteLine("left weight : " + nodes[i, j].weight_left);
                //        Console.WriteLine("up weight : " + nodes[i, j].weight_up);
                //    }
                //}

                Console.WriteLine("Time taken to add vertices and edges: {0}", timer.Elapsed);
                ImageOperations.DisplayImage(ImageMatrix, pictureBox1);
            }
            txtWidth.Text = ImageOperations.GetWidth(ImageMatrix).ToString();
            txtHeight.Text = ImageOperations.GetHeight(ImageMatrix).ToString();
            Bitmap img = new Bitmap(FilePath);
            global_img = img;
           
        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Point point = new Point(e.X, e.Y);
                Console.WriteLine("Anchor X : " + point.X + "Anchor Y : " + point.Y);
                Graphics graphics = Graphics.FromImage((Image)global_img);
                Pen redPen = new Pen(Color.Red, 2);
                // Show the coordinates of the mouse click on the label, label1.
                Rectangle rect = new Rectangle(point.X - 4, point.Y - 4, 4, 4);
                anchors[anchorsCounter].X = point.X;
                anchors[anchorsCounter].Y = point.Y;
                anchorsCounter++;
                // Draw the rectangle, starting with the given coordinates, on the picture box.
                graphics.DrawRectangle(redPen, rect);
                // draw a line from the center of the rectangle to the mouse
                pictureBox1.Image = global_img;
                pictureBox2.Image = global_img;
                if (anchorsCounter >= 2)
                {
                    //SPtest sp = new SPtest();
                    ShortestPath sp = new ShortestPath();
                    Dictionary<string, KeyValuePair<string, double>> shortest_path = new Dictionary<string, KeyValuePair<string, double>>();
                    shortest_path = sp.calculateShortestPath(anchors[anchorsCounter - 2], anchors[anchorsCounter - 1], nodes,
                        ImageOperations.GetWidth(ImageMatrix), ImageOperations.GetHeight(ImageMatrix) , global_img);

                    graphics = Graphics.FromImage((Image)global_img);
                    redPen = new Pen(Color.Blue, 2);

                    int i_Free = anchors[anchorsCounter - 1].X;
                    int j_Free = anchors[anchorsCounter - 1].Y;
                    string FreePoint = i_Free + "," + j_Free;
                    string LastPoint;
                    //int FreePoint = i_Free * ImageOperations.GetWidth(ImageMatrix) + j_Free;
                    if (shortest_path.ContainsKey(FreePoint))
                    {
                        LastPoint = shortest_path[FreePoint].Key;
                        while (LastPoint != "null")
                        {
                            string si_Last = LastPoint.Substring(0, LastPoint.IndexOf(","));
                            int i_Last = int.Parse(si_Last);
                            string sj_Last = LastPoint.Substring(LastPoint.IndexOf(",") + 1);
                            int j_Last = int.Parse(sj_Last);
                            Point tempPoint = new Point(i_Last, j_Last);
                            graphics.DrawLine(redPen, point, tempPoint);
                            LastPoint = shortest_path[LastPoint].Key;
                            point = tempPoint;

                        }
                    }
                }
            }
            
            else if (e.Button == MouseButtons.Right)
            {
                Bitmap img = new Bitmap(FilePath);
                Graphics graphics = Graphics.FromImage((Image)img);
                Pen redPen = new Pen(Color.Blue, 3);
                Point point = new Point(anchors[0].X, anchors[0].Y);
                Rectangle rect = new Rectangle(point.X - 4, point.Y - 4, 4, 4);
                // Draw the rectangle, starting with the given coordinates, on the picture box.
                graphics.DrawRectangle(redPen, rect);
                // Show the coordinates of the mouse click on the label, label1.
                anchors[anchorsCounter].X = 0;
                anchors[anchorsCounter].Y = 0;
                anchorsCounter--;
                for (int i = 0; i < anchorsCounter-1; i++)
                {
                    redPen = new Pen(Color.Blue, 3);
                    point = new Point(anchors[i+1].X, anchors[i+1].Y);
                    rect = new Rectangle(point.X - 4, point.Y - 4, 4, 4);
                    // Draw the rectangle, starting with the given coordinates, on the picture box.
                    graphics.DrawRectangle(redPen, rect);
                    // draw a line from the center of the rectangle to the mouse
                    pictureBox2.Image = img;
                    if (anchorsCounter >= 2)
                    {
                        //SPtest sp = new SPtest();
                        ShortestPath sp = new ShortestPath();
                        Dictionary<string, KeyValuePair<string, double>> shortest_path = new Dictionary<string, KeyValuePair<string, double>>();
                        shortest_path = sp.calculateShortestPath(anchors[i], anchors[i+1], nodes,
                            ImageOperations.GetWidth(ImageMatrix), ImageOperations.GetHeight(ImageMatrix) , global_img);

                        graphics = Graphics.FromImage((Image)img);
                        redPen = new Pen(Color.Red, 3);

                        int i_Free = anchors[i+1].X;
                        int j_Free = anchors[i+1].Y;
                        string FreePoint = i_Free + "," + j_Free;
                        string LastPoint;
                        //int FreePoint = i_Free * ImageOperations.GetWidth(ImageMatrix) + j_Free;
                        if (shortest_path.ContainsKey(FreePoint))
                        {
                            LastPoint = shortest_path[FreePoint].Key;
                            while (LastPoint != "null")
                            {
                                string si_Last = LastPoint.Substring(0, LastPoint.IndexOf(","));
                                int i_Last = int.Parse(si_Last);
                                string sj_Last = LastPoint.Substring(LastPoint.IndexOf(",") + 1);
                                int j_Last = int.Parse(sj_Last);
                                Point tempPoint = new Point(i_Last, j_Last);
                                graphics.DrawLine(redPen, point, tempPoint);
                                pictureBox2.Image = img;
                                LastPoint = shortest_path[LastPoint].Key;
                                point = tempPoint;

                            }
                        }
                    }
                }

                global_img = img;
            }

        }

        public void DrawShortestPath()
        {

        }

        private void pictureBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ShortestPath sp = new ShortestPath();
            Dictionary<string, KeyValuePair<string, double>> shortest_path = new Dictionary<string, KeyValuePair<string, double>>();
            //shortest_path = sp.calculateShortestPath(anchors[anchorsCounter - 2], anchors[anchorsCounter - 1], nodes,
            //    ImageOperations.GetWidth(ImageMatrix), ImageOperations.GetHeight(ImageMatrix));
            shortest_path = sp.calculateShortestPath(anchors[anchorsCounter - 1 ], anchors[0], nodes,
                ImageOperations.GetWidth(ImageMatrix), ImageOperations.GetHeight(ImageMatrix) , global_img);

            Graphics graphics = Graphics.FromImage((Image)global_img);
            Pen redPen = new Pen(Color.Red, 5);
            Point point = anchors[0];
            int i_Free = anchors[0].X;
            int j_Free = anchors[0].Y;
            string FreePoint = i_Free + "," + j_Free;
            
            string LastPoint;
            if (shortest_path.ContainsKey(FreePoint))
            {
                LastPoint = shortest_path[FreePoint].Key;
                while (LastPoint != "null")
                {
                    string si_Last = LastPoint.Substring(0, LastPoint.IndexOf(","));
                    int i_Last = int.Parse(si_Last);
                    string sj_Last = LastPoint.Substring(LastPoint.IndexOf(",") + 1);
                    int j_Last = int.Parse(sj_Last);
                    Point tempPoint = new Point(i_Last, j_Last);
                    graphics.DrawLine(redPen, point, tempPoint);
                    pictureBox2.Image = global_img;
                    LastPoint = shortest_path[LastPoint].Key;
                    point = tempPoint;

                }
            }
        }

        
        
        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            Cursor_Y.Text = e.Y.ToString();
            Cursor_X.Text = e.X.ToString();
            if (anchorsCounter >= 1)
            {
                Bitmap line_img = new Bitmap(FilePath);
                Point point = new Point(anchors[anchorsCounter - 1].X, anchors[anchorsCounter - 1].Y);
                Graphics graphics = Graphics.FromImage(line_img);
                Pen redPen = new Pen(Color.Black, 2);
                Point tempPoint = new Point(e.X, e.Y);
                graphics.DrawLine(redPen, point, tempPoint);
                pictureBox1.Image = line_img;

            }

        }

        /*
         * private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            Cursor_Y.Text = e.Y.ToString();
            Cursor_X.Text = e.X.ToString();
            if (anchorsCounter >= 1)
            {
                Bitmap line_img = new Bitmap(FilePath);
                Point point = new Point(anchors[anchorsCounter - 1].X, anchors[anchorsCounter - 1].Y);
                Graphics graphics = Graphics.FromImage(line_img);
                Pen redPen = new Pen(Color.Black, 2);
                Point tempPoint = new Point(e.X, e.Y);
                graphics.DrawLine(redPen, point, tempPoint);
                pictureBox1.Image = line_img;

            }

        }
        */

        private void Clear_Click(object sender, EventArgs e)
        {
            Bitmap img = new Bitmap(FilePath);
            global_img = img;
            pictureBox1.Image = global_img;
            pictureBox2.Image = global_img;
            anchorsCounter = 0;
            Array.Clear(anchors, 0, anchors.Length);
        }
    }
}