using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Library
{
    public class Catalog
    {
        public string ID { get; }
        public string Title { get; set; }
        public string Author { get; set; }
        [JsonIgnore]
        public static List<string> IDs = new List<string>();

        public Catalog(string title, string author)
        {
            ID = Guid.NewGuid().ToString();
            Title = title;
            Author = author;
            IDs.Add(ID);
        }

        [JsonConstructor]
        public Catalog(string id, string title, string author)
        {
            if (IDs.Contains(id))
                return;
            ID = id;
            Title = title;
            Author = author;
            IDs.Add(ID);
        }
        
        ~Catalog()
        {
            IDs.Remove(ID);
        }
        
        public override string ToString()
        {
            return "Catalog: " + ID + " Title: " + Title + " Author: " + Author;
        }
    }
}
