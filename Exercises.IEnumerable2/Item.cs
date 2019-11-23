using Exercises.IEnumerable2.Dependences;
using System.Collections.Generic;

namespace Exercises.IEnumerable2
{
    public abstract class Item
    {
        private const string Name = "Item";


        public IList<Item> ChildItems { get; private set; }

        public IEnumerable<Item> ItemsInDepth => new ItemInDepthCollection(this);

        public IEnumerable<Item> ItemInWidth => new ItemInWidthCollection(this);


        public Item()
        {
            ChildItems = new List<Item>();
        }


        public virtual void Draw()
        {
            System.Console.WriteLine(Name);
        }

        public virtual void AddItem(Item item)
        {
            ChildItems.Add(item);
        }
    }
}