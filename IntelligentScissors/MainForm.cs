using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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
        private void btnOpen_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                //Open the browsed image and display it
                string OpenedFilePath = openFileDialog1.FileName;
                ImageMatrix = ImageOperations.OpenImage(OpenedFilePath);
                FilePath = OpenedFilePath;
                //Graph graph = new Graph();
                //graph.Add_vertices(ImageMatrix);
                //graph.Add_edges(ImageMatrix);
                //int samar = 0;
                //for (int i = 0; i < graph.graph.Count; i++)
                //{
                //    for (int j = 0; j < graph.graph.Values.Count; j++)
                //    {
                //        string key = i.ToString() + "," + j.ToString();
                //        if (graph.graph.ContainsKey(key))
                //        {
                //            foreach (var item in graph.graph[key])
                //            {

                //                Console.WriteLine("Vertex : " + key + "Edge : " + item.edge);
                //                Console.WriteLine("Vertex : " + key + "weight : " + item.weight);

                //            }
                //            Console.WriteLine("Node : " + samar);
                //            samar++;
                //        }
                //    }
                //}
                ImageOperations.DisplayImage(ImageMatrix, pictureBox1);
            }
            txtWidth.Text = ImageOperations.GetWidth(ImageMatrix).ToString();
            txtHeight.Text = ImageOperations.GetHeight(ImageMatrix).ToString();
        }


       

        private void pictureBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Console.WriteLine("Clicked");
            Bitmap img = new Bitmap(FilePath);
            Console.WriteLine("Clicked");
            Point cp = PointToClient(Cursor.Position);
            Point point = new Point(cp.X-17,cp.Y-17);
            Console.WriteLine("X : " + point.X + " Y : " + point.Y);
            img.SetPixel((int)point.X, (int)point.Y, Color.Red);
            pictureBox2.Image = img;
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