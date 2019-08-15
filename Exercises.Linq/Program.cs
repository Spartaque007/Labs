using System;
using System.Collections;
using System.Linq;

namespace Exercises.Linq
{
    class Program
    {
        static void Main(string[] args)
        {
            TaskData data = new TaskData();


            //1) Find the words in the collection that start with the letter 'L' from fruits
            PrintHead(data.Fruits, 1);
            var wordsWithL = data.Fruits.Where(f => f[0] == 'L').ToList();
            PrintResult(wordsWithL);


            //2) Which of the following numbers are multiples of 4 or 6 from mixedNumbers
            PrintHead(data.MixedNumbers, 2);
            var numbers = data.MixedNumbers.Where(n => n % 4 == 0 || n % 6 == 0).ToList();
            PrintResult(numbers);


            //3) sort list of names in both directions from names
            PrintHead(data.Names, 3);
            var sortedNames = data.Names.OrderBy(n => n);
            var sortedNamesDesc = data.Names.OrderByDescending(n => n);
            PrintResult(sortedNames);
            PrintResult(sortedNamesDesc);

            
            //4)How much money have we made? purchases
            PrintHead(data.Purchases,4);
            var summ = data.Purchases.Sum();
            Console.WriteLine("\nResults:\n");
            Console.WriteLine(summ);
            Console.WriteLine("******************End******************\n");


            /*5)
               Store each number in the following List until a perfect square
               is detected.
               Ref: https://msdn.microsoft.com/en-us/library/system.math.sqrt(v=vs.110).aspx
               wheresSquaredo
            */
            PrintHead(data.wheresSquaredo,5);
            var numbersCollectoin = data.wheresSquaredo.TakeWhile(n => Math.Sqrt(n) % 1 != 0).ToList();
            PrintResult(numbersCollectoin);


            /*6)
            Given the same customer set, display how many millionaires per bank.
            from customers
            */
            PrintHead(data.Customers, 6);
            var millionaires = data.Customers
                .Where(c => c.Balance >= 1_000_000)
                .GroupBy(b => b.Bank)
                .Select(a => new { Bank = a.Key, Count = a.Count() }).ToList();
            PrintResult(millionaires);
        }

        static void PrintHead(IEnumerable dataList, double taskNumber)
        {
            Console.WriteLine($"******************Task {taskNumber}******************");
            Console.WriteLine("\n******************Input Data******************\n");
            PrintCollection(dataList);
        }

        static void PrintResult(IEnumerable resultList)
        {
            Console.WriteLine("\nResults:\n");
            PrintCollection(resultList);
            Console.WriteLine("******************End******************\n");
        }

        static void PrintCollection(IEnumerable resultList)
        {
            foreach (var item in resultList)
            {
                Console.WriteLine(item);
            }
        }
    }
}