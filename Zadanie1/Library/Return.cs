﻿using System;

namespace Library
{
    public class Return : Event
    {
        public Client Client { get; set; }
        public Inventory Inventory { get; set; }
        
        public Return(Client client, Inventory inventory, DateTime returnDate)
        {
            Client = client;
            Inventory = inventory;
            Date = returnDate;
        }
        
        public override string ToString()
        {
            return "Return{ID: " + base.ToString() + "; Client(" + Client.FirstName + ", " + Client.LastName + "); Inventory(" + Inventory.Catalog.Title + "); " + Date + "}";
        }
    }
}