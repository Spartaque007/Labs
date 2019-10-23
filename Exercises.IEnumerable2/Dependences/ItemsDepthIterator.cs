using System.Collections;
using System.Collections.Generic;

namespace Exercises.IEnumerable2.Dependences
{
    public class ItemsDepthIterator : IEnumerable
    {
        private readonly Item _item;

        public List<Item> collection { get => GetAllChildNodesInDepth(_item); }


        public ItemsDepthIterator(Item item)
        {
            _item = item;
        }

        public IEnumerator GetEnumerator()
        {
            return collection.GetEnumerator();
        }

        private List<Item> GetAllChildNodesInDepth(Item item)
        {
            var tmpQueue = new Queue<Item>();
            var allNodes = new List<Item>();
            if (item.ChildItems.Count > 0)
            {
                allNodes.AddRange(item.ChildItems);
                tmpQueue.Enqueue(item);
                while (tmpQueue.Count != 0)
                {
                    var tmpItem = tmpQueue.Dequeue();
                    if (tmpItem.ChildItems.Count > 0)
                    {
                        allNodes.AddRange(tmpItem.ChildItems);
                    }
                }
            }
            return allNodes;
        }
    }
}