using Library;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace LibraryTestProject
{
    [TestClass]
    public class DataRepositoryTest
    {
        [TestMethod]
        public void Clients()
        {
            Client c1 = new Client("Jan", "Kowalski");
            Client c2 = new Client("Kamil", "Nowak");
            Client c3 = new Client("Waldemar", "Maly");
            DataRepository repo = new DataRepository();
            repo.AddClient(c1);
            Assert.AreEqual(true, repo.DataContext.Clients.Contains(c1));
            repo.AddClient(c2);
            Assert.AreEqual(true, repo.DataContext.Clients.Contains(c2));

            List<Client> clients = new List<Client>();
            clients.Add(c1);
            clients.Add(c2);
            Assert.AreEqual(repo.GetAllClients().ToString(), clients.ToString());
 
            repo.DeleteClient(c2);
            Assert.AreEqual(false, repo.DataContext.Clients.Contains(c2));

            Assert.AreEqual(c1, repo.GetClient(c1.ID));

            repo.UpdateClient(c1.ID, c3);
            System.Console.WriteLine(repo.GetClient(c1.ID).FirstName + " " + repo.GetClient(c1.ID).LastName);
            Assert.AreEqual(c3.FirstName, repo.GetClient(c1.ID).FirstName);
            Assert.AreEqual(c3.LastName, repo.GetClient(c1.ID).LastName);
        }
    }
}
