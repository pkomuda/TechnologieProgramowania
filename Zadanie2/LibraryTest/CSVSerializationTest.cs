using System;
using System.Collections.Generic;
using System.IO;
using Library;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LibraryTest
{
    [TestClass]
    public class CSVSerializationTest
    {
        [TestMethod]
        public void SerializeClientsTest()
        {
            List<Client> clients = new List<Client>();
            clients.Add(new Client("1", "Jakub", "Nowak"));
            clients.Add(new Client("2", "Krzesimir", "Mniejszy"));
            clients.Add(new Client("3", "Kazimierz", "Wielki"));

            CSV csv = new CSV();
            csv.serialize(clients, "clients.csv");
            string result = File.ReadAllText("clients.csv");
            Assert.AreEqual("1;Jakub;Nowak;0;\n2;Krzesimir;Mniejszy;0;\n3;Kazimierz;Wielki;0;\n", result);
        }
        [TestMethod]
        public void SerializeCatalogsTest()
        {
            Dictionary<string, Catalog> books = new Dictionary<string, Catalog>();
            Catalog c1 = new Catalog("1", "Krzyzacy", "Henryk Sienkiewicz");
            Catalog c2 = new Catalog("2", "Kroniki Czarnej Kompanii", "Glen Cook");
            Catalog c3 = new Catalog("3", "Pan Tadeusz", "Adam Mickiewicz");
            books.Add(c1.ID, c1);
            books.Add(c2.ID, c2);
            books.Add(c3.ID, c3);

            CSV csv = new CSV();
            csv.serialize(books.Values, "books.csv");
            string result = File.ReadAllText("books.csv");
            //Console.WriteLine(result);
            Assert.AreEqual("1;Krzyzacy;Henryk Sienkiewicz\n2;Kroniki Czarnej Kompanii;Glen Cook\n3;Pan Tadeusz;Adam Mickiewicz\n", result);
        }
        [TestMethod]
        public void SerializeInventoriesTest()
        {
            Dictionary<string, Catalog> books = new Dictionary<string, Catalog>();
            List<Inventory> inventories = new List<Inventory>();
            Catalog c1 = new Catalog("1", "Krzyzacy", "Henryk Sienkiewicz");
            Catalog c2 = new Catalog("2", "Kroniki Czarnej Kompanii", "Glen Cook");
            Catalog c3 = new Catalog("3", "Pan Tadeusz", "Adam Mickiewicz");
            books.Add(c1.ID, c1);
            books.Add(c2.ID, c2);
            books.Add(c3.ID, c3);
            inventories.Add(new Inventory(books[c1.ID], 10));
            inventories.Add(new Inventory(books[c2.ID], 5));
            inventories.Add(new Inventory(books[c3.ID], 7));
            CSV csv = new CSV();
            csv.serialize(inventories, "inventories.csv");
            string result = File.ReadAllText("inventories.csv");
            //Console.WriteLine(result);
            Assert.AreEqual("1;10\n2;5\n3;7\n", result);
        }
        [TestMethod]
        public void SerializeNotificationsTest()
        {
            List<string> notifications = new List<string>();
            notifications.Add("TestNotifications1");
            notifications.Add("TestNotifications2");
            notifications.Add("TestNotifications3");
            CSV csv = new CSV();
            csv.serialize(notifications, "notifications.csv");
            string result = File.ReadAllText("notifications.csv");
            //Console.WriteLine(result);
            Assert.AreEqual("TestNotifications1\n"+"TestNotifications2\n"+"TestNotifications3\n", result);
        }
    }
}
