using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace Library
{
    public class DataContext
    {
        public List<Client> Clients { get; set; }
        public Dictionary<string, Catalog> Books { get; set; }
        public ObservableCollection<Event> Events { get; set; }
        public List<Inventory> Inventories { get; set; }
        public List<string> Notifications { get; set; }

        public DataContext()
        {
            Clients = new List<Client>();
            Books = new Dictionary<string, Catalog>();
            Notifications = new List<string>();
            Events = new ObservableCollection<Event>();
            Events.CollectionChanged += (sender, e) =>
            {
                if (e.Action == NotifyCollectionChangedAction.Add)
                    Notifications.Add("Added event: " + e.NewItems[0]);
            };
            Inventories = new List<Inventory>();
        }
        public DataContext(List<Client> clients, Dictionary<string, Catalog> books, List<string> notifications, ObservableCollection<Event> events, List<Inventory> inventories)
        {
            Clients = clients;
            Books = books;
            Notifications = notifications;
            Events = events;
            Events.CollectionChanged += (sender, e) =>
            {
                if (e.Action == NotifyCollectionChangedAction.Add)
                    Notifications.Add("Added event: " + e.NewItems[0]);
            };
            Inventories = inventories;
        }
    }
}
