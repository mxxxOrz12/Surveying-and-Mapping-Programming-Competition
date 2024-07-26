using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 电离层延迟改正
{
    class Algorithm
    {

        /// <summary>
        /// 将 dd.mmssss 转为弧度
        /// </summary>
        /// <param name="dms"></param>
        /// <returns></returns>
        public double Dms2Rad(double dms)
        {
            double deg, min, sec;

            deg = (int)(dms);
            min = (int)((dms - deg) * 100);
            sec = (int)(dms * 10000 - deg * 10000 - min * 100);
            return (deg + min / 60.0 + sec / 3600.0) * Math.PI / 180.0;

        }

        /// <summary>
        /// 将弧度转为 dd.mmssss
        /// </summary>
        /// <param name="rad"></param>
        /// <returns></returns>
        public double Rad2Dms(double rad)
        {
            double deg, min, sec;

            rad = rad / Math.PI * 180;
            deg = (int)(rad);
            min = (int)((rad - deg) * 60);
            sec = (int)((rad - deg - min / 60) * 3600 * 100);
            return deg + min / 100.0 + sec / 1000000.0;
        }

        public double JudgeAngle(double sa1, double t, double a)
        {
            if (sa1 > 0 && t > 0)
            {
                a = Math.Abs(a);
            }
            else if (sa1 > 0 && t < 0)
            {
                a = Math.PI - Math.Abs(a);
            }
            else if (sa1 < 0 && t < 0)
            {
                a = Math.PI + Math.Abs(a);
            }
            else
            {
                a = 2 * Math.PI - Math.Abs(a);
            }

            if (a < 0) a += Math.PI * 2;
            if (a > Math.PI * 2) a -= Math.PI * 2;
            return a;
        }

        /// <summary>
        /// 计算方位角、高度角
        /// </summary>
        /// <param name="dataCenter"></param>
        public void CalAE(DataCenter dataCenter)
        {
            double sbp = Math.Sin(dataCenter.Bp);
            double cbp = Math.Cos(dataCenter.Bp);
            double slp = Math.Sin(dataCenter.Lp);
            double clp = Math.Cos(dataCenter.Lp);


            foreach (var sat in dataCenter.satellites)
            {
                double dx, dy, dz, x, y, z;
                dx = sat.X - dataCenter.Station_x;
                dy = sat.Y - dataCenter.Station_y;
                dz = sat.Z - dataCenter.Station_z;

                x = -sbp * clp * dx - sbp * slp * dy + cbp * dz;
                y = -slp * dx + clp * dy;
                z = cbp * clp * dx + cbp * slp * dy + sbp * dz;

                sat.A = Math.Atan(y / x);
                sat.A = JudgeAngle(y, x, sat.A);
                sat.E = Math.Atan(z / Math.Sqrt(x * x + y * y));

            }
        }

        /// <summary>
        /// 计算电离层延迟
        /// </summary>
        /// <param name="dataCenter"></param>
        public void CalIonDely(DataCenter dataCenter)
        {
            foreach (var sat in dataCenter.satellites)
            {

                if (sat.E < 0)
                {
                    sat.IonDely = 0;
                    continue;
                }

                double sa = Math.Sin(sat.A);
                double ca = Math.Cos(sat.A);
                double se = Math.Sin(sat.E);
                double ce = Math.Cos(sat.E);

                // 穿刺点地磁纬度
                double lambda_ip, phi_ip, phi_m, Phi;
                double svE = sat.E / Math.PI ;
                double phi_u = dataCenter.Bp / Math.PI;
                double lambda_u = dataCenter.Lp / Math.PI;

                Phi = 0.0137 / (svE + 0.11) - 0.022;
                phi_ip = phi_u + Phi * ca;
                if (phi_ip > 0.416) phi_ip = 0.416;
                if (phi_ip < -0.416) phi_ip = -0.416;

                lambda_ip = lambda_u + Phi * sa / Math.Cos(phi_ip * Math.PI);
                phi_m = phi_ip  + 0.064 * Math.Cos((lambda_ip - 1.617) * Math.PI);

                // 克罗布歇模型系数
                double a0 = 0.1397e-7; double a1 = -0.7451e-8; double a2 = -0.5960e-7; double a3 = 0.1192e-6;
                double b0 = 0.1270e6;  double b1 = -0.1966e6;  double b2 = 0.6554e5;  double b3 = 0.2621e6;
                double A1 = 5e-9;
                double A3 = 50400.0;
                double A2 = a0 + a1 * phi_m + a2 * phi_m * phi_m + a3 * Math.Pow(phi_m, 3);
                double A4 = b0 + b1 * phi_m + b2 * phi_m * phi_m + b3 * Math.Pow(phi_m, 3);
                if (A2 < 0) A2 = 0;
                if (A4 < 72000) A4 = 72000;

                // 计算当地时间
                double T = dataCenter.t_hour * 3600.0 + dataCenter.t_min * 60.0 + dataCenter.t_sec;
                double t = 43200 * lambda_ip + T;
                if (t < 0) t += 86400;
                if (t > 86400) t -= 86400;

                // 计算电离层延迟改正
                double F, judge, c = 299792458.0;
                F = 1 + 16 * Math.Pow((0.53 - svE), 3);
                judge = Math.Abs(2 * Math.PI * (t - A3) / A4);

                //if (judge < 1.57) Tion = F * (A1 + A2 * judge);
                //else Tion = F * A1;

                if (judge < 1.57)sat.Tion = F * (A1 + A2 * (1 - judge * judge / 2 + judge * judge * judge * judge / 24));
                else sat.Tion = F * A1;

                sat.IonDely = sat.Tion * c;
            }


        }

    }
}