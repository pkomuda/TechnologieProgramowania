using System;
using System.Globalization;
using System.Runtime.Serialization;

namespace SerializationLibrary
{
    public class A : ISerializable
    {
        public string Name { get; set; }
        public float Number { get; set; }
        public DateTime Date { get; set; }
        public B ObjectB { get; set; }

        public A(string name, float number, DateTime date, B objectB)
        {
            Name = name;
            Number = number;
            Date = date;
            ObjectB = objectB;
        }

        public override string ToString()
        {
            return "A: " + this.Name + " Date: " + this.Date.ToString(new CultureInfo("pl-PL")) +" Number: " + this.Number;
        }
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            throw new System.NotImplementedException();
        }
    }
}
