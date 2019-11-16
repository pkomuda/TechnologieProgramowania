using System;

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
        
        public override string ToString()
        {
            return "Discard{ID: " + base.ToString() + "; Inventory(" + Inventory.Amount + ", " + Inventory.Catalog.Title + "); " + Date + "}";
        }
    }
}