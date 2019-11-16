using System;
using System.IO;
using Library;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace LibraryTest
{
    [TestClass]
    public class JsonSerializationTest
    {
        [TestMethod]
        public void SerializeTest()
        {
            DataService dataService = new DataService();
            dataService.AddCatalog("b1", "a", "b");
            dataService.AddInventory("b1", 5);
            dataService.AddClient("c1", "c", "d");
            dataService.AddClient("c2", "e", "f");
            dataService.RentBook("c1", "b1");
            dataService.RentBook("c2", "b1");
            foreach (Event e in dataService.EventsAfterDate(DateTime.Today))
                Console.WriteLine(e);
            
            string json = JsonConvert.SerializeObject(dataService,
                                                      Formatting.Indented,
                                                      new JsonSerializerSettings
                                                      {
                                                          PreserveReferencesHandling = PreserveReferencesHandling.All,
                                                          TypeNameHandling = TypeNameHandling.All
                                                      });
            File.WriteAllText("test.json", json);
        }

        [TestMethod]
        public void DeserializeTest()
        {
            string json = File.ReadAllText("test.json");
            DataService dataService = JsonConvert.DeserializeObject<DataService>(json,
                                                                                 new JsonSerializerSettings
                                                                                 {
                                                                                     PreserveReferencesHandling = PreserveReferencesHandling.All, 
                                                                                     TypeNameHandling = TypeNameHandling.All
                                                                                 });
            foreach (Event e in dataService.EventsAfterDate(DateTime.Today))
                Console.WriteLine(e);
        }
    }
}
