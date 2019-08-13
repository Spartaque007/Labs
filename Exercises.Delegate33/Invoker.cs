using System;
using System.Text;

namespace Exercises.Delegate33
{
    public class Invoker
    {
        private Func<string> _functions;

        public Invoker(Func<string> functions)
        {
            _functions = functions;
        }


        public string GetResultFromFunctions()
        {
            var resultStringBuilder = new StringBuilder();
            var invocationList = _functions.GetInvocationList();

            foreach (var function in invocationList)
            {
                var currentResult= function.DynamicInvoke();
                resultStringBuilder.Append(currentResult);
            }

            return resultStringBuilder.ToString();
        }
    }
}