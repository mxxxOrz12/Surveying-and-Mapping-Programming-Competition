using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 电离层延迟改正
{
    class Satellite
    {
        public string Prn;      // 卫星 PRN
        public double X;        // X 坐标
        public double Y;        // Y 坐标
        public double Z;        // Z 坐标
        public double Tion;
        public double A;        // 方位角
        public double E;        // 高度角
        public double IonDely;

        public Satellite()
        {

        }

        public Satellite(string prn,double x,double y,double z)
        {
            this.Prn = prn;
            this.X = x;
            this.Y = y;
            this.Z = z;
        }

    }
    
    class DataCenter
    {
        
        public double Bp = 30 / 180.0 * Math.PI, Lp = 114 / 180.0 * Math.PI;                                // 测站经纬度
        public double Station_x = -2225669.7744, Station_y = 4998936.1598, Station_z = 3265908.9678;        // 测站 XYZ
        public double t_year = 2016, t_mon = 8, t_day = 16, t_hour = 10, t_min = 45, t_sec = 0;       
        public List<Satellite> satellites = new List<Satellite>();

    }
}
