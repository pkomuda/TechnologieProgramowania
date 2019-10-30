using System;

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

        public override string ToString()
        {
            return "Rent{ID: " + base.ToString() + "; Client(" + Client.FirstName + ", " + Client.LastName + "); Inventory(" + Inventory.Catalog.Title + "); " + Date + "; " + ReturnDate + "}";
        }
    }
}