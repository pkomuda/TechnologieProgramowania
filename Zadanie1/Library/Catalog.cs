using System;

namespace Library
{
    public class Catalog
    {
        public string ID { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }

        public Catalog()
        {
            ID = Guid.NewGuid().ToString();
            Title = "Title not specified";
            Author = "Author not specified";
        }

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
