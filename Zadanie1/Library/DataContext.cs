using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Runtime.Remoting.Channels;

namespace Library
{
    public class DataContext
    {
        public List<Client> Clients { get; set; }
        public Dictionary<string, Catalog> Books { get; set; }
        public ObservableCollection<Event> Events { get; set; }
        public List<Inventory> Inventories { get; set; }

        public DataContext()
        {
            Clients = new List<Client>();
            Books = new Dictionary<string, Catalog>();
            Events = new ObservableCollection<Event>();
            Events.CollectionChanged += (sender, e) =>
            {
                if (e.Action == NotifyCollectionChangedAction.Add)
                    Console.WriteLine("Added event: " + e.NewItems[0]);
            };
            Inventories = new List<Inventory>();
        }
    }
}
