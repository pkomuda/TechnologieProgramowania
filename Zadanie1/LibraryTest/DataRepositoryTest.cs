using Library;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace LibraryTest
{
    [TestClass]
    public class DataRepositoryTest
    {
        [TestMethod]
        public void Clients()
        {
            Client c1 = new Client("1", "Jan", "Kowalski");
            Client c2 = new Client("2", "Kamil", "Nowak");
            Client c3 = new Client("3", "Waldemar", "Maly");
            DataRepository repo = new DataRepository();
            repo.AddClient(c1);
            Assert.AreEqual(c1, repo.GetClient(c1.ID));
            repo.AddClient(c2);
            Assert.AreEqual(c2, repo.GetClient(c2.ID));

            List<Client> clients = new List<Client>();
            clients.Add(c1);
            clients.Add(c2);
            Assert.AreEqual(repo.GetAllClients().ToString(), clients.ToString());

            repo.DeleteClient(c2.ID);
            Assert.AreEqual(false, ((List<Client>)repo.GetAllClients()).Contains(c2));

            Assert.AreEqual(c1, repo.GetClient(c1.ID));

            repo.AddClient(c3);
            repo.UpdateClientFirstName(c3.ID, "firstname");
            repo.UpdateClientLastName(c3.ID, "lastname");
            Assert.AreEqual(repo.GetClient(c3.ID).FirstName, c3.FirstName);
            Assert.AreEqual(repo.GetClient(c3.ID).LastName, c3.LastName);
        }
        [TestMethod]
        public void Catalogs()
        {
            Catalog c1 = new Catalog("1", "Basnie braci Grimm", "Bracia Grimm");
            Catalog c2 = new Catalog("2", "Harry Potter", "J. K. Rowling");
            Catalog c3 = new Catalog("3", "Ojciec Chrzestny", "Mario Putzo");
            DataRepository repo = new DataRepository();
            repo.AddCatalog(c1);
            Assert.AreEqual(c1, repo.GetCatalog(c1.ID));
            repo.AddCatalog(c2);
            Assert.AreEqual(c2, repo.GetCatalog(c2.ID));

            Dictionary<string, Catalog> catalogs = new Dictionary<string, Catalog>();
            catalogs.Add(c1.ID, c1);
            catalogs.Add(c2.ID, c2);
            repo.DeleteCatalog(c2.ID);

            Assert.AreEqual(c1, repo.GetCatalog(c1.ID));

            repo.AddCatalog(c3);
            repo.UpdateCatalogTitle(c3.ID, "newTitle");
            repo.UpdateCatalogAuthor(c3.ID, "newAuthor");
            Assert.AreEqual(repo.GetCatalog(c3.ID).Title, c3.Title);
            Assert.AreEqual(repo.GetCatalog(c3.ID).Author, c3.Author);
        }
        [TestMethod]
        public void Events()
        {
            DataRepository repo = new DataRepository();
            Client client = new Client("1", "Jan", "Kowalski");
            repo.AddClient(client);
            Catalog c1 = new Catalog("1", "Basnie braci Grimm", "Bracia Grimm");
            repo.AddCatalog(c1);

            Inventory i1 = new Inventory(repo.GetCatalog(c1.ID), 5);
            repo.AddInventory(i1);

            Event e1 = new Rent(client, i1, new System.DateTime(), new System.DateTime(2020, 1, 1));
            Event e2 = new Purchase(i1, new System.DateTime(), 5);
            Event e3 = new Return(client, i1, new System.DateTime(2022, 2, 2));
            Event e4 = new Discard(i1, new System.DateTime());
            repo.AddEvent(e1);
            repo.AddEvent(e2);
            repo.AddEvent(e3);
            repo.AddEvent(e4);

            Assert.AreEqual(e1, repo.GetEvent(e1.ID));
            Assert.AreEqual(e2, repo.GetEvent(e2.ID));
            Assert.AreEqual(e3, repo.GetEvent(e3.ID));
            Assert.AreEqual(e4, repo.GetEvent(e4.ID));
            repo.DeleteEvent(e4);
            System.InvalidOperationException e = Assert.ThrowsException<System.InvalidOperationException>(() => repo.GetEvent(e4.ID));

        }
        [TestMethod]
        public void Inventories()
        {
            Catalog c1 = new Catalog("1", "Basnie braci Grimm", "Bracia Grimm");
            Catalog c2 = new Catalog("2", "Harry Potter", "J. K. Rowling");
            Catalog c3 = new Catalog("3", "Ojciec Chrzestny", "Mario Putzo");
            DataRepository repo = new DataRepository();
            repo.AddCatalog(c1);
            repo.AddCatalog(c2);
            repo.AddCatalog(c3);

            Inventory i1 = new Inventory(repo.GetCatalog(c1.ID), 5);
            Inventory i2 = new Inventory(repo.GetCatalog(c2.ID), 25);
            Inventory i3 = new Inventory(repo.GetCatalog(c3.ID), 15);
            repo.AddInventory(i1);
            repo.AddInventory(i2);
            repo.AddInventory(i3);

            Assert.AreEqual(repo.GetInventory(c1.ID), i1);
            Assert.AreEqual(repo.GetInventory(c2.ID).Amount, 25);
            repo.UpdateInventory(c2.ID, 12);
            Assert.AreEqual(repo.GetInventory(c2.ID).Amount, 12);
            repo.DeleteInventory(c3.ID);
            Assert.AreEqual(false, (((List<Inventory>) repo.GetAllInventories()).Contains(i3)));
        }
    }
}
