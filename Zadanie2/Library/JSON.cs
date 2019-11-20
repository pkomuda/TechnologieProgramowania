using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.IO;
using Newtonsoft.Json;

namespace Library
{
    public class JSON : ISerialization
    {
        public void Serialize(List<Client> data, string path2File)
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

        public void Serialize(Dictionary<string, Catalog> data, string path2File)
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

        public void Serialize(ObservableCollection<Event> data, string path2File)
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

        public void Serialize(List<Inventory> data, string path2File)
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

        public void Serialize(List<string> data, string path2File)
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
            Dictionary<string, Catalog> catalogsDictionary = JsonConvert.DeserializeObject<Dictionary<string, Catalog>>(catalogsJson,
                                                                                                                        new JsonSerializerSettings
                                                                                                                        {
                                                                                                                            PreserveReferencesHandling = PreserveReferencesHandling.All, 
                                                                                                                            TypeNameHandling = TypeNameHandling.All
                                                                                                                        });
            dataContext.Books = catalogsDictionary;
            
            string eventsJson = File.ReadAllText(events);
            ObservableCollection<Event> eventsCollection = JsonConvert.DeserializeObject<ObservableCollection<Event>>(eventsJson,
                                                                                                                      new JsonSerializerSettings
                                                                                                                      {
                                                                                                                          PreserveReferencesHandling = PreserveReferencesHandling.All, 
                                                                                                                          TypeNameHandling = TypeNameHandling.All
                                                                                                                      });
            dataContext.Events = eventsCollection;
            dataContext.Events.CollectionChanged += (sender, e) =>
            {
                if (e.Action == NotifyCollectionChangedAction.Add)
                    dataContext.Notifications.Add("Added event: " + e.NewItems[0]);
            };

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
