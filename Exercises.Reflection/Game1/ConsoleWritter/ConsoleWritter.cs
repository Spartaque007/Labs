using System;
using System.Collections.Generic;

namespace Game1
{
    public class ConsoleWritter
    {
        private  Dictionary<string, ConsoleField> _fields;

        public ConsoleWritter()
        {
            _fields = new Dictionary<string, ConsoleField>();
        }


        public void AddField(string fieldText,int LineNumber, ConsoleColor textColor = ConsoleColor.White)
        {
            var positionTop = LineNumber;
            var field = new ConsoleField(positionTop, fieldText, textColor);
            _fields[fieldText]= field;
        }

        public void  Print(string fieldName, string text)
        {
            _fields[fieldName].Print(text);
        }

        public void Print(string fieldName, string text, int offset)
        {
            _fields[fieldName].Print(text, offset);
        }

        public void Clear(string fieldName)
        {
            _fields[fieldName].Clear();
        }
    }
}