using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        public DataContext deserialize(string path2catalogs, string path2clients, string path2notifications, string path2inventories, string path2events)
        {
            Dictionary<string, Catalog> catalogs = new Dictionary<string, Catalog>();
            using (FileStream fileStream = new FileStream(path2catalogs, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                using (StreamReader streamReader = new StreamReader(fileStream))
                {
                    string line = streamReader.ReadLine(); 
                    while (null != line)
                    {
                        string[] words = line.Split(';');
                        catalogs.Add(words[0], new Catalog(words[0], words[1], words[2]));
                        streamReader.ReadLine();
                    }
                }
            }
            List<Client> clients = new List<Client>();
            using (FileStream fileStream = new FileStream(path2clients, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                using (StreamReader streamReader = new StreamReader(fileStream))
                {
                    string line = streamReader.ReadLine();
                    while (null != line)
                    {
                        string[] words = line.Split(';');
                        List<Catalog> rentedCatalgos = new List<Catalog>();
                        for(int i = 0; i < words.Length; i++)
                        {
                            rentedCatalgos.Add(catalogs[words[i]]);
                        }
                        clients.Add(new Client(words[0], words[1], words[2], rentedCatalgos));
                        streamReader.ReadLine();
                    }
                }
            }
            List<string> notifications = new List<string>();
            using (FileStream fileStream = new FileStream(path2notifications, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                using (StreamReader streamReader = new StreamReader(fileStream))
                {
                    string line = streamReader.ReadLine();
                    while (null != line)
                    {
                        notifications.Add(line);
                        streamReader.ReadLine();
                    }
                }
            }
            List<Inventory> inventories = new List<Inventory>();
            using (FileStream fileStream = new FileStream(path2inventories, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                using (StreamReader streamReader = new StreamReader(fileStream))
                {
                    string line = streamReader.ReadLine();
                    while (null != line)
                    {
                        string[] words = line.Split(';');
                        inventories.Add(new Inventory(catalogs[words[0]], Int32.Parse(words[1])));
                        streamReader.ReadLine();
                    }
                }
            }
            ObservableCollection<Event> events = new ObservableCollection<Event>();
            using (FileStream fileStream = new FileStream(path2events, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                using (StreamReader streamReader = new StreamReader(fileStream))
                {
                    string line = streamReader.ReadLine();
                    while (null != line)
                    {
                        string[] words = line.Split(';');
                        if(words[0].Equals("Rent"))
                        {
                            Client c1 = null;
                            Inventory i1 = null;
                            foreach(Client c in clients)
                            {
                                if(words[2] == c.ID)
                                {
                                    c1 = c;
                                    break;
                                }
                            }
                            foreach (Inventory i in inventories)
                            {
                                if (words[3] == i.Catalog.ID)
                                {
                                    i1 = i;
                                    break;
                                }
                            }
                            events.Add(new Rent(words[1], c1, i1, DateTime.Parse(words[4]), DateTime.Parse(words[5])));
                        } else if (words[0].Equals("Return"))
                        {
                            Client c1 = null;
                            Inventory i1 = null;
                            foreach (Client c in clients)
                            {
                                if (words[2] == c.ID)
                                {
                                    c1 = c;
                                    break;
                                }
                            }
                            foreach (Inventory i in inventories)
                            {
                                if (words[3] == i.Catalog.ID)
                                {
                                    i1 = i;
                                    break;
                                }
                            }
                            events.Add(new Return(words[1], c1, i1, DateTime.Parse(words[4])));
                        } else if(words[0].Equals("Purchase"))
                        {
                            Inventory i1 = null;
                            foreach (Inventory i in inventories)
                            {
                                if (words[2] == i.Catalog.ID)
                                {
                                    i1 = i;
                                    break;
                                }
                            }
                            events.Add(new Purchase(words[1], i1, DateTime.Parse(words[3]), Int32.Parse(words[4])));
                        } else if (words[0].Equals("Discard"))
                        {
                            Inventory i1 = null;
                            foreach (Inventory i in inventories)
                            {
                                if (words[2] == i.Catalog.ID)
                                {
                                    i1 = i;
                                    break;
                                }
                            }
                            events.Add(new Discard(words[1], i1, DateTime.Parse(words[3])));
                        } 
                        streamReader.ReadLine();
                    }
                }
            }
            return new DataContext(clients, catalogs, notifications, events, inventories);
        }
    }
}
