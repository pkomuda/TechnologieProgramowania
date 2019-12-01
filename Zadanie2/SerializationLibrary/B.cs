using System;
using System.Globalization;
using System.Runtime.Serialization;

namespace SerializationLibrary
{
    public class B : ISerializable
    {
        public string Name { get; set; }
        public float Number { get; set; }
        public DateTime Date { get; set; }
        public C ObjectC { get; set; }

        public B(string name, float number, DateTime date, C objectC)
        {
            Name = name;
            Number = number;
            Date = date;
            ObjectC = objectC;
        }

        public override string ToString()
        {
            return "B: " + this.Name + " Date: " + this.Date.ToString(new CultureInfo("pl-PL")) + " Number: " + this.Number;
        }
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            throw new System.NotImplementedException();
        }
    }
}
