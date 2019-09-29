using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Exercises.AsyncAwait
{
    public static class TaskExtensions
    {
        public static void Forget(this Task task)
        {
            task.ContinueWith(t => LogException(t.Exception), TaskContinuationOptions.OnlyOnFaulted);
        }


        private static void LogException(Exception ex)
        {
            if (Debugger.IsAttached)
            {
                Debug.WriteLine(ex.Message);
                Debug.Close();
            }
        }
    }
}