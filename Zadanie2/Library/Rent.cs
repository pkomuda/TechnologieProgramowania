using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Library
{
    public class Rent : Event
    {
        public Client Client { get; }
        public Inventory Inventory { get; }
        public DateTime ReturnDate { get; set; }
        public static List<string> IDs = new List<string>();

        public Rent(Client client, Inventory inventory, DateTime borrowDate, DateTime returnDate)
        {
            Client = client;
            Inventory = inventory;
            Date = borrowDate;
            ReturnDate = returnDate;
            IDs.Add(ID);
        }
        [JsonConstructor]
        public Rent(string id, Client client, Inventory inventory, DateTime borrowDate, DateTime returnDate) : base(id)
        {
            if (IDs.Contains(id))
                return;
            Client = client;
            Inventory = inventory;
            Date = borrowDate;
            ReturnDate = returnDate;
            IDs.Add(ID);
        }

        ~Rent()
        {
            IDs.Remove(ID);
        }

        public override string ToString()
        {
            return "Rent;" + base.ToString() + ";" + Client.ID + ";" + Inventory.Catalog.ID + ";" + Date + ";" + ReturnDate;
        }
    }
}