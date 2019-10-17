using System;

namespace Library
{
    public class Client
    {
        public string ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public Client()
        {
            ID = Guid.NewGuid().ToString();
            FirstName = "First name not specified";
            LastName = "Last name not specified";
        }

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
