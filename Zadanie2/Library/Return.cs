using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Library
{
    public class Return : Event
    {
        public Client Client { get; }
        public Inventory Inventory { get; }
        public static List<string> IDs = new List<string>();
        
        public Return(Client client, Inventory inventory, DateTime returnDate)
        {       
            Client = client;
            Inventory = inventory;
            Date = returnDate;
            IDs.Add(ID);
        }
        [JsonConstructor]
        public Return(string id, Client client, Inventory inventory, DateTime returnDate) : base(id)
        {
            if (IDs.Contains(id))
                return;
            Client = client;
            Inventory = inventory;
            Date = returnDate;
            IDs.Add(ID);
        }

        ~Return()
        {
            IDs.Remove(ID);
        }
        
        public override string ToString()
        {
            return "Return;" + base.ToString() + ";" + Client.ID + ";" + Inventory.Catalog.ID + ";" + Date;
        }
    }
}