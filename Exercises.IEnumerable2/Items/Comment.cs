using System;
using System.Collections;

namespace Exercises.IEnumerable2.Items
{
    class Comment : Item
    {
        public Comment() : base(null)
        {

        }

        public Comment(IEnumerator enumerator) : base()
        {

        }

        public override void Draw()
        {
            Console.WriteLine(nameof(Comment));
        }
    }
}
