using Library;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace LibraryTest
{
    [TestClass]
    public class DataRepositoryTest
    {
        #region Clients 
        [TestMethod]
        public void AddClientTest()
        {
            Client c1 = new Client("1", "Jan", "Kowalski");
            DataRepository repo = new DataRepository();
            repo.AddClient(c1);
            Assert.AreEqual(c1, repo.GetClient(c1.ID));
        }
        [TestMethod]
        public void GetClientTest()
        {
            Client c1 = new Client("1", "Jan", "Kowalski");
            DataRepository repo = new DataRepository();
            repo.AddClient(c1);
            Assert.AreEqual(c1, repo.GetClient(c1.ID));
            System.InvalidOperationException e = Assert.ThrowsException<System.InvalidOperationException>(() => repo.GetClient("sth"));
        }
        [TestMethod]
        public void UpdateClientFirstNameTest()
        {
            Client c1 = new Client("1", "Jan", "Kowalski");
            DataRepository repo = new DataRepository();
            repo.AddClient(c1);
            Assert.AreEqual("Jan", repo.GetClient(c1.ID).FirstName);
            repo.UpdateClientFirstName(c1.ID, "Janusz");
            Assert.AreEqual("Janusz", repo.GetClient(c1.ID).FirstName);
        }
        [TestMethod]
        public void UpdateClientLastNameTest()
        {
            Client c1 = new Client("1", "Jan", "Kowalski");
            DataRepository repo = new DataRepository();
            repo.AddClient(c1);
            Assert.AreEqual("Kowalski", repo.GetClient(c1.ID).LastName);
            repo.UpdateClientLastName(c1.ID, "Nowak");
            Assert.AreEqual("Nowak", repo.GetClient(c1.ID).LastName);
        }
        [TestMethod]
        public void DeleteClientTest()
        {
            Client c1 = new Client("1", "Jan", "Kowalski");
            DataRepository repo = new DataRepository();
            repo.AddClient(c1);
            Assert.AreEqual(c1, repo.GetClient(c1.ID));
            repo.DeleteClient(c1.ID);
            System.InvalidOperationException e = Assert.ThrowsException<System.InvalidOperationException>(() => repo.GetClient(c1.ID));
        }
        [TestMethod]
        public void GetAllClientsTest()
        {
            Client c1 = new Client("1", "Jan", "Kowalski");
            Client c2 = new Client("2", "Kamil", "Nowak");
            DataRepository repo = new DataRepository();
            repo.AddClient(c1);
            repo.AddClient(c2);
            List<Client> ret = (List<Client>)repo.GetAllClients();
            Assert.AreEqual(2, ret.Count);
            Assert.AreEqual(true, ret.Contains(c1));
            Assert.AreEqual(true, ret.Contains(c2));
            Assert.AreEqual(c1, ret[0]);
            Assert.AreEqual(c2, ret[1]);
        } 
        #endregion
        #region Catalogs 
        [TestMethod]
        public void AddCatalogTest()
        {
            Catalog c1 = new Catalog("1", "Basnie braci Grimm", "Bracia Grimm");
            DataRepository repo = new DataRepository();
            repo.AddCatalog(c1);
            Assert.AreEqual(c1, repo.GetCatalog(c1.ID));
        }
        [TestMethod]
        public void GetCatalogTest()
        {
            Catalog c1 = new Catalog("1", "Basnie braci Grimm", "Bracia Grimm");
            DataRepository repo = new DataRepository();
            repo.AddCatalog(c1);
            Assert.AreEqual(c1, repo.GetCatalog(c1.ID));
            System.InvalidOperationException e = Assert.ThrowsException<System.InvalidOperationException>(() => repo.GetCatalog("sth"));
        }
        [TestMethod]
        public void UpdateCatalogTitleTest()
        {
            Catalog c1 = new Catalog("1", "Basnie braci Grimm", "Bracia Grimm");
            DataRepository repo = new DataRepository();
            repo.AddCatalog(c1);
            Assert.AreEqual("Basnie braci Grimm", repo.GetCatalog(c1.ID).Title);
            repo.UpdateCatalogTitle(c1.ID, "Matrix");
            Assert.AreEqual("Matrix", repo.GetCatalog(c1.ID).Title);
        }
        [TestMethod]
        public void UpdateCatalogAuthorTest()
        {
            Catalog c1 = new Catalog("1", "Basnie braci Grimm", "Bracia Grimm");
            DataRepository repo = new DataRepository();
            repo.AddCatalog(c1);
            Assert.AreEqual("Bracia Grimm", repo.GetCatalog(c1.ID).Author);
            repo.UpdateCatalogAuthor(c1.ID, "Siostry Wachowskie");
            Assert.AreEqual("Siostry Wachowskie", repo.GetCatalog(c1.ID).Author);
        }
        [TestMethod]
        public void DeleteCatalogTest()
        {
            Catalog c1 = new Catalog("1", "Basnie braci Grimm", "Bracia Grimm");
            DataRepository repo = new DataRepository();
            repo.AddCatalog(c1);
            Assert.AreEqual(c1, repo.GetCatalog(c1.ID));
            repo.DeleteCatalog(c1.ID);
            System.InvalidOperationException e = Assert.ThrowsException<System.InvalidOperationException>(() => repo.GetCatalog(c1.ID));
        }
        #endregion 
        #region Events
        [TestMethod]
        public void AddEventTest()
        {
            DataRepository repo = new DataRepository();
            Client client = new Client("1", "Jan", "Kowalski");
            repo.AddClient(client);
            Catalog c1 = new Catalog("1", "Basnie braci Grimm", "Bracia Grimm");
            repo.AddCatalog(c1);

            Inventory i1 = new Inventory(repo.GetCatalog(c1.ID), 5);
            repo.AddInventory(i1);

            Event e1 = new Rent(client, i1, new System.DateTime(), new System.DateTime(2020, 1, 1));
            repo.AddEvent(e1);
            Assert.AreEqual(e1, repo.GetEvent(e1.ID));
        }
        [TestMethod]
        public void GetEventTest()
        {
            DataRepository repo = new DataRepository();
            Client client = new Client("1", "Jan", "Kowalski");
            repo.AddClient(client);
            Catalog c1 = new Catalog("1", "Basnie braci Grimm", "Bracia Grimm");
            repo.AddCatalog(c1);

            Inventory i1 = new Inventory(repo.GetCatalog(c1.ID), 5);
            repo.AddInventory(i1);

            Event e1 = new Rent(client, i1, new System.DateTime(), new System.DateTime(2020, 1, 1));
            repo.AddEvent(e1);
            Assert.AreEqual(e1, repo.GetEvent(e1.ID));
            System.InvalidOperationException e = Assert.ThrowsException<System.InvalidOperationException>(() => repo.GetEvent("sth"));
        }
        [TestMethod]
        public void DeleteEventTest()
        {
            DataRepository repo = new DataRepository();
            Client client = new Client("1", "Jan", "Kowalski");
            repo.AddClient(client);
            Catalog c1 = new Catalog("1", "Basnie braci Grimm", "Bracia Grimm");
            repo.AddCatalog(c1);

            Inventory i1 = new Inventory(repo.GetCatalog(c1.ID), 5);
            repo.AddInventory(i1);

            Event e1 = new Rent(client, i1, new System.DateTime(), new System.DateTime(2020, 1, 1));
            repo.AddEvent(e1);
            repo.DeleteEvent(e1.ID);
            System.InvalidOperationException e = Assert.ThrowsException<System.InvalidOperationException>(() => repo.GetEvent(e1.ID));
        }
        [TestMethod]
        public void GetAllEventsTest()
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
            repo.AddEvent(e1);
            repo.AddEvent(e2);

            System.Collections.ObjectModel.ObservableCollection<Event> ret = (System.Collections.ObjectModel.ObservableCollection < Event > )repo.GetAllEvents();
            Assert.AreEqual(2, ret.Count);
            Assert.AreEqual(true, ret.Contains(e1));
            Assert.AreEqual(true, ret.Contains(e2));
            Assert.AreEqual(e1, ret[0]);
            Assert.AreEqual(e2, ret[1]);
        }
        #endregion
        #region Inventories
        [TestMethod]
        public void AddInventoryTest()
        {
            DataRepository repo = new DataRepository();
            Catalog c1 = new Catalog("1", "Ojciec Chrzestny", "Mario Putzo");
            repo.AddCatalog(c1);

            Inventory i1 = new Inventory(repo.GetCatalog(c1.ID), 5);
            repo.AddInventory(i1);

            Assert.AreEqual(i1, repo.GetInventory(c1.ID));
        }
        [TestMethod]
        public void GetInventoryTest()
        {
            DataRepository repo = new DataRepository();
            Catalog c1 = new Catalog("1", "Ojciec Chrzestny", "Mario Putzo");
            repo.AddCatalog(c1);

            Inventory i1 = new Inventory(repo.GetCatalog(c1.ID), 5);
            repo.AddInventory(i1);
            Assert.AreEqual(i1, repo.GetInventory(c1.ID));
            System.InvalidOperationException e = Assert.ThrowsException<System.InvalidOperationException>(() => repo.GetInventory("sth"));
        }
        [TestMethod]
        public void UpdateInventoryTest()
        {
            DataRepository repo = new DataRepository();
            Catalog c1 = new Catalog("1", "Ojciec Chrzestny", "Mario Putzo");
            repo.AddCatalog(c1);

            Inventory i1 = new Inventory(repo.GetCatalog(c1.ID), 5);
            repo.AddInventory(i1);

            Assert.AreEqual(5, repo.GetInventory(c1.ID).Amount);
            repo.UpdateInventory(c1.ID, 123);
            Assert.AreEqual(123, repo.GetInventory(c1.ID).Amount);
        }
        [TestMethod]
        public void DeleteInventoryTest()
        {
            DataRepository repo = new DataRepository();
            Catalog c1 = new Catalog("1", "Ojciec Chrzestny", "Mario Putzo");
            repo.AddCatalog(c1);

            Inventory i1 = new Inventory(repo.GetCatalog(c1.ID), 5);
            repo.AddInventory(i1);
            Assert.AreEqual(i1, repo.GetInventory(c1.ID));
            repo.DeleteInventory(c1.ID);
            System.InvalidOperationException e = Assert.ThrowsException<System.InvalidOperationException>(() => repo.GetInventory(c1.ID));
        }
        [TestMethod]
        public void GetAllInventoriesTest()
        {
            DataRepository repo = new DataRepository();
            Catalog c1 = new Catalog("1", "Ojciec Chrzestny", "Mario Putzo");
            Catalog c2 = new Catalog("2", "Basnie braci Grimm", "Bracia Grimm");
            
            repo.AddCatalog(c1);
            repo.AddCatalog(c2);

            Inventory i1 = new Inventory(repo.GetCatalog(c1.ID), 5);
            Inventory i2 = new Inventory(repo.GetCatalog(c2.ID), 25);
            repo.AddInventory(i1);
            repo.AddInventory(i2);

            List<Inventory> ret = (List<Inventory>)repo.GetAllInventories();
            Assert.AreEqual(2, ret.Count);
            Assert.AreEqual(true, ret.Contains(i1));
            Assert.AreEqual(true, ret.Contains(i2));
            Assert.AreEqual(i1, ret[0]);
            Assert.AreEqual(i2, ret[1]);
        }
#endregion
    }
}
