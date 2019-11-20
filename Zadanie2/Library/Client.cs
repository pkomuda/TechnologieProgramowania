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
        [JsonIgnore]
        public static List<string> IDs = new List<string>();

        public Client(string firstName, string lastName)
        {
            ID = Guid.NewGuid().ToString();
            FirstName = firstName;
            LastName = lastName;
            RentedCatalogs = new List<Catalog>();
            IDs.Add(ID);
        }

        [JsonConstructor]
        public Client(string id, string firstName, string lastName)
        {
            if (IDs.Contains(id))
                return;
            ID = id;
            FirstName = firstName;
            LastName = lastName;
            RentedCatalogs = new List<Catalog>();
            IDs.Add(ID);
        }
        
        public Client(string id, string firstName, string lastName, List<Catalog> rentedCatalogs)
        {
            ID = id;
            FirstName = firstName;
            LastName = lastName;
            RentedCatalogs = rentedCatalogs;
            IDs.Add(ID);
        }
        
        ~Client()
        {
            IDs.Remove(ID);
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
