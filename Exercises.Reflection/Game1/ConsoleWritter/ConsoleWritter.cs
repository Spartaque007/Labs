using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1
{
    public class ConsoleWritter
    {
        private Dictionary<string, ConsoleField> _fields;

        public void AddField(string fieldText,int LineNumber, ConsoleColor textColor = ConsoleColor.White)
        {
            _fields = new Dictionary<string, ConsoleField>();
            var positionTop = LineNumber;
            var field = new ConsoleField(positionTop, fieldText, textColor);
            _fields.Add(fieldText, field);
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