using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using Library;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LibraryTest
{
    [TestClass]
    public class JsonSerializationTest
    {
      [TestMethod]
        public void SerializeClientsTest()
        {
            List<Client> clients = new List<Client>();
            clients.Add(new Client("1", "Jakub", "Nowak"));
            clients.Add(new Client("2", "Krzesimir", "Mniejszy"));
            clients.Add(new Client("3", "Kazimierz", "Wielki"));

            JSON json = new JSON();
            json.Serialize(clients, "clients.json");
            string result = File.ReadAllText("clients.json");
            #region ClientsAssertion
            Assert.AreEqual(@"{
  ""$id"": ""1"",
  ""$type"": ""System.Collections.Generic.List`1[[Library.Client, Library]], mscorlib"",
  ""$values"": [
    {
      ""$id"": ""2"",
      ""$type"": ""Library.Client, Library"",
      ""ID"": ""1"",
      ""FirstName"": ""Jakub"",
      ""LastName"": ""Nowak"",
      ""Penalty"": 0,
      ""RentedCatalogs"": {
        ""$type"": ""System.Collections.Generic.List`1[[Library.Catalog, Library]], mscorlib"",
        ""$values"": []
      }
    },
    {
      ""$id"": ""3"",
      ""$type"": ""Library.Client, Library"",
      ""ID"": ""2"",
      ""FirstName"": ""Krzesimir"",
      ""LastName"": ""Mniejszy"",
      ""Penalty"": 0,
      ""RentedCatalogs"": {
        ""$type"": ""System.Collections.Generic.List`1[[Library.Catalog, Library]], mscorlib"",
        ""$values"": []
      }
    },
    {
      ""$id"": ""4"",
      ""$type"": ""Library.Client, Library"",
      ""ID"": ""3"",
      ""FirstName"": ""Kazimierz"",
      ""LastName"": ""Wielki"",
      ""Penalty"": 0,
      ""RentedCatalogs"": {
        ""$type"": ""System.Collections.Generic.List`1[[Library.Catalog, Library]], mscorlib"",
        ""$values"": []
      }
    }
  ]
}", result);
            #endregion
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

            JSON json = new JSON();
            json.Serialize(books, "books.json");
            string result = File.ReadAllText("books.json");
            #region CatalogsAssertion
            Assert.AreEqual(@"{
  ""$id"": ""1"",
  ""$type"": ""System.Collections.Generic.Dictionary`2[[System.String, mscorlib],[Library.Catalog, Library]], mscorlib"",
  ""1"": {
    ""$id"": ""2"",
    ""$type"": ""Library.Catalog, Library"",
    ""ID"": ""1"",
    ""Title"": ""Krzyzacy"",
    ""Author"": ""Henryk Sienkiewicz""
  },
  ""2"": {
    ""$id"": ""3"",
    ""$type"": ""Library.Catalog, Library"",
    ""ID"": ""2"",
    ""Title"": ""Kroniki Czarnej Kompanii"",
    ""Author"": ""Glen Cook""
  },
  ""3"": {
    ""$id"": ""4"",
    ""$type"": ""Library.Catalog, Library"",
    ""ID"": ""3"",
    ""Title"": ""Pan Tadeusz"",
    ""Author"": ""Adam Mickiewicz""
  }
}", result);
            #endregion
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
            JSON json = new JSON();
            json.Serialize(inventories, "inventories.json");
            string result = File.ReadAllText("inventories.json");
            #region InventoriesAssertion
            Assert.AreEqual(@"{
  ""$id"": ""1"",
  ""$type"": ""System.Collections.Generic.List`1[[Library.Inventory, Library]], mscorlib"",
  ""$values"": [
    {
      ""$id"": ""2"",
      ""$type"": ""Library.Inventory, Library"",
      ""Catalog"": {
        ""$id"": ""3"",
        ""$type"": ""Library.Catalog, Library"",
        ""ID"": ""1"",
        ""Title"": ""Krzyzacy"",
        ""Author"": ""Henryk Sienkiewicz""
      },
      ""Amount"": 10
    },
    {
      ""$id"": ""4"",
      ""$type"": ""Library.Inventory, Library"",
      ""Catalog"": {
        ""$id"": ""5"",
        ""$type"": ""Library.Catalog, Library"",
        ""ID"": ""2"",
        ""Title"": ""Kroniki Czarnej Kompanii"",
        ""Author"": ""Glen Cook""
      },
      ""Amount"": 5
    },
    {
      ""$id"": ""6"",
      ""$type"": ""Library.Inventory, Library"",
      ""Catalog"": {
        ""$id"": ""7"",
        ""$type"": ""Library.Catalog, Library"",
        ""ID"": ""3"",
        ""Title"": ""Pan Tadeusz"",
        ""Author"": ""Adam Mickiewicz""
      },
      ""Amount"": 7
    }
  ]
}", result);
            #endregion
        }
        
        [TestMethod]
        public void SerializeNotificationsTest()
        {
            List<string> notifications = new List<string>();
            notifications.Add("TestNotifications1");
            notifications.Add("TestNotifications2");
            notifications.Add("TestNotifications3");
            JSON json = new JSON();
            json.Serialize(notifications, "notifications.json");
            string result = File.ReadAllText("notifications.json");
            Assert.AreEqual("{\r\n  \"$id\": \"1\",\r\n  \"$type\": \"System.Collections.Generic.List`1[[System.String, m" +
                            "scorlib]], mscorlib\",\r\n  \"$values\": [\r\n    \"TestNotifications1\",\r\n    \"TestNotif" +
                            "ications2\",\r\n    \"TestNotifications3\"\r\n  ]\r\n}", result);
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
            JSON json = new JSON();
            json.Serialize(events, "events.json");
            string result = File.ReadAllText("events.json");
            #region EventsAssertion
            Assert.AreEqual("{\r\n  \"$id\": \"1\",\r\n  \"$type\": \"System.Collections.ObjectModel.ObservableCollection" +
    "`1[[Library.Event, Library]], System\",\r\n  \"$values\": [\r\n    {\r\n      \"$id\": \"2\"," +
    "\r\n      \"$type\": \"Library.Rent, Library\",\r\n      \"Client\": {\r\n        \"$id\": \"3\"" +
    ",\r\n        \"$type\": \"Library.Client, Library\",\r\n        \"ID\": \"1\",\r\n        \"Fir" +
    "stName\": \"Jakub\",\r\n        \"LastName\": \"Nowak\",\r\n        \"Penalty\": 0,\r\n        " +
    "\"RentedCatalogs\": {\r\n          \"$type\": \"System.Collections.Generic.List`1[[Libr" +
    "ary.Catalog, Library]], mscorlib\",\r\n          \"$values\": []\r\n        }\r\n      }," +
    "\r\n      \"Inventory\": {\r\n        \"$id\": \"4\",\r\n        \"$type\": \"Library.Inventory" +
    ", Library\",\r\n        \"Catalog\": {\r\n          \"$id\": \"5\",\r\n          \"$type\": \"Li" +
    "brary.Catalog, Library\",\r\n          \"ID\": \"1\",\r\n          \"Title\": \"Krzyzacy\",\r\n" +
    "          \"Author\": \"Henryk Sienkiewicz\"\r\n        },\r\n        \"Amount\": 10\r\n    " +
    "  },\r\n      \"ReturnDate\": \"2020-01-01T00:00:00\",\r\n      \"ID\": \"event1\",\r\n      \"" +
    "Date\": \"0001-01-01T00:00:00\"\r\n    },\r\n    {\r\n      \"$id\": \"6\",\r\n      \"$type\": \"" +
    "Library.Purchase, Library\",\r\n      \"Inventory\": {\r\n        \"$id\": \"7\",\r\n        " +
    "\"$type\": \"Library.Inventory, Library\",\r\n        \"Catalog\": {\r\n          \"$id\": \"" +
    "8\",\r\n          \"$type\": \"Library.Catalog, Library\",\r\n          \"ID\": \"2\",\r\n     " +
    "     \"Title\": \"Kroniki Czarnej Kompanii\",\r\n          \"Author\": \"Glen Cook\"\r\n    " +
    "    },\r\n        \"Amount\": 7\r\n      },\r\n      \"Amount\": 5,\r\n      \"ID\": \"event2\"," +
    "\r\n      \"Date\": \"0001-01-01T00:00:00\"\r\n    },\r\n    {\r\n      \"$id\": \"9\",\r\n      \"" +
    "$type\": \"Library.Return, Library\",\r\n      \"Client\": {\r\n        \"$id\": \"10\",\r\n   " +
    "     \"$type\": \"Library.Client, Library\",\r\n        \"ID\": \"2\",\r\n        \"FirstName" +
    "\": \"Krzesimir\",\r\n        \"LastName\": \"Mniejszy\",\r\n        \"Penalty\": 0,\r\n       " +
    " \"RentedCatalogs\": {\r\n          \"$type\": \"System.Collections.Generic.List`1[[Lib" +
    "rary.Catalog, Library]], mscorlib\",\r\n          \"$values\": []\r\n        }\r\n      }" +
    ",\r\n      \"Inventory\": {\r\n        \"$ref\": \"4\"\r\n      },\r\n      \"ID\": \"event3\",\r\n " +
    "     \"Date\": \"2022-02-02T00:00:00\"\r\n    },\r\n    {\r\n      \"$id\": \"11\",\r\n      \"$t" +
    "ype\": \"Library.Discard, Library\",\r\n      \"Inventory\": {\r\n        \"$id\": \"12\",\r\n " +
    "       \"$type\": \"Library.Inventory, Library\",\r\n        \"Catalog\": {\r\n          \"" +
    "$id\": \"13\",\r\n          \"$type\": \"Library.Catalog, Library\",\r\n          \"ID\": \"3\"" +
    ",\r\n          \"Title\": \"Pan Tadeusz\",\r\n          \"Author\": \"Adam Mickiewicz\"\r\n   " +
    "     },\r\n        \"Amount\": 33\r\n      },\r\n      \"ID\": \"event4\",\r\n      \"Date\": \"0" +
    "001-01-01T00:00:00\"\r\n    }\r\n  ]\r\n}", result);
            #endregion
        }
        
        [TestMethod]
        public void DeserializeTest()
        {
            JSON json = new JSON();
            DataRepository dataRepository = new DataRepository(json.Deserialize("clients.json",
                "books.json",
                "events.json",
                "inventories.json",
                "notifications.json"));
            File.Delete("clients.json");
            File.Delete("books.json");
            File.Delete("events.json");
            File.Delete("inventories.json");
            File.Delete("notifications.json");
            Assert.AreEqual(3, dataRepository.GetClientList().Count);
            Assert.AreEqual(3, dataRepository.GetCatalogDictionary().Count);
            Assert.AreEqual(4, dataRepository.GetEventCollection().Count);
            Assert.AreEqual(3, dataRepository.GetInventoryList().Count);
            Assert.AreEqual(3, dataRepository.GetNotifications().Count);
        }
    }
}
