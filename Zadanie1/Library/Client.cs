using System;
using System.Collections.Generic;

namespace Library
{
    public class Client
    {
        public string ID { get; private set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Penalty { get; set; }
        public List<Catalog> RentedCatalogs { get; private set; }

        public Client(string firstName, string lastName)
        {
            ID = Guid.NewGuid().ToString();
            FirstName = firstName;
            LastName = lastName;
            RentedCatalogs = new List<Catalog>();
        }

        public Client(string id, string firstName, string lastName)
        {
            ID = id;
            FirstName = firstName;
            LastName = lastName;
            RentedCatalogs = new List<Catalog>();
        }

        public void PayPenalty()
        {
            Penalty = 0;
        }
        public override string ToString()
        {
            return "Client: " + ID + " " + FirstName + " " + LastName + " penalty: " + Penalty; 
        }
    }
}
