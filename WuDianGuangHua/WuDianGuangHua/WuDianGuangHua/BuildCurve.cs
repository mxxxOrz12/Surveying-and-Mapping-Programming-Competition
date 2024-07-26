using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WuDianGuangHua
{
   static class BuildCurve
    {

        //补点
        public static List<MyPoint> SupplyPoint(List<MyPoint> point_list,bool isClose)
        {
            List<MyPoint> SuppliedPoints = new List<MyPoint>();
            //初始化赋值
            foreach(var point in point_list)
            {
                SuppliedPoints.Add(point);
            }

            //如果首尾闭合
            if(isClose)
            {

                //头部插点
                SuppliedPoints.Insert(0, SuppliedPoints[SuppliedPoints.Count - 1]);
                SuppliedPoints.Insert(0, SuppliedPoints[SuppliedPoints.Count - 2]);

                SuppliedPoints.Add(SuppliedPoints[2]);
                SuppliedPoints.Add(SuppliedPoints[3]);
            }
            //不闭合
            else
            {
                MyPoint p1 = SuppliedPoints[0];
                MyPoint p2 = SuppliedPoints[1];
                MyPoint p3 = SuppliedPoints[2];

                MyPoint pA = new MyPoint();
                MyPoint pB = new MyPoint();

                pA.X = p3.X - 3 * p2.X + 3 * p1.X;
                pA.Y = p3.Y - 3 * p2.Y + 3 * p1.Y;

                pB.X = p2.X - 3 * p1.X + 3 * pA.X;
                pB.Y = p2.Y - 3 * p1.Y + 3 * pA.Y;

                SuppliedPoints.Insert(0, pB);
                SuppliedPoints.Insert(1, pA);

                p1 = SuppliedPoints[SuppliedPoints.Count - 1];
                p2 = SuppliedPoints[SuppliedPoints.Count - 2];
                p3 = SuppliedPoints[SuppliedPoints.Count - 3];

                MyPoint pC = new MyPoint();
                MyPoint pD = new MyPoint();

                pC.X = p3.X - 3 * p2.X + 3 * p1.X;
                pC.Y = p3.Y - 3 * p2.Y + 3 * p1.Y;

                pD.X = p2.X - 3 * p1.X + 3 * pC.X;
                pD.Y = p2.Y - 3 * p1.Y + 3 * pC.Y;

                SuppliedPoints.Add(pC);
                SuppliedPoints.Add(pD);

            }
            return SuppliedPoints;
        }


        


        //计算各点的X，Y方向梯度
        public static MyPoint CalGradient(List<MyPoint> FivePoints )
        {
            double a1 = FivePoints[1].X - FivePoints[0].X;
            double a2 = FivePoints[2].X - FivePoints[1].X;
            double a3 = FivePoints[3].X - FivePoints[2].X;
            double a4 = FivePoints[4].X - FivePoints[3].X;

            double b1 = FivePoints[1].Y- FivePoints[0].Y;
            double b2 = FivePoints[2].Y- FivePoints[1].Y;
            double b3 = FivePoints[3].Y- FivePoints[2].Y;
            double b4 = FivePoints[4].Y- FivePoints[3].Y;


            double w2 = Math.Abs(a3 * b4 - a4 * b3);
            double w3 = Math.Abs(a1 * b2 - a2 * b1);

            double a0 = w2 * a2 + w3 * a3;
            double b0 = w2 * b2 + w3 * b3;

            FivePoints[2].cos = a0 / Math.Sqrt(Math.Pow(a0, 2) + Math.Pow(b0, 2));
            FivePoints[2].sin = b0 / Math.Sqrt(Math.Pow(a0, 2) + Math.Pow(b0, 2));

            return FivePoints[2];

        }

        public static List<Curve> CalCurve(List<MyPoint> myPoint_list,bool isClose)
        {
            List<Curve> Curve_list = new List<Curve>();
            List<MyPoint> Supplied_Points = SupplyPoint(myPoint_list, isClose);

            for(int i =2;i<Supplied_Points.Count-2;i++)
            {
                List<MyPoint> FivePoints = new List<MyPoint>();
                FivePoints.Add(Supplied_Points[i - 2]);
                FivePoints.Add(Supplied_Points[i - 1]);
                FivePoints.Add(Supplied_Points[i]);
                FivePoints.Add(Supplied_Points[i + 1]);
                FivePoints.Add(Supplied_Points[i + 2]);
                Supplied_Points[i] = CalGradient(FivePoints);
            }
                 


            for(int i =2;i<Supplied_Points.Count-2;i++)
            {

                double r = Math.Sqrt(Math.Pow(Supplied_Points[i + 1].X - Supplied_Points[i].X, 2) + Math.Pow(Supplied_Points[i + 1].Y - Supplied_Points[i].Y, 2));

                Curve curve = new Curve();
                curve.StartPoint = Supplied_Points[i];
                curve.endPoint = Supplied_Points[i + 1];
                curve.E0 = Supplied_Points[i].X;
                curve.E1 = r * Supplied_Points[i].cos;
                curve.E2 = 3 * (Supplied_Points[i + 1].X - Supplied_Points[i].X) - r * (Supplied_Points[i + 1].cos + 2 * Supplied_Points[i].cos);
                curve.E3 = -2 * (Supplied_Points[i + 1].X - Supplied_Points[i].X) + r * (Supplied_Points[i + 1].cos + Supplied_Points[i].cos);

                curve.F0 = Supplied_Points[i].Y;
                curve.F1 = r * Supplied_Points[i].sin;
                curve.F2 = 3 * (Supplied_Points[i + 1].Y - Supplied_Points[i].Y) - r * (Supplied_Points[i + 1].sin + 2 * Supplied_Points[i].sin);
                curve.F3 = -2 * (Supplied_Points[i + 1].Y - Supplied_Points[i].Y) + r * (Supplied_Points[i + 1].sin + Supplied_Points[i].sin);

                Curve_list.Add(curve);
            }


            return Curve_list;
        }



        public static double PointDistance(MyPoint p1,MyPoint p2)
        {
            return Math.Sqrt(Math.Pow(p2.X - p1.X, 2) + Math.Pow(p2.Y - p1.Y, 2));
        }
    }
}
