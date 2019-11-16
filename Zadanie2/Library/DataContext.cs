using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace Library
{
    public class DataContext
    {
        public List<Client> Clients { get; }
        public Dictionary<string, Catalog> Books { get; }
        public ObservableCollection<Event> Events { get; }
        public List<Inventory> Inventories { get; }
        public List<string> Notifications { get; }

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
    }
}
