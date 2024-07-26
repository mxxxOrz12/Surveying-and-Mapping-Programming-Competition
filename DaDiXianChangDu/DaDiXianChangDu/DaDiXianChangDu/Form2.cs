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
            richTextBox1.Text += "1,椭球长半轴a,6378137\r\n";
            richTextBox1.Text += "2,扁率倒数1/f,298.257\r\n";
            richTextBox1.Text += "3,扁率f,0.00335281\r\n";
            richTextBox1.Text += "4,椭球短半轴b,6356752.314\r\n";
            richTextBox1.Text += "5,第一偏心率平方e2,0.00669438\r\n";
            richTextBox1.Text += "6,第二偏心率平方e2,0.00673950\r\n";
            richTextBox1.Text += "7,第1条大地线u1,0.54640305\r\n";
            richTextBox1.Text += "8,第1条大地线u2,0.54863897\r\n";
            richTextBox1.Text += "9,第1条大地线经差l（弧度),-0.01496571\r\n";
            richTextBox1.Text += "10,第1条大地线a1,0.27099419\r\n";
            richTextBox1.Text += "11,第1条大地线a2,0.72900331\r\n";
            richTextBox1.Text += "12,第1条大地线b1,0.44559171\r\n";
            richTextBox1.Text += "13,第1条大地线b2,0.44335579\r\n";
            richTextBox1.Text += "14,第1条大地线系数α,0.00335199\r\n";
            richTextBox1.Text += "15,第1条大地线系数β,0.00000082\r\n";
            richTextBox1.Text += "16,第1条大地线系数γ,0.00000000\r\n";
            richTextBox1.Text += "17,第1条大地线A1（弧度）,4.88950972\r\n";
            richTextBox1.Text += "18,第1条大地线λ,-0.01496571\r\n";
            richTextBox1.Text += "19,第1条大地线σ,0.84103209\r\n";
            richTextBox1.Text += "20,第1条大地线sinA0,6378137\r\n";
            richTextBox1.Text += "21,第1条大地线系数A,0.00000016\r\n";
            richTextBox1.Text += "22,第1条大地线系数B,0.00049262\r\n";
            richTextBox1.Text += "23,第1条大地线系数C,0.00000003\r\n";
            richTextBox1.Text += "24,第1条大地线σ1,0.10674931\r\n";
            richTextBox1.Text += "25,第1条大地线长S,82461.596\r\n";
            richTextBox1.Text += "26,第2条大地线长S2,34568.185\r\n";
            richTextBox1.Text += "27,第3条大地线长S3,359612.556\r\n";
            richTextBox1.Text += "28,第4条大地线长S4,426093.119\r\n";
            richTextBox1.Text += "29,第5条大地线长S5,349528.528\r\n";

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
                MessageBox.Show("保存成功");
            }
        }
    }
}
