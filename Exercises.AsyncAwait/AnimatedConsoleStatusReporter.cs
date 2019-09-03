using Exercises.AsyncAwait.Dependences;
using System;

namespace Exercises.AsyncAwait
{
    public class AnimatedConsoleStatusReporter : IStatusReporter
    {
        private const string PatternString = "          ]";
        private const char StatusSymbol = '#';

        private readonly int _positionTopStart;
        private readonly int _positionLeftStart;
        private readonly string _startText;

        private decimal _currentPercent = 0;
        private int _currentSymbolsCount = 0;
        private int _currentSymbolPosition;


        public AnimatedConsoleStatusReporter(string text)
        {
            if (!(Console.CursorLeft == 0))
            {
                Console.WriteLine();
            }
            _startText = text + " [";
            _positionLeftStart = Console.CursorLeft + _startText.Length;
            _positionTopStart = Console.CursorTop;
            _currentSymbolPosition = _positionLeftStart;
            Console.Write(_startText);
            Console.Write(PatternString + $"{_currentPercent} % \n");
        }

        public void Update(decimal incPercent)
        {

            if (incPercent < 0)
            {
                throw new ArgumentException("incPercent < 0");
            }

            _currentPercent += incPercent;

            if (_currentPercent > 100)
            {
                _currentPercent = 100;
            }

            var statusSymbolsDelta = (_currentPercent / 10) - _currentSymbolsCount;

            if (statusSymbolsDelta > 0)
            {
                PrintSymbols(statusSymbolsDelta);
            }
        }


        private void PrintSymbols(decimal count)
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
            Console.Write($"{(int)_currentPercent} %");
            Console.CursorTop = _positionTopPrev;
            Console.CursorLeft = _positionLeftPrev;
            Console.CursorVisible = true;
        }
    }
}