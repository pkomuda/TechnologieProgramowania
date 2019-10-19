using System;

namespace Library
{
    public class Catalog
    {
        public string ID { get; private set; }
        public string Title { get; private set; }
        public string Author { get; private set; }

        public Catalog(string title, string author)
        {
            ID = Guid.NewGuid().ToString();
            Title = title;
            Author = author;
        }

        public Catalog(string id, string title, string author)
        {
            ID = id;
            Title = title;
            Author = author;
        }
    }
}
