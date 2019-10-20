using System.Collections.Generic;

namespace Library
{
    public class DataRepository
    {
        public DataContext DataContext { get; private set;  }
        private DataFill _FillData;
        public DataFill FillData { get { return _FillData; }
                                   set
                                   {
                                        _FillData = value;
                                        _FillData.Fill(DataContext);
                                   } }

        public DataRepository()
        {
            DataContext = new DataContext();
        }
        #region everything with Catalog
        public void AddCatalog(Catalog catalog)
        {
            DataContext.Books.Add(catalog.ID, catalog);
        }

        public Catalog GetCatalog(string id)
        {
            foreach (Catalog catalog in GetAllCatalog())
            {
                if (id == catalog.ID)
                    return catalog;
            }
            throw new System.InvalidOperationException("No catalog with ID: "+id+" found.");
        }

        public IEnumerable<Catalog> GetAllCatalog()
        {
            return DataContext.Books.Values;
        }

        public void UpdateCatalog(string id, Catalog catalog)
        {
            Catalog catalog1 = new Catalog(id, catalog.Title, catalog.Author);
            DeleteCatalog(GetCatalog(catalog.ID));
            AddCatalog(catalog1);

        }

        public bool DeleteCatalog(Catalog catalog)
        {
            return DataContext.Books.Remove(catalog.ID);
        }
        #endregion
        #region everything with Client
        public void AddClient(Client client)
        {
            DataContext.Clients.Add(client);
        }

        public Client GetClient(string id)
        {
            foreach (Client client in GetAllClients())
            {
                if (id == client.ID)
                    return client;
            }
            throw new System.InvalidOperationException("No client with ID: " + id + " found.");
        }
        public void UpdateClient(string id, Client client)
        {
            Client client1 = new Client(id, client.FirstName, client.LastName);
            DeleteClient(GetClient(client.ID));
            AddClient(client1);
        }

        public bool DeleteClient(Client client)
        {
            return DataContext.Clients.Remove(client);
        }

        public IEnumerable<Client> GetAllClients()
        {
            return DataContext.Clients;
        }
        #endregion
        #region everything with Event
        public void AddEvent(Event ev) 
        {
            DataContext.Events.Add(ev);
        }
        public Event GetEvent(string id)
        {
            foreach (Event ev in GetAllEvents())
            {
                if (id == ev.ID)
                    return ev;
            }
            throw new System.InvalidOperationException("No event with ID: " + id + " found.");
        }
        public void UpdateEvent(string id, Event ev)
        {
            Event newEvent = new Event(id, ev.Client, ev.Inventory, ev.BorrowDate, ev.ReturnDate);
            DeleteEvent(GetEvent(ev.ID));
            AddEvent(newEvent);
        }
        public bool DeleteEvent(Event ev)
        {
            return DataContext.Events.Remove(ev);
        }

        public IEnumerable<Event> GetAllEvents()
        {
            return DataContext.Events;
        }
        #endregion
        #region everything with Inventory
        public void AddInventory(Inventory inventory)
        {
            if (!DataContext.Books.ContainsKey(inventory.Catalog.ID))
                DataContext.Books.Add(inventory.Catalog.ID, inventory.Catalog);
            DataContext.Inventories.Add(inventory);
        }
        public Inventory GetInventory(string catalogId)
        {
            foreach (Inventory inventory in GetAllInventories())
            {
                if (catalogId == inventory.Catalog.ID)
                    return inventory;
            }
            throw new System.InvalidOperationException("No inventory with ID: " + catalogId + " found.");
        }
        public void UpdateInventory(string catalogID, int amount)
        {
            GetInventory(catalogID).Amount = amount;
        }
        public bool DeleteInventory(Inventory inventory)
        {
            return DataContext.Inventories.Remove(inventory);
        }

        public IEnumerable<Inventory> GetAllInventories()
        {
            return DataContext.Inventories;
        }
        #endregion
    }
}
