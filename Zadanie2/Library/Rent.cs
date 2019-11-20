using System;
using Newtonsoft.Json;

namespace Library
{
    public class Rent : Event
    {
        public Client Client { get; }
        public Inventory Inventory { get; }
        public DateTime ReturnDate { get; set; }

        public Rent(Client client, Inventory inventory, DateTime borrowDate, DateTime returnDate)
        {
            Client = client;
            Inventory = inventory;
            Date = borrowDate;
            ReturnDate = returnDate;
        }
        
        [JsonConstructor]
        public Rent(string id, Client client, Inventory inventory, DateTime borrowDate, DateTime returnDate) : base(id)
        {
            Client = client;
            Inventory = inventory;
            Date = borrowDate;
            ReturnDate = returnDate;
        }

        public override string ToString()
        {
            return "Rent;" + base.ToString() + ";" + Client.ID + ";" + Inventory.Catalog.ID + ";" + Date + ";" + ReturnDate;
        }
    }
}