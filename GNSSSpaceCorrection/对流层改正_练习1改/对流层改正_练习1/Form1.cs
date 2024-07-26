using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 对流层改正_练习1
{
    public partial class Form1 : Form
    {
        Datacenter datacenter=new Datacenter ();
        FileHelper fileHelper = new FileHelper();
        Algorithm algorithm = new Algorithm();
        string report="";
        public Form1()
        {
            InitializeComponent();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            try
            {
                fileHelper.Open_File(ref datacenter);
                dataGridView1.RowCount = datacenter.stations.Count;
                L_label.Text = "已导入";
            }
            catch(Exception)
            {
                MessageBox.Show("数据读取失败");
            }
            for(int i=0;i< datacenter.stations.Count;i++)
            {
                dataGridView1[0, i].Value = datacenter.stations[i].name;
                dataGridView1[1, i].Value = datacenter.stations[i].T;
                dataGridView1[2, i].Value = datacenter.stations[i].L.ToString("F3");
                dataGridView1[3, i].Value = datacenter.stations[i].B.ToString("F3");
                dataGridView1[4, i].Value = datacenter.stations[i].H.ToString("F3");
                dataGridView1[5, i].Value = datacenter.stations[i].E.ToString("F3");
            }
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            if(L_label.Text =="未导入")
            {
                MessageBox.Show("请先导入数据");
                return;
            }
            algorithm.Let_all(datacenter);
            C_label.Text = "已计算";
            report += "-------------------计算结果-------------------\n";
            report += "测站名\t" + "ZHD\t"+"  md\t" + "  mw\t"+"  ΔS\t\n";
            foreach(var sta in datacenter .stations )
            {
                report += sta.name + "\t" + sta.ZHD.ToString("F3") + "\t" + sta.md.ToString("F3") + "\t" + sta.mw.ToString("F3") + "\t"+sta.deta_S.ToString("F3") + "\t\n";
            }
            richTextBox1.Text = report;
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            if (C_label.Text == "未计算")
            {
                MessageBox.Show("请先计算数据");
                return;
            }
            try
            {
                fileHelper.Save_File(report);
            }
            catch(Exception )
            {
                MessageBox.Show("保存数据失败");
            }
        }
    }
}
