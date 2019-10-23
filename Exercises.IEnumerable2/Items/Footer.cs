using System;

namespace Exercises.IEnumerable2.Items
{
    class Footer : Item
    {
        public override void Draw() => Console.WriteLine(nameof(Footer));
    }
}