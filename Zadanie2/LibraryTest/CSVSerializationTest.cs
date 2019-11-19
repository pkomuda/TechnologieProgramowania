using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
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
            csv.Serialize(clients, "clients.csv");
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
            csv.Serialize(books, "books.csv");
            string result = File.ReadAllText("books.csv");
            //Console.WriteLine(result);
            Assert.AreEqual("1;1;Krzyzacy;Henryk Sienkiewicz\n2;2;Kroniki Czarnej Kompanii;Glen Cook\n3;3;Pan Tadeusz;Adam Mickiewicz\n", result);
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
            csv.Serialize(inventories, "inventories.csv");
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
            csv.Serialize(notifications, "notifications.csv");
            string result = File.ReadAllText("notifications.csv");
            //Console.WriteLine(result);
            Assert.AreEqual("TestNotifications1\n"+"TestNotifications2\n"+"TestNotifications3\n", result);
        }
        [TestMethod]
        public void SerializeEventsTest()
        {
            Catalog c1 = new Catalog("1", "Krzyzacy", "Henryk Sienkiewicz");
            Catalog c2 = new Catalog("2", "Kroniki Czarnej Kompanii", "Glen Cook");
            Catalog c3 = new Catalog("3", "Pan Tadeusz", "Adam Mickiewicz");
            Inventory i1 = new Inventory(c1, 10);
            Inventory i2 = new Inventory(c2, 7);
            Inventory i3 = new Inventory(c3, 33);
            ObservableCollection<Event> events = new ObservableCollection<Event>();
            events.Add(new Rent("event1", new Client("1", "Jakub", "Nowak"), i1, new System.DateTime(), new System.DateTime(2020, 1, 1)));
            events.Add(new Purchase("event2", i2, new DateTime(), 5));
            events.Add(new Return("event3", new Client("2", "Krzesimir", "Mniejszy"), i1, new System.DateTime(2022, 2, 2)));
            events.Add(new Discard("event4", i3, new DateTime()));
            CSV csv = new CSV();
            csv.Serialize(events, "events.csv");
            string result = File.ReadAllText("events.csv");
           // Console.WriteLine(result);
            Assert.AreEqual("Rent;event1;1;1;01.01.0001 00:00:00;01.01.2020 00:00:00\n" + "Purchase;event2;2;01.01.0001 00:00:00;5\n" + "Return;event3;2;1;02.02.2022 00:00:00\n" + "Discard;event4;3;01.01.0001 00:00:00\n", result);
        }
        [TestMethod]
        public void DeserializeTest()
        {
            CSV csv = new CSV();
            DataContext dc = new DataContext();
            ConstansFill fill1 = new ConstansFill();
            fill1.Fill(dc);
            #region clients
            string line = "";
            string catalogs = "";
            foreach (Client c in dc.Clients)
            {
                foreach (Catalog cat in c.RentedCatalogs)
                {
                    catalogs += cat.ID + ';';
                }
                line += c.ID + ';' + c.FirstName + ';' + c.LastName + ';' + c.Penalty + ';' + catalogs + "\n";
                catalogs = "";
            }
            File.Delete("clients.csv");
            using (Stream _stream = File.Open("clients.csv", FileMode.OpenOrCreate, FileAccess.Write))
            {
                byte[] _content = Encoding.ASCII.GetBytes(line);
                _stream.Write(_content, 0, _content.Length);
            }
            #endregion
            #region catalogs
            line = "";
            foreach (string key in dc.Books.Keys)
            {
                line += key + ";" + dc.Books[key].ID + ';' + dc.Books[key].Title + ';' + dc.Books[key].Author + "\n";
            }
            File.Delete("catalogs.csv");
            using (Stream _stream = File.Open("catalogs.csv", FileMode.OpenOrCreate, FileAccess.Write))
            {
                byte[] _content = Encoding.ASCII.GetBytes(line);
                _stream.Write(_content, 0, _content.Length);
            }
            #endregion
            #region events
            line = "";
            foreach (Event e in dc.Events)
            {
                line += e.ToString() + "\n";
            }
            File.Delete("events.csv");
            using (Stream _stream = File.Open("events.csv", FileMode.OpenOrCreate, FileAccess.Write))
            {
                byte[] _content = Encoding.ASCII.GetBytes(line);
                _stream.Write(_content, 0, _content.Length);
            }
            #endregion
            #region inventories
            line = "";
            foreach (Inventory i in dc.Inventories)
            {
                line += i.Catalog.ID + ';' + i.Amount + "\n";
            }
            File.Delete("inventories.csv");
            using (Stream _stream = File.Open("inventories.csv", FileMode.OpenOrCreate, FileAccess.Write))
            {
                byte[] _content = Encoding.ASCII.GetBytes(line);
                _stream.Write(_content, 0, _content.Length);
            }
            #endregion
            #region notifications
            line = "";
            foreach (string e in dc.Notifications)
            {
                line += e + "\n";
            }
            File.Delete("notifications.csv");
            using (Stream _stream = File.Open("notifications.csv", FileMode.OpenOrCreate, FileAccess.Write))
            {
                byte[] _content = Encoding.ASCII.GetBytes(line);
                _stream.Write(_content, 0, _content.Length);
            }
            #endregion
            DataContext deserialized = csv.Deserialize("clients.csv", "catalogs.csv", "events.csv", "inventories.csv", "notifications.csv");
            Assert.AreEqual(dc.Clients.Count, deserialized.Clients.Count);
            Assert.AreEqual(dc.Books.Count, deserialized.Books.Count);
            Assert.AreEqual(dc.Events.Count, deserialized.Events.Count);
            Assert.AreEqual(dc.Notifications.Count, deserialized.Notifications.Count);
            Assert.AreEqual(dc.Inventories.Count, deserialized.Inventories.Count);
            #region deserialized clients
            for (int i = 0; i < dc.Clients.Count; i++)
            {
                Assert.AreEqual(dc.Clients[i].ID, deserialized.Clients[i].ID);
                Assert.AreEqual(dc.Clients[i].FirstName, deserialized.Clients[i].FirstName);
                Assert.AreEqual(dc.Clients[i].LastName, deserialized.Clients[i].LastName);
                Assert.AreEqual(dc.Clients[i].Penalty, deserialized.Clients[i].Penalty);
                for (int j = 0; j < dc.Clients[i].RentedCatalogs.Count; j++)
                {
                    Assert.AreEqual(dc.Clients[i].RentedCatalogs[j].ID, deserialized.Clients[i].RentedCatalogs[j].ID);
                    Assert.AreEqual(dc.Clients[i].RentedCatalogs[j].Title, deserialized.Clients[i].RentedCatalogs[j].Title);
                    Assert.AreEqual(dc.Clients[i].RentedCatalogs[j].Author, deserialized.Clients[i].RentedCatalogs[j].Author);
                }
            }
            #endregion
            #region deserialized Books
            foreach(string key in dc.Books.Keys)
            {
                Assert.AreEqual(dc.Books[key].ID, deserialized.Books[key].ID);
                Assert.AreEqual(dc.Books[key].Title, deserialized.Books[key].Title);
                Assert.AreEqual(dc.Books[key].Author, deserialized.Books[key].Author);
            }
            #endregion
            #region deserialized Events
            for (int i = 0; i < dc.Events.Count; i++)
            {
                Assert.AreEqual(dc.Events[i].ToString(), deserialized.Events[i].ToString());
            }
            #endregion
            #region deserialized Notifications
            for (int i = 0; i < dc.Notifications.Count; i++)
            {
                Assert.AreEqual(dc.Notifications[i], deserialized.Notifications[i]);
            }
            #endregion
            #region deserialized Inventories
            for (int i = 0; i < dc.Inventories.Count; i++)
            {
                Assert.AreEqual(dc.Inventories[i].Amount, deserialized.Inventories[i].Amount);
                Assert.AreEqual(dc.Inventories[i].Catalog.ID, deserialized.Inventories[i].Catalog.ID);
                Assert.AreEqual(dc.Inventories[i].Catalog.Title, deserialized.Inventories[i].Catalog.Title);
                Assert.AreEqual(dc.Inventories[i].Catalog.Author, deserialized.Inventories[i].Catalog.Author);
            }
            #endregion
        }
    }
}
