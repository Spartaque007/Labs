
using System;
using System.Collections;

namespace Exercises.IEnumerable2.Items
{
    class Logo : Item
    {
        public Logo() : base(null)
        {

        }

        public Logo(IEnumerator enumerator) : base()
        {

        }

        public override void Draw()
        {
            Console.WriteLine(nameof(Logo));
        }
    }
}