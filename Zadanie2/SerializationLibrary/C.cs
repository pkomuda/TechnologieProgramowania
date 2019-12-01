using System;
using System.Globalization;
using System.Runtime.Serialization;

namespace SerializationLibrary
{
    public class C : ISerializable
    {
        public string Name { get; set; }
        public float Number { get; set; }
        public DateTime Date { get; set; }
        public A ObjectA { get; set; }

        public C(string name, float number, DateTime date, A objectA)
        {
            Name = name;
            Number = number;
            Date = date;
            ObjectA = objectA;
        }

        public override string ToString()
        {
            return "C: " + this.Name + " Date: " + this.Date.ToString(CultureInfo.InvariantCulture) + " Number: " + this.Number;
        }
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            throw new System.NotImplementedException();
        }
    }
}
