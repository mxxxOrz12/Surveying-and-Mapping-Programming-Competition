using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GNSSSpace
{
    static class Troposphere
    {
        //定义湿分量投影函数系数表
        static List<double> NeilWetA = new List<double>() { 0.00058021897, 0.00056794847, 0.00058118019, 0.00059727542, 0.00061641693 };
        static List<double> NeilWetB = new List<double>() { 0.0014275268, 0.0015138625, 0.0014572752, 0.0015007428, 0.0017599082 };
        static List<double> NeilWetC = new List<double>() { 0.043472961, 0.046729510, 0.043908931, 0.044626982, 0.054736038 };

        //计算湿分量
        public static void CalculateWet(TroSatellite troSatellite)
        {
            double Aw, Bw, Cw;
            double Lat = Math.Abs(troSatellite.Lat);
            if(Lat <15)
            {
                Aw = NeilWetA[0];
                Bw = NeilWetB[0];
                Cw = NeilWetC[0];
            }
            else if(Lat >= 15 && Lat<75)
            {
                int i = (int)(Lat / 15.0) - 1;
                double frac = (Lat - 15 * (i + 1)) / 15.0;
                Aw = NeilWetA[i] + frac * (NeilWetA[i + 1] - NeilWetA[i]);
                Bw = NeilWetB[i] + frac * (NeilWetB[i + 1] - NeilWetB[i]);
                Cw = NeilWetC[i] + frac * (NeilWetC[i + 1] - NeilWetC[i]);
            }
            else {
                Aw = NeilWetA[4];
                Bw = NeilWetB[4];
                Cw = NeilWetC[4];
            }
            double sinE = Math.Sin(Algorithm.Du2Rad(troSatellite.E));
            double Up = (1.0 + Aw / (1.0 + Bw / (1 + Cw)));
            double Down = (sinE + Aw / (sinE + Bw / sinE + Cw));
            troSatellite.M_w = Up / Down;
        }

        //定义干分量投影函数系数表

        static List<double> NeilDryAvg= new List<double>() { 0.0012769934,0.0012683230,0.0012465397,0.0012196049,0.0012045996 };
        static List<double> NeilDryBvg= new List<double>() { 0.0029153695,0.002915299,0.0029288445,0.0029022565,0.0029024912 };
        static List<double> NeilDryCvg= new List<double>() { 0.062610505,0.062837393,0.063721774,0.063824265,0.064258455 };
                                
        static List<double> NeilDryAmp= new List<double>() { 0.0,0.000012709626,0.000026523662,0.000034000452,0.000041202191};
        static List<double> NeilDryBmp= new List<double>() { 0.0,0.000021414979,0.000030160779,0.000072562722,0.00011723375 };
        static List<double> NeilDryCmp= new List<double>() { 0.0,0.000090128400,0.000043497037,0.00084795348,0.0017037206 };

        //计算干分量
        public static void CalculateDry(TroSatellite troSatellite)
        {
            double Aht = 2.53e-5;
            double Bht = 5.49e-3;
            double Cht = 1.14e-3;

            double Ad, Bd, Cd;

            //年积日
            int YearOfDay = 209;
            int t0 = 28;
            double FracTime = Math.Cos(Math.PI * 2 * (YearOfDay - t0) / 365.25);

            double Lat = Math.Abs(troSatellite.Lat);
            if(Lat < 15.0)
            {
                Ad = NeilDryAvg[0] + NeilDryAvg[0] * FracTime;
                Bd = NeilDryBvg[0] + NeilDryBvg[0] * FracTime;
                Cd = NeilDryCvg[0] + NeilDryCvg[0] * FracTime;

            }
            else if(Lat >= 15.0 && Lat<75)
            {
                int i = (int)(Lat / 15.0) - 1;
                double frac = (Lat - 15.0 * (i + 1)) / 15;
                Ad = NeilDryAvg[i] + (NeilDryAvg[i + 1] - NeilDryAvg[i]) * frac + NeilDryAmp[i] + (NeilDryAmp[i + 1] - NeilDryAmp[i]) * frac * FracTime;
                Bd = NeilDryBvg[i] + (NeilDryBvg[i + 1] - NeilDryBvg[i]) * frac + NeilDryBmp[i] + (NeilDryBmp[i + 1] - NeilDryBmp[i]) * frac * FracTime;
                Cd = NeilDryCvg[i] + (NeilDryCvg[i + 1] - NeilDryCvg[i]) * frac + NeilDryCmp[i] + (NeilDryCmp[i + 1] - NeilDryCmp[i]) * frac * FracTime;


            }
            else
            {
                Ad = NeilDryAvg[4] + NeilDryAvg[4] * FracTime;
                Bd = NeilDryBvg[4] + NeilDryBvg[4] * FracTime;
                Cd = NeilDryCvg[4] + NeilDryCvg[4] * FracTime;
            }

            double SinE = Math.Sin(Algorithm.Du2Rad(troSatellite.E));
            double left_up = 1.0 + Ad / (1 + Bd / (1 + Cd));
            double left_down = SinE + Ad / (SinE + Bd / (SinE + Cd));
            double right_up = 1.0 + Aht / (1 + Bht / (1 + Cht));
            double right_down = SinE + Aht / (SinE + Bht / (SinE + Cht));
            double right = 1.0 / SinE - right_up / right_down;
            double left = left_up / left_down;

            troSatellite.M_d = left + right * troSatellite.H / 1000;

        }
        //延迟改正计算
        public static void CalculateTroDelay(TroSatellite troSatellite)
        {
            troSatellite.ZHD = 2.29951 * Math.Pow(Math.E, -0.000116 * troSatellite.H);
            troSatellite.ZWD = 0.1;
            troSatellite.DeltaS = troSatellite.ZHD * troSatellite.M_d + troSatellite.ZWD * troSatellite.M_w;

        }
        
    }
}
