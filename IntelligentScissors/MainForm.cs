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
        
        bool lineDraw = false;
        Point rec_point = new Point();
        public List<string> path = new List<string>();
        string[,] parents;

        private void pictureBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Bitmap img = new Bitmap(FilePath);
            Point cp = PointToClient(Cursor.Position);
            Point point = new Point(cp.X - 17, cp.Y - 17);
            Console.WriteLine("Anchor X : " + point.X + "Anchor Y : " + point.Y);
            img.SetPixel((int)point.X, (int)point.Y, Color.Red);
            Graphics graphics = Graphics.FromImage((Image)global_img);
            Pen redPen = new Pen(Color.Red, 1);
            // Show the coordinates of the mouse click on the label, label1.
            Rectangle rect = new Rectangle(point.X - 2, point.Y - 2, 2, 2);
            rec_point.X = point.X - 2;
            rec_point.Y = point.Y - 2;
            // Draw the rectangle, starting with the given coordinates, on the picture box.
            graphics.DrawRectangle(redPen, rect);
            // draw a line from the center of the rectangle to the mouse
            pictureBox1.Image = global_img;
            pictureBox2.Image = global_img;
            double[,] distance = new double[ImageOperations.GetWidth(ImageMatrix), ImageOperations.GetHeight(ImageMatrix)];
            ShortestPath sp = new ShortestPath();
            //SP sp = new SP();
            parents = new string[ImageOperations.GetWidth(ImageMatrix), ImageOperations.GetHeight(ImageMatrix)];
            parents = sp.calculateShortestPath(point, graph.graph, ref distance); 
            lineDraw = true;
            Console.SetBufferSize(Int16.MaxValue - 1, Int16.MaxValue - 1);
            //for (int i = 0; i < ImageOperations.GetWidth(ImageMatrix); i++)
            //{
            //    for (int j = 0; j < ImageOperations.GetHeight(ImageMatrix); j++)
            //    {
            //        Console.WriteLine("i : " + i + "j : " + j + " " + parents[i, j] + " ");
            //        Console.WriteLine("i : " + i + "j : " + j + " " + distance[i, j] + " ");
            //    }
            //}
            //Console.WriteLine("Last Value = " + double.MaxValue);
            //FileStream sw = new FileStream("outputText.txt", FileMode.OpenOrCreate, FileAccess.Write);
            //StreamWriter swr = new StreamWriter(sw);
            //swr.WriteLine("Last Value = " + double.MaxValue);
            //for (int i = 0; i < parent.GetLength(0); i++)
            //{
            //    for (int j = 0; j < parent.GetLength(1); j++)
            //    {
            //        swr.Write(parent[i, j] + " ");

            //    }
            //    swr.Write("\n");
            //}

            //sw.Close();            
        }

        

        


        private void pictureBox1_MouseClick_1(object sender, MouseEventArgs e)
        {
            Point cp = PointToClient(Cursor.Position);
            Point point = new Point(cp.X - 17, cp.Y - 17);
            if (lineDraw == true)
            {
                Graphics graphics = Graphics.FromImage((Image)global_img);
                Pen redPen = new Pen(Color.Red, 1);
                ////Show the coordinates of the mouse click on the label, label1.
                path = Backtrack.backtrack(parents, point);
                Point tempPoint = rec_point;
                for (int i = path.Count-2; i >= 0; i -=2 )
                {
                   
                        
                    string si_Last = path[i].Substring(0, path[i].IndexOf(","));
                    int i_Last = int.Parse(si_Last);
                    string sj_Last = path[i].Substring(path[i].IndexOf(",") + 1);
                    int j_Last = int.Parse(sj_Last);
                    point = new Point(i_Last, j_Last);
                    //Rectangle rect = new Rectangle(point.X - 2, point.Y - 2, 1, 1);
                    graphics.DrawLine(redPen, tempPoint, point);
                    pictureBox1.Image = global_img;
                    tempPoint = point;
                    
                }
                //foreach (string s in path)
                //{
                //    if (!(s.Equals("null")) || !(string.IsNullOrEmpty(s)))
                //    {
                //        if (s.Equals("null") || string.IsNullOrEmpty(s))
                //        {
                //            continue;
                //        }
                //        Point tempPoint = point;
                //        string si_Last = s.Substring(0, s.IndexOf(","));
                //        int i_Last = int.Parse(si_Last);
                //        string sj_Last = s.Substring(s.IndexOf(",") + 1);
                //        int j_Last = int.Parse(sj_Last);
                //        point = new Point(i_Last, j_Last);
                //        //Rectangle rect = new Rectangle(point.X - 2, point.Y - 2, 1, 1);
                //        graphics.DrawLine(redPen, tempPoint, point);
                //        pictureBox1.Image = global_img;
                //    }

                //}

                pictureBox2.Image = global_img;
                lineDraw = false;
            }
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            Point cp = PointToClient(Cursor.Position);
            Point point = new Point(cp.X - 17, cp.Y - 17);
            Cursor_Y.Text = point.Y.ToString();
            Cursor_X.Text = point.X.ToString();
        }
    }
}