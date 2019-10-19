using System.Collections.Generic;

namespace Library
{
    public class DataRepository
    {
        public DataContext DataContext { get; set; }
        public DataFill fillData { get; set; }

        public DataRepository()
        {
            DataContext = new DataContext();
        }
        public void Fill()
        {
            fillData.Fill(DataContext);
        }
        public void AddCatalog(Catalog catalog)
        {
            DataContext.Books.Add(catalog.ID, catalog);
        }

        public Catalog GetCatalog(string id)
        {
            return DataContext.Books[id];
        }

        public IEnumerable<Catalog> GetAllCatalog()
        {
            return DataContext.Books.Values;
        }

        public void UpdateCatalog(string id, Catalog catalog)
        {
            Catalog catalog1 = new Catalog(id, catalog.Title, catalog.Author);
            DeleteCatalog(catalog);
            AddCatalog(catalog1);
        }

        public bool DeleteCatalog(Catalog catalog)
        {
            return DataContext.Books.Remove(catalog.ID);
        }

        public void AddClient(Client client)
        {
            DataContext.Clients.Add(client);
        }

        public Client GetClient(string id)
        {
            foreach (Client client in DataContext.Clients)
            {
                if (id == client.ID)
                    return client;
            }
            return null;
        }

        public IEnumerable<Client> GetAllClients()
        {
            return DataContext.Clients;
        }

        public void UpdateClient(string id, Client client)
        {
            Client client1 = new Client(id, client.FirstName, client.LastName);
            DeleteClient(client);
            AddClient(client1);
        }

        public bool DeleteClient(Client client)
        {
            return DataContext.Clients.Remove(client);
        }
    }
}
