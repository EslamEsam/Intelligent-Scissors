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
        
        Point[] anchors = new Point[1000];
        int anchorsCounter = 0 , clicksCounter = 0;
        public List<string> path = new List<string>();

        private void pictureBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            clicksCounter++;
            Bitmap img = new Bitmap(FilePath);
            Point cp = PointToClient(Cursor.Position);
            Point point = new Point(cp.X - 17, cp.Y - 17);
            Console.WriteLine("Anchor X : " + point.X + "Anchor Y : " + point.Y);
            img.SetPixel((int)point.X, (int)point.Y, Color.Red);
            Graphics graphics = Graphics.FromImage((Image)global_img);
            Pen redPen = new Pen(Color.Red, 1);
            // Show the coordinates of the mouse click on the label, label1.
            Rectangle rect = new Rectangle(point.X , point.Y , 2, 2);
            anchors[anchorsCounter].X = point.X ;
            anchors[anchorsCounter].Y = point.Y ;
            anchorsCounter++;
            // Draw the rectangle, starting with the given coordinates, on the picture box.
            graphics.DrawRectangle(redPen, rect);
            // draw a line from the center of the rectangle to the mouse
            pictureBox1.Image = global_img;
            pictureBox2.Image = global_img;
            if (clicksCounter >= 2)
            {
                SPtest sp = new SPtest();
                Dictionary<string, KeyValuePair<string, double>> shortest_path = new Dictionary<string, KeyValuePair<string, double>>();
                shortest_path = sp.calculateShortestPath(anchors[anchorsCounter - 2], anchors[anchorsCounter - 1], graph.graph,
                ImageOperations.GetWidth(ImageMatrix), ImageOperations.GetHeight(ImageMatrix));
                graphics = Graphics.FromImage((Image)global_img);
                redPen = new Pen(Color.Blue, 1);

                int i_Free = anchors[anchorsCounter - 1].X;
                int j_Free = anchors[anchorsCounter - 1].Y;
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
                        pictureBox1.Image = global_img;
                        LastPoint = shortest_path[LastPoint].Key;
                        point = tempPoint;

                    }
                }
                Console.SetBufferSize(Int16.MaxValue - 1, Int16.MaxValue - 1);
            }
        }

        private void FinishCropping_Click(object sender, EventArgs e)
        {
            SPtest sp = new SPtest();
            Dictionary<string, KeyValuePair<string, double>> shortest_path = new Dictionary<string, KeyValuePair<string, double>>();
            shortest_path = sp.calculateShortestPath(anchors[0], anchors[anchorsCounter - 1], graph.graph,
            ImageOperations.GetWidth(ImageMatrix), ImageOperations.GetHeight(ImageMatrix));
            Graphics graphics = Graphics.FromImage((Image)global_img);
            Pen redPen = new Pen(Color.Red, 1);
            Point point = anchors[anchorsCounter - 1];
            int i_Free = anchors[anchorsCounter - 1].X;
            int j_Free = anchors[anchorsCounter - 1].Y;
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
                    pictureBox1.Image = global_img;
                    LastPoint = shortest_path[LastPoint].Key;
                    point = tempPoint;

                }
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