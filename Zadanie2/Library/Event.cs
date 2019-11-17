using System;

namespace Library
{
    public abstract class Event
    {
        public string ID { get; }
        public DateTime Date { get; set; }

        public Event()
        {
            ID = Guid.NewGuid().ToString();
        }
        public Event(string id)
        {
            ID = id;
        }
        public override string ToString()
        {
            return ID;
        }
    }
}
