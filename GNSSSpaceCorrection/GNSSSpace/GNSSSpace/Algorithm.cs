using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GNSSSpace
{
    static class Algorithm
    {
          static double Xp = -2225669.7744;
          static double Yp = 4998936.1598;
          static double Zp = 3265908.9678;
          static double Bp = Du2Rad(30);
          static double Lp = Du2Rad(114);
        public static void PositionTransFrom(Satellite satellite)
        {
            //定义测站坐标

            double dx, dy, dz;
            dx = satellite.X - Xp;
            dy = satellite.Y - Yp;
            dz = satellite.Z - Zp;

            satellite.X = dx * (-Math.Sin(Bp) * Math.Cos(Lp)) + dy * (-Math.Sin(Bp) * Math.Sin(Lp)) + dz * Math.Cos(Bp);
            satellite.Y = dx * (-Math.Sin(Lp)) + dy * Math.Cos(Lp);
            satellite.Z = dx * (Math.Cos(Bp) * Math.Cos(Lp)) + dy * (Math.Cos(Bp) * Math.Sin(Lp)) + dz * Math.Sin(Bp);


            satellite.A = FangWei(satellite.Y, satellite.X);
            satellite.E = Math.Atan(satellite.Z / Math.Sqrt(Math.Pow(satellite.X, 2) + Math.Pow(satellite.Y, 2)));

        }

        //计算穿刺点地磁纬度、电离层延迟量
        public static void CalGeomLatitude(Satellite satellite,SateTime sateTime)
        {
            if(satellite.E <0)
            {
                satellite.IonDelay = 0;
                return;
            }
            //将弧度转换为半周
            double E = satellite.E / Math.PI;

            double Fai_u = Bp / Math.PI;
            double lamda_u = Lp / Math.PI;
            double Fai = 0.0137 / (E + 0.11) - 0.022;
            double Fai_IP = Fai_u + Fai*Math.Cos(satellite.A);
            //Cos中，要将半周转为弧度
            double lamda_IP = lamda_u + Fai * Math.Sin(satellite.A) / Math.Cos(Fai_IP * Math.PI);
            if (Fai_IP > 0.416) Fai_IP = 0.416;
            if (Fai_IP < -0.416) Fai_IP = -0.416;

            //地磁纬度
            //Cos中，要将半周转为弧度
            double Fai_m = Fai_IP + 0.064 * Math.Cos((lamda_IP - 1.617)*Math.PI);

            //计算电离层延迟量
            double F = 1 + 16 * Math.Pow((0.53 -E), 3);
            double A1 = 5e-9;
            double A3 = 50400.0;
            //定义观测时间T
            double T = sateTime.Hour * 3600 + sateTime.Minute * 60 + sateTime.Second;
            double t = lamda_IP * 43200 + T;
            if (t < 0) t += 86400;
            if (t > 86400) t -= 86400;


            //定义计算A2，A4的系数
            double a0 = 0.1397e-7;
            double a1 = -0.7451e-8;
            double a2 = -0.596e-7;
            double a3 = 0.1192e-6;

            double b0 = 0.127e+6;
            double b1 = -0.1966e+6;
            double b2 = 0.6554e+5;
            double b3 = 0.2621e+6;

            double A2 = a0 + a1 * Fai_m + a2 * Math.Pow(Fai_m, 2) + a3 * Math.Pow(Fai_m,3);
            double A4 = b0 + b1 * Fai_m + b2 * Math.Pow(Fai_m, 2) + b3 * Math.Pow(Fai_m, 3);

            //条件判断，计算Tion
            double Judge = Math.Abs(2 * Math.PI * (t - A3) / A4);
            double Tion;
            if (Judge < 1.57)
            {
                Tion = F * (A1 + A2 * (1 - Math.Pow(Judge, 2)/2 + Math.Pow(Judge, 4) / 24));
            }
            else
            {
                Tion = F * A1;
            }
            double c = 299792458;
            satellite.IonDelay = Tion * c;

        }

        public static double Du2Rad(double Du)
        {
            return Du * Math.PI / 180;
        }
        public static double Rad2Du(double rad)
        {
            return rad * 180 / Math.PI;
        }
        
        //计算方位角
        public static double FangWei(double y,double x)
        {

            if(x>0 && y> 0)
            {
                return Math.Atan(y / x);
            }
            else if(x>0 &&y<0)
            {
                //注意添加绝对值
                return Math.PI * 2 - Math.Abs( Math.Atan(y / x));
            }
            else if(x<0 && y>0)
            {
                return Math.PI -Math.Abs( Math.Atan(y / x));
            }
            else
            {
                return Math.PI + Math.Atan(y / x);
            }

        }

        public static double DDmmss2Rad(double dms)
        {
            double deg, min, sec;
            deg = (int)dms;
            min = (int)((dms - deg) * 100);
            sec = (int)(dms * 10000 - deg * 10000 - min * 100);
            //转为弧度
            return (deg + min / 60.0 + sec / 3600.0) / 180.0 * Math.PI;
        }


        public static double Rad2DDmmss(double rad)
        {
            double deg, min, sec;
            rad = rad * 180 / Math.PI;


            deg = (int)rad;

            //*60是因为1度=60min
            min = (int)((rad - deg) * 60);
            sec = (int)((rad - deg - min / 60) * 3600*100);
            //将分，秒转换为小数
            return deg + min / 100.0 + sec / 1000000.0;
        }
    }
}
