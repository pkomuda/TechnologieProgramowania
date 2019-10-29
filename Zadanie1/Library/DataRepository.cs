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
        public void AddCatalog(string id, string title, string author)
        {
            DataContext.Books.Add(id, new Catalog(id, title, author));
        }
        public Catalog GetCatalog(string id)
        {
            foreach (Catalog catalog in GetAllCatalogs())
            {
                if (id == catalog.ID)
                    return catalog;
            }
            throw new System.InvalidOperationException("No book with ID: " + id + " found.");
            // Wydaje mi się że lepsze jest rzucanie własnego wyjątku chociażby ze względu na wyświetlany komunikat - wtedy łatwiej jest znaleźć jego przyczynę
            // return DataContext.Books[id];
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
        public void AddClient(string firstName, string lastName)
        {
            DataContext.Clients.Add(new Client(firstName, lastName));
        }
        public void AddClient(string id, string firstName, string lastName)
        {
            DataContext.Clients.Add(new Client(id, firstName, lastName));
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
        #region everything with Event and
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
        
        public bool DeleteEvent(Event ev)
        {
            return DataContext.Events.Remove(ev);
        }

        public IEnumerable<Event> GetAllEvents()
        {
            return DataContext.Events;
        }
        public Rent CreateRent(Client client, Inventory inventory, System.DateTime borrowDate, System.DateTime returnDate)
        {
           return new Rent(client, inventory, borrowDate, returnDate);
        }
        public Return CreateReturn(Client client, Inventory inventory, System.DateTime returnDate)
        {
            return new Return(client, inventory, returnDate);
        }
        public Discard CreateDiscard(Inventory inventory, System.DateTime discardDate)
        {
           return new Discard(inventory, discardDate);
        }
        public Purchase CreatePurchase(Inventory inventory, System.DateTime purchaseDate, int amount)
        {
           return new Purchase(inventory, purchaseDate, amount);
        }
        #endregion
        #region everything with Inventory
        public void AddInventory(Inventory inventory)
        {
            if (!DataContext.Books.ContainsKey(inventory.Catalog.ID))
                DataContext.Books.Add(inventory.Catalog.ID, inventory.Catalog);
            DataContext.Inventories.Add(inventory);
        }
        public void AddInventory(string catalogID, int amount)
        {
            if (DataContext.Books.ContainsKey(catalogID))
                DataContext.Inventories.Add(new Inventory(GetCatalog(catalogID), amount));
            else
                throw new System.InvalidOperationException("No catalog with ID: " + catalogID + " found.");
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
                    inventory.reduceAmount(inventory.Amount);
                    inventory.increaseAmount(amount);
                    break;
                }
            }
        }
        public bool DeleteInventory(string catalogID)
        {
            return DataContext.Inventories.Remove(GetInventory(catalogID));
        }

        public IEnumerable<Inventory> GetAllInventories()
        {
            return DataContext.Inventories;
        }
        #endregion
    }
}
