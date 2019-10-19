using System.Collections;

namespace Exercises.IEnumerable2
{
    class ItemIenumeratorWidth : IEnumerator
    {
        private IList collection;

        private int _currentIndex;

        public int Capacity { get; private set; }
        public object Current { get; set; }


        public ItemIenumeratorWidth(IList collection)
        {
            Capacity = collection.Count;
            _currentIndex = 0;
            Current = collection[_currentIndex];
        }


        public bool MoveNext()
        {

            if (++_currentIndex > collection.Count)
            {
                return false;
            }
            else
            {
                _currentIndex++;
                Current = collection[_currentIndex];
                return true;
            }
        }

        public void Reset()
        {
            _currentIndex = 0;
            Current = collection[_currentIndex];
        }
    }
}
