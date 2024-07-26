using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WuDianGuangHua
{
    class MyPoint
    {
        public int ID { get; set; }
        public double X { get; set; }
        public double Y { get; set; }

        public double cos { get; set; }
        public double sin { get; set; }


        public MyPoint()
        {

        }
        public MyPoint(int ID,double X,double Y)
        {
            this.X = X;
            this.Y = Y;
            this.ID = ID;

        }


    }

    class Curve
    {
        public double E0, E1, E2, E3;
        public double F0, F1, F2, F3;

        public MyPoint StartPoint { get; set; }
        public MyPoint endPoint { get; set; }

    }
}
