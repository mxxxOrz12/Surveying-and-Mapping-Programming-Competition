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

namespace GNSSSpace
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public static string report;
        SateTime SateTime = new SateTime();
        List<Satellite> Satellites = new List<Satellite>();


        private void Form1_Load(object sender, EventArgs e)
        {
            dataGridView1.Rows[0].Height = 30;
        }

        private void 打开文件ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog of = new OpenFileDialog();
            of.Filter = "txt|*.txt";
            if (of.ShowDialog() == DialogResult.OK)
            {
               
                using (StreamReader sr = new StreamReader(of.FileName))
                {

                    List<List<string>> str = new List<List<string>>();
                        
                    string line;
                    string[] time;
                    line = sr.ReadLine();
                    time = line.Split(new char[] { ' '},StringSplitOptions.RemoveEmptyEntries);
                    SateTime.Year = Convert.ToInt32(time[1]);
                    SateTime.Month = Convert.ToInt32(time[2]);
                    SateTime.Day = Convert.ToInt32(time[3]);
                    SateTime.Hour = Convert.ToInt32(time[4]);
                    SateTime.Minute = Convert.ToInt32(time[5]);
                    SateTime.Second = Convert.ToDouble(time[6]);

                    textBox1.Text = line.Substring(1,30);


                    while ((line = sr.ReadLine()) != null)
                    {
                        List<string> DataLine = new List<string>(line.Split(new char[] {' ' },StringSplitOptions.RemoveEmptyEntries));
                        str.Add(DataLine);
                    }

                    foreach(var list in str)
                    {
                        int rowIndex = dataGridView1.Rows.Add();
                        for(int colIndex = 0;colIndex<list.Count;colIndex++)
                        {
                            dataGridView1.Rows[rowIndex].Cells[colIndex].Value = list[colIndex];
                        }
                        string ID = Convert.ToString(dataGridView1.Rows[rowIndex].Cells[0].Value);
                        double X = Convert.ToDouble(dataGridView1.Rows[rowIndex].Cells[1].Value);
                        double Y = Convert.ToDouble(dataGridView1.Rows[rowIndex].Cells[2].Value);
                        double Z = Convert.ToDouble(dataGridView1.Rows[rowIndex].Cells[3].Value);

                        Satellite satellite = new Satellite(ID, X, Y, Z);
                        Satellites.Add(satellite);

                    }

                }

          
            }
        
        }

        private void 电离层延迟计算ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach(var satellite in Satellites)
            {
                Algorithm.PositionTransFrom(satellite);
                Algorithm.CalGeomLatitude(satellite, SateTime);
            }

            report += "---------------------------计算结果---------------------------\r\n";
            report += "\r\n";
            report += string.Format("{0,-14}{1,-14}{2,-14}{3,-14}\r\n","ID", "Height-E","Fangwei-A","IonDelay");
            foreach (var satellite in  Satellites)
            {
                double satE =  Algorithm.Rad2DDmmss(satellite.E);
                double satA = Algorithm.Rad2DDmmss(satellite.A);
                report += string.Format("{0,-14}{1,-14}{2,-14}{3,-14}\r\n", satellite.ID,satE,satA,Math.Round(satellite.IonDelay,3));
            }
            MessageBox.Show("计算成功");

        }

        private void 电离层改正报告ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            form2.ShowDialog();

        }

        private void 对流层延迟计算ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3();
            form3.ShowDialog();
        }
    }
}
