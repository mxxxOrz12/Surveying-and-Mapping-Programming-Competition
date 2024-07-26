using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GNSSSpace
{
    class TroSatellite
    {
        public string ID { get; set; }
        public string Time { get; set; }
        public double E { get; set; } //高度角E；
        public double Lon { get; set; } //经度
        public double Lat { get; set; } //纬度
        public double H { get; set; }  //大地高
        public double ZHD { get; set; }
        public double M_d { get; set; } //干分量
        public double M_w { get; set; } //湿分量
        public double ZWD { get; set; }
        public double DeltaS { get; set; } 

        public TroSatellite()
        {

        }

        public TroSatellite(string ID,string Time,double Lon,double Lat,double H,double E)
        {
            this.ID = ID;
            this.Time = Time;
            this.Lon = Lon;
            this.Lat = Lat;
            this.H = H;
            this.E = E;

        }
    }

}
  
