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
            Assert.AreEqual(false, ((List<Client>) repo.GetAllClients()).Contains(c2));

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
           // Assert.AreEqual(((Dictionary<string, Catalog>) repo.GetAllCatalogs())[c1.ID], catalogs[c1.ID]);
           // Assert.AreEqual(((Dictionary<string, Catalog>) repo.GetAllCatalogs())[c2.ID], catalogs[c2.ID]);
          /////////////////Jak to sprawdzić?
            repo.DeleteCatalog(c2.ID);
           // Assert.AreEqual(false, ((Dictionary<string, Catalog>)repo.GetAllCatalogs()).ContainsKey(c2.ID));

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
            
        }
        [TestMethod]
        public void Inventories()
        {

        }
    }
}
