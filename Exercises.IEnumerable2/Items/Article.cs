using System;
using System.Collections;

namespace Exercises.IEnumerable2.Items
{
    class Article : Item
    {
        public Article() : base(null)
        {

        }

        public Article(IEnumerator enumerator) : base()
        {

        }

        public override void Draw()
        {
            Console.WriteLine(nameof(Article));
        }
    }
}