using System.Collections.Generic;
using System.Linq;

namespace Exrercises.Linq2
{
    class FullExercise1
    {
       
        // The following method should return true if each element in the squares sequence
        // is equal to the square of the corresponding element in the numbers sequence.
        public static bool TestForSquares(IEnumerable<int> numbers, IEnumerable<int> squares)
        {
            var a = numbers.ToArray();
            var b = squares.ToArray();

            if (a.Length != b.Length)
            {
                return false;
            }
            for (int i = 0; i < a.Length; i++)
            {
                if (a[i] * a[i] != b[i])
                {
                    return false;
                }
            }

            return true;
        }
        //public static bool TestForSquaresLinq(IEnumerable<int> numbers, IEnumerable<int> squares)
        //{


        //}
    }
}
