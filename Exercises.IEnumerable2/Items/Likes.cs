using System;
using System.Collections;

namespace Exercises.IEnumerable2.Items
{
    class Likes : Item
    {
        public override void Draw() => Console.WriteLine(nameof(Likes));
    }
}