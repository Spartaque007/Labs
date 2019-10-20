using System;
using System.Collections;

namespace Exercises.IEnumerable2.Items
{
    public class Root : Item
    {
        public Root() : base (null)
        {

        }

        public Root(IEnumerator  enumerator ) : base ()
        {

        }

        public override void Draw()
        {
           Console.WriteLine(nameof(Root));
        }
    }
}