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
        }


       

        private void pictureBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Bitmap img = new Bitmap(FilePath);
            Point cp = PointToClient(Cursor.Position);
            Point point = new Point(cp.X-17,cp.Y-17);
            Console.WriteLine("X : " + point.X + " Y : " + point.Y);
            img.SetPixel((int)point.X, (int)point.Y, Color.Red);
            double[,] distance = new double[ImageOperations.GetHeight(ImageMatrix), ImageOperations.GetWidth(ImageMatrix)];
            ShortestPath sp = new ShortestPath();
            pictureBox2.Image = img;
            string[,] parent = new string[ImageOperations.GetHeight(ImageMatrix), ImageOperations.GetWidth(ImageMatrix)];
            parent = sp.calculateShortestPath(point, graph.graph, ref distance);
            for (int i = 0; i < parent.GetLength(0); i++)
            {
                for (int j = 0; j < parent.GetLength(1); j++)
                {
                    Console.WriteLine("Parent : " + parent[i, j]);
                    //Console.WriteLine("Distance : " + distance[i, j]);
                }
            }
            Console.WriteLine("Last Value = " + double.MaxValue);
            FileStream sw = new FileStream("outputText.txt", FileMode.OpenOrCreate, FileAccess.Write);
            StreamWriter swr = new StreamWriter(sw);
            swr.WriteLine("Last Value = " + double.MaxValue);
            for (int i = 0; i < parent.GetLength(0); i++)
            {
                for (int j = 0; j < parent.GetLength(1); j++)
                {
                    swr.Write(parent[i, j] + " ");
                    
                }
                swr.Write("\n");
            }
                
            sw.Close();
            
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            Point cp = PointToClient(Cursor.Position);
            Point point = new Point(cp.X-17, cp.Y-17);
            Cursor_Y.Text = point.Y.ToString();
            Cursor_X.Text = point.X.ToString();
        }

        
    }
}