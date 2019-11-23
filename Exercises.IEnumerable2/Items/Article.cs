using System;

namespace Exercises.IEnumerable2.Items
{
    class Article : Item
    {
        private const string Name = "Article";

        public override void Draw()
        {
            Console.WriteLine(Name);
        }
    }
}