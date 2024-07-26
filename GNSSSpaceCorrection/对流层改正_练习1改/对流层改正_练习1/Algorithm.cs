using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 对流层改正_练习1
{
    class Algorithm
    {
        public double dms_rad(double dms)
        {
            double d = (int)dms;
            double m = (int)((dms - d) * 100);
            double s = ((dms - d) * 100 - m) * 100;
            return (d + m / 60 + s / 3600) * 180 / Math.PI;
        }
       public double Get_w_avg(double w_down,double w_up,int n,double B)//获得干分量的内插系数
        {
            double d15 = dms_rad(15);
            double w_avg=w_down+(w_up-w_down)*(dms_rad(B)-n*d15)/d15;
            return w_avg;
       }
     
        public void Let_mw(Station station,Datacenter datacenter)//使一个测站有湿分量
        {
            double aw=0, bw=0, cw = 0;
            double d15 =15;
            int n = (int)(Math.Abs(station.B) / d15);
            if(n==0)
            {
                aw = datacenter.aw[0];
                bw = datacenter.bw[0];
                cw = datacenter.cw[0];
         
            }
            else if(n==5)
            {
                aw = datacenter.aw[4];
                bw = datacenter.bw[4];
                cw = datacenter.cw[4];
            }
            else
            {
                aw = Get_w_avg(datacenter.aw[n - 1], datacenter.aw[n], n, Math.Abs(station.B));
                bw = Get_w_avg(datacenter.bw[n - 1], datacenter.bw[n], n, Math.Abs(station.B));
                cw = Get_w_avg(datacenter.cw[n - 1], datacenter.cw[n], n, Math.Abs(station.B));
            }
            double fz = 1.0 / (1 + aw / (1 + bw / (1 + cw)));
            double fm = 1.0 / (station.sin_E + aw / (station.sin_E + bw / (station.sin_E + cw)));
            station.mw = fm/fz;
        }

        public double Get_d(double avg_down,double avg_up,double amp_down, double amp_up, int n,double B,double cos_theta)//获得湿分量的内插系数
        {
            double d15 = dms_rad(15);
            return avg_down + (avg_up - avg_down) * (dms_rad(B) - n * d15) / d15 
                +amp_down +(amp_up -amp_down )* (dms_rad(B) - n * d15)/d15*cos_theta;
        }

        public void Let_md(Station station, Datacenter datacenter)//使一个测站有干分量
        {
            double ah_avg = 0, bh_avg = 0, ch_avg = 0, ah_amp = 0, bh_amp = 0, ch_amp = 0 ;
            double ad = 0,bd=0,cd = 0;
            double cos_theta=Math.Cos(2*Math.PI*(station .t -28)/365.25 );
            int n = (int)(Math.Abs(station.B) / 15);
            if (n == 0)
            {
                ad = datacenter.ah_avg[0] + datacenter.ah_amp[0] * cos_theta;
                bd = datacenter.bh_avg[0] + datacenter.bh_amp[0] * cos_theta;
                cd = datacenter.ch_avg[0] + datacenter.ch_amp[0] * cos_theta;
            }
            else if (n == 5)
            {
                ad = datacenter.ah_avg[4] + datacenter.ah_amp[4] * cos_theta;
                bd = datacenter.bh_avg[4] + datacenter.bh_amp[4] * cos_theta;
                cd = datacenter.ch_avg[4] + datacenter.ch_amp[4] * cos_theta;
            }
            else
            {
                ad = Get_d(datacenter.ah_avg[n - 1], datacenter.ah_avg[n], datacenter.ah_amp[n - 1], datacenter.ah_amp[n], n, Math.Abs(station.B), cos_theta);
                bd = Get_d(datacenter.bh_avg[n - 1], datacenter.bh_avg[n], datacenter.bh_amp[n - 1], datacenter.bh_amp[n], n, Math.Abs(station.B), cos_theta);
                cd = Get_d(datacenter.ch_avg[n - 1], datacenter.ch_avg[n], datacenter.ch_amp[n - 1], datacenter.ch_amp[n], n, Math.Abs(station.B), cos_theta);
            }
            double aht = 2.53e-5;
            double bht = 5.49e-3;
            double cht = 1.14e-3;

            double up1 = 1.0 / (1 + ad / (1 + bd / (1 + cd)));
            double down1 = 1.0 / (station.sin_E + ad / (station.sin_E + bd / (station.sin_E + cd)));
            double left = down1 / up1;

            double up2 = 1.0 / (1 + aht / (1 + bht / (1 + cht)));
            double down2 = 1.0 / (station.sin_E + aht / (station.sin_E + bht / (station.sin_E + cht)));
            double right = (1.0/station.sin_E - down2 / up2 )* station .H  / 1000;
            station.md = left + right;
        }


        public void Let_all(Datacenter datacenter)
        {
            
           foreach(var sta in datacenter.stations  )
            {
                sta.ZHD = 2.29951 * Math.Exp(-0.000116 * sta.H);
                double ZWD = 0.1;
                Let_mw(sta,datacenter);
                Let_md(sta,datacenter);
                sta.deta_S = sta.ZHD * sta.md + ZWD * sta.mw;
            }

        }
    }
}
