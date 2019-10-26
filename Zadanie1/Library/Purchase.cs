using System;

namespace Library
{
    public class Purchase : Event
    {
        public Inventory Inventory { get; set; }

        public Purchase(Inventory inventory, DateTime purchaseDate)
        {
            Inventory = inventory;
            Date = purchaseDate;
        }
        
        public override string ToString()
        {
            return "Purchase{ID: " + base.ToString() + "; Inventory(" + Inventory.Amount + ", " + Inventory.Catalog.Title + "); " + Date + "}";
        }
    }
}