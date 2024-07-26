using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace SpatialAnalysis
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dataGridView1.Rows[0].Height = 30;
        }

        public static string report;
        List<SpatialPoint> spatialPoints = new List<SpatialPoint>();
        Ellipse ellipse = new Ellipse();
        //空间权重矩阵
        List<List<double>> SpatialMatrix = new List<List<double>>();
        //局部莫兰指数
        List<double> LocalMoran = new List<double>();


        private void 打开数据ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog of = new OpenFileDialog();
            of.Filter = "txt|*.txt";

            //创建双重列表读取文件
            List<List<string>> DataFile = new List<List<string>>();
            if(of.ShowDialog() == DialogResult.OK)
            {

                using (StreamReader sr = new StreamReader(of.FileName))
                {
                    string line;
                    sr.ReadLine();
                    while((line = sr.ReadLine()) != null)
                    {
                        //保留一行内容信息，通过，进行分割，初始化dataline
                        List<string> dataline = new List<string>(line.Split(new char[] {','}));
                        DataFile.Add(dataline);
                    }

                    //遍历数据，填充到数据框中
                    foreach(var list in DataFile)
                    {
                        int RowIndex = dataGridView1.Rows.Add();
                        for(int ColIndex = 0;ColIndex<list.Count;ColIndex++)
                        {
                            dataGridView1.Rows[RowIndex].Cells[ColIndex].Value = list[ColIndex];
                        }
                        int ID = Convert.ToInt16(dataGridView1.Rows[RowIndex].Cells[0].Value);
                        double X = Convert.ToDouble(dataGridView1.Rows[RowIndex].Cells[1].Value);
                        double Y = Convert.ToDouble(dataGridView1.Rows[RowIndex].Cells[2].Value);
                        double Value = Convert.ToDouble(dataGridView1.Rows[RowIndex].Cells[3].Value);

                        //定义点变量，对数据存储，对空间点列表进行赋值
                        SpatialPoint spatialPoint = new SpatialPoint(ID, X, Y, Value);
                        spatialPoints.Add(spatialPoint);
                    }

                    //初始化空间权重矩阵
                    foreach(var list in spatialPoints)
                    {
                        List<double> lineMatrix = new List<double>();
                        foreach (var point in spatialPoints)
                        {
                            double Wij = 0.0;
                            lineMatrix.Add(Wij);
                        }
                        SpatialMatrix.Add(lineMatrix);
                    }

                }
            }
            MessageBox.Show("文件打开成功");
        }

        private void 计算标准差椭圆ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Alogrithm.CalEllipse(spatialPoints, ellipse);
            report += "------------------------标准差椭圆计算结果------------------------\r\n";
            report += "\r\n";
            report += "标准差椭圆θ：" + Math.Round(Alogrithm.Rad2Du(ellipse.Theta), 4)+"\r\n";
            report += "标准差椭圆长半轴X：" + Math.Round(ellipse.Half_X, 6) + "\r\n";
            report += "标准差椭圆短半轴：" + Math.Round(ellipse.Half_Y, 6) + "\r\n";

            MessageBox.Show("标准差椭圆计算成功");

        }

        private void 打开报告ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            form2.ShowDialog();
        }


        private void 计算全局莫兰指数ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            double MoranI;
            //计算空间权重矩阵
            Alogrithm.CalSpatialMatrix(spatialPoints, SpatialMatrix);
            MoranI =  Alogrithm.CalGlobalMoran(spatialPoints, SpatialMatrix);

            report += "------------------------全局莫兰指数计算结果------------------------\r\n";
            report += "\r\n";
            report += "全局莫兰指数：" + Math.Round(MoranI, 6) + "\r\n";

            MessageBox.Show("全局莫兰指数计算成功");

        }

        private void 计算局部莫兰指数ToolStripMenuItem_Click(object sender, EventArgs e)
        {
          
            Alogrithm.CalSpatialMatrix(spatialPoints, SpatialMatrix);
            LocalMoran =  Alogrithm.CalLocalMoran(spatialPoints, SpatialMatrix);



            MessageBox.Show("局部莫兰指数计算成功");


        }
    }
}
