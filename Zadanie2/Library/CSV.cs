using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    class CSV
    {
        void export(IEnumerable<Client> data, string path2file)
        {
            string line = "";
            string catalogs = "";
            foreach (Client c in data)
            {
                foreach(Catalog cat in c.RentedCatalogs)
                {
                    catalogs += cat.ID + ';';
                }
                line += c.ID + ';' + c.FirstName + ';' + c.LastName + ';' + c.Penalty + ';' + catalogs + "/n";
                catalogs = "";
            }
            System.IO.File.WriteAllText(path2file, line);
        }
        void export(IEnumerable<Catalog> data, string path2file)
        {
            string line = "";
            foreach (Catalog c in data)
            {
                line += c.ID + ';' + c.Title + ';' + c.Author + "/n";
            }
            System.IO.File.WriteAllText(path2file, line);
        }
        void export(IEnumerable<Event> data, string path2file)
        {

        }
        void export(IEnumerable<Inventory> data, string path2file)
        {
            string line = "";
            foreach (Inventory i in data)
            {
                line += i.Catalog.ID + ';' + i.Amount + "/n";
            }
            System.IO.File.WriteAllText(path2file, line);
        }
        void export(IEnumerable<string> data, string path2file)
        {

        }
    }
}
