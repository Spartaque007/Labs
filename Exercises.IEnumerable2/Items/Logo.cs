
using System;
using System.Collections;

namespace Exercises.IEnumerable2.Items
{
    class Logo : Item
    {
        public override void Draw() => Console.WriteLine(nameof(Logo));
    }
}