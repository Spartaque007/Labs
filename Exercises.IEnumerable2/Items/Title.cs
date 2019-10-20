using System;
using System.Collections;

namespace Exercises.IEnumerable2.Items
{
    class Title : Item
    {
        public Title() : base(null)
        {

        }

        public Title(IEnumerator enumerator) : base()
        {

        }

        public override void Draw()
        {
            Console.WriteLine(nameof(Title));
        }
    }
}