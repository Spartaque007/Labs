using System;
using System.Collections;

namespace Exercises.IEnumerable2.Items
{
    class SimpleItem :Item
    {
        public SimpleItem() : base(null)
        {

        }

        public SimpleItem(IEnumerator enumerator) : base()
        {

        }

        public override void Draw()
        {
            Console.WriteLine(nameof(SimpleItem));
        }
    }
}