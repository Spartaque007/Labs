using System.Collections.Generic;

namespace Exercises.IEnumerable2
{
    public interface IItem
    {
        string Name { get; set; }

        IList<IItem> Items { get; }

        void AddItem(IItem item);
     }
}
