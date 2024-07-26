using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZongHengDuanMian
{
    static class Alogrithm
    {
        public static double H0 = 100.000;
        //计算坐标方位角
        public static double FangWei(double y,double x)
        {
            if(x>0 && y>0)
            {
                return Math.Atan(y / x);
            }
            else if(x< 0 && y>0)
            {
                return Math.PI - Math.Abs(Math.Atan(y / x));
            }
            else if(x <0 && y<0)
            {
                return Math.PI + Math.Atan(y / x);
            }
            else if (x > 0 && y < 0)
            {
                return Math.PI *2 - Math.Atan(y / x);
            }
            else if(x == 0 && y >0)
            {
                return Math.Atan(Alogrithm.Du2Rad(90));
            }
            else if (x == 0 && y < 0)
            {
                return Math.Atan(Alogrithm.Du2Rad(270));
            }
            else
            {
                return Math.Abs(Math.Atan(y / x));
            }
        }

        public static double Du2Rad(double Du)
        {
            return Du * Math.PI / 180;
        }


        //计算两点间距离
        public static double CalDistance(ZHPoints p1,ZHPoints p2)
        {
            return Math.Sqrt(Math.Pow(p1.X - p2.X, 2) + Math.Pow(p1.Y - p2.Y, 2));
        }

        //计算一个点的五个最邻近点
        public static void CalLeastFivePoints(List<ZHPoints> points)
        {
            //定义一个点到其它点的距离集合
            for(int i = 0;i<points.Count;i++)
            {
                List<ZHPoints> OtherPoints = new List<ZHPoints>();
                for(int j =0;j<points.Count;j++)
                {
                    if(i == j)
                    {
                        continue;
                    }
                    else
                    {
                        points[j].Dis = CalDistance(points[i],points[j]);
                        OtherPoints.Add(points[j]);
                    }
                }
                points[i].LeastFivePoints =  OtherPoints.OrderBy(p => p.Dis).Take(5).ToList();
                OtherPoints = null;
            }
        }
        //内插高程计算，需先计算五个最近点
        public static void CalPointsHeight(ZHPoints point)
        {
            double Sum_up = 0, Sum_down = 0;
            for(int i =0; i<point.LeastFivePoints.Count;i++)
            {
                Sum_up += point.LeastFivePoints[i].H / CalDistance(point, point.LeastFivePoints[i]);
                Sum_down += 1 / CalDistance(point, point.LeastFivePoints[i]);
            }
            point.H = Sum_up / Sum_down;
        }

        //计算两点间断面面积
        public static double CalDuanMianArea(ZHPoints p1,ZHPoints p2)
        {
            double Si = (p1.H + p2.H - 2 * H0) / 2.0 * CalDistance(p2, p1);
            return Si;
        }


    }
}
