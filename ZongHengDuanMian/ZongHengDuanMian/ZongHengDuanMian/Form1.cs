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

namespace ZongHengDuanMian
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
            report += "-----------------计算结果-----------------\r\n";
            report += "\r\n";
        }

        private void 打开报告ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2  form2= new Form2();
            form2.ShowDialog();
        }

        List<List<string>> AllData = new List<List<string>>();
        //参考高程
        public static double H0;
        static List<ZHPoints> KeyPoints = new List<ZHPoints>();
        static List<ZHPoints> TestPoints = new List<ZHPoints>();
        static List<ZHPoints> SanDianPoints = new List<ZHPoints>();
        public static string report;
        //第一条纵断面内插点
        List<ZHPoints> F1ZHPoints = new List<ZHPoints>();
        List<ZHPoints> F2ZHPoints = new List<ZHPoints>();

        private void 打开文件ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog of = new OpenFileDialog();
            of.Filter = "txt|*.txt";
            if(of.ShowDialog() == DialogResult.OK)
            {
                using (StreamReader sr = new StreamReader(of.FileName))
                {
             
                    H0 = Convert.ToDouble(sr.ReadLine().Split(',')[1]);

                    //读取关键点点号
                    string[] SecondLine = sr.ReadLine().Split(',');

                    ZHPoints K0 = new ZHPoints(SecondLine[0]);
                    ZHPoints K1 = new ZHPoints(SecondLine[1]);
                    ZHPoints K2 = new ZHPoints(SecondLine[2]);

                    string[] pointa = sr.ReadLine().Split(',');
                    string[] pointb = sr.ReadLine().Split(',');

                    //读取A，B
                    ZHPoints PointA = new ZHPoints(pointa[0],Convert.ToDouble(pointa[1]), Convert.ToDouble(pointa[2]));
                    ZHPoints PointB = new ZHPoints(pointb[0], Convert.ToDouble(pointb[1]), Convert.ToDouble(pointb[2]));
                    TestPoints.Add(PointA);
                    TestPoints.Add(PointB);


                    sr.ReadLine();

                    string line;

                    while((line = sr.ReadLine()) != null )
                    {
                        List<string> lineData = new List<string>(line.Split(','));
                        AllData.Add(lineData);
                    }

                    foreach(var list in AllData)
                    {
                        int RowIndex = dataGridView1.Rows.Add();
                        for(int ColIndex = 0;ColIndex < list.Count;ColIndex++)
                        {
                            dataGridView1.Rows[RowIndex].Cells[ColIndex].Value = list[ColIndex];
                        }
                        string ID =  Convert.ToString(dataGridView1.Rows[RowIndex].Cells[0].Value);
                        double X = Convert.ToDouble(dataGridView1.Rows[RowIndex].Cells[1].Value);
                        double Y = Convert.ToDouble(dataGridView1.Rows[RowIndex].Cells[2].Value);
                        double H = Convert.ToDouble(dataGridView1.Rows[RowIndex].Cells[3].Value);

                        ZHPoints zHPoints = new ZHPoints(ID, X, Y, H);
                        SanDianPoints.Add(zHPoints);
                    }
                }
                foreach(var list in AllData)
                {
                    if(list[0] == "K0" || list[0] == "K1" || list[0] == "K2" )
                    {
                        string ID = list[0];
                        double X = Convert.ToDouble(list[1]);
                        double Y = Convert.ToDouble(list[2]);
                        double H = Convert.ToDouble(list[3]);


                        ZHPoints KeyPoint = new ZHPoints(ID, X, Y, H);
                        KeyPoints.Add(KeyPoint);
                    }
                }

            }
            MessageBox.Show("打开文件成功");
        }

        private void 清空数据ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
        }
        double A01;
        double A12;

        private void 纵断面计算ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            report += "-----------------测试点高程计算-----------------\r\n";

            SanDianPoints.Add(TestPoints[0]);
            Alogrithm.CalLeastFivePoints(SanDianPoints);
            Alogrithm.CalPointsHeight(TestPoints[0]);
            report += TestPoints[0].ID + "   :  " + Math.Round(TestPoints[0].H, 3) + "\r\n";
            SanDianPoints.RemoveAt(SanDianPoints.Count - 1);

            SanDianPoints.Add(TestPoints[1]);
            Alogrithm.CalLeastFivePoints(SanDianPoints);
            Alogrithm.CalPointsHeight(TestPoints[1]);
            report += TestPoints[1].ID + "   :  " + Math.Round(TestPoints[1].H, 3) + "\r\n";
            SanDianPoints.RemoveAt(SanDianPoints.Count - 1);

            //计算AB方位角
            report+= "A和B之间方位角："+ Math.Round( Alogrithm.FangWei((TestPoints[1].Y - TestPoints[0].Y), (TestPoints[1].X - TestPoints[0].X)),5) + "\r\n";


            //计算AB梯形面积
            report += "AB梯形面积:" +  Alogrithm.CalDuanMianArea(TestPoints[1], TestPoints[0]).ToString("F3") + "\r\n";

            //计算关键点之间距离
            double K0_1 = Math.Round(Alogrithm.CalDistance(KeyPoints[0], KeyPoints[1]),3);
            double K1_2 = Math.Round(Alogrithm.CalDistance(KeyPoints[1], KeyPoints[2]),3);


            //方位角
             A01 = Math.Round(Alogrithm.FangWei(KeyPoints[1].Y - KeyPoints[0].Y, KeyPoints[1].X - KeyPoints[0].X ),5);
             A12 = Math.Round (Alogrithm.FangWei(KeyPoints[2].Y - KeyPoints[1].Y, KeyPoints[2].X - KeyPoints[1].X),5);

            //纵断面内插
            double Li = 10;
            int i = 1;
            while( Li<K0_1)
            {
                double Xi = KeyPoints[0].X + Li * Math.Cos(A01);
                double Yi = KeyPoints[0].Y + Li * Math.Sin(A01);
                string ID = "Z"+i;
                Li += 10;
                i++;
                ZHPoints p1 = new ZHPoints(ID, Xi, Yi);
                F1ZHPoints.Add(p1);
            }
            foreach(var point in F1ZHPoints)
            {
                SanDianPoints.Add(point);
                Alogrithm.CalLeastFivePoints(SanDianPoints);
                Alogrithm.CalPointsHeight(point);
                SanDianPoints.RemoveAt(SanDianPoints.Count - 1);
            }

  
            i = 1;
            while (Li<K1_2 + K0_1)
            {
                double Xi = KeyPoints[1].X + (Li-  K0_1) * Math.Cos(A12);
                double Yi = KeyPoints[1].Y + (Li - K0_1) * Math.Sin(A12);
                string ID = "Y" + i;
                Li += 10;
                i++;
                ZHPoints p1 = new ZHPoints(ID, Xi, Yi);
                F2ZHPoints.Add(p1);
            }
            foreach (var point in F2ZHPoints)
            {
                SanDianPoints.Add(point);
                Alogrithm.CalLeastFivePoints(SanDianPoints);
                Alogrithm.CalPointsHeight(point);
                SanDianPoints.RemoveAt(SanDianPoints.Count - 1);
            }
            double S1 = 0;
            F1ZHPoints.Insert(0, KeyPoints[0]);
            F1ZHPoints.Add(KeyPoints[1]);
            for(int j =0;j<F1ZHPoints.Count-1;j++)
            {
                S1 += Alogrithm.CalDuanMianArea(F1ZHPoints[j], F1ZHPoints[j + 1]);

            }
            double S2 = 0;
            F2ZHPoints.Insert(0, KeyPoints[1]);
            F2ZHPoints.Add(KeyPoints[2]);
            for (int j = 0; j < F2ZHPoints.Count - 1; j++)
            {
                S2 += Alogrithm.CalDuanMianArea(F2ZHPoints[j], F2ZHPoints[j + 1]);

            }
            string S1s = S1.ToString("F3");
            string S2s = S1.ToString("F3");
            double Szong = S1 + S2;


            MessageBox.Show("纵断面计算成功");
          
        }

        List<ZHPoints> HengKeyPoints = new List<ZHPoints>();
        List<ZHPoints> H1ZHPoints = new List<ZHPoints>();
        List<ZHPoints> H2ZHPoints = new List<ZHPoints>();

        private void 横断面计算ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            double M0X = (KeyPoints[0].X + KeyPoints[1].X) / 2;
            double M0Y = (KeyPoints[0].Y + KeyPoints[1].Y) / 2;
            ZHPoints M0 = new ZHPoints("M0", M0X, M0Y);

            double M1X = (KeyPoints[1].X + KeyPoints[2].X) / 2;
            double M1Y = (KeyPoints[1].Y + KeyPoints[2].Y) / 2;
            ZHPoints M1 = new ZHPoints("M0", M1X, M1Y);

            double AM0 = A01 + 90 * Math.PI / 180;
            double AM1 = A12 + 90 * Math.PI / 180;

            double Delta = 5;
            int p = 1;
            for(int i = -5;i<=5;i++)
            {
                double Xj = M0X + i*Delta * Math.Cos(AM0);
                double Yj = M0Y+ i * Delta * Math.Sin(AM0);
                string ID = "Q" + p;
                p++;
                ZHPoints p1 = new ZHPoints(ID, Xj, Yj);
                H1ZHPoints.Add(p1);
            }
            p = 1;
            for (int i = -5; i <= 5; i++)
            {
                double Xj = M1X + i * Delta * Math.Cos(AM1);
                double Yj = M1Y + i * Delta * Math.Sin(AM1);
                string ID = "W" + p;
                p++;
                ZHPoints p1 = new ZHPoints(ID, Xj, Yj);
                H2ZHPoints.Add(p1);
            }
            foreach (var point in H1ZHPoints)
            {
                SanDianPoints.Add(point);
                Alogrithm.CalLeastFivePoints(SanDianPoints);
                Alogrithm.CalPointsHeight(point);
                SanDianPoints.RemoveAt(SanDianPoints.Count - 1);
            }
            foreach (var point in H2ZHPoints)
            {
                SanDianPoints.Add(point);
                Alogrithm.CalLeastFivePoints(SanDianPoints);
                Alogrithm.CalPointsHeight(point);
                SanDianPoints.RemoveAt(SanDianPoints.Count - 1);
            }

            double S1 = 0;
            for (int j = 0; j < H1ZHPoints.Count - 1; j++)
            {
                S1 += Alogrithm.CalDuanMianArea(H1ZHPoints[j], H1ZHPoints[j + 1]);

            }
            double S2 = 0;
            for (int j = 0; j < H2ZHPoints.Count - 1; j++)
            {
                S2 += Alogrithm.CalDuanMianArea(H2ZHPoints[j], H2ZHPoints[j + 1]);

            }
            double Szong = S1 + S2;

            MessageBox.Show("纵断面计算成功");

        }
    }
}
