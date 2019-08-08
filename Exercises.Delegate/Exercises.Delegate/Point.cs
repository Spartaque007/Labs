using System;

namespace Exercises.Delegate
{
    public class Point : IComparable
    {
        private double _radius;
        private double _angle;

        public double X
        {
            get
            {
                return GetRadiusX(_radius, _angle);
            }
        }

        public double Y
        {
            get
            {
                return GetRadiusY(_radius, _angle);
            }
        }

        public double Radius
        {
            get
            {
                return _radius;
            }
        }

        public double Angle
        {
            get
            {
                return _angle;
            }
        }


        public Point(double x, double y)
        {
            _radius = GetRadiusXy(x, y);
            _angle = GetAngleXy(x, y);
        }

        public Point(double par1, double par2, PointRepresentation pr)
        {
            if (pr == PointRepresentation.Polar)
            {
                _radius = par1;
                _angle = par2;
            }
            else
            {
                _radius = GetRadiusXy(par1, par2);
                _angle = GetAngleXy(par1, par2);
            }
        }


        public int CompareTo(object obj)
        {
            Point x = obj as Point;
            if (x != null)
            {
                return _radius.CompareTo(x._radius);
            }
            else
            {
                throw new ArgumentException(" Parameters type filed");
            }
        }

        public override string ToString()
        {
            return $"({X}, {Y})";
        }

        public void Move(double dx, double dy)
        {
            var x = GetRadiusX(_radius, _angle);
            var y = GetRadiusY(_radius, _angle);
            _radius = GetRadiusXy(x + dx, y + dy);
            _angle = GetAngleXy(x + dx, y + dy);
        }

        public void Rotate(double angle)
        {
            _angle += angle;
        }


        private static double GetRadiusXy(double x, double y)
        {
            return Math.Sqrt(x * x + y * y);
        }

        private static double GetAngleXy(double x, double y)
        {
            return Math.Atan2(y, x);
        }

        private static double GetRadiusX(double r, double a)
        {
            return r * Math.Cos(a);
        }

        private static double GetRadiusY(double r, double a)
        {
            return r * Math.Sin(a);
        }
    }
}