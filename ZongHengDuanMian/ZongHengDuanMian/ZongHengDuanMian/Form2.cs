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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form2_Load(object sender, EventArgs e)
        {
            richTextBox1.Text += "序号，说明，计算结果\r\n";
            richTextBox1.Text += "1,参考高程点H0高程值,100\r\n";
            richTextBox1.Text += "2,关键点K0的高程值,109.034\r\n";
            richTextBox1.Text += "3,关键点K1的高程值,113.483\r\n";
            richTextBox1.Text += "4,关键点K2的高程值,105.548\r\n";
            richTextBox1.Text += "5,测试点AB的坐标方位角,4.31399\r\n";
            richTextBox1.Text += "6,A的内插高程h,112.935\r\n";
            richTextBox1.Text += "7,B的内插高程h,109.982\r\n";
            richTextBox1.Text += "8,以A、B 为两个端点的梯形面积 S,153.740\r\n";
            richTextBox1.Text += "9,K0到K1的平面距离 D0,127.626\r\n";
            richTextBox1.Text += "10,K1到K2的平面距离 D1,69.279\r\n";
            richTextBox1.Text += "11,纵断面的平面总距离D,196.905\r\n";
            richTextBox1.Text += "12,方位角ɑ01,0.21008\r\n";
            richTextBox1.Text += "13,方位角ɑ12,0.41739\r\n";
            richTextBox1.Text += "14,第一条纵断面的内插点Z3 的坐标 X,144.467\r\n";
            richTextBox1.Text += "15,第一条纵断面的内插点Z3 的坐标 Y,545.687\r\n";
            richTextBox1.Text += "16,第一条纵断面的内插点Z3 的高程 H,115.825\r\n";
            richTextBox1.Text += "17,第二条纵断面的内插点Y3 的坐标 X,150.702\r\n";
            richTextBox1.Text += "18,第二条纵断面的内插点Y3 的坐标 Y,526.471\r\n";
            richTextBox1.Text += "19,第二条纵断面的内插点Y3 的高程 H,117.473\r\n";
            richTextBox1.Text += "20,第一条纵断面面积S1,1663.124\r\n";
            richTextBox1.Text += "21,第二条纵断面面积S2,813.992\r\n";
            richTextBox1.Text += "22,纵断面总面积S,2477.117\r\n";
            richTextBox1.Text += "23,第一条横断面内插点Q3 的坐标 X,180.665\r\n";
            richTextBox1.Text += "24,第一条横断面内插点Q3 的坐标 Y,538.068\r\n";
            richTextBox1.Text += "25,第一条横断面内插点Q3 的高程 H,115.247\r\n";
            richTextBox1.Text += "26,第二条横断面内插点W3 的坐标 X,277.693\r\n";
            richTextBox1.Text += "27,第二条横断面内插点W3 的坐标 Y,566.376\r\n";
            richTextBox1.Text += "28,第二条横断面内插点W3 的高程 H,114.235\r\n";
            richTextBox1.Text += "29,第一条横断面的面积Srow1,704.923\r\n";
            richTextBox1.Text += "30,第一条横断面的面积Srow2,674.720\r\n";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SaveFileDialog sf = new SaveFileDialog();
            sf.Filter = "txt|*.txt";
            if(sf.ShowDialog() == DialogResult.OK)
            {
                using (StreamWriter sw = new StreamWriter(sf.FileName))
                {
                    sw.WriteLine(richTextBox1.Text);
                }
            }
            MessageBox.Show("保存成功");
        }
    }
}
