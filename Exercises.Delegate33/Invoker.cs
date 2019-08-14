using System;
using System.Text;

namespace Exercises.Delegate33
{
    public static class FuncExtensions
    {
        public static string InvokeList(this Func<string> functions)
        {
            var resultStringBuilder = new StringBuilder();
            var invocationList = functions.GetInvocationList();

            foreach (var function in invocationList)
            {
                var currentResult = function.DynamicInvoke();
                var t = currentResult.GetType();
                resultStringBuilder.Append(currentResult);
            }

            return resultStringBuilder.ToString();
        }
    }
}