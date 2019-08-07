using System;

namespace Exercises.Delegate
{
    class Program
    {
        static void Main(string[] args)
        {
            var points = new Point[10];
            ArrayIni(points);
            var b = Array.Find(points, GetMinPoint);
            Console.WriteLine($"First min point: {b}");
            Console.WriteLine();
            PrintArray<Point>(points);
            Array.Sort(points, Compare);
            PrintArray<Point>(points);
            ArrayIni(points);
            PrintArray<Point>(points);
            Array.Sort(points);
            PrintArray<Point>(points);
            Console.ReadLine();
        }
        
        static void ArrayIni(Point[] array)
        {
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = new Point(array.Length - i, array.Length - i);
            }
        }
        
        static bool GetMinPoint(Point point)
        {
            var shortWay = Math.Sqrt(Math.Pow(point.X, 2) + Math.Pow(point.Y, 2));
            return  shortWay<= 2;
        }
        
        static void PrintArray<T>(T[] array)
        {
            Console.WriteLine("Array :");
            for (int i = 0; i < array.Length; i++)
            {
                Console.Write(array[i]);
            }
            Console.WriteLine();
        }
        
        static int Compare(Point a, Point b)
        {
            return (a.X + a.Y).CompareTo(b.X + b.Y);
        }


    }
}