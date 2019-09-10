using System;
using System.Threading;

namespace Common.Extensions
{
    public static class EventsExtensions
    {
        public static void Raise(this EventHandler eventHandler, object sender)
        {
            var localHandler = Volatile.Read(ref eventHandler);
            localHandler?.Invoke(sender, EventArgs.Empty);
        }

        public static void Raise<TEventArgs>(this EventHandler<TEventArgs> eventHandler, object sender, TEventArgs eventArgs)
            where TEventArgs : EventArgs
        {
            var localHandler = Volatile.Read(ref eventHandler);
            localHandler?.Invoke(sender, eventArgs);
        }
    }
}