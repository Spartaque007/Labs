using System;

namespace Exercises.IEnumerable2.Items
{
    class Article : Item
    {
        public override void Draw() => Console.WriteLine(nameof(Article));
    }
}