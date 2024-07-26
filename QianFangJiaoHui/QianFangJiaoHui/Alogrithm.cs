using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QianFangJiaoHui
{
   static class Alogrithm
    {

        public static void PosiitonTransfer(PhotoImage p1)
        {
            double a1 = Math.Cos(p1.fai) * Math.Cos(p1.k) - Math.Cos(p1.fai) * Math.Sin(p1.omega) * Math.Sin(p1.k);
            double a2 = -Math.Cos(p1.fai) * Math.Sin(p1.k) - Math.Sin(p1.fai) * Math.Sin(p1.omega) * Math.Sin(p1.k);
            double a3 = -Math.Sin(p1.fai) * Math.Cos(p1.omega);

            double b1 = Math.Cos(p1.omega) * Math.Sin(p1.k);
            double b2 = Math.Cos(p1.omega) * Math.Cos(p1.k);
            double b3 = -Math.Sin(p1.omega);

            double c1 = (Math.Sin(p1.fai) * Math.Cos(p1.k)) + (Math.Cos(p1.fai) * Math.Sin(p1.omega) * Math.Sin(p1.k));
            double c2 = (-Math.Sin(p1.omega)*Math.Cos(p1.k)) +(Math.Cos(p1.fai) * Math.Sin(p1.omega) * Math.Sin(p1.k));
            double c3 = Math.Cos(p1.fai) * Math.Cos(p1.omega);

            p1.u = a1 * p1.x + a2 * p1.y - a3 * p1.f;
            p1.v = b1 * p1.x + b2 * p1.y - b3 * p1.f;
            p1.w = c1 * p1.x + c2 * p1.y - c3 * p1.f;

        }


        public static Model ProjectionIndex(PhotoImage p1,PhotoImage p2)
        {
            Model model = new Model();
            model.Bu = p2.Xs - p1.Xs;
            model.Bv = p2.Ys - p1.Xs;
            model.Bw = p2.Zs - p1.Zs;

            double btm = p1.u * p2.w - p2.u * p1.w;

            model.N1 = (model.Bu * p2.w - model.Bw * p2.u)/btm;
            model.N2 = (model.Bu * p1.w - model.Bw * p1.u) / btm;


            return model;
        }

        public static GroundP  GroundPResult(PhotoImage p1,PhotoImage p2,Model model)
        {
            GroundP groundp = new GroundP();

            groundp.X = p1.Xs + model.N1 * p1.u;
            groundp.Y = 0.5 * ((p1.Ys + model.N1 * p1.v) + (p2.Ys + model.N2 * p2.v));
            groundp.Z = p1.Zs + model.N1 * p1.w;

            return groundp;
        }
    }
}
