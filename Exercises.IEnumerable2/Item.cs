using System.Collections;
using System.Collections.Generic;

namespace Exercises.IEnumerable2
{
    public abstract class Item : IEnumerable
    {
        private IEnumerator<Item> _enumerator;
        private IList<Item> _collection;

        public IEnumerator Enumerator
        {
            get
            {
                return _enumerator;
            }
            private set
            {
                if (value == null)
                {
                    _enumerator = new ItemIenumeratorWidth(_collection);
                }
                else
                {
                    _enumerator = value;
                }
            }
        }

        public string Name { get; set; }

        public Item() : this(null)
        {

        }

        public Item(IEnumerator<Item> enumerator)
        {
            _collection = new List<Item>();
            Enumerator = enumerator;
            
        }

        public IEnumerator GetEnumerator()
        {
            return _enumerator;
        }

        public virtual void Draw()
        {
            System.Console.WriteLine(Name);
        }

        public virtual void AddItem(Item item)
        {
            _collection.Add(item);
        }
    }
}