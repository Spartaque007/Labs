using System.Collections;
using System.Collections.Generic;

namespace Exercises.IEnumerable2.Dependences
{
    public class ItemInWidthCollection : IEnumerable<Item>, IEnumerator<Item>
    {
        private Item _mainItem;
        private Stack<Item> stack;

        public Item Current { get; private set; }

        object IEnumerator.Current => (object)Current;


        public ItemInWidthCollection(Item item)
        {
            stack = new Stack<Item>();
            stack.Push(item);
            _mainItem = item;
            Current = item;
        }


        public void Dispose()
        {
        }

        public IEnumerator<Item> GetEnumerator()
        {
            return this;
        }

        public bool MoveNext()
        {
            if (stack.Count > 0)
            {
                Current = stack.Pop();
                AddChildItemsInQueue(Current);
                return true;
            }

            return false;
        }

        public void Reset()
        {
            Current = _mainItem;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this;
        }


        private void AddChildItemsInQueue(Item item)
        {
            if (item.ChildItems.Count > 0)
            {
                foreach (var i in Current.ChildItems)
                {
                    stack.Push(i);
                }
            }
        }
    }
}