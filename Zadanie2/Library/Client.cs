using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Library
{
    public class Client
    {
        public string ID { get; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Penalty { get; set; }
        public List<Catalog> RentedCatalogs { get; }

        public Client(string firstName, string lastName)
        {
            ID = Guid.NewGuid().ToString();
            FirstName = firstName;
            LastName = lastName;
            RentedCatalogs = new List<Catalog>();
        }

        [JsonConstructor]
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
