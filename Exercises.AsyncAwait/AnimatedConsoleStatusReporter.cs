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
        private readonly int _currentSymbolsCount;
        private readonly int _currentSymbolPosition;

        private decimal _currentPercent;


        public AnimatedConsoleStatusReporter(string text)
        {
            if (ConsoleWithLocker.CursorLeft != 0)
            {
                ConsoleWithLocker.WriteLine();
            }
            var startText = text + " [";
            _positionLeftStart = ConsoleWithLocker.CursorLeft + startText.Length;
            _positionTopStart = ConsoleWithLocker.CursorTop;
            _currentSymbolPosition = _positionLeftStart;
            ConsoleWithLocker.Write(startText);
            ConsoleWithLocker.Write(PatternString + $"{_currentPercent} % \n");
            _currentPercent = 0;
            _currentSymbolsCount = 0;
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
            var positionTopPrev = ConsoleWithLocker.CursorTop;
            var positionLeftPrev = ConsoleWithLocker.CursorLeft;
            ConsoleWithLocker.CursorVisible = false;
            ConsoleWithLocker.CursorLeft = _currentSymbolPosition;
            ConsoleWithLocker.CursorTop = _positionTopStart;

            for (int i = 0; i < count; i++)
            {
                ConsoleWithLocker.Write(StatusSymbol);
                if (ConsoleWithLocker.CursorLeft == _positionLeftStart + PatternString.Length - 1)
                {
                    break;
                }
            }

            ConsoleWithLocker.CursorLeft = _currentSymbolPosition + PatternString.Length;
            ConsoleWithLocker.Write($"{(int)_currentPercent} %");
            ConsoleWithLocker.CursorTop = positionTopPrev;
            ConsoleWithLocker.CursorLeft = positionLeftPrev;
            ConsoleWithLocker.CursorVisible = true;
        }
    }
}