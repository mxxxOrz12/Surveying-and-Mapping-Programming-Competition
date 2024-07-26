using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaDiXianChangDu
{
    static class Alogrithm
    {
        public static double DDmmss2Rad(double Dms)
        {
            double deg, min, sec;
            deg = (int)Dms;
            min = (int)((Dms - deg) * 100);
            sec = Dms * 10000 - deg * 10000 - min * 100;
            return (deg + min / 60.0 + sec / 3600.0) * Math.PI / 180.0;
        }

        public static double Rad2DDmmss(double Rad)
        {
            double deg, min, sec;
            Rad = Rad * 180 / Math.PI;
            deg = (int)Rad;
            min = (int)((Rad - deg) * 60);
            sec = (int)((Rad - deg - min / 60) * 3600 * 100);
            return  Math.Round(deg + min / 100.0 + sec / 1000000.0,5);

        }

        public static void CalEllipese(Ellipse ellipse)
        {
            ellipse.f = 1.0 / ellipse.fdao;
            ellipse.b = ellipse.a * (1 - ellipse.f);
            double a2 = Math.Pow(ellipse.a, 2);
            double b2 = Math.Pow(ellipse.b, 2);
            ellipse.e12 = (a2 - b2) / a2;
            ellipse.e22 = (a2 - b2) / b2;

        }

        public static double Panduan(double rad)
        {
            if (rad < 0)
            {
                rad = rad + Math.PI * 2;
            }
            else if (rad > Math.PI * 2)
            {
                rad = rad - Math.PI * 2;
            }
                return rad;
        }

        public static double FangWei(double y,double x)
        {
            if(x>0 && y>0)
            {
                return Panduan(Math.Abs(Math.Atan(y / x)));
            }
            if (x < 0 && y > 0)
            {
                return Panduan(Math.PI - Math.Abs(Math.Atan(y / x)));
            }
            if (x < 0 && y < 0)
            {
                return Panduan(Math.PI + Math.Abs(Math.Atan(y / x)));
            }
            if (x > 0 && y < 0)
            {
                return Panduan(Math.PI*2 -Math.Abs(Math.Atan(y / x)));
            }
            else
            {
                return Math.Abs(Math.Atan(y / x));
            }
        }

    }
}
