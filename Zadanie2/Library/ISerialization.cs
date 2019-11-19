using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    public interface ISerialization
    {
        void Serialize(List<Client> data, string path2File);
        void Serialize(Dictionary<string, Catalog> data, string path2File);
        void Serialize(ObservableCollection<Event> data, string path2File);
        void Serialize(List<Inventory> data, string path2File);
        void Serialize(List<string> data, string path2File);
        DataContext Deserialize(string path2clients, string path2catalogs, string path2events, string path2inventories, string path2notifications);
    }
}
