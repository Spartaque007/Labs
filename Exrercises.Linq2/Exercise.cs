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
        // TRY to do it all using only LINQ statements. No loops or if statements.
        public static string GetTheLastWord(IEnumerable<string> words)
        {
            return words.Where(w => !w.Contains('e')).Select(w => w).OrderByDescending(w => w).FirstOrDefault();
        }

        // There is an array with some numbers.All numbers are equal except for one.Try to find it!
        //It’s guaranteed that array contains more than 3 numbers.
        public static T FindUniq<T>(IEnumerable<T> numbers)
        {
            return numbers
                    .GroupBy(n => n, (number, count) => new { number, count })
                    .Select(n => n.number)
                    .Last();
        }

        //Take an integer `n(n >= 0)` and a digit `d(0 <= d <= 9)` as an integer.Square
        //all numbers `k (0 <= k <= n)` between `0` and `n`. Count the numbers of digits 
        //`d` used in the writing of all the `k**2`. Call `NbDig` the function taking `n` and 
        //`d` as parameters and returning this count.
        public static int GetCountOfNumbers(int checkingNumber, int number)
        {
            var r = GetArraayFromNumer(number)
                .Select(n => n * n)
                .Select(n => GetCountNumberInNumbers(n, checkingNumber))
                .Sum();

            return r;
        }

        public static int GetCountOfNumbersWithYeald(int checkingNumber, int number)
        {
            var r = GetNumbers(number)
                .Select(n => n * n)
                .Select(n => GetCountNumberInNumbers(n, checkingNumber))
                .Sum();

            return r;
        }


        private static IEnumerable<int> GetArraayFromNumer(int number)
        {
            var array = new int[number];

            for (int i = 0; i < number; i++)
            {
                array[i] = (i + 1);
            }
            return array;
        }

        private static int GetCountNumberInNumbers(int number, int numberCheck)
        {
            int count = 0;

            do
            {
                if (number / 10 == numberCheck)
                {
                    count++;
                }
                if (number % 10 == numberCheck)
                {
                    count++;
                }
                number /= 10;
            }
            while (number / 10 > 0);

            return count;
        }

        private static IEnumerable<int> GetNumbers(int number)
        {
            for (int i = 0; i < number; i++)
            {
                yield return i + 1;
            }
        }
    }
}