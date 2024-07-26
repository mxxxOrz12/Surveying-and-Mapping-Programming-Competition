using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpatialAnalysis
{
    class SpatialPoint
    {
        public int ID { get; set; }
        public double Point_X { get; set; }
        public double Point_Y { get; set; }
        public double Value { get; set; }

        public SpatialPoint()
        {

        }
        public SpatialPoint(int ID,double X,double Y,double Value)
        {
            this.ID = ID;
            this.Point_X = X;
            this.Point_Y = Y;
            this.Value = Value;
        }
    }

    class Ellipse
    {
        public double Theta { get; set; } //标准差椭圆长轴与竖直方向夹角
        public double Half_X { get; set; } //长半轴
        public double Half_Y { get; set; } //短半轴

    }

}
