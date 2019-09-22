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

        private int _currentSymbolsCount;
        private int _currentSymbolPosition;
        private decimal _currentPercent;

        public int CurrentStatus { get => (int)_currentPercent; }


        public AnimatedConsoleStatusReporter(string text)
        {
            var currentPosition = ConsoleWithLocker.GetCurrentCursorPosition();
            if (currentPosition.Left != 0)
            {
                ConsoleWithLocker.WriteLine();
            }
            var startText = text + " [";
            _positionLeftStart = currentPosition.Left + startText.Length;
            _positionTopStart = currentPosition.Top;
            _currentSymbolPosition = 0;
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

            if (_currentPercent > 99)
            {
                _currentPercent = 100;
            }

            var statusSymbolsDelta = (int)((_currentPercent / 10) - _currentSymbolsCount);

            if (statusSymbolsDelta > 0)
            {
                PrintSymbols(statusSymbolsDelta);
            }
        }


        private void PrintSymbols(int count)
        {
            for (int i = 0; i < count; i++)
            {

                var position = new ConsoleCursorPosition
                {
                    Top = _positionTopStart,
                    Left = _positionLeftStart + _currentSymbolPosition,
                    Visible = false
                };

                if (_currentSymbolPosition < 10)
                {
                    ConsoleWithLocker.Write(StatusSymbol, position);
                    _currentSymbolPosition++;
                    _currentSymbolsCount++;
                }

                position.Left = _positionLeftStart + PatternString.Length;
                ConsoleWithLocker.Write((int)_currentPercent + " %", position);
            }
        }
    }
}