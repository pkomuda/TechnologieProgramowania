using System.Collections.Generic;

namespace Library
{
    public class DataRepository
    {
        private DataContext DataContext { get; set;  }
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
            return DataContext.Books[id];
        }

        public IEnumerable<Catalog> GetAllCatalogs()
        {
            return DataContext.Books.Values;
        }

        public void UpdateCatalogTitle(string id, string newTitle)
        {
            DataContext.Books[id].Title = newTitle;
        }
        public void UpdateCatalogAuthor(string id, string newAuthor)
        {
            DataContext.Books[id].Author = newAuthor;
        }
        public bool DeleteCatalog(string catalogID)
        {
            return DataContext.Books.Remove(catalogID);
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
        public void UpdateClientFirstName(string id, string newFirstName)
        {
            foreach(Client c in DataContext.Clients)
            {
                if(id == c.ID)
                {
                    c.FirstName = newFirstName;
                    break;
                }
            }
        }
        public void UpdateClientLastName(string id, string newLastName)
        {
            foreach (Client c in DataContext.Clients)
            {
                if (id == c.ID)
                {
                    c.LastName = newLastName;
                    break;
                }
            }
        }

        public bool DeleteClient(string id)
        {
            return DataContext.Clients.Remove(GetClient(id));
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
        public void UpdateEventReturnDate(string id, System.DateTime newReturnDate)
        {
            foreach (Event ev in GetAllEvents())
            {
                if (id == ev.ID)
                {
                    ev.ReturnDate = newReturnDate;
                    break;
                }
            }
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
            foreach (Inventory inventory in GetAllInventories())
            {
                if (catalogID == inventory.Catalog.ID)
                {
                    inventory.Amount = amount;
                    break;
                }
            }
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
