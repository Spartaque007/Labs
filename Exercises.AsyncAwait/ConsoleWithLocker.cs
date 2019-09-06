using System;

namespace Exercises.AsyncAwait
{
    public static class ConsoleWithLocker
    {
        private static readonly object Locker;


        public static bool CursorVisible
        {
            get
            {
               return LockFuncAndRun(()=>Console.CursorVisible);
            }
            set
            {
                LockActionAndRun(() => Console.CursorVisible = value);
            }
        }

        public static int CursorLeft
        {
            get
            {
                return LockFuncAndRun(() => Console.CursorLeft);
            }

            set
            {
                LockActionAndRun(() => Console.CursorLeft = value);
            }
        }

        public static int CursorTop
        {
            get
            {
                return LockFuncAndRun(() => Console.CursorTop);
            }

            set
            {
                LockActionAndRun(() => Console.CursorTop = value);
            }
        }


        static ConsoleWithLocker()
        {
            Locker = new Object();
        }


        public static void Write<T>(T s)
        {
            LockActionAndRun(() => Console.Write(s));
        }


        public static void WriteLine<T>(T s)
        {
            LockActionAndRun(() => Console.WriteLine(s));
        }

        public static void WriteLine()
        {
            LockActionAndRun(Console.WriteLine);
        }

        public static string ReadLine()
        {
            return Console.ReadLine();
        }

        public static void Clear()
        {
            CursorLeft = 0;
            CursorTop = 0;
            CursorVisible = true;
            Console.Clear();
        }


        private static void LockActionAndRun(Action op)
        {
            lock (Locker)
            {
                op.Invoke();
            }
        }

        private static T LockFuncAndRun<T>(Func<T> func)
        {
            lock (Locker)
            {
               return func.Invoke();
            }
        }
    }
}