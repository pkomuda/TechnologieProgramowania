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
            string catalogID = "1";
            string clientID = "11";
            dataService.AddCatalog(catalogID, "Pan Tadeusz", "Adam Mickiewicz");
            dataService.AddInventory(catalogID, 5);
            dataService.AddClient(clientID, "Jan", "Kowalski");
            Assert.AreEqual(5, dataService.NumberOfBooks(catalogID));
            Assert.AreEqual(0, dataService.CurrentlyRentedBooksNumber(clientID));
            Assert.AreEqual(0, dataService.EventsForClient(clientID).Count);
            dataService.RentBook(clientID, catalogID);
            Assert.AreEqual(4, dataService.NumberOfBooks(catalogID));
            Assert.AreEqual(1, dataService.CurrentlyRentedBooksNumber(clientID));
            Assert.AreEqual(1, dataService.EventsForClient(clientID).Count);
        }
        
        [TestMethod]
        public void RentNotEnoughBooksExceptionTest()
        {
            DataService dataService = new DataService();
            string catalogID = "1";
            string clientID = "11";
            dataService.AddCatalog(catalogID, "Pan Tadeusz", "Adam Mickiewicz");
            dataService.AddInventory(catalogID, 0);
            dataService.AddClient(clientID, "Jan", "Kowalski");
            InvalidOperationException e = Assert.ThrowsException<InvalidOperationException>(()=>dataService.RentBook(clientID, catalogID));
//            Console.WriteLine(e.Message);
        }
        
        [TestMethod]
        public void RentAlreadyRentedExceptionTest()
        {
            DataService dataService = new DataService();
            string catalogID = "1";
            string clientID = "11";
            dataService.AddCatalog(catalogID, "Pan Tadeusz", "Adam Mickiewicz");
            dataService.AddInventory(catalogID, 5);
            dataService.AddClient(clientID, "Jan", "Kowalski");
            dataService.RentBook(clientID, catalogID);
            InvalidOperationException e = Assert.ThrowsException<InvalidOperationException>(()=>dataService.RentBook(clientID, catalogID));
//            Console.WriteLine(e.Message);
        }
        
        [TestMethod]
        public void RentExceededPenaltyExceptionTest()
        {
           DataService dataService = new DataService();
            string catalogID = "1";
            string catalogID2 = "2";
            string clientID = "11";
            dataService.AddCatalog(catalogID, "Pan Tadeusz", "Adam Mickiewicz");
            dataService.AddInventory(catalogID, 5);
            dataService.AddCatalog(catalogID2, "Krzyzacy", "Henryk Sienkiewicz");
            dataService.AddInventory(catalogID2, 5);
            dataService.AddClient(clientID, "Jan", "Kowalski");
            dataService.RentBook(clientID, catalogID);
            dataService.ProlongRent(dataService.EventsForClient(clientID)[0].ID, -18);
            dataService.ReturnBook(clientID, catalogID);
            InvalidOperationException e = Assert.ThrowsException<InvalidOperationException>(()=>dataService.RentBook(clientID, catalogID2));
//            Console.WriteLine(e.Message);
        }
        
        [TestMethod]
        public void ReturnTest()
        {
            DataService dataService = new DataService();
            string catalogID = "1";
            string clientID = "11";
            dataService.AddCatalog(catalogID, "Pan Tadeusz", "Adam Mickiewicz");
            dataService.AddInventory(catalogID, 5);
            dataService.AddClient(clientID, "Jan", "Kowalski");
            dataService.RentBook(clientID, catalogID);
            Assert.AreEqual(4, dataService.NumberOfBooks(catalogID));
            Assert.AreEqual(1, dataService.CurrentlyRentedBooksNumber(clientID));
            Assert.AreEqual(1, dataService.EventsForClient(clientID).Count);
            dataService.ReturnBook(clientID, catalogID);
            Assert.AreEqual(5, dataService.NumberOfBooks(catalogID));
            Assert.AreEqual(0, dataService.CurrentlyRentedBooksNumber(clientID));
            Assert.AreEqual(2, dataService.EventsForClient(clientID).Count);
        }

        [TestMethod]
        public void PurchaseTest()
        {
            DataService dataService = new DataService();
            string catalogID = "1";
            dataService.AddCatalog(catalogID, "Pan Tadeusz", "Adam Mickiewicz");
            dataService.AddInventory(catalogID, 5);
            Assert.AreEqual(5, dataService.NumberOfBooks(catalogID));
            Assert.AreEqual(0, dataService.NumberOfEvents());
            dataService.PurchaseBook(catalogID, 1);
            Assert.AreEqual(6, dataService.NumberOfBooks(catalogID));
            Assert.AreEqual(1, dataService.NumberOfEvents());
        }
        
        [TestMethod]
        public void DiscardTest()
        {
            DataService dataService = new DataService();
            string catalogID = "1";
            dataService.AddCatalog(catalogID, "Pan Tadeusz", "Adam Mickiewicz");
            dataService.AddInventory(catalogID, 5);
            Assert.AreEqual(5, dataService.NumberOfBooks(catalogID));
            Assert.AreEqual(0, dataService.NumberOfEvents());
            dataService.DiscardBook(catalogID, 1);
            Assert.AreEqual(4, dataService.NumberOfBooks(catalogID));
            Assert.AreEqual(1, dataService.NumberOfEvents());
        }

        [TestMethod]
        public void DiscardTooManyExceptionTest()
        {
            DataService dataService = new DataService();
            string catalogID = "1";
            dataService.AddCatalog(catalogID, "Pan Tadeusz", "Adam Mickiewicz");
            dataService.AddInventory(catalogID, 5);
            InvalidOperationException e = Assert.ThrowsException<InvalidOperationException>(()=>dataService.DiscardBook(catalogID, 6));
//            Console.WriteLine(e.Message);
        }
        
        [TestMethod]
        public void PenaltyTest()
        {
            DataService dataService = new DataService();
            string catalogID = "1";
            string clientID = "11";
            dataService.AddCatalog(catalogID, "Pan Tadeusz", "Adam Mickiewicz");
            dataService.AddInventory(catalogID, 5);
            dataService.AddClient(clientID, "Jan", "Kowalski");
            dataService.RentBook(clientID, catalogID);
            Assert.AreEqual(0, dataService.PenaltySumForAllClients());
            dataService.ProlongRent(dataService.EventsForClient(clientID)[0].ID, -8);
            dataService.ReturnBook(clientID,catalogID);
            Assert.AreEqual(1, dataService.PenaltySumForAllClients());
        }

        [TestMethod]
        public void EventsBetweenTest()
        {
            DataService dataService = new DataService();
            string catalogID = "1";
            dataService.AddCatalog(catalogID, "Pan Tadeusz", "Adam Mickiewicz");
            dataService.AddInventory(catalogID, 5);
            Assert.AreEqual(0, dataService.EventsBetweenDates(DateTime.Today.AddDays(-1), DateTime.Today.AddDays(1)).Count);
            dataService.PurchaseBook(catalogID, 1);
            Assert.AreEqual(1, dataService.EventsBetweenDates(DateTime.Today.AddDays(-1), DateTime.Today.AddDays(1)).Count);
        }
        
        [TestMethod]
        public void EventsBeforeTest()
        {
            DataService dataService = new DataService();
            string catalogID = "1";
            dataService.AddCatalog(catalogID, "Pan Tadeusz", "Adam Mickiewicz");
            dataService.AddInventory(catalogID, 5);
            Assert.AreEqual(0, dataService.EventsBeforeDate(DateTime.Today.AddDays(1)).Count);
            dataService.PurchaseBook(catalogID, 1);
            Assert.AreEqual(1, dataService.EventsBeforeDate(DateTime.Today.AddDays(1)).Count);
        }
        
        [TestMethod]
        public void EventsAfterTest()
        {
            DataService dataService = new DataService();
            string catalogID = "1";
            dataService.AddCatalog(catalogID, "Pan Tadeusz", "Adam Mickiewicz");
            dataService.AddInventory(catalogID, 5);
            Assert.AreEqual(0, dataService.EventsAfterDate(DateTime.Today.AddDays(-1)).Count);
            dataService.PurchaseBook(catalogID, 1);
            Assert.AreEqual(1, dataService.EventsAfterDate(DateTime.Today.AddDays(-1)).Count);
        }
    }
}