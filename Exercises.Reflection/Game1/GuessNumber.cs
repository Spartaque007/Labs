using GameInterface;
using System;

namespace Game1
{
    [GameName("Guess Number")]
    class GuessNumber : IGame
    {
        public void Run()
        {
            bool exit = false;
            var random = new Random();
            var secretNumber = random.Next(0, 10);
            while (!exit)
            {
                bool numberIsGuessed = false;
                int number;
                Console.WriteLine("Please, enter number from 1 to 10");
                var input = Console.ReadLine();
                if (Int32.TryParse(input, out number))
                {
                    if (number == secretNumber)
                    {
                        numberIsGuessed = true;
                    }
                    else if (input.ToUpper() == "EXIT")
                    {
                        exit = true;
                    }
                    if(numberIsGuessed)
                    {
                        bool inputValid = false;
                        Console.WriteLine("You win!!! \n Play again?\n Enter \"Y\" or \"N\" ");

                        while(!inputValid)
                        {
                            var decision = Console.ReadLine();
                            if (decision.ToUpper() == "Y")
                            {
                                inputValid = true;
                                Console.Clear();
                                secretNumber = random.Next(0, 10);
                            }
                            else if (decision.ToUpper() == "N")
                            {
                                inputValid = true;
                                exit = true;
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("You didn't guess");
                    }
                }
            }
        }
    }
}