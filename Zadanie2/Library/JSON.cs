using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.IO;
using Newtonsoft.Json;

namespace Library
{
    public class JSON : ISerialization
    {
        public void Serialize(IEnumerable<Client> data, string path2File)
        {
            string json = JsonConvert.SerializeObject(data,
                                                      Formatting.Indented,
                                                      new JsonSerializerSettings
                                                      {
                                                          PreserveReferencesHandling = PreserveReferencesHandling.All,
                                                          TypeNameHandling = TypeNameHandling.All
                                                      });
            File.WriteAllText(path2File, json);
        }

        public void Serialize(IEnumerable<Catalog> data, string path2File)
        {
            string json = JsonConvert.SerializeObject(data,
                                                      Formatting.Indented,
                                                      new JsonSerializerSettings
                                                      {
                                                          PreserveReferencesHandling = PreserveReferencesHandling.All,
                                                          TypeNameHandling = TypeNameHandling.All
                                                      });
            File.WriteAllText(path2File, json);
        }

        public void Serialize(IEnumerable<Event> data, string path2File)
        {
            string json = JsonConvert.SerializeObject(data,
                                                      Formatting.Indented,
                                                      new JsonSerializerSettings
                                                      {
                                                          PreserveReferencesHandling = PreserveReferencesHandling.All,
                                                          TypeNameHandling = TypeNameHandling.All
                                                      });
            File.WriteAllText(path2File, json);
        }

        public void Serialize(IEnumerable<Inventory> data, string path2File)
        {
            string json = JsonConvert.SerializeObject(data,
                                                      Formatting.Indented,
                                                      new JsonSerializerSettings
                                                      {
                                                          PreserveReferencesHandling = PreserveReferencesHandling.All,
                                                          TypeNameHandling = TypeNameHandling.All
                                                      });
            File.WriteAllText(path2File, json);
        }

        public void Serialize(IEnumerable<string> data, string path2File)
        {
            string json = JsonConvert.SerializeObject(data,
                                                      Formatting.Indented,
                                                      new JsonSerializerSettings
                                                      {
                                                          PreserveReferencesHandling = PreserveReferencesHandling.All,
                                                          TypeNameHandling = TypeNameHandling.All
                                                      });
            File.WriteAllText(path2File, json);
        }

        public DataContext Deserialize(string clients, string catalogs, string events, string inventories, string notifications)
        {
            DataContext dataContext = new DataContext();
            string clientsJson = File.ReadAllText(clients);
            List<Client> clientsList = JsonConvert.DeserializeObject<List<Client>>(clientsJson,
                                                                                   new JsonSerializerSettings
                                                                                   {
                                                                                       PreserveReferencesHandling = PreserveReferencesHandling.All, 
                                                                                       TypeNameHandling = TypeNameHandling.All
                                                                                   });
            dataContext.Clients = clientsList;
            
            string catalogsJson = File.ReadAllText(catalogs);
            IEnumerable<Catalog> catalogsEnumerable = JsonConvert.DeserializeObject<IEnumerable<Catalog>>(catalogsJson,
                                                                                                          new JsonSerializerSettings
                                                                                                          {
                                                                                                              PreserveReferencesHandling = PreserveReferencesHandling.All, 
                                                                                                              TypeNameHandling = TypeNameHandling.All
                                                                                                          });
            Dictionary<string, Catalog> catalogsDictionary = new Dictionary<string, Catalog>();
            foreach (Catalog catalog in catalogsEnumerable)
                catalogsDictionary.Add(catalog.ID, catalog);
            dataContext.Books = catalogsDictionary;
            
            string eventsJson = File.ReadAllText(events);
            ObservableCollection<Event> eventsCollection = JsonConvert.DeserializeObject<ObservableCollection<Event>>(eventsJson,
                                                                                                                      new JsonSerializerSettings
                                                                                                                      {
                                                                                                                          PreserveReferencesHandling = PreserveReferencesHandling.All, 
                                                                                                                          TypeNameHandling = TypeNameHandling.All
                                                                                                                      });
            dataContext.Events.CollectionChanged += (sender, e) =>
            {
                if (e.Action == NotifyCollectionChangedAction.Add)
                    dataContext.Notifications.Add("Added event: " + e.NewItems[0]);
            };
            dataContext.Events = eventsCollection;
            
            string inventoriesJson = File.ReadAllText(inventories);
            List<Inventory> inventoriesList = JsonConvert.DeserializeObject<List<Inventory>>(inventoriesJson,
                                                                                             new JsonSerializerSettings
                                                                                             {
                                                                                                 PreserveReferencesHandling = PreserveReferencesHandling.All, 
                                                                                                 TypeNameHandling = TypeNameHandling.All
                                                                                             });
            dataContext.Inventories = inventoriesList;
            
            string notificationsJson = File.ReadAllText(notifications);
            List<string> notificationsList = JsonConvert.DeserializeObject<List<string>>(notificationsJson,
                                                                                         new JsonSerializerSettings
                                                                                         {
                                                                                             PreserveReferencesHandling = PreserveReferencesHandling.All, 
                                                                                             TypeNameHandling = TypeNameHandling.All
                                                                                         });
            dataContext.Notifications = notificationsList;
            
            return dataContext;
        }
    }
}
