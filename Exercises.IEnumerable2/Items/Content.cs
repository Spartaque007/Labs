using System;
using System.Collections;

namespace Exercises.IEnumerable2.Items
{
    class Content : Item
    {
        public Content() : base(null)
        {

        }

        public Content(IEnumerator enumerator) : base()
        {

        }

        public override void Draw()
        {
            Console.WriteLine(nameof(Content));
        }
    }
}