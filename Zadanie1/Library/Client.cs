using System;

namespace Library
{
    public class Client
    {
        public string ID { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }


        public Client(string firstName, string lastName)
        {
            ID = Guid.NewGuid().ToString();
            FirstName = firstName;
            LastName = lastName;
        }

        public Client(string id, string firstName, string lastName)
        {
            ID = id;
            FirstName = firstName;
            LastName = lastName;
        }
    }
}
