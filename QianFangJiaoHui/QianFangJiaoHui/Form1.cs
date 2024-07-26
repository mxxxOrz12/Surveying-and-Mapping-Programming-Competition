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

namespace QianFangJiaoHui
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public static string result;

        private void 打开文件ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog of = new OpenFileDialog();
            of.Filter = "txt | *.txt";

            if(of.ShowDialog() ==DialogResult.OK)
            {
                try
                {
                    using (StreamReader sr = new StreamReader(of.FileName))
                    {
                        List<List<string>> str = new List<List<string>> { new List<string>(9), new List<string>(9) };

                        string line;
                        int k = 0;
                        while((line = sr.ReadLine()) != null)
                        {
                            if(k<9)
                            {
                                str[0].Add(line.Split(' ')[0]);
                            }
                            else
                            {
                                str[1].Add(line.Split(' ')[0]);
                            }
                            k++;
                        }

                        int i = 0;

                        dataGridView1.Rows.Add();
                        foreach (var list in str)
                        {
                 
                            int j = 0;
        
                            foreach (var item in list)
                            {
                                dataGridView1.Rows[i].Cells[j].Value = item;
                                j++;
                            }

                            j = 0;
                            i++;

                        }
                    }
                }
                catch
                {

                }
            }

            MessageBox.Show("打开成功");



        }

        private void 计算ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                List<PhotoImage> photoImages = new List<PhotoImage>();
                //初始化赋值
                for(int i =0;i<dataGridView1.Rows.Count;i++)
                {
                    PhotoImage photoImage = new PhotoImage();
                    photoImage.Xs = Convert.ToDouble(dataGridView1.Rows[i].Cells[0].Value);
                    photoImage.Ys = Convert.ToDouble(dataGridView1.Rows[i].Cells[1].Value);
                    photoImage.Zs = Convert.ToDouble(dataGridView1.Rows[i].Cells[2].Value);
                    photoImage.fai = Convert.ToDouble(dataGridView1.Rows[i].Cells[3].Value);
                    photoImage.omega = Convert.ToDouble(dataGridView1.Rows[i].Cells[4].Value);
                    photoImage.k = Convert.ToDouble(dataGridView1.Rows[i].Cells[5].Value);
                    photoImage.x = Convert.ToDouble(dataGridView1.Rows[i].Cells[6].Value);
                    photoImage.y = Convert.ToDouble(dataGridView1.Rows[i].Cells[7].Value);
                    photoImage.f = Convert.ToDouble(dataGridView1.Rows[i].Cells[8].Value);

                    photoImages.Add(photoImage);
                }
                Alogrithm.PosiitonTransfer(photoImages[0]);
                Alogrithm.PosiitonTransfer(photoImages[1]);

                Model model = new Model();
                model = Alogrithm.ProjectionIndex(photoImages[0], photoImages[1]);

                GroundP groundp = new GroundP();
                groundp = Alogrithm.GroundPResult(photoImages[0], photoImages[1], model);

                result += "--------------------空间前方交会计算报告--------------------\r\n";
                result += "\r\n";
                result += "--------------------像空间辅助坐标系坐标--------------------\r\n";
                result += string.Format("{0,-20}{1,-20}{2,-20}\r\n", "u", "v", "w");

                foreach (var list in photoImages)
                {
                    result += string.Format("{0,-20}{1,-20}{2,-20}\r\n", Math.Round(list.u, 6), Math.Round(list.v, 6), Math.Round(list.w, 6));
                }

                result += "--------------------投影系数--------------------\r\n";
                result += "N1:" + Math.Round(model.N1, 6) + "\r\n";
                result += "N2:" + Math.Round(model.N2, 6) + "\r\n";
                result += "--------------------地面点坐标--------------------\r\n";
                result += string.Format("{0,-20}{1,-20}{2,-20}\r\n", "X", "Y", "Z");
                result += string.Format("{0,-20}{1,-20}{2,-20}\r\n", Math.Round(groundp.X, 6), Math.Round(groundp.Y, 6), Math.Round(groundp.Z, 6));





                MessageBox.Show("计算成功");

            }
            catch
            {

            }
        }

        private void 打开报告ToolStripMenuItem_Click(object sender, EventArgs e)
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
