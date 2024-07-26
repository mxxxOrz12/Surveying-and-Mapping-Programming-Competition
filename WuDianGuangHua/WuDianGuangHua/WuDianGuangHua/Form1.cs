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

namespace WuDianGuangHua
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dataGridView1.Rows[0].Height = 35;
        }

        //定义初始点集合
        List<MyPoint> myPoints = new List<MyPoint>();
        List<Curve> myCurves = new List<Curve>();
        bool isFit = false;
        public static string report;


        private void 打开文件ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog of = new OpenFileDialog();
            of.Filter = "txt|*.txt";

            if (of.ShowDialog() == DialogResult.OK)
            {

                try
                {
                    using (StreamReader sr = new StreamReader(of.FileName))
                    {
                        List<List<string>> str = new List<List<string>>(10);
                        string line;


                        while ((line = sr.ReadLine()) != null)
                        {
                            List<string> lineData = new List<string>(line.Split(','));
                            str.Add(lineData);
                        }


                        foreach (var list in str)
                        {
                            int rowIndex = dataGridView1.Rows.Add();
                            Console.WriteLine(rowIndex);
                            for (int colIndex = 0; colIndex < list.Count; colIndex++)
                            {
                                dataGridView1.Rows[rowIndex].Cells[colIndex].Value = list[colIndex];
                            }
                            double X = Convert.ToDouble(dataGridView1.Rows[rowIndex].Cells[1].Value);
                            double Y = Convert.ToDouble(dataGridView1.Rows[rowIndex].Cells[2].Value);
                            int ID = Convert.ToInt32(dataGridView1.Rows[rowIndex].Cells[0].Value);

                            MyPoint myPoint = new MyPoint(ID, X, Y);
                            myPoints.Add(myPoint);

                        }

                        //判断是否拟合
                        if (myPoints[0].X == myPoints[myPoints.Count - 1].X && myPoints[0].Y == myPoints[myPoints.Count - 1].Y)
                        {
                            isFit = true;
                        }


                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }



        }




        private void 首尾闭合拟合ToolStripMenuItem_Click(object sender, EventArgs e)
        {


            myCurves = BuildCurve.CalCurve(myPoints, true);

            report += "------------------计算结果-----------------\r\n";
            report += "\r\n";
            report += "------------------基本信息-----------------\r\n";
            report += "站点数" + myPoints.Count + "\r\n";
            report += "X边界：" + Math.Round(myPoints[0].X, 6) + "至" + Math.Round(myPoints[myPoints.Count - 1].X,6) + "\r\n";
            report += "Y边界：" + Math.Round(myPoints[0].Y, 6) + "至" + Math.Round(myPoints[myPoints.Count - 1].Y, 6) + "\r\n";
            report += "是否闭合：" + isFit+"\r\n";
            report += "------------------拟合曲线-----------------\r\n";



            report += string.Format("{0,-10}{1,-10:F3}{2,-10:F3}{3,-10}{4,-10:F3}{5,-10:F3}{6,-10:F3}{7,-10:F3}{8,-10:F3}{9,-10:F3}{10,-10:F3}{11,-10:F3}{12,-10:F3}{13,-10:F3}\r\n", "StartID","Start","StartY", "endID", "endX", "endY","E0","E1","E2","E3","F0","F1","F2","F3");

            foreach(var curve in myCurves)
            {
                report += string.Format("{0,-10}{1,-10:F3}{2,-10:F3}{3,-10}{4,-10:F3}{5,-10:F3}{6,-10:F3}{7,-10:F3}{8,-10:F3}{9,-10:F3}{10,-10:F3}{11,-10:F3}{12,-10:F3}{13,-10:F3}\r\n",
                    curve.StartPoint.ID, 
                    Math.Round(curve.StartPoint.X,3), 
                    Math.Round(curve.StartPoint.Y,3), 
                    curve.endPoint.ID,
                    Math.Round(curve.endPoint.X, 3),
                    Math.Round(curve.endPoint.Y, 3),
                    Math.Round(curve.E0,3), 
                    Math.Round(curve.E1, 3), 
                    Math.Round(curve.E2, 3), 
                    Math.Round(curve.E3, 3),
                    Math.Round(curve.F0, 3), 
                    Math.Round(curve.F1, 3),
                    Math.Round(curve.F2, 3), 
                    Math.Round(curve.F3, 3));
            }



            MessageBox.Show("计算成功");

        }

        private void 首尾不闭合拟合ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            myCurves = BuildCurve.CalCurve(myPoints, false);

            report += "------------------计算结果-----------------\r\n";
            report += "\r\n";
            report += "------------------基本信息-----------------\r\n";
            report += "站点数" + myPoints.Count + "\r\n";
            report += "X边界：" + Math.Round(myPoints[0].X, 6) + "至" + Math.Round(myPoints[myPoints.Count - 1].X, 6) + "\r\n";
            report += "Y边界：" + Math.Round(myPoints[0].Y, 6) + "至" + Math.Round(myPoints[myPoints.Count - 1].Y, 6) + "\r\n";
            report += "是否闭合：" + isFit + "\r\n";
            report += "------------------拟合曲线-----------------\r\n";



            report += string.Format("{0,-10}{1,-10:F3}{2,-10:F3}{3,-10}{4,-10:F3}{5,-10:F3}{6,-10:F3}{7,-10:F3}{8,-10:F3}{9,-10:F3}{10,-10:F3}{11,-10:F3}{12,-10:F3}{13,-10:F3}\r\n", "StartID", "Start", "StartY", "endID", "endX", "endY", "E0", "E1", "E2", "E3", "F0", "F1", "F2", "F3");

            foreach (var curve in myCurves)
            {
                report += string.Format("{0,-10}{1,-10:F3}{2,-10:F3}{3,-10}{4,-10:F3}{5,-10:F3}{6,-10:F3}{7,-10:F3}{8,-10:F3}{9,-10:F3}{10,-10:F3}{11,-10:F3}{12,-10:F3}{13,-10:F3}\r\n",
                    curve.StartPoint.ID,
                    Math.Round(curve.StartPoint.X, 3),
                    Math.Round(curve.StartPoint.Y, 3),
                    curve.endPoint.ID,
                    Math.Round(curve.endPoint.X, 3),
                    Math.Round(curve.endPoint.Y, 3),
                    Math.Round(curve.E0, 3),
                    Math.Round(curve.E1, 3),
                    Math.Round(curve.E2, 3),
                    Math.Round(curve.E3, 3),
                    Math.Round(curve.F0, 3),
                    Math.Round(curve.F1, 3),
                    Math.Round(curve.F2, 3),
                    Math.Round(curve.F3, 3));
            }


        }

        private void 计算报告ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            form2.ShowDialog();
        }

        private void 清空数据ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
        }
    }
}
