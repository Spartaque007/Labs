using System.Collections.Generic;
using System.Linq;

namespace Exrercises.Linq2
{
    class Exercise
    {
        // The following method should return true if each element in the squares sequence
        // is equal to the square of the corresponding element in the numbers sequence.
        public static bool TestForSquares(IEnumerable<int> numbers, IEnumerable<int> squares)
        {
            var a = numbers.Select((number, index) =>
            {
                var item = squares.Skip(index).Take(1).Single();
                return number * number == item;
            })
               .All(x => x);

            return a;
        }

        // Given a sequence of words, get rid of any that don't have the character 'e' in them,
        // then sort the remaining words alphabetically, then return the following phrase using
        // only the final word in the resulting sequence:
        //    -> "The last word is <word>"
        // If there are no words with the character 'e' in them, then return null.
        //   
        // TRY to do it all using only LINQ statements. No loops or if statements.
        public static string GetTheLastWord(IEnumerable<string> words)
        {
            return words.Where(w => !w.Contains('e')).Select(w => w).OrderByDescending(w => w).FirstOrDefault();
        }

        // There is an array with some numbers.All numbers are equal except for one.Try to find it!
        //It’s guaranteed that array contains more than 3 numbers.
        public static T FindUniq<T>(IEnumerable<T> numbers)
        {
            return  numbers
                    .GroupBy(n => n, (number, count) => new { number, count })
                    .Select(n => n.number)
                    .Last();
        }

        public static int GetCountNumberInNumbersSqr(int CheckNumber, int number )
        {
            var array = new int[number];
            for (int i = 0; i < number; i++)
            {
                array[i] = number * number;
            }

            var g = array.Select(n=>n).
            return 0;
        }
    }
}
