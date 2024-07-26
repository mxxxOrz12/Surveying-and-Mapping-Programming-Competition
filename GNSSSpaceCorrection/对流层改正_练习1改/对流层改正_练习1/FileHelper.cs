using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;


namespace 对流层改正_练习1
{
    class FileHelper
    {
        public void Open_File(ref Datacenter datacenter)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Filter = "文本数据|*.txt";
            op.Title = "选择打开的文本数据";
            if(op.ShowDialog ()==DialogResult.OK)
            {
                StreamReader sr = new StreamReader(op.FileName);
                sr.ReadLine();//读走第一行
                while (!sr.EndOfStream)
                {
                    string[] str = sr.ReadLine().Trim().Split(',');
                    Station station = new Station();
                    station.name = str[0];

                    station.T = str[1];
                    int year = int.Parse(str[1].Substring(0, 4));
                    int month = int.Parse(str[1].Substring(4, 2));
                    int day = int.Parse(str[1].Substring(6, 2));
                    DateTime dateTime = new DateTime(year, month, day);
                    station.t = dateTime.DayOfYear;

                    station.L = double.Parse(str[2]);
                    station.B = double.Parse(str[3]);
                    station.H= double.Parse(str[4]);
                    station.E = double.Parse(str[5]);
                   
                    station.sin_E = Math.Sin(station.E/180.0*Math.PI);
                    datacenter.stations.Add(station);
                }
            }
            
        }

        public void Save_File(string report)
        {
            SaveFileDialog sf = new SaveFileDialog();
            sf.Title = "选择保存路径";
            sf.Filter = "文本数据|*.txt";
            if(sf.ShowDialog ()==DialogResult.OK)
            {
                StreamWriter sw = new StreamWriter(sf.FileName );
                sw.Write(report);
                sw.Flush();
            }
        }
    }
}
