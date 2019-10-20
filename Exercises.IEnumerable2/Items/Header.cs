using System;
using System.Collections;

namespace Exercises.IEnumerable2.Items
{
    class Header : Item
    {
        public Header() : base(null)
        {

        }

        public Header(IEnumerator enumerator) : base()
        {

        }

        public override void Draw()
        {
            Console.WriteLine(nameof(Header));
        }
    }
}