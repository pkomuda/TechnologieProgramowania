using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Library
{
    public class Purchase : Event
    {
        public Inventory Inventory { get; }
        public int Amount { get; }
        public static List<string> IDs = new List<string>();
        public Purchase(Inventory inventory, DateTime purchaseDate, int amount)
        {
            Inventory = inventory;
            Date = purchaseDate;
            Amount = amount;
            IDs.Add(ID);
        }
        [JsonConstructor]
        public Purchase(string id, Inventory inventory, DateTime purchaseDate, int amount) : base(id)
        {
            if (IDs.Contains(id))
                return;
            Inventory = inventory;
            Date = purchaseDate;
            Amount = amount;
            IDs.Add(ID);
        }

        ~Purchase()
        {
            IDs.Remove(ID);
        }

        public override string ToString()
        {
            return "Purchase;" + base.ToString() + ";" + Inventory.Catalog.ID + ";" + Date + ";" + Amount;
        }
    }
}