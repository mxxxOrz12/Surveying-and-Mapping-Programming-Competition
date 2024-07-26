using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaDiXianChangDu
{
    //椭球类
    class Ellipse
    {
        public double a { get; set; }
        public double b { get; set; }
        public double fdao { get; set; } //扁率倒数
        public double e12 { get; set; } //第一偏心率
        public double e22 { get; set; }
        public double f { get; set; }
        public Ellipse()
        {

        }
        public Ellipse(double a,double fdao)
        {
            this.a = a;
            this.fdao = fdao;
        }

    }
    class DPoints
    {
        public string ID { get; set; }
        public double B { get; set; } //纬度
        public double L { get; set; } //经度

        public DPoints()
        {

        }
        public DPoints(string ID,double B,double L)
        {
            this.ID = ID;
            this.B = B;
            this.L = L;
        }
    }
    class GeoLine
    {
        public double u1 { get; set; }
        public double u2 { get; set; }
        public double l { get; set; } //L2-L1
        public double a1 { get; set; }
        public double a2 { get; set; }
        public double b1 { get; set; }
        public double b2 { get; set; }

        public double alpha { get; set; }
        public double beita { get; set; }
        public double gama { get; set; }
        public double A1 { get; set; }
        public double sigema { get; set; }
        public double sinA0 { get; set; }
        public double cos2A0 { get; set; }

        public double delta { get; set; }
        public double A { get; set; }
        public double B { get; set; }
        public double C { get; set; }
        public double sigema1 { get; set; }
        public double S { get; set; }
        public double lamda1 { get; set; }
        public double k2 { get; set; }

    }


}
