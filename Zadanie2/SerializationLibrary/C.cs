using System;
using System.Globalization;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace SerializationLibrary
{
    public class C : ISerializable
    {
        public string Name { get; set; }
        public float Number { get; set; }
        public DateTime Date { get; set; }
        public A ObjectA { get; set; }

        [JsonConstructor]
        public C(string name, float number, DateTime date, A objectA)
        {
            Name = name;
            Number = number;
            Date = date;
            ObjectA = objectA;
        }
        public C(SerializationInfo info, StreamingContext context)
        {
            Name = (string)info.GetValue("Name", typeof(string));
            Number = (float)info.GetValue("Number", typeof(float));
            Date = (DateTime)info.GetValue("Date", typeof(DateTime));
            ObjectA = (A)info.GetValue("ObjectA", typeof(A));
        }
        public override string ToString()
        {
            return "C: " + this.Name + " Date: " + this.Date.ToString(CultureInfo.InvariantCulture) + " Number: " + this.Number;
        }
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Name", Name);
            info.AddValue("Number", Number);
            info.AddValue("Date", Date);
            info.AddValue("ObjectA", ObjectA, typeof(A));
        }
    }
}
