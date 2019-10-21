using System.Collections.Generic;
using System.Collections.ObjectModel;

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
            Inventories = new List<Inventory>();
        }
    }
}
