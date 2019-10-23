using System;

namespace Exercises.IEnumerable2.Items
{
    class Comment : Item
    {
        public override void Draw() => Console.WriteLine(nameof(Comment));
    }
}
