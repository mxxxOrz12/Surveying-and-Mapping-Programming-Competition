using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpatialAnalysis
{
    static class Alogrithm
    {
        //定义全局数据的平均值
        public static double X_avg = 0, Y_avg = 0,Value_avg = 0;
        //对所有数据点计算标准差椭圆
        public static void CalEllipse(List<SpatialPoint> spatialPoints,Ellipse ellipse)
        {
         
            double Sum_X = 0,Sum_Y = 0;
            foreach(var point in spatialPoints)
            {
                Sum_X += point.Point_X;
                Sum_Y += point.Point_Y;
            }
            X_avg = Sum_X / spatialPoints.Count;
            Y_avg = Sum_Y / spatialPoints.Count;

            double SigemaAi = 0;
            double SigemaBi = 0;
            double SigemaAiBi = 0;

            //求解Theta
            foreach (var point in spatialPoints)
            {
                SigemaAi += Math.Pow((point.Point_X - X_avg),2);
                SigemaBi += Math.Pow((point.Point_Y - Y_avg),2);
                SigemaAiBi += (point.Point_X - X_avg) * (point.Point_Y - Y_avg);
            }
            double y = (SigemaAi - SigemaBi) + Math.Sqrt(Math.Pow((SigemaAi - SigemaBi), 2) + 4 * Math.Pow(SigemaAiBi, 2));
            double x = 2 * SigemaAiBi;
            ellipse.Theta = FangWei(y , x);

            //求解长短半轴
            double HalfX_Sum = 0, HalfY_Sum = 0;
            foreach(var point in spatialPoints)
            {
                double ai = point.Point_X - X_avg;
                double bi = point.Point_Y - Y_avg;
                HalfX_Sum += Math.Pow(ai * Math.Cos(ellipse.Theta) - bi * Math.Sin(ellipse.Theta), 2);
                HalfY_Sum += Math.Pow(ai * Math.Sin(ellipse.Theta) + bi * Math.Cos(ellipse.Theta), 2);
            }
            ellipse.Half_X = Math.Sqrt(2) * Math.Sqrt(HalfX_Sum / spatialPoints.Count);
            ellipse.Half_Y = Math.Sqrt(2) * Math.Sqrt(HalfY_Sum / spatialPoints.Count);

        }
        //计算方位角
        public static double FangWei(double y,double x)
        {
            if (x > 0 && y > 0)
            {
                return Math.Atan(y / x);
            }
            else if(x<0 && y >0)
            {
                return Math.PI - Math.Abs(Math.Atan(y / x));
            }
            else if (x < 0 && y < 0)
            {
                return Math.PI + Math.Abs(Math.Atan(y / x));
            }
            else 
            {
                return Math.PI *2 - Math.Atan(y / x);
            }

        }
        public static double Rad2Du(double rad)
        {
            return rad * 180 / Math.PI;
        }

        public static double CalDistance (SpatialPoint p1,SpatialPoint p2)
        {
            return Math.Sqrt(Math.Pow(p2.Point_X - p1.Point_X, 2) + Math.Pow(p2.Point_Y - p1.Point_Y, 2));
        }

        //使用反距离权重计算空间权重矩阵
        public static void CalSpatialMatrix(List<SpatialPoint> spatialPoints,List<List<double>> SpatialMatrix)
        {
            for(int i = 0;i<spatialPoints.Count;i++)
            {
                for(int j = 0;j<spatialPoints.Count;j++)
                {
                    if(i==j)
                    {
                        SpatialMatrix[i][j] = 0.0;
                        continue;
                    }
                    SpatialMatrix[i][j] = 1.0/CalDistance(spatialPoints[i], spatialPoints[j]) ;
                }
            }
        }

        public static double CalGlobalMoran(List<SpatialPoint> spatialPoints,List<List<double>> SpatialMatrix)
        {
            double MoranI;
            double M_Up = 0;
            double M_down = 0;
            double S0 = 0;

            //计算属性平均值
            double Value_Sum = 0;
            foreach(var point in spatialPoints)
            {
                Value_Sum += point.Value;
            }
            Value_avg = Value_Sum / spatialPoints.Count;

            for(int i = 0;i<spatialPoints.Count;i++)
            {
                M_down += Math.Pow(spatialPoints[i].Value - Value_avg, 2);
            }

            for(int i =0;i<spatialPoints.Count;i++)
            {
                for(int j = 0;j<spatialPoints.Count;j++)
                {
                    if(i == j )
                    {
                        continue;
                    }
                    M_Up += SpatialMatrix[i][j] * (spatialPoints[i].Value - Value_avg) * (spatialPoints[j].Value - Value_avg);
                    S0 += SpatialMatrix[i][j];
                }
            }
            MoranI = (spatialPoints.Count * M_Up) / (S0 * M_down);

            return MoranI;
        }

        public static List<double> CalLocalMoran(List<SpatialPoint> spatialPoints, List<List<double>> SpatialMatrix)
        {
            List<double> LocalMoran = new List<double>();
            double Si2 = 0;
            double LocalMoranIi = 0;
            double Right_Sum = 0;

            for(int i =0;i<spatialPoints.Count;i++)
            {
                Si2 += Math.Pow(spatialPoints[i].Value - X_avg, 2);
            }
            Si2 = Si2/ spatialPoints.Count;

            for (int i =0;i< spatialPoints.Count;i++)
            {
                for(int j = 0;j<spatialPoints.Count;j++)
                {
                    if(i == j)
                    {
                        continue;
                    }
                    Right_Sum += SpatialMatrix[i][j] * (spatialPoints[j].Value - Value_avg);
                }
                LocalMoranIi = (spatialPoints[i].Value - Value_avg) / Si2 * Right_Sum;
                LocalMoran.Add(LocalMoranIi);

                Right_Sum = 0;
            }

            return LocalMoran;
       
        }


    }
}
