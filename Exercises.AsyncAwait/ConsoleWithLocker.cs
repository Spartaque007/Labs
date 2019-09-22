using System;

namespace Exercises.AsyncAwait
{
    public static class ConsoleWithLocker
    {
        private static readonly object Locker;


        static ConsoleWithLocker()
        {
            Locker = new Object();
        }


        public static void Write<T>(T s)
        {
            LockActionAndRun(() => Console.Write(s));
        }

        public static void Write<T>(T s, ConsoleCursorPosition position)
        {
            LockActionAndRun(() =>
            {
                var prevPos = new ConsoleCursorPosition
                {
                    Left = Console.CursorLeft,
                    Top = Console.CursorTop,
                    Visible = Console.CursorVisible
                };

                MoveCursor(position);
                Console.Write(s);
                MoveCursor(prevPos);
            });
        }

        public static void SetCursorPosition(int top, int left, bool visible)
        {
            LockActionAndRun(() =>
            {
                var cursorPosition = new ConsoleCursorPosition
                {
                    Left = left,
                    Top = top,
                    Visible = visible
                };
                MoveCursor(cursorPosition);
            });
        }

        public static void SetCursorPosition(ConsoleCursorPosition position)
        {
            LockActionAndRun(() =>
            {
                var cursorPosition = new ConsoleCursorPosition
                {
                    Left = position.Left,
                    Top = position.Top,
                    Visible = position.Visible
                };
                MoveCursor(cursorPosition);
            });
        }

        public static ConsoleCursorPosition GetCurrentCursorPosition()
        {
            return LockFuncAndRun(() =>
            {
                var position = new ConsoleCursorPosition
                {
                    Top = Console.CursorTop,
                    Left = Console.CursorLeft,
                    Visible = Console.CursorVisible
                };
                return position;
            });
        }

        public static void WriteLine<T>(T s)
        {
            LockActionAndRun(() => Console.WriteLine(s));
        }
        public static void WriteException<T>(T s)
        {
            LockActionAndRun(() =>
            {
                var consoleColor = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(s);
                Console.ForegroundColor = consoleColor;
            });
        }

        public static void WriteLine()
        {
            LockActionAndRun(Console.WriteLine);
        }

        public static string ReadLine()
        {
            return LockFuncAndRun(Console.ReadLine);
        }

        public static int Read()
        {
            return LockFuncAndRun(Console.Read);
        }

        public static void Clear()
        {
            LockActionAndRun(() =>
            {
                Console.CursorTop = 0;
                Console.CursorLeft = 0;
                Console.CursorVisible = true;
                Console.Clear();
            });
        }


        private static void MoveCursor(ConsoleCursorPosition position)
        {
            Console.CursorTop = position.Top;
            Console.CursorLeft = position.Left;
            Console.CursorVisible = position.Visible;
        }

        private static void LockActionAndRun(Action op)
        {
            lock (Locker)
            {
                op();
            }
        }

        private static T LockFuncAndRun<T>(Func<T> func)
        {
            lock (Locker)
            {
                return func();
            }
        }
    }
}