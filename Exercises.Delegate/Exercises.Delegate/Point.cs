using System;
using System.Collections;

namespace Exercises.Delegate
{
    public class Point : IComparable
    {

        public enum PointRepresentation { Polar, Rectangular }

        private double r, a;

        public Point(double x, double y)
        {
            r = RadiusGivenXy(x, y);
            a = AngleGivenXy(x, y);
        }

        public Point(double par1, double par2, PointRepresentation pr)
        {
            if (pr == PointRepresentation.Polar)
            {
                r = par1; a = par2;
            }
            else
            {
                r = RadiusGivenXy(par1, par2);
                a = AngleGivenXy(par1, par2);
            }
        }

        public double X
        {
            get { return XGivenRadiusAngle(r, a); }
        }

        public double Y
        {
            get { return YGivenRadiusAngle(r, a); }
        }


        public double Radius
        {
            get { return r; }
        }

        public double Angle
        {
            get { return a; }
        }

        public void Move(double dx, double dy)
        {
            double x, y;
            x = XGivenRadiusAngle(r, a); y = YGivenRadiusAngle(r, a);
            r = RadiusGivenXy(x + dx, y + dy);
            a = AngleGivenXy(x + dx, y + dy);
        }

        public void Rotate(double angle)
        {
            a += angle;
        }

        public override string ToString()
        {
            return "(" + XGivenRadiusAngle(r, a) + "," + YGivenRadiusAngle(r, a) + ")";
        }


        private static double RadiusGivenXy(double x, double y)
        {
            return Math.Sqrt(x * x + y * y);
        }

        private static double AngleGivenXy(double x, double y)
        {
            return Math.Atan2(y, x);
        }

        private static double XGivenRadiusAngle(double r, double a)
        {
            return r * Math.Cos(a);
        }

        private static double YGivenRadiusAngle(double r, double a)
        {
            return r * Math.Sin(a);
        }

        
        public int CompareTo(object obj)
        {
            Point x = obj as Point;
            if (x != null )
            {
                return this.r.CompareTo(x.r);
            }
            else
            {
                throw new ArgumentException(" Parameters type filed");
            }
        }
    }

}
