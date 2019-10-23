using System.Collections.Generic;

namespace Exercises.IEnumerable2
{
    public abstract class Item
    {
        public IList<Item> ChildItems { get; private set; }

        public Item()
        {
            ChildItems = new List<Item>();
        }
        public virtual void Draw() => System.Console.WriteLine(nameof(Item));

        public virtual void AddItem(Item item)
        {
            ChildItems.Add(item);
        }
    }
}