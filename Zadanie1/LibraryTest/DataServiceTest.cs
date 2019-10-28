using System;
using Library;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LibraryTest
{
    [TestClass]
    public class DataServiceTest
    {
        [TestMethod]
        public void RentTest()
        {
            DataService dataService = new DataService();
            Inventory inventory = new Inventory(new Catalog("Pan Tadeusz", "Adam Mickiewicz"), 5);
            Client client = new Client("Jan", "Kowalski");
            dataService.AddInventory(inventory);
            dataService.AddClient(client);
            Assert.AreEqual(5, dataService.NumberOfBooks(inventory.Catalog.ID));
            Assert.AreEqual(0, dataService.CurrentlyRentedBooksNumber(client.ID));
            Assert.AreEqual(0, dataService.EventsForClient(client.ID).Count);
            dataService.RentBook(client.ID, inventory.Catalog.ID);
            Assert.AreEqual(4, dataService.NumberOfBooks(inventory.Catalog.ID));
            Assert.AreEqual(1, dataService.CurrentlyRentedBooksNumber(client.ID));
            Assert.AreEqual(1, dataService.EventsForClient(client.ID).Count);
        }
        
        [TestMethod]
        public void RentNotEnoughBooksExceptionTest()
        {
            DataService dataService = new DataService();
            Inventory inventory = new Inventory(new Catalog("Pan Tadeusz", "Adam Mickiewicz"), 0);
            Client client = new Client("Jan", "Kowalski");
            dataService.AddInventory(inventory);
            dataService.AddClient(client);
            InvalidOperationException e = Assert.ThrowsException<InvalidOperationException>(()=>dataService.RentBook(client.ID, inventory.Catalog.ID));
//            Console.WriteLine(e.Message);
        }
        
        [TestMethod]
        public void RentAlreadyRentedExceptionTest()
        {
            DataService dataService = new DataService();
            Inventory inventory = new Inventory(new Catalog("Pan Tadeusz", "Adam Mickiewicz"), 5);
            Client client = new Client("Jan", "Kowalski");
            dataService.AddInventory(inventory);
            dataService.AddClient(client);
            dataService.RentBook(client.ID, inventory.Catalog.ID);
            InvalidOperationException e = Assert.ThrowsException<InvalidOperationException>(()=>dataService.RentBook(client.ID, inventory.Catalog.ID));
//            Console.WriteLine(e.Message);
        }
        
        [TestMethod]
        public void RentExceededPenaltyExceptionTest()
        {
            DataService dataService = new DataService();
            Inventory inventory = new Inventory(new Catalog("Pan Tadeusz", "Adam Mickiewicz"), 5);
            Client client = new Client("Jan", "Kowalski");
            client.Penalty = 11;
            dataService.AddInventory(inventory);
            dataService.AddClient(client);
            InvalidOperationException e = Assert.ThrowsException<InvalidOperationException>(()=>dataService.RentBook(client.ID, inventory.Catalog.ID));
//            Console.WriteLine(e.Message);
        }
        
        [TestMethod]
        public void ReturnTest()
        {
            DataService dataService = new DataService();
            Inventory inventory = new Inventory(new Catalog("Pan Tadeusz", "Adam Mickiewicz"), 5);
            Client client = new Client("Jan", "Kowalski");
            dataService.AddInventory(inventory);
            dataService.AddClient(client);
            dataService.RentBook(client.ID, inventory.Catalog.ID);
            Assert.AreEqual(4, dataService.NumberOfBooks(inventory.Catalog.ID));
            Assert.AreEqual(1, dataService.CurrentlyRentedBooksNumber(client.ID));
            Assert.AreEqual(1, dataService.EventsForClient(client.ID).Count);
            dataService.ReturnBook(client.ID, inventory.Catalog.ID);
            Assert.AreEqual(5, dataService.NumberOfBooks(inventory.Catalog.ID));
            Assert.AreEqual(0, dataService.CurrentlyRentedBooksNumber(client.ID));
            Assert.AreEqual(2, dataService.EventsForClient(client.ID).Count);
        }

        [TestMethod]
        public void PurchaseTest()
        {
            DataService dataService = new DataService();
            Inventory inventory = new Inventory(new Catalog("Pan Tadeusz", "Adam Mickiewicz"), 5);
            dataService.AddInventory(inventory);
            Assert.AreEqual(5, dataService.NumberOfBooks(inventory.Catalog.ID));
            Assert.AreEqual(0, dataService.NumberOfEvents());
            dataService.PurchaseBook(inventory.Catalog.ID, 1);
            Assert.AreEqual(6, dataService.NumberOfBooks(inventory.Catalog.ID));
            Assert.AreEqual(1, dataService.NumberOfEvents());
        }
        
        [TestMethod]
        public void DiscardTest()
        {
            DataService dataService = new DataService();
            Inventory inventory = new Inventory(new Catalog("Pan Tadeusz", "Adam Mickiewicz"), 5);
            dataService.AddInventory(inventory);
            Assert.AreEqual(5, dataService.NumberOfBooks(inventory.Catalog.ID));
            Assert.AreEqual(0, dataService.NumberOfEvents());
            dataService.DiscardBook(inventory.Catalog.ID, 1);
            Assert.AreEqual(4, dataService.NumberOfBooks(inventory.Catalog.ID));
            Assert.AreEqual(1, dataService.NumberOfEvents());
        }

        [TestMethod]
        public void DiscardTooManyExceptionTest()
        {
            DataService dataService = new DataService();
            Inventory inventory = new Inventory(new Catalog("Pan Tadeusz", "Adam Mickiewicz"), 5);
            dataService.AddInventory(inventory);
            InvalidOperationException e = Assert.ThrowsException<InvalidOperationException>(()=>dataService.DiscardBook(inventory.Catalog.ID, 6));
//            Console.WriteLine(e.Message);
        }
        
        [TestMethod]
        public void PenaltyTest()
        {
            DataService dataService = new DataService();
            Inventory inventory = new Inventory(new Catalog("Pan Tadeusz", "Adam Mickiewicz"), 5);
            Client client = new Client("Jan", "Kowalski");
            dataService.AddInventory(inventory);
            dataService.AddClient(client);
            dataService.RentBook(client.ID, inventory.Catalog.ID);
            Assert.AreEqual(0, dataService.PenaltySumForAllClients());
            dataService.ProlongRent(dataService.EventsForClient(client.ID)[0].ID, -8);
            dataService.ReturnBook(client.ID, inventory.Catalog.ID);
            Assert.AreEqual(1, dataService.PenaltySumForAllClients());
        }

        [TestMethod]
        public void EventsBetweenTest()
        {
            DataService dataService = new DataService();
            Inventory inventory = new Inventory(new Catalog("Pan Tadeusz", "Adam Mickiewicz"), 5);
            dataService.AddInventory(inventory);
            Assert.AreEqual(0, dataService.EventsBetweenDates(DateTime.Today.AddDays(-1), DateTime.Today.AddDays(1)).Count);
            dataService.PurchaseBook(inventory.Catalog.ID, 1);
            Assert.AreEqual(1, dataService.EventsBetweenDates(DateTime.Today.AddDays(-1), DateTime.Today.AddDays(1)).Count);
        }
        
        [TestMethod]
        public void EventsBeforeTest()
        {
            DataService dataService = new DataService();
            Inventory inventory = new Inventory(new Catalog("Pan Tadeusz", "Adam Mickiewicz"), 5);
            dataService.AddInventory(inventory);
            Assert.AreEqual(0, dataService.EventsBeforeDate(DateTime.Today.AddDays(1)).Count);
            dataService.PurchaseBook(inventory.Catalog.ID, 1);
            Assert.AreEqual(1, dataService.EventsBeforeDate(DateTime.Today.AddDays(1)).Count);
        }
        
        [TestMethod]
        public void EventsAfterTest()
        {
            DataService dataService = new DataService();
            Inventory inventory = new Inventory(new Catalog("Pan Tadeusz", "Adam Mickiewicz"), 5);
            dataService.AddInventory(inventory);
            Assert.AreEqual(0, dataService.EventsAfterDate(DateTime.Today.AddDays(-1)).Count);
            dataService.PurchaseBook(inventory.Catalog.ID, 1);
            Assert.AreEqual(1, dataService.EventsAfterDate(DateTime.Today.AddDays(-1)).Count);
        }
    }
}