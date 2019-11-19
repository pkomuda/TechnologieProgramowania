using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    public interface ISerialization
    {
        void Serialize(IEnumerable<Client> data, string path2File);
        void Serialize(IEnumerable<Catalog> data, string path2File);
        void Serialize(IEnumerable<Event> data, string path2File);
        void Serialize(IEnumerable<Inventory> data, string path2File);
        void Serialize(IEnumerable<string> data, string path2File);
        DataContext Deserialize(string clients, string catalogs, string events, string inventories, string notifications);
    }
}
