using Exercises.IEnumerable2.Dependences;
using Exercises.IEnumerable2.Items;

namespace Exercises.IEnumerable2
{
    class Program
    {
        static void Main(string[] args)
        {
            var root = new Root();
            var header = new Header();
            var logo = new Logo();
            var navBar = new NavBar();
            var item = new SimpleItem();
            var item2 = new SimpleItem();
            var footer = new Footer();
            var content = new Content();
            var title = new Title();
            var article = new Article();
            var comment = new Comment();
            var comment2 = new Comment();
            var likes = new Likes();

            root.AddItem(header);
            root.AddItem(navBar);
            root.AddItem(footer);
            root.AddItem(content);
            header.AddItem(logo);
            navBar.AddItem(item);
            navBar.AddItem(item2);
            content.AddItem(title);
            content.AddItem(article);
            content.AddItem(comment);
            content.AddItem(comment2);
            content.AddItem(likes);

            foreach (var i in root.ItemsInDepth)
            {
                i.Draw();
            }

            System.Console.WriteLine("***********************");

            foreach (var i in root.ItemInWidth)
            {
                i.Draw();
            }
        }
    }
}