using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    public class CSV : ISerialization
    {
        public void Serialize(IEnumerable<Client> data, string path2File)
        {
            string line = "";
            string catalogs = "";
            foreach (Client c in data)
            {
                foreach(Catalog cat in c.RentedCatalogs)
                {
                    catalogs += cat.ID + ';';
                }
                line += c.ID + ';' + c.FirstName + ';' + c.LastName + ';' + c.Penalty + ';' + catalogs + "\n";
                catalogs = "";
            }
            File.Delete(path2File);
            using (Stream _stream = File.Open(path2File, FileMode.OpenOrCreate, FileAccess.Write))
            {
                byte[] _content = Encoding.ASCII.GetBytes(line);
                _stream.Write(_content, 0, _content.Length);
            }
        }
        public void Serialize(IEnumerable<Catalog> data, string path2File)
        {
            string line = "";
            foreach (Catalog c in data)
            {
                line += c.ID + ';' + c.Title + ';' + c.Author + "\n";
            }
            File.Delete(path2File);
            using (Stream _stream = File.Open(path2File, FileMode.OpenOrCreate, FileAccess.Write))
            {
                byte[] _content = Encoding.ASCII.GetBytes(line);
                _stream.Write(_content, 0, _content.Length);
            }
        }
        public void Serialize(IEnumerable<Event> data, string path2File)
        {
            string line = "";
            foreach (Event e in data)
            {
                line += e.ToString() + "\n";
            }
            File.Delete(path2File);
            using (Stream _stream = File.Open(path2File, FileMode.OpenOrCreate, FileAccess.Write))
            {
                byte[] _content = Encoding.ASCII.GetBytes(line);
                _stream.Write(_content, 0, _content.Length);
            }
        }
        public void Serialize(IEnumerable<Inventory> data, string path2File)
        {
            string line = "";
            foreach (Inventory i in data)
            {
                line += i.Catalog.ID + ';' + i.Amount + "\n";
            }
            File.Delete(path2File);
            using (Stream _stream = File.Open(path2File, FileMode.OpenOrCreate, FileAccess.Write))
            {
                byte[] _content = Encoding.ASCII.GetBytes(line);
                _stream.Write(_content, 0, _content.Length);
            }
        }
        public void Serialize(IEnumerable<string> data, string path2File)
        {
            string line = "";
            foreach (string e in data)
            {
                line += e + "\n";
            }
            File.Delete(path2File);
            using (Stream _stream = File.Open(path2File, FileMode.OpenOrCreate, FileAccess.Write))
            {
                byte[] _content = Encoding.ASCII.GetBytes(line);
                _stream.Write(_content, 0, _content.Length);
            }
        }
        public DataContext Deserialize(string clients, string catalogs, string events, string inventories, string notifications)
        {
            DataContext dataContext = new DataContext();
            // TODO
            return dataContext;
        }
    }
}
