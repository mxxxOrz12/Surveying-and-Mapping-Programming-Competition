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
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        public static string report;

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Form3_Load(object sender, EventArgs e)
        {
            dataGridView1.Rows[0].Height = 30;
        }

        List<TroSatellite> troSatellites = new List<TroSatellite>();

        List<List<string>> AllData = new List<List<string>>();


        private void 打开对流层数据ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog of = new OpenFileDialog();
            of.Filter = "txt|*.txt";
         
            if(of.ShowDialog() == DialogResult.OK)
            {
                using (StreamReader sr = new StreamReader(of.FileName,Encoding.Default))
                {
                    string line;
                    sr.ReadLine();

                    while((line = sr.ReadLine())!= null)
                    {
                        List<string> DataLine = new List<string>(line.Split(','));
                        AllData.Add(DataLine);
                    }

                    foreach(var list in AllData)
                    {
                        int RowIndex = dataGridView1.Rows.Add();
                        for(int ColIndex = 0;ColIndex<list.Count;ColIndex ++)
                        {
                            dataGridView1.Rows[RowIndex].Cells[ColIndex].Value = list[ColIndex];
                        }
                        string ID = Convert.ToString( dataGridView1.Rows[RowIndex].Cells[0].Value);
                        string Time = Convert.ToString(dataGridView1.Rows[RowIndex].Cells[1].Value);
                        double Lon = Convert.ToDouble(dataGridView1.Rows[RowIndex].Cells[2].Value);
                        double Lat = Convert.ToDouble(dataGridView1.Rows[RowIndex].Cells[3].Value);
                        double H = Convert.ToDouble(dataGridView1.Rows[RowIndex].Cells[4].Value);
                        double E = Convert.ToDouble(dataGridView1.Rows[RowIndex].Cells[5].Value);

                        TroSatellite troSatellite = new TroSatellite(ID, Time, Lon, Lat, H, E);
                        troSatellites.Add(troSatellite);

                    }
                }
            }
        }

        private void 对流层改正计算ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach(var troSatellite in troSatellites)
            {
                Troposphere.CalculateWet(troSatellite);
                Troposphere.CalculateDry(troSatellite);
                Troposphere.CalculateTroDelay(troSatellite);
            }

            report += "----------------------------对流层改正计算报告----------------------------\r\n";
            report += "\r\n";
            report += string.Format("{0,-10}{1,-10}{2,-10}{3,-10}{4,-10}{5,-10}{6,-10}\r\n", "ID", "E", "ZHD", "M_D","ZWD","M_W","TroDelay");
            foreach(var sate in troSatellites)
            {
                report += string.Format("{0,-10}{1,-10:F2}{2,-10:F3}{3,-10:F3}{4,-10:F3}{5,-10:F3}{6,-10:F3}\r\n", sate.ID, sate.E, Math.Round(sate.ZHD,3), Math.Round(sate.M_d,3), Math.Round(sate.ZWD,3), Math.Round(sate.M_w,3), Math.Round(sate.DeltaS,3));
            }
            MessageBox.Show("对流层计算完毕");
        }

        private void 打开报告ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form4 form4 = new Form4();
            form4.ShowDialog();
        }

        private void 清空数据ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
        }
    }
}
