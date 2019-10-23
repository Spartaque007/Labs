using System;

namespace Exercises.IEnumerable2.Items
{
    public class Root : Item
    {
        public override void Draw() => Console.WriteLine(nameof(Root));
    }
}