using System;

namespace Exercises.AsyncAwait
{
    public class StatusLineEventArgs : EventArgs
    {
        public decimal Delta { get; }


        public StatusLineEventArgs(decimal delta)
        {
            Delta = delta;
        }
    }
}