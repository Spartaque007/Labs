using System;

namespace Exercises.IEnumerable2.Items
{
    class Title : Item
    {
        public override void Draw() => Console.WriteLine(nameof(Title));
    }
}