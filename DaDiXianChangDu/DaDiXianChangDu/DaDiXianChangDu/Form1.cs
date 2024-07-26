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

namespace DaDiXianChangDu
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
       
            Alogrithm.Rad2DDmmss(Alogrithm.DDmmss2Rad(31.23315));

        }

        List<DPoints> dPoints = new List<DPoints>();
        Ellipse ellipse;

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView1.Rows[0].Height = 30;
        }

        private void 打开文件ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog of = new OpenFileDialog();
            of.Filter = "txt|*.txt";
            List<List<string>> AllData = new List<List<string>>();
            if(of.ShowDialog() == DialogResult.OK)
            {
                using (StreamReader sr = new StreamReader(of.FileName))
                {
                    string[] f1Line;
                    f1Line = sr.ReadLine().Split(',');
                    ellipse = new Ellipse(Convert.ToDouble( f1Line[0]), Convert.ToDouble(f1Line[1]));

                    sr.ReadLine();
                    string line;
                    while ((line = sr.ReadLine()) != null )
                    {
                        List<string> lineData = new List<string>(line.Split(','));
                        AllData.Add(lineData);
                    }

                    foreach(var list in AllData)
                    {
                        int RowIndex = dataGridView1.Rows.Add();
                        for(int ColIndex = 0;ColIndex<list.Count;ColIndex++)
                        {
                            dataGridView1.Rows[RowIndex].Cells[ColIndex].Value = list[ColIndex];
                        }
                        string ID1 = Convert.ToString(dataGridView1.Rows[RowIndex].Cells[0].Value);
                        double B1 = Convert.ToDouble(dataGridView1.Rows[RowIndex].Cells[1].Value);
                        double L1 = Convert.ToDouble(dataGridView1.Rows[RowIndex].Cells[2].Value);
                        DPoints dpoint1 = new DPoints(ID1, B1, L1);
                        string ID2 = Convert.ToString(dataGridView1.Rows[RowIndex].Cells[3].Value);
                        double B2 = Convert.ToDouble(dataGridView1.Rows[RowIndex].Cells[4].Value);
                        double L2 = Convert.ToDouble(dataGridView1.Rows[RowIndex].Cells[5].Value);
                        DPoints dpoint2 = new DPoints(ID2, B2, L2);

                        dPoints.Add(dpoint1);
                        dPoints.Add(dpoint2);
                    }
                    MessageBox.Show("读取文件成功");



                }
            }
        }
        List<GeoLine> geoLines = new List<GeoLine>();
        private void 大地线长度计算ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //辅助计算
            //计算椭球基本参数
            Alogrithm.CalEllipese(ellipse);


            foreach(var point in dPoints)
            {
                point.B = Alogrithm.DDmmss2Rad(point.B);
                point.L = Alogrithm.DDmmss2Rad(point.L);

            }
            
            for(int i =0;i<dPoints.Count;i+=2)
            {
                GeoLine geoLine = new GeoLine();
                geoLine.u1 = Math.Atan(Math.Sqrt(1 - ellipse.e12) * Math.Tan(dPoints[i].B));
                geoLine.u2 = Math.Atan(Math.Sqrt(1 - ellipse.e12) * Math.Tan(dPoints[i+1].B));
                geoLine.l = dPoints[i+1].L - dPoints[i].L;
                geoLine.a1 =  Math.Sin(geoLine.u1) * Math.Sin(geoLine.u2);
                geoLine.a2 =  Math.Cos(geoLine.u1) * Math.Cos(geoLine.u2);
                geoLine.b1 =  Math.Cos(geoLine.u1) * Math.Sin(geoLine.u2);
                geoLine.b2 =  Math.Sin(geoLine.u1) * Math.Cos(geoLine.u2);



                geoLines.Add(geoLine);
            }

            //计算起点大地方位角
            foreach(var geoline in geoLines)
            {
                double delta1 = 0;

                geoline.delta = 0;
       
                do
                {
                    delta1 = geoline.delta;
                    double lamda = geoline.l + geoline.delta;
                    double p = Math.Cos(geoline.u2) * Math.Sin(lamda);
                    double q = geoline.b1 - geoline.b2 * Math.Cos(lamda);
                    geoline.A1 = Alogrithm.FangWei(p, q);

                    double Sinsigema = p * Math.Sin(geoline.A1) + q * Math.Cos(geoline.A1);
                    double Cossigema = geoline.a1 + geoline.a2 * Math.Cos(lamda);
    
                    if(Cossigema>0)
                    {
                        geoline.sigema = Math.Abs(geoline.sigema);
                    }
                    else
                    {
                        geoline.sigema = Math.PI - Math.Abs(geoline.sigema);
                    }
                    geoline.sigema = Math.Atan(Sinsigema / Cossigema);


                    geoline.sinA0 = Math.Cos(geoline.u1) * Math.Sin(geoline.A1);
                    geoline.cos2A0 = 1 - Math.Pow(geoline.sinA0, 2);
                    geoline.sigema1 = Math.Atan(Math.Tan(geoline.u1) / Math.Cos(geoline.A1));
                    double e4 = Math.Pow(ellipse.e12, 2);
                    double e6 = Math.Pow(ellipse.e12, 3);
                    geoline.alpha = (ellipse.e12 / 2 + e4 / 8 + e6 / 16) - (e4 / 16 + e6 / 16) * geoline.cos2A0 + (3 * e6 / 128) * Math.Pow(geoline.cos2A0, 2);
                    geoline.beita = (e4 / 16 + e6 / 16) * geoline.cos2A0 - (e6 / 32) * Math.Pow(geoline.cos2A0, 2);
                    geoline.gama = (e6 / 256) * Math.Pow(geoline.cos2A0, 2);

                    geoline.delta = (geoline.alpha * geoline.sigema + geoline.beita * Math.Cos(geoline.sigema1 * 2 + geoline.sigema) * Math.Sin(geoline.sigema) + geoline.gama * Math.Sin(2 * geoline.sigema) * Math.Cos(4 * geoline.sigema1 + 2 * geoline.sigema)) * geoline.sinA0;
                    geoline.lamda1 = geoline.l + geoline.delta;
                }while (Math.Abs(geoline.delta - delta1) > 0.000000001);
            }

   


            //计算大地线长度
            foreach(var geoline in geoLines)
            {
                geoline.k2 = ellipse.e22 * geoline.cos2A0;
                double k2 = geoline.k2;
                double k4 = Math.Pow(geoline.k2, 2);
                double k6 = Math.Pow(geoline.k2, 3);
                geoline.A = (1 - (k2 / 4) + 7 * k4 / 64 - 15 * k6 / 256) / ellipse.b;
                geoline.B = k2 / 4 - (k4 / 8) +( 37 * k6 / 512);
                geoline.C = k4 / 128 - k6 / 128;

                geoline.sigema1 = Math.Atan(Math.Tan(geoline.u1) / Math.Cos(geoline.A1));
                double Xs = geoline.C * Math.Sin(2 * geoline.sigema) * Math.Cos(4 * geoline.sigema1 + 2 * geoline.sigema);
                geoline.S = (geoline.sigema - geoline.B * Math.Sin(geoline.sigema) * Math.Cos(2 * geoline.sigema1 + geoline.sigema) - Xs) / geoline.A;

            }

            Console.WriteLine(geoLines[0].C.ToString("F8"));


            MessageBox.Show("大地线长度计算成功");



        }

        private void 打开报告ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            form2.ShowDialog();
        }

        private void 清除数据ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
        }
    }
}
