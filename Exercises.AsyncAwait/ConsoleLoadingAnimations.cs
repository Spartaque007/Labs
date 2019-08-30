using System;

namespace Exercises.AsyncAwait
{
    public class ConsoleLoadingAnimations
    {
        private const string PatternString = "          ]";
        private const char StatusSymbol = '#';

        private readonly int _positionTopStart;
        private readonly int _positionLeftStart;

        private int _currentPercent = 0;
        private int _currentSymbolsCount = 0;
        private int _currentSymbolPosition;


        public ConsoleLoadingAnimations(string text)
        {
            if (!(Console.CursorLeft == 0))
            {
                Console.WriteLine();
            }
            var startText = text + " [";
            _positionLeftStart = Console.CursorLeft + startText.Length;
            _positionTopStart = Console.CursorTop;
            Console.Write(startText);
            Console.Write(PatternString + $"{_currentPercent} % ");
            _currentSymbolPosition = _positionLeftStart;
        }


        public void Update(int incPercent)
        {

            if (incPercent < 0)
            {
                throw new ArgumentException("incPercent < 0");
            }

            _currentPercent += incPercent;

            if (_currentPercent > 100)
            {
                throw new ArgumentException("total percent>100");
            }

            var statusSymbolsDelta = (_currentPercent / 10) - _currentSymbolsCount;

            if (statusSymbolsDelta > 0)
            {
                PrintSymbols(statusSymbolsDelta);
            }
        }


        private void PrintSymbols(int count)
        {
            int _positionTopPrev = Console.CursorTop;
            int _positionLeftPrev = Console.CursorLeft;
            Console.CursorVisible = false;
            Console.CursorLeft = _currentSymbolPosition;
            Console.CursorTop = _positionTopStart;

            for (int i = 0; i < count; i++)
            {
                Console.Write(StatusSymbol);
                if (Console.CursorLeft == _positionLeftStart + PatternString.Length - 1)
                {
                    break;
                }
            }

            Console.CursorLeft = _currentSymbolPosition + PatternString.Length;
            Console.Write($"{_currentPercent} %");
            Console.CursorTop = _positionTopPrev;
            Console.CursorLeft = _positionLeftPrev;
            Console.CursorVisible = true;
        }
    }
}