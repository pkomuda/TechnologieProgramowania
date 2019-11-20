using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Library
{
    public class Discard : Event
    {
        public Inventory Inventory { get; }
        [JsonIgnore]
        public static List<string> IDs = new List<string>();
        
        public Discard(Inventory inventory, DateTime discardDate)
        {
            Inventory = inventory;
            Date = discardDate;
            IDs.Add(ID);
        }
        
        [JsonConstructor]
        public Discard(string id, Inventory inventory, DateTime discardDate) : base(id)
        {
            if (IDs.Contains(id))
                return;
            Inventory = inventory;
            Date = discardDate;
            IDs.Add(ID);
        }
        
        ~Discard()
        {
            IDs.Remove(ID);
        }

        public override string ToString()
        {
            return "Discard;" + base.ToString() + ";" + Inventory.Catalog.ID + ";" + Date;
        }
    }
}