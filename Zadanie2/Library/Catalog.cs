using System;

namespace Library
{
    public class Catalog
    {
        public string ID { get; }
        public string Title { get; set; }
        public string Author { get; set; }

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
        public override string ToString()
        {
            return "Catalog: " + ID + " Title: " + Title + " Author: " + Author;
        }
    }
}
