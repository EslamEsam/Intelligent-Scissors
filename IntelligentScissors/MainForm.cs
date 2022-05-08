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
            Console.WriteLine("X : " + point.X + " Y : " + point.Y);
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

            pictureBox2.Image = global_img;
            lineDraw = true;

            double[,] distance = new double[ImageOperations.GetHeight(ImageMatrix), ImageOperations.GetWidth(ImageMatrix)];
            //ShortestPath sp = new ShortestPath();
            SP sp = new SP();
            pictureBox2.Image = img;
            string[,] parent = new string[ImageOperations.GetHeight(ImageMatrix), ImageOperations.GetWidth(ImageMatrix)];
            parent = sp.calculateShortestPath(point, graph.graph, ref distance);
            parents = parent;
            Console.SetBufferSize(Int16.MaxValue - 1, Int16.MaxValue - 1);
            for (int i = 0; i < ImageOperations.GetHeight(ImageMatrix); i++)
            {
                for (int j = 0; j < ImageOperations.GetWidth(ImageMatrix); j++)
                {
                    Console.WriteLine("i : " + i + "j : " + j + " " + parent[i, j] + " ");
                }
            }
        }

    

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            Point cp = PointToClient(Cursor.Position);
            Point point = new Point(cp.X - 17, cp.Y - 17);
            Cursor_Y.Text = point.Y.ToString();
            Cursor_X.Text = point.X.ToString();
            if (lineDraw == true)
            {

                Graphics graphics = Graphics.FromImage((Image)global_img);
                Pen redPen = new Pen(Color.Red, 1);
                //Show the coordinates of the mouse click on the label, label1.
                path = Backtrack.backtrack(parents, point);
                foreach (string s in path)
                {
                    if (s != "null")
                    {
                        Point tempPoint = point;
                        string si_Last = s.Substring(0, s.IndexOf(","));
                        int i_Last = int.Parse(si_Last);
                        string sj_Last = s.Substring(s.IndexOf(",") + 1);
                        int j_Last = int.Parse(sj_Last);
                        point = new Point(i_Last, j_Last);
                        graphics.DrawLine(redPen, tempPoint, point);
                        pictureBox1.Image = global_img;
                    }

                }
                //double lowest = 1000000;
                //int i_lowest = 0;
                //int j_lowest = 0;
                //for (int i = 0; i < ImageOperations.GetHeight(ImageMatrix); i++)
                //{
                //    for (int j = 0; j < ImageOperations.GetWidth(ImageMatrix); j++)
                //    {
                //        if (parents[i, j] < lowest)
                //        {
                //            lowest = parents[i, j];
                //            i_lowest = i;
                //            j_lowest = j;
                //        }
                //    }
                //}
                //while (e.Clicks == 2)
                //{

                //}
                //Point tempPoint = new Point(i_lowest, j_lowest);
                pictureBox2.Image = global_img;
                lineDraw = false;
            }
        }


    }
}