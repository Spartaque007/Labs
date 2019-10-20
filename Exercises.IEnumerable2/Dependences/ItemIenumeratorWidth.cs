using System;
using System.Collections;
using System.Collections.Generic;

namespace Exercises.IEnumerable2
{
    class ItemIenumeratorWidth : IEnumerator<Item>
    {
        private Queue<Item> queue;
        public Item Current { get; private set; }

        object IEnumerator.Current => throw new NotImplementedException();

        public ItemIenumeratorWidth(IList<Item> items)
        {
            foreach (var item in items)
            {
                queue.Enqueue(item);
            }
        }
    

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public bool MoveNext()
        {
            throw new NotImplementedException();
        }

        public void Reset()
        {
            throw new NotImplementedException();
        }
    }
}