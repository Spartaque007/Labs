using System;

namespace Exrercises.Linq2
{
    class Program
    {
        static void Main(string[] args)
        {
            var numbers = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            var numdersSqr = new int[] { 1, 4, 9, 16, 25, 36, 49, 64, 100, 81 };
            var result1 = Exercise.TestForSquares(numbers, numdersSqr);
            Console.WriteLine(result1);

            var words = new string[]
            {
                "One", "Two", "Tree ", "four", "five","six"
            };

            var result2 = Exercise.GetTheLastWord(words);
            Console.WriteLine(result2);

            var numbers1 =new int[]{1, 1, 1, 2, 1, 1};
            var numbers2 = new double[] { 0, 0, 0.55, 0, 0 };
            Console.WriteLine(Exercise.FindUniq(numbers1));
            Console.WriteLine(Exercise.FindUniq(numbers2));
            
            Console.ReadKey();
        }


    }
}