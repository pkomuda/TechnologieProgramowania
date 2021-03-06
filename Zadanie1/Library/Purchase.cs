﻿using System;

namespace Library
{
    public class Purchase : Event
    {
        public Inventory Inventory { get; }
        public int Amount { get; }

        public Purchase(Inventory inventory, DateTime purchaseDate, int amount)
        {
            Inventory = inventory;
            Date = purchaseDate;
            Amount = amount;
        }
        
        public override string ToString()
        {
            return "Purchase{ID: " + base.ToString() + "; Inventory(" + Inventory.Amount + ", " + Inventory.Catalog.Title + "); " + Date + "}";
        }
    }
}