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

        private void btnOpen_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                //Open the browsed image and display it
                string OpenedFilePath = openFileDialog1.FileName;
                ImageMatrix = ImageOperations.OpenImage(OpenedFilePath);
                Graph graph = new Graph();
                graph.Add_vertices(ImageMatrix);
                graph.Add_edges(ImageMatrix);
                for (int i = 0; i < graph.graph.Count; i++)
                {
                    for (int j = 0; j < graph.graph.Values.Count; j++)
                    {
                        string key = i.ToString() + "," + j.ToString();
                        if (graph.graph.ContainsKey(key))
                        {
                            foreach (var item in graph.graph[key])
                            {
                               
                                Console.WriteLine("Node : " + i);
                                Console.WriteLine("Vertex : " + key + "Edge : " + item.edge);
                                Console.WriteLine("Vertex : " + key + "weight : " + item.weight);


                            }
                        }
                    }
                }
                ImageOperations.DisplayImage(ImageMatrix, pictureBox1);
            }
            txtWidth.Text = ImageOperations.GetWidth(ImageMatrix).ToString();
            txtHeight.Text = ImageOperations.GetHeight(ImageMatrix).ToString();
        }

        private void btnGaussSmooth_Click(object sender, EventArgs e)
        {
            double sigma = double.Parse(txtGaussSigma.Text);
            int maskSize = (int)nudMaskSize.Value ;
            ImageMatrix = ImageOperations.GaussianFilter1D(ImageMatrix, maskSize, sigma);
            ImageOperations.DisplayImage(ImageMatrix, pictureBox2);
        }

       
       
    }
}