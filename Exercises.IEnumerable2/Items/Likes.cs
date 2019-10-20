using System;
using System.Collections;

namespace Exercises.IEnumerable2.Items
{
    class Likes : Item
    {
        public Likes() : base(null)
        {

        }

        public Likes(IEnumerator enumerator) : base()
        {

        }

        public override void Draw()
        {
            Console.WriteLine(nameof(Likes));
        }
    }
}