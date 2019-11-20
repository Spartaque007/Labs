using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameInterface
{
    public class GameNameAttribute : Attribute
    {
        public string GameName { get; set; }

        public GameNameAttribute(string gameName)
        {
            GameName = gameName;
        }
    }
}