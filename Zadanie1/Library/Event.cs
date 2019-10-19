using System;

namespace Library
{
    public class Event
    {
        public Client Client { get; set; }
        public Inventory Inventory { get; set; }
        public DateTime BorrowDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public string ID { get; private set; }

        public Event(Client client, Inventory inventory, DateTime borrowDate, DateTime returnDate)
        {
            Client = client;
            Inventory = inventory;
            BorrowDate = borrowDate;
            ReturnDate = returnDate;
            ID = Guid.NewGuid().ToString();
        }
        public Event(String id, Client client, Inventory inventory, DateTime borrowDate, DateTime returnDate)
        {
            Client = client;
            Inventory = inventory;
            BorrowDate = borrowDate;
            ReturnDate = returnDate;
            ID = id;
        }
    }
}
