using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZongHengDuanMian
{
    class ZHPoints
    {
        public string ID { get; set; }
        public double X { get; set; }
        public double Y { get; set; }
        public double H { get; set; }
        public double Dis { get; set; } //距离另一个点的距离
        public List<ZHPoints> LeastFivePoints;//最近的五个离散点

        public ZHPoints()
        {

        }
        public ZHPoints(string ID)
        {
            this.ID = ID;
        }
        public ZHPoints(string ID,double X,double Y,double H)
        {
            this.ID = ID;
            this.X = X;
            this.Y = Y;
            this.H = H;

        }
        public ZHPoints(string ID, double X, double Y)
        {
            this.ID = ID;
            this.X = X;
            this.Y = Y;
        }

    }

    //class OtherPoint
    //{
    //    public 
    //}
}
