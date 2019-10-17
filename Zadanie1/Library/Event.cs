using System;

namespace Library
{
    class Event
    {
        public Client Client { get; set; }
        public Inventory Inventory { get; set; }
        public DateTime BorrowDate { get; set; }
        public DateTime ReturnDate { get; set; }

        public Event(Client client, Inventory inventory, DateTime borrowDate, DateTime returnDate)
        {
            Client = client;
            Inventory = inventory;
            BorrowDate = borrowDate;
            ReturnDate = returnDate;
        }
    }
}
