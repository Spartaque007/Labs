﻿using System;

namespace Exercises.Delegate33
{
    public class Logger
    {
        private string _result = "";

        public Func<string> GetWrapper(Func<string> func)
        {
            return () =>
            {
                var a = func();
                _result += a;
                return _result.ToString();
            };
        }
    }
}