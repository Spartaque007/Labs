using GameInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Reflector
{
    class Program
    {
        static void Main()
        {
            Assembly game = Assembly.LoadFrom(@"d:\Labs\Labs\Exercises.Reflection\Game1\bin\Release\Game1.dll");
            var t = game.GetType("Game1.Game");
            IGame a = (IGame)Activator.CreateInstance(t);
            a.Run();
            
        }
    }
}