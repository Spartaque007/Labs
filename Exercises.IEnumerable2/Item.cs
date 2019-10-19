using System.Collections;
using System.Collections.Generic;

namespace Exercises.IEnumerable2
{
    public class Item : IEnumerable, IItem
    {
        private IEnumerator _enumerator;

        public string Name { get; set; }

        public IList<IItem> Items { get; }

        public IEnumerator Enumerator
        {
            get
            {
                return _enumerator;
            }
            set
            {
                if (value != null)
                {
                    _enumerator = value;
                }
                else
                {
                    _enumerator = new ItemIenumeratorDepth();
                }
            }
        }

        public Item() : this(null) 
        {
            
        }

        public Item(IEnumerator enumerator)
        {
            Items = new List<IItem>();
            Enumerator = enumerator;
        }


        public void AddItem(IItem item)
        {
            Items.Add(item);
        }

        public IEnumerator GetEnumerator()
        {
            return Enumerator;
        }


    }
}