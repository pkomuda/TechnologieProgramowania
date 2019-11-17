using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    public class CSV
    {
        public void serialize(IEnumerable<Client> data, string path2file)
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
            File.Delete(path2file);
            using (Stream _stream = File.Open(path2file, FileMode.OpenOrCreate, FileAccess.Write))
            {
                byte[] _content = Encoding.ASCII.GetBytes(line);
                _stream.Write(_content, 0, _content.Length);
            }
        }
        public void serialize(IEnumerable<Catalog> data, string path2file)
        {
            string line = "";
            foreach (Catalog c in data)
            {
                line += c.ID + ';' + c.Title + ';' + c.Author + "\n";
            }
            File.Delete(path2file);
            using (Stream _stream = File.Open(path2file, FileMode.OpenOrCreate, FileAccess.Write))
            {
                byte[] _content = Encoding.ASCII.GetBytes(line);
                _stream.Write(_content, 0, _content.Length);
            }
        }
        public void serialize(IEnumerable<Event> data, string path2file)
        {

        }
        public void serialize(IEnumerable<Inventory> data, string path2file)
        {
            string line = "";
            foreach (Inventory i in data)
            {
                line += i.Catalog.ID + ';' + i.Amount + "\n";
            }
            File.Delete(path2file);
            using (Stream _stream = File.Open(path2file, FileMode.OpenOrCreate, FileAccess.Write))
            {
                byte[] _content = Encoding.ASCII.GetBytes(line);
                _stream.Write(_content, 0, _content.Length);
            }
        }
        public void serialize(IEnumerable<string> data, string path2file)
        {
            string line = "";
            foreach (string e in data)
            {
                line += e + "\n";
            }
            File.Delete(path2file);
            using (Stream _stream = File.Open(path2file, FileMode.OpenOrCreate, FileAccess.Write))
            {
                byte[] _content = Encoding.ASCII.GetBytes(line);
                _stream.Write(_content, 0, _content.Length);
            }
        }
    }
}
