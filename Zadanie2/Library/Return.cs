using System;
using Newtonsoft.Json;

namespace Library
{
    public class Return : Event
    {
        public Client Client { get; }
        public Inventory Inventory { get; }
        
        public Return(Client client, Inventory inventory, DateTime returnDate)
        {       
            Client = client;
            Inventory = inventory;
            Date = returnDate;
        }
        
        [JsonConstructor]
        public Return(string id, Client client, Inventory inventory, DateTime returnDate) : base(id)
        {
            Client = client;
            Inventory = inventory;
            Date = returnDate;
        }
        
        public override string ToString()
        {
            return "Return;" + base.ToString() + ";" + Client.ID + ";" + Inventory.Catalog.ID + ";" + Date;
        }
    }
}