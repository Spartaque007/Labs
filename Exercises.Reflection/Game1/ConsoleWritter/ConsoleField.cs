using System;

namespace Game1
{
    public class ConsoleField
    {
        private int _prevCursorTopPos;
        private int _prevCursorLeftPos;
        private ConsoleColor _prevTextColor;

        public int TopPositionStart { get; private set; }

        public int LeftPositionStart { get; private set; }

        public int LeftPositionCurrent { get; private set; }

        public ConsoleColor TextColor { get; set; }


        public ConsoleField(int LineNumber, string permanentText, ConsoleColor textColor = ConsoleColor.White)
        {
            TextColor = textColor;
            Console.ForegroundColor = textColor;
            TopPositionStart = LineNumber;
            Console.CursorTop = LineNumber;
            Console.CursorLeft = 0;
            Console.Write(permanentText);
            LeftPositionStart = Console.CursorLeft;
            LeftPositionCurrent = LeftPositionStart;
        }


        public void Print(string text)
        {
            SavePrevCursorPosition();
            Console.CursorTop = TopPositionStart;
            Console.CursorLeft = LeftPositionCurrent;
            Console.Write(text);
            LeftPositionCurrent = Console.CursorLeft;
            RestorePrevCursorPosition();
        }

        public void Print(string text, int offset)
        {
            SavePrevCursorPosition();
            Console.CursorTop = TopPositionStart;
            Console.CursorLeft = LeftPositionStart + offset;
            Console.Write(text);
            if (Console.CursorLeft > LeftPositionCurrent)
            {
                LeftPositionCurrent = Console.CursorLeft;
            }
            RestorePrevCursorPosition();
        }

        public void Clear()
        {
            SavePrevCursorPosition();
            Console.CursorTop = TopPositionStart;
            Console.CursorLeft = LeftPositionStart;

            for (int i = LeftPositionStart; i <= LeftPositionCurrent; i++)
            {
                Console.Write(" ");
            }
            RestorePrevCursorPosition();
        }


        private void SavePrevCursorPosition()
        {
            _prevCursorLeftPos = Console.CursorLeft;
            _prevCursorTopPos = Console.CursorTop;
            _prevTextColor = Console.ForegroundColor;
        }

        private void RestorePrevCursorPosition()
        {
            Console.CursorLeft = _prevCursorLeftPos;
            Console.CursorTop = _prevCursorTopPos;
            Console.ForegroundColor = _prevTextColor;
        }
    }
}