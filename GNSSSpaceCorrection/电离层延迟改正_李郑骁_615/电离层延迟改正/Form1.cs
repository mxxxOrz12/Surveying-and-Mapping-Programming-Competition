using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 电离层延迟改正
{
    public partial class Form1 : Form
    {
        DataCenter dataCenter = new DataCenter();
        FileHelper fileHelper = new FileHelper();
        Algorithm algorithm = new Algorithm();
        string Report = "";

        public Form1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 导入数据文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            fileHelper.LoadData(dataCenter);

            if (dataCenter.satellites.Count > 0)
            {
                dataGridView1.RowCount = dataCenter.satellites.Count;
                for (int i = 0; i < dataCenter.satellites.Count; i++)
                {
                    dataGridView1.Rows[i].Cells[0].Value = dataCenter.satellites[i].Prn;
                    dataGridView1.Rows[i].Cells[1].Value = dataCenter.satellites[i].X.ToString();
                    dataGridView1.Rows[i].Cells[2].Value = dataCenter.satellites[i].Y.ToString();
                    dataGridView1.Rows[i].Cells[3].Value = dataCenter.satellites[i].Z.ToString();

                }
                toolStripStatusLabel2.Text = "成功导入数据文件";
            }
        }

        /// <summary>
        /// 电离层延迟计算
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            algorithm.CalAE(dataCenter);
            for (int i = 0; i < dataCenter.satellites.Count; i++)
            {
                dataGridView1.Rows[i].Cells[4].Value = algorithm.Rad2Dms(dataCenter.satellites[i].E).ToString();
                dataGridView1.Rows[i].Cells[5].Value = algorithm.Rad2Dms(dataCenter.satellites[i].A).ToString();
            }
            algorithm.CalIonDely(dataCenter);
            for (int i = 0; i < dataCenter.satellites.Count; i++)
            {
                dataGridView1.Rows[i].Cells[6].Value = dataCenter.satellites[i].IonDely.ToString();

            }


            Report = "";
            for (int i = 0; i < dataCenter.satellites.Count; i++)
            {
                Report += dataCenter.satellites[i].Prn + "\t" + dataCenter.satellites[i].E.ToString("F3") + "\t" + dataCenter.satellites[i].A.ToString("F3") + "\t" + dataCenter.satellites[i].IonDely.ToString("F3") + "\r\n";
            }

            textBox1.Text = Report;

        }

        /// <summary>
        /// 保存计算报告
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            fileHelper.SaveReport(Report);

        }

    }
}
