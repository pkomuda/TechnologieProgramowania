using Library;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace LibTest
{
    [TestClass]
    public class DataRepositoryTest
    {
        [TestMethod]
        public void Clients()
        {
            Client c1 = new Client("a", "Jan", "Kowalski");
            Client c2 = new Client("b", "Kamil", "Nowak");
            Client c3 = new Client("c", "Waldemar", "Maly");
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

            repo.AddClient(c3);
            repo.UpdateClient("newID", c3);
            //System.Console.WriteLine(repo.GetClient(c1.ID).FirstName + " " + repo.GetClient(c1.ID).LastName);
            Assert.AreEqual(c3.FirstName, repo.GetClient("newID").FirstName);
            Assert.AreEqual(c3.LastName, repo.GetClient("newID").LastName);
        }
    }
}
