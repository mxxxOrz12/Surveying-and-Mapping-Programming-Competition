using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GNSSSpace
{
    class Satellite
    {
        public string ID { get; set; }
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }
        public double A { get; set; } //卫星S的方位角
        public double E { get; set; } //卫星S的高度角
        public double IonDelay { get; set; } //卫星S的电离层延迟

        public Satellite()
        {

        }

        public Satellite(string ID,double X,double Y,double Z)
        {
            this.ID = ID;
            this.X = X;
            this.Y = Y;
            this.Z = Z;
        }
    }
    //时间
    class SateTime
    {
        public int Year { get; set; }
        public int Month { get; set; }
        public int Day { get; set; }
        public int Hour { get; set; }
        public int Minute { get; set; }
        public double Second { get; set; }


    }
}
