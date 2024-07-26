using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 电离层延迟改正
{
    class FileHelper
    {
        public void LoadData(DataCenter dataCenter)
        {
            try
            {
                OpenFileDialog opf = new OpenFileDialog();
                opf.Filter = "文本文件|*.txt";
                opf.Title = "请选择要导入的数据文件";
                if (opf.ShowDialog() == DialogResult.OK)
                {
                    StreamReader sr = new StreamReader(opf.FileName);
                    string[] line1 = sr.ReadLine().Trim().Split(' ');


                    dataCenter.satellites = new List<Satellite>();
                    Satellite satellite = new Satellite();
                    while (!sr.EndOfStream)
                    {
                        satellite = new Satellite();
                        string lines = sr.ReadLine();
                        satellite = new Satellite(lines.Substring(0, 3),double.Parse(lines.Substring(4, 13)) * 1000,
                                double.Parse(lines.Substring(18, 13)) * 1000, double.Parse(lines.Substring(32, 13)) * 1000);
                        dataCenter.satellites.Add(satellite);

                    }

                }


            }
            catch (Exception)
            {
                System.Windows.Forms.MessageBox.Show("导入数据文件失败，请重新选择数据文件！");
                throw;
            }

        }

        public void SaveReport(string report)
        {
            try
            {
                SaveFileDialog svf = new SaveFileDialog();
                svf.Filter = "文本文件|*.txt";
                svf.ShowDialog();
                StreamWriter sw = new StreamWriter(svf.FileName);
                sw.Write(report);
                sw.Flush();
            }
            catch (Exception)
            {

                throw;
            }

        }


    }
}
