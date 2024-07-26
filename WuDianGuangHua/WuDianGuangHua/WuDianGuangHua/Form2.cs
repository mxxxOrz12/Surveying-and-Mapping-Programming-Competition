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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            richTextBox1.Text = Form1.report;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SaveFileDialog sf = new SaveFileDialog();
            sf.Filter = "txt|*.txt";
            if(sf.ShowDialog() ==DialogResult.OK)
            {
                try
                {
                    using (StreamWriter sw = new StreamWriter(sf.FileName))
                    {
                        sw.WriteLine(Form1.report);
                    }
                    MessageBox.Show("保存成功");
                }
                catch
                {

                }
         
            }
        }
    }
}
