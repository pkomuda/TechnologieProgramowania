using System;
using Newtonsoft.Json;

namespace Library
{
    public class Discard : Event
    {
        public Inventory Inventory { get; }

        public Discard(Inventory inventory, DateTime discardDate)
        {
            Inventory = inventory;
            Date = discardDate;
        }
        [JsonConstructor]
        public Discard(string id, Inventory inventory, DateTime discardDate) : base(id)
        {
            Inventory = inventory;
            Date = discardDate;
        }

        public override string ToString()
        {
            return "Discard;" + base.ToString() + ";" + Inventory.Catalog.ID + ";" + Date;
        }
    }
}