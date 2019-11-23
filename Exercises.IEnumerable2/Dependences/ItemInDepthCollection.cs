using System.Collections;
using System.Collections.Generic;

namespace Exercises.IEnumerable2.Dependences
{
    public class ItemInDepthCollection : IEnumerable<Item>, IEnumerator<Item>
    {
        private Item _mainItem;
        private Queue<Item> _queue;

        public Item Current { get; private set; }

        object IEnumerator.Current => (object)Current;


        public ItemInDepthCollection(Item item)
        {
            _queue = new Queue<Item>();
            _queue.Enqueue(item);
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
            if (_queue.Count > 0)
            {
                Current = _queue.Dequeue();
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
                    _queue.Enqueue(i);
                }
            }
        }
    }
}
