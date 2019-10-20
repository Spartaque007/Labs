using System;
using System.Collections;

namespace Exercises.IEnumerable2.Items
{
    class Footer : Item
    {
        public Footer() : base(null)
        {

        }

        public Footer(IEnumerator enumerator) : base()
        {

        }

        public override void Draw()
        {
            Console.WriteLine(nameof(Footer));
        }
    }
}