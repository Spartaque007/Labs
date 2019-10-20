using System;
using System.Collections;

namespace Exercises.IEnumerable2.Items
{
    class NavBar : Item
    {

        public NavBar() : base(null)
        {

        }

        public NavBar(IEnumerator enumerator) : base()
        {

        }

        public override void Draw()
        {
            Console.WriteLine(nameof(NavBar));
        }
    }
}